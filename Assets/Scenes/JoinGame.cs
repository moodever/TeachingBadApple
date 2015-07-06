using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

public class JoinGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		loadPlayersetting ();

	}


	public void onChangeBtnClick(){
		
		Registry.put ("Previous Scene", "JoinGame");
		Application.LoadLevel ("Setting");

	}

	void loadPlayersetting (){

		InputField ipField = GameObject.Find ("IPInputField").GetComponent<InputField> ();
		ipField.text = NetworkManager.singleton.networkAddress;
		
		string playerNameString = "";
		playerNameString = PlayerPrefs.GetString("Player Name");
		
		InputField nameField = GameObject.Find ("NameInputField").GetComponent<InputField> ();
		
		nameField.text = playerNameString;
		
		int avatarIndex = PlayerPrefs.GetInt("Player Avatar");
		
		RawImage avatarImg = GameObject.Find ("AvatarImg").GetComponent<RawImage> ();
		
		
		string path=String.Format("avatar/avatar{0}",avatarIndex);
		
		
		avatarImg.texture = Resources.Load(path) as Texture;
		
		Debug.Log ("the avatar image path is:" + path);

	}


	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnJoinGame() {
		InputField ipField = GameObject.Find ("IPInputField").GetComponent<InputField> ();
		NetworkManager.singleton.networkAddress = ipField.text;

		InputField nameField = GameObject.Find ("NameInputField").GetComponent<InputField> ();
		string userName = nameField.text;

		PlayerPrefs.SetString("Player Name", userName);
		PlayerPrefs.Save ();


		NetworkManager.singleton.StartClient();

		// Disable Buttons and wait
		// Server will trigger a change scene when game starts
	}

	public void OnBackToMain() {
		Application.LoadLevel ("StartMenu");
	}
}
