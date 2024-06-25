using System.Collections.Generic;
using UnityEngine;

public class TemporaryBurnable : MonoBehaviour
{
    [SerializeField]
    private GameObject _fire;

    [SerializeField] 
    private Rain _rain;

    [SerializeField]
    private List<Collider> _detectingCollideres;

    [SerializeField]
    [Tooltip("Is object burning")]
    private bool _isBurning;

    [SerializeField]
    [Tooltip("How long fire exist befor rain")]
    private float _timeBeforRain;

    private void Start()
    {
        _isBurning = false;
        
        GameObject rainObj = GameObject.FindWithTag("Rain");
        if (rainObj != null)
        {
            _rain = rainObj.GetComponent<Rain>();
        }
        _timeBeforRain = 1.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isBurning)
        {
            Vector3 hitPoint = Vector3.zero;
            float shortestDistance = float.MaxValue;
            foreach (var collider in _detectingCollideres)
                {
                    Vector3 tmpHitPoint = HitCalculation(other, collider);
                    float distance = Vector3.Distance(tmpHitPoint, other.gameObject.transform.position);
                    if (distance < shortestDistance)
                    {
                        hitPoint = tmpHitPoint;
                        shortestDistance = distance;
                    }
                }
            GameObject bonfire = other.gameObject;
            if (bonfire != null)
            {
                Burnable otherBurnable = bonfire.GetComponent<Burnable>();
                if (otherBurnable != null && otherBurnable._isBurning)
                {
                    StartBurning(hitPoint);
                }
                Lighter lighter = other.gameObject.GetComponent<Lighter>();
                if (lighter != null && lighter.fireSpawned)
                {
                    StartBurning(hitPoint);
                }
                else
                if (other.gameObject.tag == "FireStarter")
                {
                    StartBurning(hitPoint);
                }
            }
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

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Here");
        if (!_isBurning)
        {
            //GameObject bonfire = collision.gameObject.transform.GetChild(0).gameObject;
            GameObject bonfire = collision.gameObject.transform.parent.GetChild(0).gameObject;
            if(bonfire != null)
            {
                Debug.Log("HereHere");
                ContactPoint contact = collision.GetContact(0);
                Burnable otherBurnable = bonfire.GetComponent<Burnable>();
                if (otherBurnable != null && otherBurnable._isBurning)
                {
                    Debug.Log("HereHereHere");
                    StartBurning(contact.point);
                }
                Lighter lighter = collision.gameObject.GetComponent<Lighter>();
                if (lighter != null && lighter.fireSpawned)
                {
                    StartBurning(contact.point);
                }
                else
                if (collision.gameObject.tag == "FireStarter")
                {
                    StartBurning(contact.point);
                }
            }
        }
    }
    */

    private void StartBurning(Vector3 vec)
    {
        _isBurning = true;

        // Instantiate the fire at the contact point
        GameObject _newFire = Instantiate(_fire, vec, Quaternion.identity, transform);

        //_newFire.transform.position = contact.point;
        FireSize bonfire = _newFire.GetComponent<FireSize>();

        if (bonfire != null)
        {
            bonfire.StartAll();
        }

        Invoke("StartRain", _timeBeforRain);
    }
    private void StartRain()
    {
        _rain.StartRain();
    }
}
