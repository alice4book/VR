using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlintBehavior : MonoBehaviour
{
    int igniteChance;
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Flint")
        {
            igniteChance = Random.Range(1, 101);
            if(igniteChance > 0)
            {
                Debug.Log("Fire");
            }
        }
    }
}
