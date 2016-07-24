using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // neded in order to load scenes

public class GameController : MonoBehaviour {

	//The player
	public PlayerController player;

	//Various UI and score and stuff
	public SuspicionController suspicion;
	public ScoreController score;
	public TimeController time;
    public GameObject gameOverPrefab;

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

			//Don't count time and go to the next level
			this.time.CountTime(begin);

			//Handle houses for night
			this.HandleHouseNight();

			//Handle chickens for night
			this.HandleChickenNight();

			//Handle cops
			this.HandleCopNight();
		}
		else
		{
			//Don't count time and go to the next level
			this.time.CountTime(begin);

            //display game over score and go to start menu
            GameOverScreen();
        }

		//Toggle the moon and sun
		this.sunMoon.ToggleSprite();
	}

    void GameOverScreen()
    {
        // instantiate the game over screen
        GameObject scoreDisplay = Instantiate(this.gameOverPrefab);
        scoreDisplay.GetComponent<GameOverScoreController>().eggCount = this.score.score;

        //It should follow the player
        scoreDisplay.transform.localPosition = player.transform.localPosition;
        scoreDisplay.transform.parent = player.transform;

        ExecuteAfterTime();

    }

    // go to start menu after time
    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(4.0f);
        // go to start menu again
        SceneManager.LoadScene(0);
    }

//Handle houses for night
void HandleHouseNight()
	{
		//What is the current suspicion?
		float suspicion = this.suspicion.currentSuspicion;

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

	//Handle chickens for night
	void HandleChickenNight()
	{
		//What is the current suspicion?
		float suspicion = this.suspicion.currentSuspicion;

		//Find all houses
		GameObject[] chickens = GameObject.FindGameObjectsWithTag(Toolbox.TAG_CHICKEN);

		//Go through each house
		foreach (GameObject chicken in chickens)
		{
			//Get the controller
			chickenController chickenController = chicken.GetComponent<chickenController>();

			//Tell it it is night time...
			chickenController.HandleNight();
		}
	}

	//Handle cops for night
	void HandleCopNight()
	{
		//What is the current suspicion?
		float suspicion = this.suspicion.currentSuspicion;

		//Find all houses
		GameObject[] cops = GameObject.FindGameObjectsWithTag(Toolbox.TAG_COP);

		//Go through each house
		foreach (GameObject cop in cops)
		{
			//Get the controller
			ChickenCopController copController = cop.GetComponent<ChickenCopController>();

			//Tell it it is night time...
			copController.HandleNight();

			//Also how suspicious were we?
			copController.HandleSuspicion(suspicion);
		}
	}
}
