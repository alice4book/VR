using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public bool _isBurning;

    [SerializeField]
    private CapsuleCollider _burningCapsule;

    [SerializeField]
    private CapsuleCollider _detectingCapsule;

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
            Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
            if (otherBurnable != null && otherBurnable._isBurning)
            {
                StartBurning();
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                StartCoroutine(ScaleOverTime());
            }
            Lighter lighter = other.gameObject.GetComponent<Lighter>();
            if (lighter != null && lighter.fireSpawned)
            {
                _isBurning = true;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                StartCoroutine(ScaleOverTime());
            }
        }
        if (_isBurning)
        {
            if (false) // Here detection of gaœnica
            {
                StopBurning();
                StopCoroutine(ScaleOverTime());
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
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
                _isBurning = true;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                StartCoroutine(ScaleOverTime());
            }
            Lighter lighter = other.gameObject.GetComponent<Lighter>();
            if (lighter != null && lighter.fireSpawned)
            {
                _isBurning = true;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                StartCoroutine(ScaleOverTime());
            }
        }
        if (_isBurning)
        {
            if (false) // And here detection of gaœnica
            {
                StopBurning();
                StopCoroutine(ScaleOverTime());
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnValidate()
    {
        if (_isBurning)
        {
            Debug.Log("Start burning");
            StartBurning();
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            StartCoroutine(ScaleOverTime());
        }
        else
        {
            Debug.Log("Stop burning");
            StopAllCoroutines();
            StopBurning();
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
        _burningCapsule.enabled = true;
        _detectingCapsule.enabled = false;
    }
    private void StopBurning()
    {
        _isBurning = false;
        fireSize.ResetValues();
        _burningCapsule.enabled = false;
        _detectingCapsule.enabled = true;
        this.transform.localPosition = startPosition;
    }
}
