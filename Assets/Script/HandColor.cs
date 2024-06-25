using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColor : MonoBehaviour
{
    private float timeElapsed;
    private bool isChangingColor = false;
    private bool isHeld = false;

    public float duration = 5.0f; // Duration of the color change

    [SerializeField] private Material material;
    [SerializeField] private Burnable burnable;
    [SerializeField] private CannotHoldOnFire _holdingController;
    private Color startColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
    private Color endColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    [SerializeField] private Color handColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField] private Color hotColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    [SerializeField] private Color currentColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    private Coroutine colorChanging; 

    private void Start()
    {
        _holdingController = GetComponent<CannotHoldOnFire>();
        burnable.OnStartBurning += ChangeColor;
        currentColor = handColor;
    }

    public void IsBeingHeld()
    {
        isHeld = true;
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (isHeld && burnable._isBurning && !isChangingColor)
        {
            isChangingColor = true;
            startColor = currentColor;
            endColor = hotColor;
            colorChanging = StartCoroutine(ChangeColorOverTime());
        }
    }

    public void RevertColor()
    {
        StopCoroutine(colorChanging);
        isChangingColor = true;
        startColor = currentColor;
        endColor = handColor;
        StartCoroutine(ChangeColorOverTime());
    }

    IEnumerator ChangeColorOverTime()
    {
        timeElapsed = 0f;

        while (isChangingColor)
        {
            // Lerp color based on time elapsed
            currentColor = Color.Lerp(startColor, endColor, timeElapsed / duration);
            material.color = currentColor;
            // Increment the time elapsed
            timeElapsed += Time.deltaTime;

            // Wait for the next frame
            yield return new WaitForFixedUpdate();
        }
        isChangingColor = false;
        isHeld = false;
        _holdingController.CannotHold();
        RevertColor();
    }
}
