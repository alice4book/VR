using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class CannotHoldOnFireA : MonoBehaviour
{
    [SerializeField] private Burnable _burnable;
    [SerializeField] public XRGrabInteractable _XRGrab;
    //[SerializeField] private bool _canHoldAlways;

    public void CannotHold()
    {
        CannotHoldForSecond();
    }

    private void CannotHoldForSecond()
    {
        _XRGrab.enabled = false;
    }

}
