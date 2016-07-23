using UnityEngine;
using System.Collections;

public class chickenController : MonoBehaviour {

    public GameObject house;

    private float houseX;
    private float yardSize;

    private float targetX;
    private float startX;
    private float chickenSpeed = 2f;
    private float startTime;
    private float travelDirection;

    // is chicken waiting to move
    private bool chickenWaiting;
    private float waitTime = 90f;

	// Use this for initialization
	void Start () {
        houseX = house.transform.localPosition.x;
        yardSize = house.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        setTargetX();
	}
	
	// Update is called once per frame
    void Update () {
        // is chicken waiting to move
        if (chickenWaiting)
        {
            // count down to move
            waitTime = waitTime - 1;
            // if wait time expired reset and set targetX
            if (waitTime <= 0)
            {
                waitTime = 100;
                chickenWaiting = false;
                setTargetX();
            }
        }

        // else move the chicken
        else
        {
            Vector3 currentPos = this.transform.localPosition;
            // updated x
            currentPos.x = startX + travelDirection*chickenSpeed*(Time.time-startTime);
            this.transform.localPosition = currentPos;

            // if chicken moving right and past the target stop and wait
            if (travelDirection>0 & currentPos.x > targetX)
            {
                chickenWaiting = true;
            }
            // if chicken moving left and past the target stop and wait
            if (travelDirection < 0 & currentPos.x < targetX)
            {
                chickenWaiting = true;
            }
        }
	}

    void setTargetX()
    {
        targetX = Random.Range(0, yardSize);
        startTime = Time.time;
        startX = transform.localPosition.x;
        travelDirection = 1;
        if (startX > targetX)
        {
            travelDirection = -1;
        }
    }

    // not used
    void getWaitTime()
    {

    }
}
