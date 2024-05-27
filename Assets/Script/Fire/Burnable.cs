using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    [SerializeField]
    bool _isBurning;
    [SerializeField]
    GameObject _fire;

    private void Awake()
    {
        _isBurning = false;

    }

    private void Start()
    {
        Transform bonfireTransform = transform.Find("Bonfire");

        // Check if the Bonfire child was found
        if (bonfireTransform != null)
        {
            _fire = bonfireTransform.gameObject;
        }
    }

    private void OnValidate()
    {
        if (_isBurning)
        {
            _fire.SetActive(true);
        }
        else
        {
            _fire.SetActive(false);
        }
    }
}
