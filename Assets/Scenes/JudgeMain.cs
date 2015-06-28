using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections;

public class JudgeMain : MonoBehaviour, IDropCallback {

	public GameObject cardPlayerPrefab;

	private int choiceCounter = 0;

	// Use this for initialization
	void Start () {
		LobbyPlayer localPlayer = (LobbyPlayer)Registry.get ("localPlayer");
		// Initialize Interface
		int qIndex = localPlayer.question;
		string question = Data.QUESTIONS[qIndex];
		GameObject.Find ("Question Text").GetComponent<Text> ().text = question;

		// Initialize Counter
		choiceCounter = 0;
	}

	// Card Chosen by Player and displayed to Judge
	public void OnCardChosen(int playerId, int cardId) {
		string cardName = String.Format ("Card {0}", choiceCounter++);
		Card card = GameObject.Find (cardName).GetComponent<Card>();
		card.cardTitleText.text = String.Format ("{0}", playerId);
		card.cardText.text = Data.ANSWERS [cardId];
	}

	public void OnDrop(GameObject target) {
		Card card = target.GetComponent<Card> ();
		LobbyPlayer cp = (LobbyPlayer) Registry.get ("localPlayer");
		cp.CmdAnswerChosen (Int32.Parse(card.cardTitleText.text),card.cardId);
	}

	public void OnTestCard0() {
		OnCardChosen (0, 5);
	}

	public void OnTestCard1() {
		OnCardChosen (1, 10);
	}

	public void OnTestAnswerChosen() {
		// Card 0 is chosen by judge
		OnDrop (GameObject.Find ("Card 0"));
	}
}
