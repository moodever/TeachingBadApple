using UnityEngine;
using UnityEngine.Networking;

using System.Collections;

public class PlayerConsole {

	private static int PORT = 12349;

	public static PlayerConsole INSTANCE = new PlayerConsole ();

	public NetworkClient client;

	private PlayerConsole() {

	}

	public void connectToLocal(NetworkMessageDelegate onConnect) {
		client = ClientScene.ConnectLocalServer ();
		client.RegisterHandler (MsgType.Connect, onConnect);
	}

	public void connect(string server, NetworkMessageDelegate onConnect) {
		client = new NetworkClient ();
		if(onConnect != null)
			client.RegisterHandler (MsgType.Connect, onConnect);
		client.Connect (server, PORT);
	}
}

public class ServerConsole {
	private static int PORT = 12349;

	public static ServerConsole INSTANCE = new ServerConsole();

	private ServerConsole() {
	
	}

	public void startServer(NetworkMessageDelegate onConnect) {
		NetworkServer.Listen (PORT);
		if (onConnect != null) {
			NetworkServer.RegisterHandler (MsgType.Connect, onConnect);
		}
	}
}