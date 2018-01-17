using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour
{
    // To scale background to the screen

	void Start ()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        // get x dimension of sprite
        float width = sr.sprite.bounds.size.x;

        // get height of game world - camera in Unity
        float worldHeight = Camera.main.orthographicSize * 2;
        // Screen.Height/Width gives size of screen - the resolution?
        float worldWidth = worldHeight / Screen.height * Screen.width;

        tempScale.x = worldWidth / width;
        transform.localScale = tempScale;
    }
	
}
