using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{

    private GameObject[] backgrounds;

    private float lastY;

	void Start ()
    {
        GetBackgroundsAndSetLastY();
    }

    void GetBackgroundsAndSetLastY()
    {
        // get all background objects
        backgrounds = GameObject.FindGameObjectsWithTag("Background");

        lastY = backgrounds[0].transform.position.y;

        // start at 1 since we already started with 0 above to compare to the rest of backgrounds array
        // backgrounds array is not sorted in order, so following should help with that
        for (int i = 1; i < backgrounds.Length; i++)
        {
            // if lastY is more than the current backgroudns position, reassign lastY (more than because we are going down)
            if (lastY > backgrounds[i].transform.position.y)
            {
                lastY = backgrounds[i].transform.position.y;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        // if collides with background tagged objects
        if (target.tag == "Background")
        {
            // check if it's the last background object
            if (target.transform.position.y == lastY)
            {
                Vector3 temp = target.transform.position;
                // get height of box collider to get size of object and tile correctly
                float height = ((BoxCollider2D)target).size.y;

                for (int i = 0; i < backgrounds.Length; i++)
                {
                    // iff backgrounds not active
                    if (!backgrounds[i].activeInHierarchy)
                    {
                        // set new last position
                        temp.y -= height;
                        lastY = temp.y;

                        backgrounds[i].transform.position = temp;
                        // set object to active
                        backgrounds[i].SetActive(true);
                    }
                }
            }
        }
    }
}
