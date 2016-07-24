using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement; // neded in order to load scenes

public class StartMenuController : MonoBehaviour {
    public Button startButton;
    public Button quitButton;

    // Use this for initialization
    void Start () {
        startButton = startButton.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();
    }

    public void StartLevel() //this function will be used on our Play button

    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame() //This function will be used on our quit button

    {
        Application.Quit(); //this will quit our game. Note this will only work after building the game

    }
}
