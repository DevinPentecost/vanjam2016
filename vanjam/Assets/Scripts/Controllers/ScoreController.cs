using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	//What is the user's score?
	public int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Update the text with the suspicion level
		this.UpdateScore();
	}

	//Update the Score UI
	void UpdateScore()
	{
		//Just set the string
		this.GetComponent<Text>().text = "Score: " + this.score;
	}

	//Increase the score
	public void IncreaseScore(int eggs)
	{
		//Simply add
		this.score += eggs;
	}
}
