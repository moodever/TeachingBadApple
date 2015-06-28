using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ResultPlayer : MonoBehaviour {

	[SyncVar]
	public int id;

	// Use this for initialization
	void Start () {
	
	}

	[Command]
	public void CmdNextRound() {
		Debug.Log ("Next Round");
		int judgeIndex = Registry.getInt("judgeIndex");
		int sum = Registry.getInt ("playerCount");
		judgeIndex = (judgeIndex + 1) % sum;
		Registry.put ("judgeIndex", judgeIndex);
		
		int question = Data.genQuestion ();
		int[][] data = Data.genCardSets (sum - 1, 5);
		
		int counter = 0;
		foreach (ResultPlayer player in GetComponents<ResultPlayer> ()) {
			if(player.id == judgeIndex) {
				player.RpcStartAsJudge (question);
			} else {
				player.RpcStartAsPlayer (question, data[counter++]);
			}
		}
	}

	[ClientRpc]
	public void RpcStartAsJudge(int question) {
		// Player together with id will get lost after switching scene
		// Save in Registry
		Registry.put ("playerId", id);
		Registry.put ("question", question);
		Registry.put ("judge", true);
		SwitchScene ("JudgeMain");
	}
	
	[ClientRpc]
	public void RpcStartAsPlayer(int question, int[] choices) {
		// Player together with id will get lost after switching scene
		// Save in Registry
		Debug.Log ("here");
		Registry.put ("playerId", id);
		Registry.put ("question", question);
		Registry.put ("choices", choices);
		Registry.put ("judge", false);
		SwitchScene("PlayerMain");
	}

	void SwitchScene (string name) {
		Application.LoadLevel (name);
	}

}
