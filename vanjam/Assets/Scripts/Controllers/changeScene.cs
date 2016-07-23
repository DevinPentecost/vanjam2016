using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Are we colliding with something?
    void OnTriggerEnter2D(Collider2D collision)
    {
        //What did we collide with?
        if (collision.gameObject.tag == Toolbox.TAG_PLAYER)
        {
            //Handle that here
            
        }
    }

    
}
