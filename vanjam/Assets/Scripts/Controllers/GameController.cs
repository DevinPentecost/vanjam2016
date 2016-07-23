using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//The player
	public PlayerController player;

	//Various UI and score and stuff
	public SuspicionController suspicion;
	public ScoreController score;
	public TimeController time;

	//What is the game state?
	public bool isNight = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//The player looked into a house, up the suspicion
	public void InspectedHouse()
	{
		//We let the suspicion thing know
		this.suspicion.UpdateSuspicionInspection();
	}

	//The player collected some eggs
	public void EggsCollected(int eggs)
	{
		//How many? Let the score know
		this.score.IncreaseScore(eggs);
	}

	//It is night time, time to check the clock!
	public void NightTime(bool begin)
	{
		//Do we count time now then?
		if (begin)
		{
			//Tell the timer to count
			this.time.CountTime(begin);
		}
		else
		{
			//Don't count time and go to the next level
			this.time.CountTime(begin);
		}
	}
}
