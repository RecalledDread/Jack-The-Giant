using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class dealing with spawning of clouds
public class CloudSpawner : MonoBehaviour
{

    // Serialized and Private so nothing can access it, but we can see it in inspector
    [SerializeField]
    private GameObject[] clouds;

    // distance between clouds on y axis
    private float distanceBetweenClouds = 3f;
    // used to position clouds between these values
    private float minX, maxX;
    // lasts clouds Y position
    private float lastCloudPositionY;
    // control X position of clouds when randomizing position
    private float controlX;

    [SerializeField]
    private GameObject[] collectables;
    // Player used to spawn them above the first cloud
    private GameObject player;


	void Awake ()
    {
        controlX = 0;
        SetMinAndMaxX();
        CreateClouds();
        player = GameObject.Find("Player");

        for (int i = 0; i < collectables.Length; i++)
        {
            // deactivate all collectables to start
            collectables[i].SetActive(false);
        }

    }

    void Start()
    {
        PositionThePlayer();
    }

    // to set min and max X values
    void SetMinAndMaxX()
    {
        // Find screen coords and convert to Unity's world coords (ScreenToWorldPoint)
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)); // don't really need Screen.height for this part

        minX = -bounds.x + 0.5f; // + 0.5f so the cloud doesn't spawn 1/2 way off the screen
        maxX = bounds.x - 0.5f; // - 0.5f so the cloud doesn't spawn 1/2 way off screen
    }

    // Shuffle the clouds array to randomise positions of gameobjects in array
    void Shuffle(GameObject[] arrayToShuffle)
    {
        for (int i =0; i < arrayToShuffle.Length; i++)
        {
            GameObject temp = arrayToShuffle[i];
            // create random number to rearrange items in array
            int random = Random.Range(i, arrayToShuffle.Length);
            // switch current array item with random array item
            arrayToShuffle[i] = arrayToShuffle[random];
            arrayToShuffle[random] = temp;
        }
    }

    void CreateClouds()
    {
        Shuffle(clouds);

        float positionY = 0f;

        for (int i = 0; i < clouds.Length; i++)
        {
            // get position
            Vector3 temp = clouds[i].transform.position;

            temp.y = positionY;

            // make cloud spawning a little better for gameplay
            // add some control to clouds to add zigzag positions
            // stops clouds being directly underneath each other
            if (controlX == 0)
            {
                // randomize x position
                temp.x = Random.Range(0.0f, maxX);
                controlX = 1;
            }
            else if (controlX == 1)
            {
                // randomize x position
                temp.x = Random.Range(0.0f, minX);
                controlX = 2;

            }
            else if (controlX == 2)
            {
                // randomize x position
                temp.x = Random.Range(1.0f, maxX);
                controlX = 3;

            }
            else if (controlX == 3)
            {
                // randomize x position
                temp.x = Random.Range(-1.0f, minX);
                controlX = 0;

            }

            // store last cloud position (to find last cloud spawned)
            lastCloudPositionY = positionY;
            // set new position
            clouds[i].transform.position = temp;
            // apply distance between clouds
            positionY -= distanceBetweenClouds;
        }


    }

    // Find where to initially position the player
    // Need to be careful of spawning on top of a dark cloud due to Shuffle()
    void PositionThePlayer()
    {
        // find all clouds
        GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
        GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag("Cloud");

        for(int i = 0; i < darkClouds.Length; i++)
        {
            // if dark clouds y == 0
            if (darkClouds[i].transform.position.y == 0f)
            {
                // store position of dark cloud
                Vector3 temp = darkClouds[i].transform.position;
                // position dark cloud as 1st cloud on screen (so we can reposition it)
                darkClouds[i].transform.position = new Vector3(cloudsInGame[0].transform.position.x,
                                                               cloudsInGame[0].transform.position.y,
                                                               cloudsInGame[0].transform.position.z);
                cloudsInGame[0].transform.position = temp; // reposition the dark cloud with the item at array[i]
            }
        }

        Vector3 temp2 = cloudsInGame[0].transform.position;

        for (int i = 1; i < cloudsInGame.Length; i++)
        {
            // find 1st clouds y position
            if (temp2.y < cloudsInGame[i].transform.position.y)
            {
                temp2 = cloudsInGame[i].transform.position;
            }
        }

        // add a little to the y position, to position player correctly above the cloud (should probably add the players height really...)
        temp2.y += 0.8f;
        // set player position to 1st clouds position
        player.transform.position = temp2;
    }
	
    void OnTriggerEnter2D(Collider2D target)
    {
        // if we collide with cloud/dark cloud, 
        // if it's the last cloud
        // shuffle clouds and collectables (to spawn new ones)

        if(target.tag == "Cloud" || target.tag == "Deadly")
        {
            if (target.transform.position.y == lastCloudPositionY)
            {
                Shuffle(clouds);
                Shuffle(collectables);

                // Randomise X axis of new clouds
                Vector3 temp = target.transform.position;
                for (int i = 0; i < clouds.Length; i++)
                {
                    // if clouds[i] is not active (from CloudCollector)
                    // set the random position
                    if (!clouds[i].activeInHierarchy)
                    {
                        if (controlX == 0)
                        {
                            // randomize x position
                            temp.x = Random.Range(0.0f, maxX);
                            controlX = 1;
                        }
                        else if (controlX == 1)
                        {
                            // randomize x position
                            temp.x = Random.Range(0.0f, minX);
                            controlX = 2;

                        }
                        else if (controlX == 2)
                        {
                            // randomize x position
                            temp.x = Random.Range(1.0f, maxX);
                            controlX = 3;

                        }
                        else if (controlX == 3)
                        {
                            // randomize x position
                            temp.x = Random.Range(-1.0f, minX);
                            controlX = 0;

                        }
                        // keep distance between clouds
                        temp.y -= distanceBetweenClouds;
                        // set position of new last cloud
                        lastCloudPositionY = temp.y;

                        clouds[i].transform.position = temp;
                        // set cloud[i] to active
                        clouds[i].SetActive(true);

                        // posiiton collectables above cloud
                        int random = Random.Range(0, collectables.Length);
                        // if not a dark cloud, place collectable
                        if (clouds[i].tag != "Deadly")
                        {
                            // if collectables isn't active, place it above cloud
                            if (!collectables[random].activeInHierarchy)
                            {
                                Vector3 temp2 = clouds[i].transform.position;
                                temp2.y += 0.7f;

                                if (collectables[random].tag == "Life")
                                {
                                    // don't allow more than 3 lives, sets active if player has less than 3 lives
                                    if (PlayerScore.lifeCount < 3)
                                    {
                                        collectables[random].transform.position = temp2;
                                        collectables[random].SetActive(true);
                                    }
                                       
                                }
                                else
                                {
                                    collectables[random].transform.position = temp2;
                                    collectables[random].SetActive(true);
                                }
                            }
                        }

                    }
                }

                
            }
        }
    }

}
