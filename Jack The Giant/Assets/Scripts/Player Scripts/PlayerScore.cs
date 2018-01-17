using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinClip, lifeClip;

    private CameraScript cameraScript;

    private Vector3 previousPosition;
    private bool countScore; // whether or not score should be counting

    public static int score;
    public static int lifeCount;
    public static int coinCount;

    void Awake()
    {
        // get camera
        cameraScript = Camera.main.GetComponent<CameraScript>();
    }

    void Start ()
    {
        previousPosition = transform.position;
        countScore = true;
	}
	
	void Update ()
    {
        CountScore();
	}

    void CountScore()
    {
        // if we are counting the score
        // if we are lower than previous position
        // increase score
        if (countScore)
        {
            if (transform.position.y < previousPosition.y)
            { 
                score++;
            }
            // set new previous position
            previousPosition = transform.position;
            GameplayController.instance.SetScore(score);
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        // when player touches coin, life, dark cloud, upper and lower bounds

        // collect coin, add to score, and coin count
        // play CoinClip sound, set coin to inactive
        if (target.tag == "Coin")
        {
            coinCount++;
            score += 100;
            // set score values
            GameplayController.instance.SetScore(score);
            GameplayController.instance.SetCoinScore(coinCount);

            AudioSource.PlayClipAtPoint(coinClip, transform.position);
            target.gameObject.SetActive(false);
        }

        if (target.tag == "Life")
        {
            lifeCount++;
            score += 200;

            GameplayController.instance.SetScore(score);
            GameplayController.instance.SetLifeScore(lifeCount);

            AudioSource.PlayClipAtPoint(lifeClip, transform.position);
            target.gameObject.SetActive(false);
        }

        // if player hits bounds or dark cloud, kill player, stop camera, stop score counting
        if (target.tag == "Bounds")
        {
            cameraScript.moveCamera = false;
            countScore = false;
            // deal with game over and pass through scores
            GameplayController.instance.GameOverShowPanel(score, coinCount);

            transform.position = new Vector3(500, 500, 0); // reposition player outside of camera, so we can reset later
            lifeCount--;
        }

        if (target.tag == "Deadly")
        {
            cameraScript.moveCamera = false;
            countScore = false;

            GameplayController.instance.GameOverShowPanel(score, coinCount);

            transform.position = new Vector3(500, 500, 0); // reposition player outside of camera, so we can reset later
            lifeCount--;
        }

    }
}
