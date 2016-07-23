using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//The velocity when walking
	public float walkingSpeed = 0.5f;
	private float currentSpeed = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Is the user pressing left?
		this.currentSpeed = Input.GetAxis("Movement") * this.walkingSpeed;

		//If we are moving...
		if(this.currentSpeed != 0)
		{
			//Update our position
			Vector3 currentPosition = this.transform.localPosition;
			currentPosition.x += this.currentSpeed;

			//Now set the position
			this.transform.localPosition = currentPosition;
		}
	
	}
}
