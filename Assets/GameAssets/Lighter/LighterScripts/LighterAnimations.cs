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

    [SerializeField] private float time = 0.2f;

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
        animator.CrossFade(openLid,time);
    }

    public void CloseLid() 
    {
        animator.CrossFade(closeLid,time);
    }

    public void TurnWheel() 
    {
       //animator.Play(turnWheel,0,0);
    }

    public void IdleOpen() 
    {
        animator.CrossFade(idleOpen,time);
    }

    public void IdleClosed() 
    {
        animator.CrossFade(idleClosed,time);
    }


}
