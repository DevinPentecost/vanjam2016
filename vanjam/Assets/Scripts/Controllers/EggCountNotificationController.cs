using UnityEngine;
using System.Collections;

public class EggCountNotificationController : MonoBehaviour {

	//The number to display
	public int eggCount = 0;

	//How long till I DIE!?!?
	private float notificationLife = 3f;

	// Use this for initialization
	void Start () {
		//First thing we do is set our text to match the egg count
		this.GetComponent<TextMesh>().text = this.eggCount.ToString();

		//After some time, kill the notification
		Destroy(this.gameObject, this.notificationLife);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
