using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MatchBoxOpen : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private bool _open;
    [SerializeField] private bool _close;

    private void Start()
    {
        _animator.enabled = true;
        _boxCollider.enabled = false;

    }

    public void OpenBox()
    {
        _open = true;
        _close = false;
        _animator.SetTrigger("Open");
        Invoke("EnableGrab",0.3f);
    }

    private void EnableGrab()
    {
        _boxCollider.enabled = true;
    }

    public void CloseBox()
    {
        _open = false;
        _boxCollider.enabled = false;
        _close = true;
        _animator.SetTrigger("Close");
    }

    private void OnValidate()
    {
        if (_open)
            OpenBox();
        else if (_close)
            CloseBox();
    }
}
