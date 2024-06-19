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

    [SerializeField] private float time = 0.2f;

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
        animator.CrossFade(idleNotPressed, time);
    }

    public void IdlePressed() 
    {
        animator.CrossFade(idlePressed, time);
    }

    public void PressHandle() 
    {
       animator.CrossFade(pressHandle, time);
    }

    public void ReleaseHandle() 
    {
        animator.CrossFade(releaseHandle, time);
    }
}
