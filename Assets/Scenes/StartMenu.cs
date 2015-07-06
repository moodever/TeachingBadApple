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
		Registry.put ("host", true);
		Application.LoadLevel ("WaitPlayer");
	}

	public void OnJoinButtonClick() {
		Application.LoadLevel ("JoinGame");
	}

	public void OnSettingButtonClick() {
		Registry.put ("Previous Scene", "StartMenu");
		Application.LoadLevel ("Setting");
	}

	public void OnInstructionButtonClick() {
		Registry.put ("Previous Scene", "StartMenu");
		Application.LoadLevel ("instruction");
	}

}
