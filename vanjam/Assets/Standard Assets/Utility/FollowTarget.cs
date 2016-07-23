using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 0f, 0f);
        public GameObject player;

        // Distance from the center of the level
        public float levelBound = 15;

        private void LateUpdate()
        {
            // Camera follows the player if it is within the level bounds
            if(player.transform.position.x < levelBound & player.transform.position.x > -levelBound){
                transform.position = target.position + offset;
            }
        }
    }
}
