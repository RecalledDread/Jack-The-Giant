using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour
{
    void OnEnable() // called when activated (OnDisable is when deactivated)
    {
        // call the function, after this length of time (seconds)
        // in this case, if collectable isn't picked up after 10s, call this function
        Invoke("DestroyCollectable", 10f);
    }
    


    void DestroyCollectable()
    {
        // deactivate collectable
        gameObject.SetActive(false);
    }
	
}
