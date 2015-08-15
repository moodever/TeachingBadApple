using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections;

public class JudgeMain : MonoBehaviour, IDropCallback {

	public GameObject cardPrefab;

	private int choiceCounter = 0;

	// Use this for initialization
	void Start () {
		LobbyPlayer localPlayer = (LobbyPlayer)Registry.get ("localPlayer");
		// Initialize Interface
		int qIndex = localPlayer.question;
		string question = Data.QUESTIONS[qIndex];
		GameObject.Find ("Question Card").GetComponent<Card> ().cardText.text = question;

		// Initialize Counter
		choiceCounter = 0;

		int playerLength = GameObject.FindObjectsOfType<LobbyPlayer> ().Length;
		Debug.Log(playerLength);
		playerLength = playerLength - 1;
		for (int i = 0; i < playerLength; i++) {
			AddCard(i);
		}
	}

	void Update() {

	}

	// Card Chosen by Player and displayed to Judge
	public void OnCardChosen(int playerId, int cardId) {
		string cardName = String.Format ("Card {0}", choiceCounter++);
		Card card = GameObject.Find (cardName).GetComponent<Card>();
		card.cardTitleText.text = String.Format ("{0}", playerId);
		card.cardText.text = Data.ANSWERS [cardId];

		//Score playerScore = GameObject.Find(
	}

	public void OnDrop(GameObject target) {
		Card card = target.GetComponent<Card> ();
		LobbyPlayer cp = (LobbyPlayer) Registry.get ("localPlayer");
		cp.CmdAnswerChosen (Int32.Parse(card.cardTitleText.text),card.cardId);
	}

	public void AddCard(int i) {
		GameObject container = GameObject.Find ("Cards"); 
		GameObject newCard = GameObject.Instantiate (cardPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		newCard.name = String.Format ("Card {0}", i);
		newCard.GetComponent<LayoutElement> ().preferredHeight = 100;
		newCard.GetComponent<Transform>().SetParent(container.GetComponent<Transform>());
	}
}
