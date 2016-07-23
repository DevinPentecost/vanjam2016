using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

	//How much time remains
	private float nightTime = 120; //Seconds
	private float currentTime = 0;

	//Are we counting time?
	private bool countingTime = false;

	// Use this for initialization
	void Start () {
		//Start the time at max
		this.currentTime = this.nightTime;

		//Every second decrease the time
		InvokeRepeating("DecreaseTime", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		//Update the text with the suspicion level
		this.UpdateTime();

		//Are we out of time?
		this.CheckTime();
	}

	//Update the Score UI
	void UpdateTime()
	{
		//Just set the string
		this.GetComponent<Text>().text = "Time: " + this.currentTime;
	}

	//Check if we're out of time
	void CheckTime()
	{
		//What time remains? Is it out?
		if(this.currentTime <= 0)
		{
			//We're done for!
		}
	}

	//Decrease timer
	void DecreaseTime()
	{
		//Simple...
		if (this.countingTime)
		{
			this.currentTime -= 1;
		}
	}

	//Stop the timer?
	public void CountTime(bool count)
	{
		//Do we?
		this.countingTime = count;
	}
}
