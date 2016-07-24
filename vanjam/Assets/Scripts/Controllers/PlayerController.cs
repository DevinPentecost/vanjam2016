using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//A variety of prefabs
	public GameController gameController;
	public GameObject eggCountNotificationPrefab;

	//Where should notifications be?
	private float notificationY = 1;
	private float notificationZ = -1;

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
	public bool canRob = false;
	public bool isRobbing = false;
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
		bool busy = this.isHiding || this.isInspecting || this.isRobbing;

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
			if ((this.canInspect && !this.isInspecting) || (this.canRob && !this.isRobbing))
			{
				//We handle that
				this.HandleInspection(this.canRob);
			}
		}


		//Update the speed if we are not otherwise busy
		if (!busy)
		{
			this.UpdateSpeed();
		}

		//If we are inspecting we need to behave accordingly
		if (this.isInspecting || this.isRobbing)
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
	void HandleInspection(bool robbing = false)
	{
		//Get the player's current position and the time
		this.inspectionStartPosition = this.transform.localPosition;
		this.inspectionStartTime = Time.time;

		//Now we start inspecting
		this.inspectionStage = 0;

		//Are we robbing?
		if (!robbing)
		{
			//Update our meter
			this.isInspecting = true;
			gameController.InspectedHouse();
		}
		else
		{
			//We're robbing instead
			this.isRobbing = true;
		}
		
	}

	//Update inspection
	void UpdateInspection()
	{
		//Get a target position
		Vector3 finalPosition = this.transform.localPosition;

		//What is the windows position in world coordinates?
		Vector3 targetPoint;
		if (this.isInspecting)
		{
			targetPoint = this.inspectionTarget.windowPoint;
		}else
		{
			targetPoint = this.inspectionTarget.doorPoint;
		}
		Vector3 targetPosition = this.inspectionTarget.transform.localPosition + targetPoint;

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

				//Show them eggs
				//Display the number of eggs to the player
				int eggs = this.inspectionTarget.eggs;
				this.DisplayEggNotification(eggs);
			}
			else
			{
				//Keep moving towards the window
				finalPosition = Vector3.Lerp(this.inspectionStartPosition, targetPosition, time);
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

				//Are we robbing?
				if (this.isRobbing)
				{
					//We should take eggs from the house
					int eggs = this.inspectionTarget.RobHouse();

					//Let the game controller know we took some eggs
					this.gameController.EggsCollected(eggs);
				}
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
				this.isRobbing = false;
			}
			else
			{
				//Keep moving towards the gate
				finalPosition = Vector3.Lerp(targetPosition, this.inspectionStartPosition, time);
			}
		}

		//Now lets set the target position
		this.transform.localPosition = finalPosition;
	}

	//Display the egg notification
	private void DisplayEggNotification(int eggs)
	{
		//We want to make the notification
		GameObject notification = Instantiate(this.eggCountNotificationPrefab);
		notification.GetComponent<EggCountNotificationController>().eggCount = eggs;

		//It should follow the player while it is still alive
		notification.transform.parent = this.transform;

		//Place it above the player
		Vector3 notificationPosition = new Vector3(0, this.notificationY, this.notificationZ);
		notification.transform.localPosition = notificationPosition;
	}
}
