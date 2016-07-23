using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {

    public GameController controller;
    public GameObject player;
    public float boundSide = 1f;

	// Initialize collision mesh
	void Start () {
        float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        BoxCollider2D boxCollider = GetComponent("BoxCollider2D") as BoxCollider2D;
        Vector2 bound = new Vector2 (horzExtent * boundSide,0);
        boxCollider.offset = bound;  
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Are we colliding with something?
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If we player collides with bound
        if (collision.gameObject.tag == Toolbox.TAG_PLAYER)
        {
            // Flip isNight value
            controller.isNight=(!controller.isNight);

            // Move player back to the center of the level
            Vector3 newPos = player.transform.localPosition;
            newPos.x = 0;
            player.transform.localPosition = newPos ;
        }
    }


}
