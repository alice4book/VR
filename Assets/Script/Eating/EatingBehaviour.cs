using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource _eating;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private FullScreenController _fullScreen;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Food")
        {
            Eating(other.gameObject);
        }
    }

    

    private void Eating(GameObject gameObj)
    {
        _eating.Play();
        _particleSystem.Play();
        if (gameObj.GetComponent<Mushroom>() != null && gameObj.GetComponent<Mushroom>().changeColor && _fullScreen != null)
            _fullScreen.StartEffect();   
        Destroy(gameObj); 
    }
}
