using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The fire that is created")]
    GameObject firePrefab = null;

    [SerializeField]
    [Tooltip("The point that the fire is created")]
    Transform firePoint = null;
    
    bool fireSpawned = false;
    GameObject fire;

    public void FireOn()
    {
        if(!fireSpawned) {
            fire = Instantiate(firePrefab, firePoint.position, firePoint.rotation, this.transform);
            fireSpawned = true;
        }
    }

    public void FireOff()
    {
        if(fireSpawned) {
            Destroy(fire);
            fireSpawned = false;
        }
    }

    void Start() {
        //GameObject newObject = Instantiate(firePrefab, firePoint.position, firePoint.rotation, this.transform);
    }
}
