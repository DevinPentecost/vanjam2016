using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//The player
	public PlayerController player;

	//Various UI and score and stuff
	public SuspicionController suspicion;
	public ScoreController score;
	public TimeController time;

	//Other stuff to track
	public SunMoonControlller sunMoon;

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
		//Set night time
		this.isNight = begin;

		//Do we count time now then?
		if (begin)
		{
			//Fade to black

			//Put the player in the right spot

			//Update sprites on the player

			//Tell the timer to count
			this.time.CountTime(begin);

			//Stop counting suspicion
			this.suspicion.countSuspicion = false;

			//What is the current suspicion?
			float suspicion = this.suspicion.currentSuspicion;

			//Don't count time and go to the next level
			this.time.CountTime(begin);

			//Find all houses
			GameObject[] houses = GameObject.FindGameObjectsWithTag(Toolbox.TAG_HOUSE);

			//Go through each house
			foreach (GameObject house in houses)
			{
				//Get the controller
				HouseController houseController = house.GetComponent<HouseController>();

				//Tell it it is night time...
				houseController.HandleNight();

				//Also how suspicious were we?
				houseController.HandleSuspicion(suspicion);
			}
		}
		else
		{
			//Don't count time and go to the next level
			this.time.CountTime(begin);

			//Fade to black, next level, etc
		}

		//Toggle the moon and sun
		this.sunMoon.ToggleSprite();
	}
}
