using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    // create instance of class (variable of it)
    public static GameplayController instance;

    [SerializeField]
    private Text scoreText, coinText, livesText, gameOverScoreText, gameOverCoinText;

    [SerializeField]
    private GameObject pausePanel, gameOverPanel;

    [SerializeField]
    private GameObject readyButton;

    void Awake ()
    {
        MakeInstance();
	}

    void Start()
    {
        Time.timeScale = 0f;

    }
	
	void MakeInstance ()
    {
        // if instance is not created, instance is equal to this class
		if (instance == null)
        {
            instance = this;
        }
	}

    public void GameOverShowPanel(int finalScore, int finalCoinScore)
    {
        gameOverPanel.SetActive(true);
        // convert the number to string to place in text box, so 10 == "10"
        gameOverScoreText.text = finalScore.ToString();
        gameOverCoinText.text = finalCoinScore.ToString();
        // start coroutine
        StartCoroutine("GameOverLoadMainMenu");
    }

    IEnumerator GameOverLoadMainMenu()
    {
        // wait a few seconds, then load main menu
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }

    public void SetScore(int score)
    {
        scoreText.text = "x" + score;
    }

    public void SetCoinScore (int coinScore)
    {
        coinText.text = "x" + coinScore;
    }

    public void SetLifeScore(int lifeScore)
    {
        livesText.text = "x" + lifeScore;
    }

    public void PauseGame()
    {
        // stops/pauses game
        Time.timeScale = 0f;
        // activate pause menu
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        // resumes game
        Time.timeScale = 1f;
        // deactivate pause menu
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        // resume time because otherwise it won't go to main menu, no animations will work etc
        // putting this on 0f here would prevent main menu animations from playing
        Time.timeScale = 1f;
        // go back to main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void StartTheGame()
    {
        Time.timeScale = 1f;
        readyButton.SetActive(false);
    }

}
