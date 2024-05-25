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

    private LighterAnimations animations;

    public void FireOn()
    {
        if(!fireSpawned) {
            animations.OpenLid();
            fire = Instantiate(firePrefab, firePoint.position, firePoint.rotation, this.transform);
            fireSpawned = true;
        }
    }

    public void FireOff()
    {
        if(fireSpawned) {
            animations.CloseLid();
            Destroy(fire);
            fireSpawned = false;
        }
    }

    void Start() {

        animations = GetComponent<LighterAnimations>();

        //animations.OpenLid();
        
        //firePrefab.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        //GameObject newObject = Instantiate(firePrefab, firePoint.position, firePoint.rotation, this.transform);
    }

}
