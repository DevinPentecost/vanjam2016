using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // neded in order to load scenes

public class HowToController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            StartLevel();
        }

    }
    void StartLevel()
    {
        SceneManager.LoadScene(2);
    }
}
