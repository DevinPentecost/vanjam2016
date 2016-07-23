﻿using UnityEngine;
using System.Collections;

public class ChickenCopController : MonoBehaviour {

	//The waypoints it walks between
	public GameObject waypointOne;
	public GameObject waypointTwo;

	//How fast does it walk?
	private float speed = 0.5f;

	//How long does it wait?
	private float waitTime = 3f;
	private float waitTimer = -1;

	//Is it walking or waiting?
	private bool walking = false;

	//What is the next waypoint? Where did we start at?
	private GameObject nextWaypoint;
	private Vector3 startPosition;
	private float patrolLength;
	private float startTime;

	// Use this for initialization
	void Start () {
		//Walk towards the first waypoint
		this.nextWaypoint = waypointOne;
	}
	
	// Update is called once per frame
	void Update () {

		//Are we walking?
		if (this.walking)
		{
			//Figure out where we should be
			float distance = (Time.time - this.startTime) * this.speed;
			float patrolPercentage = distance / this.patrolLength;

			//Put it that amount along the path
			this.transform.localPosition = Vector3.Lerp(this.startPosition, this.nextWaypoint.transform.localPosition, patrolPercentage);

			//Are we all the way there?
			if(patrolPercentage >= 1)
			{
				//We need to wait a bit
				this.walking = false;
				this.waitTimer = Time.time;
			}
		}
		else
		{
			//Count down the timer
			float timeWaited = Time.time - this.waitTimer;

			//Did we wait long enough?
			if(timeWaited >= this.waitTime)
			{
				//We start walking again
				if(this.nextWaypoint == this.waypointOne)
				{
					//Next!
					this.nextWaypoint = this.waypointTwo;
				}
				else
				{
					//Go to one
					this.nextWaypoint = this.waypointOne;
				}

				//Set up the patrol
				this.startTime = Time.time;
				this.startPosition = this.transform.localPosition;
				this.patrolLength = Vector3.Distance(this.transform.localPosition, this.nextWaypoint.transform.localPosition);
				this.walking = true;
			}
		}
	}
}
