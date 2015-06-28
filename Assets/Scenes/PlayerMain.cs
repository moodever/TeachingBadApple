using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerMain : MonoBehaviour, IDropCallback {

	public GameObject cardPlayerPrefab;

	// Use this for initialization
	void Start () {
		LobbyPlayer localPlayer = (LobbyPlayer)Registry.get ("localPlayer");

		// Initialize Interface
		int qIndex = localPlayer.question;
		int[] choices = localPlayer.options;
		GameObject.Find ("Question Text").GetComponent<Text> ().text = Data.QUESTIONS [qIndex];

		for (int i = 0; i < choices.Length; i++) {
			string name = String.Format("Card {0}",i);
			GameObject cardgo = GameObject.Find (name);
			Card card = cardgo.GetComponent<Card> ();
			card.cardId = choices[i];
			card.cardText.text = Data.ANSWERS[choices[i]];
		}
	}

	public void OnDrop(GameObject target) {
		Debug.Log ("Card Chosen by Player");
		Card card = target.GetComponent<Card> ();
		foreach (LobbyPlayer cp in FindObjectsOfType<LobbyPlayer>()){
			if (cp.isLocalPlayer) {
				cp.CmdCardChosen (cp.id, card.cardId);
			}
		}
	}
}
