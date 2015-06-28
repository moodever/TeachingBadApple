using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class JoinGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InputField ipField = GameObject.Find ("IPInputField").GetComponent<InputField> ();
		ipField.text = NetworkManager.singleton.networkAddress;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnJoinGame() {
		Debug.Log ("Try to join game");
		InputField ipField = GameObject.Find ("IPInputField").GetComponent<InputField> ();

		NetworkManager.singleton.networkAddress = ipField.text;
		NetworkManager.singleton.StartClient();

		// Disable Buttons and wait
		// Server will trigger a change scene when game starts
	}

	public void OnBackToMain() {
		Application.LoadLevel ("StartMenu");
	}
}
