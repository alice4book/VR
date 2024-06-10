using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Burnable : MonoBehaviour
{
    public bool _isBurning;

    [SerializeField]
    private CapsuleCollider _burningCapsule;

    [SerializeField]
    private CapsuleCollider _detectingCapsule;

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
    private Vector3 defaultPosition;

    [SerializeField]
    private float duration;

    [SerializeField]
    private float delay;

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

    private void OnTriggerEnter(Collider other)
    {
        if (!_isBurning)
        {
            Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
            if (otherBurnable != null && otherBurnable._isBurning)
            {
                Vector3 hitPoint = HitCalculation(other, _detectingCapsule);

                transform.position = hitPoint;
                Vector3 newPos = transform.localPosition;
                newPos.y = 0f;
                newPos.x = 0f;
                transform.localPosition = newPos;
                startPosition = newPos;
                StartBurning();
                return;
            }
            Lighter lighter = other.gameObject.GetComponent<Lighter>();
            if (lighter != null && lighter.fireSpawned)
            {
                Vector3 hitPoint = HitCalculation(other, _detectingCapsule);

                StartBurning();
                return;
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

    // Can be little to much for VR
    private void OnTriggerStay(Collider other)
    {
        if (!_isBurning)
        {
            Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
            if (otherBurnable != null && otherBurnable._isBurning)
            {
                StartBurning();
            }
            Lighter lighter = other.gameObject.GetComponent<Lighter>();
            if (lighter != null && lighter.fireSpawned)
            {
                StartBurning();
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
            Debug.Log("Start burning");
            StartBurning();
        }
        else
        {
            Debug.Log("Stop burning");
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

    private void StartBurning()
    {
        _isBurning = true; 
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
        fireSize.ResetValues();
        if (_burningCapsule != null)
            _burningCapsule.enabled = false;
        if (_detectingCapsule != null)
            _detectingCapsule.enabled = true;
        this.transform.localPosition = defaultPosition;
        StopAllCoroutines();
        fireSize.StopAll();
    }
}
