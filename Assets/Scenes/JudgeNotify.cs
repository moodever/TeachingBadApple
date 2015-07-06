using UnityEngine;
using System.Collections;

public class JudgeNotify : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FindJudgeName() {
	
		Debug.Log ("Find judge name");
		
		LobbyPlayer[] players = FindObjectsOfType<LobbyPlayer> ();

		int playerNumber = players.Length;
		
		for (int i=0; i< playerNumber; i++) {

		}
	

	}
}
