using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public static int score;
	private Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text>();
		Reset();
	}

	public void Score(int points){
		score += points;
		scoreText.text = score.ToString();
	}

	public static void Reset(){
		score = 0;
	}
}
