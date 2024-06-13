using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VelocityInteractable : XRGrabInteractable
{
    private ControllerVelocity controllerVelocity = null;
    private MeshRenderer meshRenderer = null;

    protected override void Awake()
    {
        base.Awake();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    protected internal void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controllerVelocity = args.interactor.GetComponent<ControllerVelocity>();
    }

    protected internal void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        controllerVelocity = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(isSelected)
        {
            if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                UseVelocity();
            }
        }
    }

    private void UseVelocity()
    {
        Vector3 velocityTMP = controllerVelocity ? controllerVelocity.velocity : Vector3.zero;
        Color color = new Color(velocityTMP.x, velocityTMP.y, velocityTMP.z);
        meshRenderer.material.color = color;
    }
}
