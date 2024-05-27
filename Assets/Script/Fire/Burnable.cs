using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public bool _isBurning;

    [SerializeField]
    private CapsuleCollider _capsule;

    [SerializeField]
    private ParticlesAlwaysUp _particlesAlwaysUp;

    [Range(1.0f, 100.0f)]
    public float percentage;

    [SerializeField]
    private Vector3 startScale;

    [SerializeField]
    private Vector3 endScale;

    [SerializeField]
    private Vector3 startPosition;

    [SerializeField]
    private Vector3 endPosition;

    [SerializeField]
    private float duration;

    [SerializeField]
    private float delay;

    [SerializeField]
    private FireSize fireSize;


    private void Awake()
    {
        _isBurning = false;
        _capsule = GetComponent<CapsuleCollider>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
        if (!_isBurning && otherBurnable != null && otherBurnable._isBurning)
        {
            _isBurning = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            StartCoroutine(ScaleOverTime());
        }
        Lighter lighter = other.gameObject.GetComponent<Lighter>();
        if (!_isBurning && lighter != null && lighter.fireSpawned)
        {
            _isBurning = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            StartCoroutine(ScaleOverTime());
        }
    }

    private void OnValidate()
    {
        if (_isBurning)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            StartCoroutine(ScaleOverTime());
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator ScaleOverTime()
    {
        // Optional delay
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            // Interpolate the scale based on the elapsed time
            Vector3 scaleValues = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            fireSize.lenght_x = scaleValues.x;
            fireSize.lenght_y = scaleValues.y;
            fireSize.lenght_z = scaleValues.z;
            _capsule.height = scaleValues.y * 0.0002f;

            fireSize.UpdateValues();
            Vector3 posValues = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            transform.localPosition = posValues;
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        fireSize.UpdateValues();
    }
}
