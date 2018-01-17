using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {
        // deactivate backgrounds as camera moves past
        if (target.tag == "Background")
        {
            target.gameObject.SetActive(false);
        }
    }
	
}
