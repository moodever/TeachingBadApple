using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CardPlayer : NetworkBehaviour {

	[SyncVar]
	public int id; 

	[SyncVar]
	public string username;

	[SyncVar]
	public bool isJudge;


	[Command]
	public void CmdCardChosen(int playerId, int cardId) {
		foreach (PlayerController pc in NetworkManager.singleton.client.connection.playerControllers) {
			CardPlayer cp = pc.gameObject.GetComponent<CardPlayer> ();
			if (cp.isJudge) {
				cp.RpcPlayerChoseCard (playerId, cardId);
			}
		}
	}

	[ClientRpc]
	public void RpcPlayerChoseCard(int playerId, int cardId) {
		JudgeMain judgeMain = GameObject.Find ("Controller").GetComponent<JudgeMain>();
		judgeMain.OnCardChosen (playerId, cardId);
	}

	[Command]
	public void CmdAnswerChosen(int playerId, int cardId) {
		// TODO: Update Score

		NetworkManager.singleton.ServerChangeScene ("GameResult");
	}
}
