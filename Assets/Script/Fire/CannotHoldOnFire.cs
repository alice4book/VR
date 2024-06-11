using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CannotHoldOnFire : MonoBehaviour
{
    [SerializeField] private Burnable _burnable;
    [SerializeField] private XRGrabInteractable _XRGrab;
    [SerializeField] private bool _canHoldAlways;


    private void OnEnable()
    {
        if (_burnable != null)
        {
            _burnable.OnStartBurning += CannotHold;
            _burnable.OnStopBurning += CanHold;
        }
    }

    private void OnDisable()
    {
        if (_burnable != null)
        {
            _burnable.OnStartBurning -= CannotHold;
            _burnable.OnStopBurning -= CanHold;
        }
    }

    private void CanHold()
    {
        _XRGrab.enabled = true;
    }

    private void CannotHold()
    {
        if (_canHoldAlways)
            return;
        _XRGrab.enabled = false;
    }

}
