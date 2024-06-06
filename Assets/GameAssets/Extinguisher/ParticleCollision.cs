using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {

        if(other.CompareTag("ExtinguisherClouds")) {
            Debug.Log("HIT");
        }
        else {
            Debug.Log("WRONG PARTICLE");
        }
    }
}
