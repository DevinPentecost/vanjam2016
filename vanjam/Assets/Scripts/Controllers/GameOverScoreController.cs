using UnityEngine;
using System.Collections;

public class GameOverScoreController : MonoBehaviour {

    public int eggCount = 0;

    // Use this for initialization
    void Start () {
        //First thing we do is set our text to match the egg count
        this.GetComponent<TextMesh>().text = this.GetComponent<TextMesh>().text + this.eggCount.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
