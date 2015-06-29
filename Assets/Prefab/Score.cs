using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Score : MonoBehaviour {

	public List<int> arrayOfScores;

	
	public Score() {

		arrayOfScores = new List<int>();
		
	}


	public int getScore(int index) {

		return arrayOfScores [index];

	}



	public void setScore(int index,int score) {


		while (index>=arrayOfScores.Count) {

			arrayOfScores.Add(0);

		}

		arrayOfScores [index] = score;

	}

}
