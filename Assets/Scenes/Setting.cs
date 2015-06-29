using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Setting : MonoBehaviour {

	int avatarIndex; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonClick(){
		InputField field = GameObject.Find ("InputField").GetComponent<InputField> ();
		string userName = field.text;
		PlayerPrefs.SetString("Player Name", userName);
		PlayerPrefs.Save ();
		Application.LoadLevel("StartMenu");
	}



	public void SwitchAvatar(int index ){
		RawImage avatarImgTarget = GameObject.Find ("currentAvatarImg").GetComponent<RawImage> ();

		string sourceBtnName = String.Format("AvatarBtn{0}",index);

		Button avatarButton = GameObject.Find (sourceBtnName).GetComponent<Button> ();
		avatarImgTarget.texture = avatarButton.image.mainTexture;

		//avatarImgTarget.texture=
//		string userName = field.text;

	}


	public void OnAvatarBtn1Click(){
		//image.texture
		PlayerPrefs.SetInt("Player Avatar", 1);
		PlayerPrefs.Save ();
		SwitchAvatar (1);
	}


	public void OnAvatarBtn2Click(){
		PlayerPrefs.SetInt("Player Avatar", 2);
		PlayerPrefs.Save ();
		SwitchAvatar (2);

	}

	public void OnAvatarBtn3Click(){
		PlayerPrefs.SetInt("Player Avatar", 3);
		PlayerPrefs.Save ();
		SwitchAvatar (3);
	}

	public void OnAvatarBtn4Click(){
		PlayerPrefs.SetInt("Player Avatar", 4);
		PlayerPrefs.Save ();
		SwitchAvatar (4);
	}

	public void OnAvatarBtn5Click(){
		PlayerPrefs.SetInt("Player Avatar", 5);
		PlayerPrefs.Save ();
		SwitchAvatar (5);
	}

	public void OnAvatarBtn6Click(){
		PlayerPrefs.SetInt("Player Avatar", 6);
		PlayerPrefs.Save ();
		SwitchAvatar (6);
	}


}
