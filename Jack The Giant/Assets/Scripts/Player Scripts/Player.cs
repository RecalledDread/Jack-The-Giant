using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Player Class

    public float speed = 8f, maxVelocity = 4f;
    //public float maxVelocity = 4f;

    //[SerializeField] // this is a way to show following variable in inspector, without making it public
    private Rigidbody2D myBody;
    private Animator    anim;

    void Awake()
    {
        // following is instead of usign SerializedField
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

	void Start ()
    {

    }

    void FixedUpdate()
    {
        // FixedUpdate() is best place to put calculations dealing with physics
        // Update() called every frame (i.e. 60 fps, called 60 times per second), FixedUpdate called every few frames
        PlayerMoveKeyboard();
    }

    void PlayerMoveKeyboard()
    {
        // Player movement for keyboard
        float forceX = 0f;
        float velocity = Mathf.Abs(myBody.velocity.x); // returns absolute value (always positive)

        float h = Input.GetAxisRaw("Horizontal"); // get x axis of input, left/right, a/d - will return -1, or 0, or +1 (a, nothing, d)

        if(h > 0) // if going right
        {
            if (velocity < maxVelocity) // if we can still move
                forceX = speed;     // move right

            // set scale positive for facing direction
            Vector3 temp = transform.localScale;
            temp.x = 1.3f;
            transform.localScale = temp;

            // set walk to true, from animator window
            anim.SetBool("Walk", true);

        }
        else if(h < 0) // if going left
        { 
            if (velocity < maxVelocity)
                forceX = -speed;    // move left

            //set scale negative for facing direction
            Vector3 temp = transform.localScale;
            temp.x = -1.3f;
            transform.localScale = temp;

            // set walk to true, from animator window
            anim.SetBool("Walk", true);
        }
        else
        {
            // set walk to false to stop moving animation
            anim.SetBool("Walk", false);
        }

        myBody.AddForce(new Vector2(forceX, 0));
    }
}
