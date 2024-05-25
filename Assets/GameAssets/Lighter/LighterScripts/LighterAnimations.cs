using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterAnimations : MonoBehaviour
{

    private Animator animator;

    private int openLid;
    private int closeLid;
    private int turnWheel;
    private int idleOpen;
    private int idleClosed;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        openLid = Animator.StringToHash("OpenLid");
        closeLid = Animator.StringToHash("CloseLid");
        //turnWheel = Animator.StringToHash("TurnWheel");
        idleClosed = Animator.StringToHash("IdleClosed");
        idleOpen = Animator.StringToHash("IdleOpen");

    }

    public void OpenLid() 
    {
        animator.Play(openLid,0,0);
    }

    public void CloseLid() 
    {
        animator.Play(closeLid,0,0);
    }

    public void TurnWheel() 
    {
       //animator.Play(turnWheel,0,0);
    }

    public void IdleOpen() 
    {
        animator.Play(idleOpen,0,0);
    }

    public void IdleClosed() 
    {
        animator.Play(idleClosed,0,0);
    }


}
