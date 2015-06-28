using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CustomMsgType {

	/// <summary>
	/// The card chosen by player. This is a message sent to server from client
	/// </summary>
	public static short CardChosen = MsgType.Highest + 1;

	/// <summary>
	/// The answer chosen by judge. This is a message broadcast to clients
	/// </summary>
	public static short AnswerChosen = MsgType.Highest + 2;

	/// <summary>
	/// The id assigned to a client. This is a message sent to a client
	/// </summary>
	public static short AssignID = MsgType.Highest + 3;
}

public class CardChosenMessage : MessageBase {
	public int cardChosen;
	public int playerId;
}

public class AnswerChosenMessage: MessageBase {
	public int answerChosen;
}

public class AssignIDMessage : MessageBase {
	public int id;
}