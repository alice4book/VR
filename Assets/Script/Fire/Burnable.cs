using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Burnable : MonoBehaviour
{
    public event Action OnStartBurning;
    public event Action OnStopBurning;

    [Tooltip("Is object burning")]
    public bool _isBurning;

    [Tooltip("Collider when object is burning")]
    [SerializeField]
    private CapsuleCollider _burningCapsule;

    [Tooltip("Collider when object is NOT burning")]
    [SerializeField]
    private Collider _detectingCapsule;

    [Tooltip("The percentage of fire consumption")]
    [Range(1.0f, 100.0f)]
    public float percentage;

    [Tooltip("Scale when fire starts burning")]
    [SerializeField]
    private Vector3 startScale;

    [Tooltip("Max scale of fire we want to cover the object")]
    [SerializeField]
    private Vector3 endScale;

    [Tooltip("Position where fire starts burning")]
    [SerializeField]
    private Vector3 startPosition;

    [Tooltip("Ending position if fire needs to move during scaling")]
    [SerializeField]
    private Vector3 endPosition;

    [Tooltip("Position where collider gonna comback if fire was extinguish")]
    [SerializeField]
    private Vector3 defaultPosition;

    [Tooltip("How quickly the fire spreads")]
    [SerializeField]
    private float duration;

    [Tooltip("How long it takes for fire to spread spreads")]
    [SerializeField]
    private float delay;

    [Tooltip("Script controlling fire size")]
    [SerializeField]
    private FireSize fireSize;


    private void Awake()
    {
        _isBurning = false;
        if(_burningCapsule != null)
        {
            _burningCapsule.enabled = false;
        }
        if (_detectingCapsule != null)
        {
            _detectingCapsule.enabled = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!_isBurning)
        {
            Vector3 hitPoint = HitCalculation(other, _detectingCapsule);
            Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
            if (otherBurnable != null && otherBurnable._isBurning)
            {
                StartBurning(hitPoint);
            }
            Lighter lighter = other.gameObject.GetComponent<Lighter>();
            if (lighter != null && lighter.fireSpawned)
            {
                StartBurning(hitPoint);
            }
        }
        else
        if (_isBurning)
        {
            if (other.transform.gameObject.tag == "ExtinguisherClouds")
            {
                StopBurning();
            }
        }
    }

    // Can be little to much for VR
    private void OnTriggerStay(Collider other)
    {
        if (!_isBurning)
        {
            Vector3 hitPoint = HitCalculation(other, _detectingCapsule);
            Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
            if (otherBurnable != null && otherBurnable._isBurning)
            {
                StartBurning(hitPoint);
            }
            Lighter lighter = other.gameObject.GetComponent<Lighter>();
            if (lighter != null && lighter.fireSpawned)
            {
                StartBurning(hitPoint);
            }
        }else
        if (_isBurning)
        {
            if (other.transform.gameObject.tag == "ExtinguisherClouds")
            {
                StopBurning();
            }
        }
    }
    private void OnValidate()
    {
        if (_isBurning)
        {
            //Debug.Log("Start burning");
            StartBurning(transform.position);
        }
        else
        {
            //Debug.Log("Stop burning");
            StopBurning();
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
            _burningCapsule.height = scaleValues.y * 0.0002f;

            fireSize.UpdateValues();
            Vector3 posValues = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            transform.localPosition = posValues;
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        fireSize.UpdateValues();
    }

    public void StartBurning(Vector3 hitPoint)
    {
        transform.position = hitPoint;
        Vector3 newPos = transform.localPosition;
        newPos.y = 0f;
        newPos.x = 0f;
        transform.localPosition = newPos;
        startPosition = newPos;
        _isBurning = true;
        OnStartBurning?.Invoke();
        if (_burningCapsule != null)
            _burningCapsule.enabled = true;
        if (_detectingCapsule != null)
            _detectingCapsule.enabled = false;

        fireSize.StartAll();
        StartCoroutine(ScaleOverTime());
    }

    private void StopBurning()
    {
        _isBurning = false;
        OnStopBurning?.Invoke();
        fireSize.ResetValues();
        if (_burningCapsule != null)
            _burningCapsule.enabled = false;
        if (_detectingCapsule != null)
            _detectingCapsule.enabled = true;
        this.transform.localPosition = defaultPosition;
        StopAllCoroutines();
        fireSize.StopAll();
    }

    private Vector3 HitCalculation(Collider a, Collider b)
    {
        Vector3 direction;
        float distance;

        Physics.ComputePenetration(
            a, a.transform.position, a.transform.rotation,
            b, b.transform.position, b.transform.rotation,
            out direction, out distance);

        Vector3 hitPoint = a.transform.position + direction * distance;
        return hitPoint;
    }
}
