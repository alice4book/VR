using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource _eating;
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Food")
        {
            
        }
    }

    

    private void Eating(GameObject gameObj)
    {
        Destroy(gameObj);
        _eating.Play();
        _particleSystem.Play();
    }
}
