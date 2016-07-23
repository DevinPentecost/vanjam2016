using UnityEngine;
using System.Collections;

public class BushController : MonoBehaviour {

	//Notifications
	public Sprite canHideNotification;
	public Sprite unHideNotification;
	public GameObject hideNotification;
	private PlayerController notificationTarget;
	private GameObject currentNotification;

	//How high above the player is the notification?
	private float notificationHeight = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Update the notification
		this.UpdateNotification();
	}

	void UpdateNotification()
	{
		//Do we even have one?
		if(this.currentNotification && this.notificationTarget)
		{
			//Should it be regular?
			if (notificationTarget.isHiding)
			{
				//We use the up one
				currentNotification.GetComponent<SpriteRenderer>().sprite = this.unHideNotification;
			}
			else
			{
				//Point down
				currentNotification.GetComponent<SpriteRenderer>().sprite = this.canHideNotification;
			}

			//We need it to follow the player but be up a bit
			Vector3 targetPosition = this.notificationTarget.transform.localPosition;
			targetPosition.y = targetPosition.y + this.notificationHeight;
			this.currentNotification.transform.localPosition = targetPosition;
		}
	}

	//Are we colliding with something?
	void OnTriggerEnter2D(Collider2D collision)
	{
		//What did we collide with?
		if (collision.gameObject.tag == Toolbox.TAG_PLAYER)
		{
			//Handle that
			this.HandleTouchingPlayer(collision.gameObject, enter:true);
		}
	}

	//Are we leaving the player
	void OnTriggerExit2D(Collider2D collision)
	{
		//What did we collide with?
		if (collision.gameObject.tag == Toolbox.TAG_PLAYER)
		{
			//Handle that
			this.HandleTouchingPlayer(collision.gameObject, enter: false);
		}
	}

	//Handle colliding with a bush
	void HandleTouchingPlayer(GameObject targetObject, bool enter = false)
	{
		//Are we beginning to touch a bush?
		if (enter)
		{
			//We need to let the player know they can hide
			this.currentNotification = Instantiate(this.hideNotification);
			this.notificationTarget = targetObject.GetComponent<PlayerController>();

			//The user can hide?
			this.notificationTarget.canHide = enter;
		}
		else
		{
			//The player can no longer hide
			Destroy(this.currentNotification);
			this.notificationTarget = null;
		}	
	}
}
