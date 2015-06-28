using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections;

public class WaitPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		int playerNumber = FindObjectsOfType<LobbyPlayer> ().Length;
		string info = String.Format("{0}/3", playerNumber);
		GameObject.Find ("Player Info Text").GetComponent<Text> ().text = info;
		if (playerNumber == 3) {
			OnStartGame();
		}
	}

	public void OnStartGame() {
		LobbyPlayer local = (LobbyPlayer)Registry.get ("localPlayer");
		local.CmdStartGame();
	}

	public void OnBackToMain() {
		NetworkManager.singleton.StopHost ();
		Application.LoadLevel ("StartMenu");
	}
}
