using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class Instruction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBackButtonClick() {
		//Registry.put ("Previous Scene", "StartMenu");
		Application.LoadLevel ("StartMenu");
	}
}
