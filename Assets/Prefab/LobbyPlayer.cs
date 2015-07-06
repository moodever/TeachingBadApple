ausing UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;

public class LobbyPlayer : NetworkBehaviour {

	[SyncVar]
	public int id;

	[SyncVar]
	public string playerName;

	[SyncVar]
	public int avatarIndex;

	[SyncVar]
	public bool isHost;

	[SyncVar]
	public bool isJudge;

	[SyncVar]
	public bool isWinner;

	[SyncVar]
	public int question;

	[SyncVar]
	public int[] options;

	[SyncVar]
	public int score;

	private static int CARD_NUM = 5;

	public LobbyPlayer() {

	}

	public override void OnStartLocalPlayer() {
		// Reference of local player
		Debug.Log ("OnStartLocalPlayer");
		DontDestroyOnLoad (this);
		Registry.put ("localPlayer", this);
	
		this.CmdUpdateUserInfo(PlayerPrefs.GetInt ("Player Avatar"), 
		                       PlayerPrefs.GetString ("Player Name"),
		                       (null != Registry.get ("host")));
	}

	[Command]
	public void CmdUpdateUserInfo(int avatarIndex, string playerName, bool isHost) {
		this.avatarIndex = avatarIndex;
		this.playerName = playerName;
		this.isHost = isHost;	

	}

	[Command]
	public void CmdStartGame() {
		// Assign ID to each player
		int counter = 0;
		LobbyPlayer[] lobbyPlayers = FindObjectsOfType<LobbyPlayer> ();
		foreach (LobbyPlayer player in lobbyPlayers) {
			player.id = counter++;
			Debug.Log (String.Format("Player {0} found",player.id));
			player.RpcRetainPlayers();
		}

		int sum = lobbyPlayers.Length;

		// Set Player 0 as judge
		Registry.put ("judgeIndex", 0);
		Registry.put ("playerCount", sum);

		int judgeIndex = Registry.getInt("judgeIndex");


		int question = Data.genQuestion ();
		int[][] data = Data.genCardSets (sum - 1, CARD_NUM);

	    counter = 0;
		LobbyPlayer localPlayer = (LobbyPlayer)Registry.get ("localPlayer");
		foreach (LobbyPlayer player in FindObjectsOfType<LobbyPlayer> ()) {
			player.question = question;
			if(player.id == judgeIndex) {
				player.isJudge = true;

			} else {
				player.isJudge = false;
				player.options = data[counter++];
			}
			
:			if(player == localPlayer) {
				if(player.isJudge) {
					player.StartAsJudge();
				} else {
					player.StartAsPlayer();
				}
			} else {
				if(player.isJudge) {
					player.RpcSstartAsJudge();
				} else {
					player.RpcStartAsPlayer();
				}
			}
		}
	}

	[ClientRpc]
	void RpcRetainPlayers() {
		foreach (LobbyPlayer player in FindObjectsOfType<LobbyPlayer>()) {
			DontDestroyOnLoad(player);
		}
	}

	[ClientRpc]
	void RpcStartAsJudge() {
		Debug.Log ("RpcStartAsJudge called");
		if (isLocalPlayer) {
			Debug.Log("Player "+id +" will switch to judge scene");
			//Registry.put ("Previous Scene", "JudgeMain");
			//Application.LoadLevel("JudgeNotify");
			Application.LoadLevel ("JudgeMain");
		} else {
			Debug.Log("Play as Judge Command from non-local player");
		}
	}

	void StartAsJudge() {
		Debug.Log ("StartAsJudge called");
		if (isLocalPlayer) {
			Debug.Log("Player "+id +" will switch to judge scene");

			//Registry.put ("Previous Scene", "JudgeMain");
			//Application.LoadLevel("JudgeNotify");
			Application.LoadLevel ("JudgeMain");
		} else {
			Debug.Log("Play as Judge Command from non-local player");
		}
	}

	[ClientRpc]
	void RpcStartAsPlayer() {
		Debug.Log ("RpcStartAsPlayer called");
		if (isLocalPlayer) {
			Debug.Log("Player "+id +" will switch to player scene");

			//Registry.put ("Previous Scene", "PlayerMain");
			//Application.LoadLevel("JudgeNotify");

			Application.LoadLevel ("PlayerMain");
		} else {	
			Debug.Log ("Play as Player Command from non-local player");
		}
	}

	void StartAsPlayer() {
		Debug.Log ("StartAsPlayer called");
		if (isLocalPlayer) {
			Debug.Log("Player "+id +" will switch to player scene");

			//Registry.put ("Previous Scene", "PlayerMain");
			//Application.LoadLevel("JudgeNotify");


			Application.LoadLevel ("PlayerMain");
		} else {	
			Debug.Log ("Play as Player Command from non-local player");
		}
	}

	[Command]
	public void CmdCardChosen(int playerId, int cardId) {
		Debug.Log ("Card Chosen");
		foreach (LobbyPlayer lp in FindObjectsOfType<LobbyPlayer>()) {
			if (lp.isJudge) {
				if(lp.isLocalPlayer) {
					lp.PlayerChoseCard(playerId,cardId);
				} else {
					lp.RpcPlayerChoseCard (playerId, cardId);
				}
			}
		}
	}
	
	[ClientRpc]
	void RpcPlayerChoseCard(int playerId, int cardId) {
		if (isLocalPlayer) {
			JudgeMain judgeMain = GameObject.Find ("Controller").GetComponent<JudgeMain> ();
			judgeMain.OnCardChosen (playerId, cardId);
		}
	}
	
	void PlayerChoseCard(int playerId, int cardId) {
		if (isLocalPlayer) {
			JudgeMain judgeMain = GameObject.Find ("Controller").GetComponent<JudgeMain> ();
			judgeMain.OnCardChosen (playerId, cardId);
		}
	}
	
	[Command]
	public void CmdAnswerChosen(int playerId, int cardId) {
		Debug.Log ("AnswerChose - update score is called");

		LobbyPlayer[] players = FindObjectsOfType<LobbyPlayer> ();
		int playerNumber = players.Length;
		string info = String.Format("{0}/3", playerNumber);

		for (int i=0; i< playerNumber; i++) {
			
			LobbyPlayer player = players[i];
			
			if (player.isJudge == true) {

				player.score = player.score +2;
				player.isWinner = false;
			}

			else if (player.id == playerId) {

				player.score = player.score + 10;
				player.isWinner = true;

			}

			else {

				player.score = player.score +1;
				player.isWinner = false;
			}
		}

		NetworkManager.singleton.ServerChangeScene ("GameResult");
	
	}



	[Command]
	public void CmdNextRound() {
		Debug.Log ("Next Round");
		int judgeIndex = Registry.getInt("judgeIndex");
		int sum = Registry.getInt ("playerCount");
		judgeIndex = (judgeIndex + 1) % sum;
		Registry.put ("judgeIndex", judgeIndex);
		Debug.Log ("New judge will be " + judgeIndex);
		int question = Data.genQuestion ();
		int[][] data = Data.genCardSets (sum - 1, CARD_NUM);
		
		int counter = 0;
//		LobbyPlayer localPlayer = (LobbyPlayer)Registry.get ("localPlayer");
		foreach (LobbyPlayer player in FindObjectsOfType<LobbyPlayer> ()) {
			player.question = question;
			if(player.id == judgeIndex) {
				player.isJudge = true;
			} else {
				player.isJudge = false;
				player.options = data[counter++];
			}

			if(player.isLocalPlayer) {
				if(player.isJudge) {
					player.StartAsJudge();
				} else {
					player.StartAsPlayer();
				}
			} else {
				if(player.isJudge) {
					player.RpcStartAsJudge();
				} else {
					player.RpcStartAsPlayer();
				}
			}
		}
	}
}
