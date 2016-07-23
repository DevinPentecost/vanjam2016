using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuspicionController : MonoBehaviour {

	//What is the current suspicion at? Out of 100
	public float currentSuspicion = 0f;
	public float maxSuspicion = 100f;

	//Are we counting suspicion?
	public bool countSuspicion = true;

	//What is the rate/s of suspicion?
	public float suspicionRate = 0.25f;
	public float suspicionInspect = 10f;

	// Use this for initialization
	void Start () {
		//Every second, increase the suspicion
		InvokeRepeating("UpdateSuspicionOverTime", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {

		//Update the text with the suspicion level
		this.UpdateSuspicion();
	}

	//Update the Suspicion UI
	void UpdateSuspicion()
	{
		//Suspicion cannot go over a limit
		this.currentSuspicion = Mathf.Clamp(this.currentSuspicion, 0, this.maxSuspicion);

		//Just set the string
		this.GetComponent<Text>().text = "Suspicion: " + this.currentSuspicion + "%";
    }

	//Update the suspicion over time
	void UpdateSuspicionOverTime()
	{
		//Are we even updating?
		if (this.countSuspicion)
		{
			//Increase it
			this.currentSuspicion += this.suspicionRate;
		}
	}

	//Update because the house was looked at
	public void UpdateSuspicionInspection()
	{
		//Are we even updating?
		if (this.countSuspicion)
		{
			//Increase it
			this.currentSuspicion += this.suspicionInspect;
		}
	}
}
