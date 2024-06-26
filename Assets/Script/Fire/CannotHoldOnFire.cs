using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class CannotHoldOnFire : MonoBehaviour
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
        XRBaseInteractableExtension.ForceDeselect(_XRGrab);

    }

}
public static class XRBaseInteractableExtension
{
    /// <summary>
    /// Force deselect the selected interactable.
    ///
    /// This is an extension method for <c>XRBaseInteractable</c>.
    /// </summary>
    /// <param name="interactable">Interactable that has been selected by some interactor</param>
    public static void ForceDeselect(this XRBaseInteractable interactable)
    {
        interactable.interactionLayers = InteractionLayerMask.GetMask("InertLayer");
        interactable.interactionManager.CancelInteractableSelection(interactable);
        Assert.IsFalse(interactable.isSelected);
    }
}
