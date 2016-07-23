using UnityEngine;
using System.Collections;

public class SunMoonControlller : MonoBehaviour {

	//The sprites
	public Sprite sunSprite;
	public Sprite moonSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Toggle which we are
	public void ToggleSprite()
	{
		//Check the current sprite
		Sprite currentSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

		//Which is it?
		Sprite newSprite;
		if(currentSprite == this.sunSprite)
		{
			//Change it
			newSprite = this.moonSprite;
		}
		else
		{
			//We're going to the sun
			newSprite = this.sunSprite;
		}

		//Now use it
		this.gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
	}
}
