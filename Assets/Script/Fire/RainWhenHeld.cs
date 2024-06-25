using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainWhenHeld : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField] private GameObject _colliderGameObject;

    [SerializeField] 
    private BoxCollider _collider;

    private Quaternion _initialRotation;

    private void Start()
    {
        if(_particleSystem != null) 
            _particleSystem.Stop();
        if(_collider != null)
            _collider.enabled = false;
        _initialRotation = _colliderGameObject.transform.rotation;
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

    void LateUpdate()
    {
        if (_collider != null)
        {
            // Lock the object's rotation to the initial world rotation
            _colliderGameObject.transform.rotation = _initialRotation;
        }
    }
}
