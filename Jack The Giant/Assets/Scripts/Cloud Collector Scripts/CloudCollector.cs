using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {
        // when cloud (or any 2D game object) hits collector do this:

        if(target.tag == "Cloud" | target.tag == "Deadly")
        {
            // deactivate target
            target.gameObject.SetActive(false);
        }
    }

}
