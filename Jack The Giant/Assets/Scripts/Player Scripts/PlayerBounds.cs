using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{

    private float minX, maxX;



	void Start ()
    {
        // set min and max boundaries
        SetMinAndMaxX();

    }
	
	void Update ()
    {
        // limit player movement left and right
		if (transform.position.x < minX)
        {
            Vector3 temp = transform.position;
            temp.x = minX;
            transform.position = temp;
        }
        else if (transform.position.x > maxX)
        {
            Vector3 temp = transform.position;
            temp.x = maxX;
            transform.position = temp;
        }
    }

    void SetMinAndMaxX()
    {
        // Find screen coords and convert to Unity's world coords (ScreenToWorldPoint)
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // can edit this to stop them going off screen at all (similar to cloud spawner code)
        minX = -bounds.x;
        maxX = bounds.x;
    }
}
