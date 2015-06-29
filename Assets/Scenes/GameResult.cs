using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class GameResult : MonoBehaviour {

	public GameObject resultPlayerPrefab;

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitAndRun ());
	}


	void setScore(){

		for each player,

		if Judge, +3
		if normal Player, +2
		if Winner + 10 ;



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
