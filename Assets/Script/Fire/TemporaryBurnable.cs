using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBurnable : MonoBehaviour
{
    [SerializeField]
    private GameObject _fire;

    [SerializeField] 
    private Rain _rain;

    [SerializeField] 
    private MeshCollider _collider;

    [SerializeField]
    [Tooltip("Is object burning")]
    private bool _isBurning;

    private void Start()
    {
        _isBurning = false;
        /*
        GameObject rainObj = GameObject.FindWithTag("rain");
        if (rainObj != null)
        {
            _rain = rainObj.GetComponent<Rain>();
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isBurning)
        {
            GameObject bonfire = collision.gameObject.transform.GetChild(0).gameObject;
            if(bonfire != null) {            
                Burnable otherBurnable = bonfire.GetComponent<Burnable>();
                if (otherBurnable != null && otherBurnable._isBurning)
                {
                    StartBurning(collision);
                }
                Lighter lighter = bonfire.GetComponent<Lighter>();
                if (lighter != null && lighter.fireSpawned)
                {
                    StartBurning(collision);
                } 
            }
        }
    }

    private void StartBurning(Collision collision) 
    {
        _isBurning = true;

        // Get the first contact point of the collision
        ContactPoint contact = collision.GetContact(0);

        // Instantiate the fire at the contact point
        GameObject _newFire = Instantiate(_fire, contact.point, Quaternion.identity);

        FireSize bonfire = _newFire.GetComponent<FireSize>();
        if (bonfire != null)
        {
            bonfire.StartAll();
        }
    }
}
