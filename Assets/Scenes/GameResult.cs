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
