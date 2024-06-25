using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColor : MonoBehaviour
{
    private float timeElapsed;
    private bool isChangingColor = false;

    public float duration = 5.0f; // Duration of the color change

    [SerializeField] private Material material;
    [SerializeField] private Burnable burnable;
    private Color startColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
    private Color endColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    [SerializeField] private Color handColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField] private Color hotColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    public void ChangeColor()
    {
        if (burnable._isBurning && !isChangingColor)
        {
            isChangingColor = true;
            startColor = handColor;
            endColor = hotColor;
            StartCoroutine(ChangeColorOverTime());
        }
    }

    public void RevertColor()
    {
        if (burnable._isBurning && !isChangingColor)
        {
            isChangingColor = true;
            startColor = hotColor;
            endColor = handColor;
            StartCoroutine(ChangeColorOverTime());
        }
    }

    IEnumerator ChangeColorOverTime()
    {
        timeElapsed = 0f;

        while (isChangingColor)
        {
            // Lerp color based on time elapsed
            material.color = Color.Lerp(startColor, endColor, timeElapsed / duration);

            // Increment the time elapsed
            timeElapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }


    }
}
