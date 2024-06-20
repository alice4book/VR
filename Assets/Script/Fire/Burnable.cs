using System;
using System.Collections;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public event Action OnStartBurning;
    public event Action OnStopBurning;

    [Tooltip("Is object burning")]
    public bool _isBurning;

    [Tooltip("Collider when object is burning")]
    [SerializeField]
    private Collider _burningCapsule;

    [Tooltip("Collider when object is NOT burning")]
    [SerializeField]
    private Collider _detectingCapsule;

    //[Tooltip("The percentage of fire consumption")]
    //[Range(1.0f, 100.0f)]
    //public float percentage;

    [Tooltip("Scale when fire starts burning")]
    [SerializeField]
    private Vector3 _startScale;

    [Tooltip("Max scale of fire we want to cover the object")]
    [SerializeField]
    private Vector3 _endScale;

    [Tooltip("Position where fire starts burning")]
    [SerializeField]
    private Vector3 _startPosition;

    [Tooltip("Ending position if fire needs to move during scaling")]
    [SerializeField]
    private Vector3 _endPosition;

    [Tooltip("Position where collider gonna comback if fire was extinguish")]
    [SerializeField]
    private Vector3 _defaultPosition;

    [Tooltip("How quickly the fire spreads")]
    [SerializeField]
    private float _duration;

    [Tooltip("How long it takes for fire to spread spreads")]
    [SerializeField]
    private float _delay;

    [Tooltip("Script controlling fire size")]
    [SerializeField]
    private FireSize fireSize;

    [Tooltip("Fire's HP")]
    [SerializeField]
    private int _HP;

    [Tooltip("Default fire's HP")]
    [SerializeField]
    private int _defaultHP;

    [SerializeField]
    private bool _isInFire;

    // Add to that when you wanna do something when object starts burning
    public delegate void BurningDelegate();
    public static event BurningDelegate OnBurningDelegate;

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
        _defaultHP = _HP;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (!_isBurning)
        {
            Vector3 hitPoint = HitCalculation(other, _detectingCapsule);
            Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
            if (otherBurnable != null && otherBurnable._isBurning)
            {
                StartBurning(hitPoint);
                OnBurningDelegate?.Invoke();
                return;
            }
            Lighter lighter = other.gameObject.GetComponent<Lighter>();
            if (lighter != null && lighter.fireSpawned)
            {
                StartBurning(hitPoint);
                OnBurningDelegate?.Invoke();
                return;
            }
            else
            if (other.gameObject.tag == "FireStarter")
            {
                StartBurning(hitPoint);
                OnBurningDelegate?.Invoke();
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
    */

    // Can be little to much for VR
    private void OnTriggerStay(Collider other)
    {
        if (!_isBurning)
        {
            Vector3 hitPoint = HitCalculation(other, _detectingCapsule);
            Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
            Lighter lighter = other.gameObject.GetComponent<Lighter>();
            if ((lighter != null && lighter.fireSpawned) || (otherBurnable != null && otherBurnable._isBurning) || (other.gameObject.tag == "FireStarter"))
            {
                if (!_isInFire)
                {
                    StartCoroutine(LoseHP());
                }
                if (_HP <= 0)
                {
                    StartBurning(hitPoint);
                }
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
        if (_delay > 0)
        {
            yield return new WaitForSeconds(_delay);
        }

        float elapsedTime = 0.0f;

        while (elapsedTime < _duration)
        {
            // Interpolate the scale based on the elapsed time
            Vector3 scaleValues = Vector3.Lerp(_startScale, _endScale, elapsedTime / _duration);
            fireSize.lenght_x = scaleValues.x;
            fireSize.lenght_y = scaleValues.y;
            fireSize.lenght_z = scaleValues.z;
            // Settings for capsule
            CapsuleCollider capsuleCollider = (CapsuleCollider)_burningCapsule;
            if(capsuleCollider != null)
                capsuleCollider.height = scaleValues.y * 0.0002f;

            fireSize.UpdateValues();
            Vector3 posValues = Vector3.Lerp(_startPosition, _endPosition, elapsedTime / _duration);
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
        _startPosition = newPos;
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
        this.transform.localPosition = _defaultPosition;
        StopAllCoroutines();
        fireSize.StopAll();
        _HP = _defaultHP;
    }

    IEnumerator LoseHP()
    {
        _isInFire = true;
        _HP--;
        yield return new WaitForSeconds(0.01f);
        _isInFire = false;
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
