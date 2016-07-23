using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//A variety of prefabs

	//The velocity when walking
	public float walkingSpeed = 0.5f;
	private float currentSpeed = 0f;

	//Is the player hiding?
	public bool canHide = false;
	private bool isHiding = false;

	//Where to go when hiding in a bush
	private float hidingZ = -1;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update()
	{



		//Update the speed
		this.UpdateSpeed();
	}

	//Update the speed every frame
	void UpdateSpeed()
	{
		//Is the user pressing left?
		this.currentSpeed = Input.GetAxis("Movement") * this.walkingSpeed;

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
}
