using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBoxOpen : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _open;
    [SerializeField] private bool _close;

    private void Start()
    {
        _animator.enabled = true;

    }

    public void OpenBox()
    {
        _open = true;
        _close = false;
        _animator.SetTrigger("Open");
    }

    public void CloseBox()
    {
        _open = false;
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
