using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MatchBoxOpen : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider _1boxCollider;
    [SerializeField] private BoxCollider _2boxCollider;
    [SerializeField] private BoxCollider _3boxCollider;
    [SerializeField] private BoxCollider _4boxCollider;
    [SerializeField] private bool _open;
    [SerializeField] private bool _close;

    private void Start()
    {
        _animator.enabled = true;
        _1boxCollider.enabled = false;
        _2boxCollider.enabled = false;
        _3boxCollider.enabled = false;
        _4boxCollider.enabled = false;

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
        _1boxCollider.enabled = true;
        _2boxCollider.enabled = true;
        _3boxCollider.enabled = true;
        _4boxCollider.enabled = true;
    }

    public void CloseBox()
    {
        _open = false;
        _1boxCollider.enabled = false;
        _2boxCollider.enabled = false;
        _3boxCollider.enabled = false;
        _4boxCollider.enabled = false;
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
