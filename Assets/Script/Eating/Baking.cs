using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baking : MonoBehaviour
{
    [SerializeField] private Color startColor = Color.white; // Starting color
    [SerializeField] private Color endColor = Color.red; // Target color
    [SerializeField] private float duration = 5.0f; // Duration of the color change

    private Renderer objectRenderer;
    private Coroutine colorChangeCoroutine;
    private float timeElapsed;
    private bool isChangingColor = false;

    void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();

        // Set initial color
        objectRenderer.material.color = startColor;
    }

    void OnTriggerEnter(Collider other)
    {
        Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
        Lighter lighter = other.gameObject.GetComponent<Lighter>();
        if ((lighter != null && lighter.fireSpawned) || (otherBurnable != null && otherBurnable._isBurning) || (other.gameObject.tag == "FireStarter"))
        {
            if(!isChangingColor)
            {
                isChangingColor = true;
                colorChangeCoroutine = StartCoroutine(ChangeColorOverTime());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
        Lighter lighter = other.gameObject.GetComponent<Lighter>();
        if ((lighter != null && lighter.fireSpawned) || (otherBurnable != null && otherBurnable._isBurning) || (other.gameObject.tag == "FireStarter"))
        {
            if (isChangingColor)
            {
                isChangingColor = false;
                StopCoroutine(colorChangeCoroutine);
                startColor = objectRenderer.material.color;
            }
        }
    }

    IEnumerator ChangeColorOverTime()
    {
        timeElapsed = 0f;

        while (isChangingColor)
        {
            // Lerp color based on time elapsed
            objectRenderer.material.color = Color.Lerp(startColor, endColor, timeElapsed / duration);

            // Increment the time elapsed
            timeElapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }
    }
}
