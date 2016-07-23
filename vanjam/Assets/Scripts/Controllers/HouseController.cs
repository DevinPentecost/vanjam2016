using UnityEngine;
using System.Collections;

public class HouseController : MonoBehaviour {

	//The inspection sprite
	public Sprite inspectionSprite;
	public Sprite stealSprite;
	public GameObject houseNotification;
	private PlayerController notificationTarget;
	private GameObject currentNotification;

	//Where on the house should we inspect to?
	public Vector3 windowPoint = new Vector3();

	//Where is the door?
	public Vector3 doorPoint = new Vector3();

	//How high above the player is the notification?
	private float notificationHeight = 1;

	// Use this for initialization
	void Start () {

		//Get the window and door positions
		this.windowPoint = transform.FindChild("Window").transform.localPosition;
		this.doorPoint = transform.FindChild("Door").transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {
		//The notification should follow...
		this.UpdateNotification();
	}

	void UpdateNotification()
	{
		//Do we even have one?
		if (this.currentNotification && this.notificationTarget)
		{
			//Should it be regular?
			if (notificationTarget.isInspecting)
			{
				//We use the up one
				currentNotification.GetComponent<SpriteRenderer>().sprite = this.inspectionSprite;
			}
			else
			{
				//Point down
				currentNotification.GetComponent<SpriteRenderer>().sprite = this.inspectionSprite;
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
			this.HandleTouchingPlayer(collision.gameObject, enter: true);
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
			//We need to let the player know they can look at the house
			this.currentNotification = Instantiate(this.houseNotification);
			this.notificationTarget = targetObject.GetComponent<PlayerController>();

			//The user can inspect or rob?
			this.notificationTarget.canInspect = enter;
			this.notificationTarget.inspectionTarget = this;
		}
		else
		{
			//The player can no longer look
			this.notificationTarget.canInspect = enter;
			Destroy(this.currentNotification);
			this.notificationTarget = null;
		}
	}
}
