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
		LobbyPlayer[] players = FindObjectsOfType<LobbyPlayer> ();
		int playerNumber = players.Length;
		string info = String.Format("{0}/3", playerNumber);

		/*
		foreach (LobbyPlayer player in players) {

			int imgIndex = player.avatarIndex;
			string avatarImgName = String.Format("Avatar{0}.png",imgIndex);


		}
		*/


		for (int i=0; i< playerNumber; i++) {

			LobbyPlayer player = players[i];

			int imgIndex = player.avatarIndex;

			string avatarImgName = String.Format("avatar{0}",imgIndex);

			string logoImgName = String.Format("avatarlogo{0}",i+1);

			string path=String.Format("avatar/{0}",avatarImgName);

			RawImage targetImg = GameObject.Find (logoImgName).GetComponent<RawImage> ();

			targetImg.texture = Resources.Load(path) as Texture;


			string joinedPlayerName = player.playerName;

			string playerIntro = String.Format("Welcome {0} join the game!",joinedPlayerName);

			string playerTextName = String.Format ("PlayerText{0}",i+1);

			Text targetText = GameObject.Find (playerTextName).GetComponent<Text> ();

			targetText.text = playerIntro;



		}




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
