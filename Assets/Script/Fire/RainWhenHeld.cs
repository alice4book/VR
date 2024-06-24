using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainWhenHeld : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField] 
    private BoxCollider _collider;

    private void Start()
    {
        if(_particleSystem != null) 
            _particleSystem.Stop();
        if(_collider != null)
            _collider.enabled = false;
    }

    public void isHeld()
    {
        if (_particleSystem != null)
            _particleSystem.Play();
        if (_collider != null)
            _collider.enabled = true;
    }

    public void isDroped()
    {
        if (_particleSystem != null)
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        if (_collider != null)
            _collider.enabled = false;
    }
}
