using UnityEngine;
using System.Collections;

public class DebugSpriteController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Are we debugging?
		if (!Toolbox.DEBUG)
		{
			//We destroy ourselves
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
