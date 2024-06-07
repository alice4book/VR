using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherAnimations : MonoBehaviour
{

    private Animator animator;

    private int idleNotPressed;
    private int idlePressed;
    private int pressHandle;
    private int releaseHandle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        idleNotPressed = Animator.StringToHash("IdleNotPressed");
        idlePressed = Animator.StringToHash("IdlePressed");
        pressHandle = Animator.StringToHash("PressHandle");
        releaseHandle = Animator.StringToHash("ReleaseHandle");
    }

    public void IdleNotPressed() 
    {
        animator.Play(idleNotPressed,0,0);
    }

    public void IdlePressed() 
    {
        animator.Play(idlePressed,0,0);
    }

    public void PressHandle() 
    {
       animator.Play(pressHandle,0,0);
    }

    public void ReleaseHandle() 
    {
        animator.Play(releaseHandle,0,0);
    }
}
