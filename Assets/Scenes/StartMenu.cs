using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartButtonClick() {
		NetworkManager.singleton.StartHost ();

		Application.LoadLevel ("WaitPlayer");
	}

	public void OnJoinButtonClick() {
		Application.LoadLevel ("JoinGame");
	}
}
