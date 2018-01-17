using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // game manager needs to be singleton so that it is accesable in all scenes
    // it also doens't allow multiple copies of the object
    public static GameManager instance;

    // don't want following manipulated in inspector window
    [HideInInspector]
    public bool gameStarted, gameRestarted;
    [HideInInspector]
    public int score, coinScore, lifeScore;

    void Awake ()
    {
        MakeSingleton();
    }
    
    // following 2 are delegates
    void OnEnable()
    {// subscribe to event
        SceneManager.sceneLoaded += LevelFinishedLoading; // subscribe to LevelFinishedLoading function
    }
    void OnDisable()
    {// unsubscribe from event
        SceneManager.sceneLoaded -= LevelFinishedLoading;
    }

    void LevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // if current scene is gameplay
        // if game has restarted (after player died)
        // set new values for scores
        // else if started from main menu
        // set initial values
        if (scene.name == "Gameplay")
        {
            if (gameRestarted) // after death
            {
                GameplayController.instance.SetScore(score);
                GameplayController.instance.SetCoinScore(coinScore);
                GameplayController.instance.SetLifeScore(lifeScore);

                PlayerScore.score = score;
                PlayerScore.coinCount = coinScore;
                PlayerScore.lifeCount = lifeScore;
            }
            else if (gameStarted) // from main menu
            {
                PlayerScore.score = 0;
                PlayerScore.coinCount = 0;
                PlayerScore.lifeCount = 2;
                
                GameplayController.instance.SetScore(0);
                GameplayController.instance.SetCoinScore(0);
                GameplayController.instance.SetLifeScore(2);
            }
            
        }
    }

    void MakeSingleton ()
    {
        // if instance already exists, destroy this game object
        // otherwise don't destroy
        // this prevents duplicate GameManagers
        if (instance != null)
        { 
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
	}

    public void CheckGameStatus(int score, int coinscore, int lifeScore)
    {
        if (lifeScore < 0)
        {
            gameStarted = false;
            gameRestarted = false;

            // gameplayController
        }
        else
        {

        }
    }

}
