using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    // controls main menu interactions (switching screens)

	void Start ()
    {
		
	}
	
    // following 3 LoadScenes need to be named exactly the same as the scene they will go to
    public void StartGame()
    {
        // set that game has started from main menu
        GameManager.instance.gameStarted = true;
        // load gameplay scene to start game
        SceneManager.LoadScene("Gameplay");
    }


    public void GoToHighscoreMenu()
    {
        // this will attach to the Highscore Button

        SceneManager.LoadScene("HighScore");
    }


    public void GoToOptions()
    {
        // this will attach to the Options Button

        SceneManager.LoadScene("OptionsMenu");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void MusicButton()
    {

    }
}
