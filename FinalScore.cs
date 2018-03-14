using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text scoreText = GetComponent<Text>();
		scoreText.text = ScoreController.score.ToString();
		ScoreController.Reset();
	}
}
