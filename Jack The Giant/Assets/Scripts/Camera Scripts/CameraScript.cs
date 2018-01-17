using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    // set speed variables for camera movement
    private float speed = 1f;
    private float accelleration = 0.2f;
    private float maxSpeed = 3.2f;

    // hide variables in inspector if not needed
    [HideInInspector]
    public bool moveCamera;

    
	void Start ()
    {
        moveCamera = true;
	}

	void Update ()
    {
        if (moveCamera)
            MoveCamera();        
	}

    void MoveCamera()
    {
        // store temp position of camera
        Vector3 temp = transform.position;
        // store previous position of camera
        float oldY = temp.y;
        // store new camera y position
        float newY = temp.y - (speed * Time.deltaTime);

        // Clamp assigns temp.y between oldY and newY, doesn't allow it to go outside of values
        temp.y = Mathf.Clamp(temp.y, oldY, newY);

        // assign new position to camera
        transform.position = temp;

        // increase camera speed over time
        speed += accelleration * Time.deltaTime;

        // limit speed of camera over time
        if (speed > maxSpeed)
            speed = maxSpeed;

    }
}
