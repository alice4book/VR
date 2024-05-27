using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public bool _isBurning;

    [SerializeField]
    private CapsuleCollider _capsule;

    [SerializeField]
    private ParticlesAlwaysUp _particlesAlwaysUp;

    [Range(1.0f, 100.0f)]
    public float percentage;



    private void Awake()
    {
        _isBurning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
        if (!_isBurning && otherBurnable != null && otherBurnable._isBurning)
        {
            _isBurning = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private void OnValidate()
    {
        if (_isBurning)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
