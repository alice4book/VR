using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<GameObject> _objects = new List<GameObject>();
    public Spawner spawner;
    bool spawnflag;
    void OnTriggerEnter(Collider other)
    {
        if (_objects.Contains(other.gameObject) == false)
        {
            _objects.Add(other.gameObject); // Keeps track
        }

        if (_objects.Count < 10)
        {
            spawner.spawningfalg = true;
        }
        else
        {
            spawner.spawningfalg = false;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (_objects.Contains(other.gameObject) == true)
        {
            _objects.Remove(other.gameObject);
            if (_objects.Count < 10)
            {
                if(!spawner.spawningfalg)
                {
                    spawner.spawningfalg = true;
                    StartCoroutine(spawner.spawning());
                }
            }
            else
            {
                spawner.spawningfalg = false;
            }
        }
    }
}

