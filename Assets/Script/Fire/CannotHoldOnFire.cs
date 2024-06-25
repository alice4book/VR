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
            _burnable.OnStopBurning += CanHold;
        }
    }

    private void OnDisable()
    {
        if (_burnable != null)
        {
            _burnable.OnStopBurning -= CanHold;
        }
    }

    private void CanHold()
    {
        _XRGrab.enabled = true;
    }

    public void CannotHold()
    {
        if (_canHoldAlways)
            return;
        _XRGrab.enabled = false;
        Invoke("CanHold", 0.2f);
    }

}
