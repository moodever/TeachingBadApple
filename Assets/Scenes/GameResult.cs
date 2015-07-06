using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using UnityEngine.UI;

public class GameResult : MonoBehaviour {

	public GameObject resultPlayerPrefab;

	// Use this for initialization
	void Start () {
		displayScore ();
		StartCoroutine(WaitAndRun ());
	}

	void Update() {
		displayScore ();
	}

	void displayScore(){

		//for each player,
		//if Judge, +3
		//if normal Player, +2
		//if Winner + 10 ;

		Text winnerName = GameObject.Find ("WinnerNameText").GetComponent<Text> ();
		Text winnerScore = GameObject.Find ("SoreBoardText").GetComponent<Text> ();

		LobbyPlayer[] players = FindObjectsOfType<LobbyPlayer> ();
//		 ./h  hnhnhhhhh...
		int playerNumber = players.Length;
		string winnerNameString;

		string longScoreText="";

		Debug.Log ("start to display score ");

		for (int i=0; i<playerNumber; i++) {
			if (players[i].isWinner == true){

				winnerNameString = String.Format("{0} is the winner",players[i].playerName);
				Debug.Log (winnerNameString);

				winnerName.text = winnerNameString;

			}

		
			longScoreText += String.Format("{0} score is {1} \n",players[i].playerName,players[i].score);

			Debug.Log (longScoreText);
		}

		winnerScore.text =longScoreText;

	}


	IEnumerator WaitAndRun () {
		yield return new WaitForSeconds (5);

		// The previous judge prepare next round
		Debug.Log ("Finding Previous Judge");
		LobbyPlayer localPlayer = (LobbyPlayer)Registry.get ("localPlayer");
		if (localPlayer.isJudge) {
				localPlayer.CmdNextRound ();
		}
	}
}
