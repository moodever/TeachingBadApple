using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MyNetworkManager : NetworkManager {

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
		GameObject player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		Object.DontDestroyOnLoad (player);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
}
