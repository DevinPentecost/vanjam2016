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

	//Where to go when hiding in a bush
	private float hidingZ = -1;
	private float standardZ = -2;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update()
	{
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
		}


		//Update the speed
		this.UpdateSpeed();
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
}
