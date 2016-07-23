using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//A variety of prefabs

	//The velocity when walking
	public float walkingSpeed = 0.5f;
	private float currentSpeed = 0f;

	//Is the player hiding?
	public bool canHide = false;
	public bool isHiding = false;

	//Is the player inspecting or robbing?
	public HouseController inspectionTarget;
	public bool canInspect = false;
	public bool isInspecting = false;
	private float inspectionWalkTime = 1.0f;
	private float inspectionWaitTime = 5.0f;
	private int inspectionStage = -1; //Walking towards, looking in the window, or walking back
	private Vector3 inspectionStartPosition;
	private float inspectionStartTime;

	//Where to go when hiding in a bush
	private float hidingZ = -1;
	private float standardZ = -2;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update()
	{
		//Are we busy?
		bool busy = this.isHiding || this.isInspecting;

		//Did the fire button get pushed
		if (Input.GetKeyDown(Toolbox.KEY_ACTION))
		{
			//We want to interact with stuff

			//Can we hide?
			if (this.canHide)
			{
				//We handle that
				this.HandleHide(!this.isHiding);
			}

			//What about inspecting or robbing?
			if (this.canInspect && !this.isInspecting)
			{
				//We handle that
				this.HandleInspection();
			}
		}


		//Update the speed if we are not otherwise busy
		if (!busy)
		{
			this.UpdateSpeed();
		}

		//If we are inspecting we need to behave accordingly
		if (this.isInspecting)
		{
			//Update it
			this.UpdateInspection();
		}
	}

	//Update the speed every frame
	void UpdateSpeed()
	{
		//Is the user pressing left?
		this.currentSpeed = Input.GetAxis(Toolbox.AXIS_MOVEMENT) * this.walkingSpeed;

		//If we are moving...
		if (this.currentSpeed != 0)
		{
			//Update our position
			Vector3 currentPosition = this.transform.localPosition;
			currentPosition.x += this.currentSpeed;

			//Now set the position
			this.transform.localPosition = currentPosition;
		}
	}

	//Hiding
	void HandleHide(bool hiding = true)
	{
		//Set that we are hiding
		this.isHiding = hiding;

		//Move our Z
		float targetZ = this.standardZ;
		if (this.isHiding)
		{
			//We instead hide behind
			targetZ = this.hidingZ;
		}

		//Now set the transform
		Vector3 newTransform = this.transform.localPosition;
		newTransform.z = targetZ;

		//And finally...
		this.transform.localPosition = newTransform;
	}

	//Inspecting
	void HandleInspection()
	{
		//Get the player's current position and the time
		this.inspectionStartPosition = this.transform.localPosition;
		this.inspectionStartTime = Time.time;

		//Now we start inspecting
		this.isInspecting = true;
		this.inspectionStage = 0;
	}

	//Update inspection
	void UpdateInspection()
	{
		//Get a target position
		Vector3 targetPosition = this.transform.localPosition;

		//What is the windows position in world coordinates?
		Vector3 windowPosition = this.inspectionTarget.transform.localPosition + this.inspectionTarget.windowPoint;

		//What stage are we at?
		if(this.inspectionStage == 0)
		{
			//How long has it been?
			float time = (Time.time - this.inspectionStartTime);
			time = time / inspectionWalkTime;

			//Are we done?
			if(time >= 1)
			{
				//Move on
				this.inspectionStage = 1;
				this.inspectionStartTime = Time.time;
			}
			else
			{
				//Keep moving towards the window
				targetPosition = Vector3.Lerp(this.inspectionStartPosition, windowPosition, time);
			}			
		}

		//Are we in stage 2? Just wait
		if(this.inspectionStage == 1)
		{
			//Waiting...
			float time = (Time.time - this.inspectionStartTime) - this.inspectionWaitTime;

			//Are we done?
			if(time > 0)
			{
				//We did it
				this.inspectionStage = 2;
				this.inspectionStartTime = Time.time;
			}
        }

		//We're walking back then
		if(this.inspectionStage == 2)
		{
			//How long has it been?
			float time = (Time.time - this.inspectionStartTime) / inspectionWalkTime;

			//Are we done?
			if (time >= 1)
			{
				//Wait for another inspection
				this.inspectionStage = -1;
				this.isInspecting = false;
			}
			else
			{
				//Keep moving towards the gate
				targetPosition = Vector3.Lerp(windowPosition, this.inspectionStartPosition, time);
			}
		}

		//Now lets set the target position
		this.transform.localPosition = targetPosition;
	}
}
