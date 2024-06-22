using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlintBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How many tries befor reset")]
    private int _howManyTries;

    [SerializeField]
    [Tooltip("How many tries currently")]
    private int _triesCount;

    [SerializeField]
    [Tooltip("Minimal posible random number")]
    private int _minChance;

    [SerializeField]
    private int _igniteChance;

    [SerializeField]
    private CapsuleCollider _capsuleCollider;

    [SerializeField]
    private BoxCollider _boxCollider;

    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private Vector3 _startingPos;

    bool _bIsTriggeredOnce = false;


    private void Awake()
    {
        _triesCount = 0;
        _minChance = 1;
        _howManyTries = 4;
        _capsuleCollider.enabled = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Flint")
        {
            _bIsTriggeredOnce = false;
            _startingPos = HitCalculation(collision, _boxCollider);
            StartCoroutine(WaitNextFrame(collision));
        }
    }

    IEnumerator WaitNextFrame(Collider collision)
    {
        yield return 0;
        _bIsTriggeredOnce = true;
        _triesCount++;
        _igniteChance = Random.Range(_minChance, 101);
        if (_igniteChance > 75)
        {
            Vector3 hit = HitCalculation(collision, _boxCollider);
            _particleSystem.gameObject.transform.position = hit;
            Vector3 dir = hit - _startingPos;
            if (dir.z < 0)
            {
                _particleSystem.gameObject.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            }
            else
            {
                _particleSystem.gameObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
            }
            _particleSystem.Play();
            _capsuleCollider.enabled = true;
            Invoke("StopSparks", 0.2f);
        }
        _minChance += 25;
        if (_howManyTries < _triesCount)
        {
            _minChance = 1;
            _triesCount = 0;
        }
    }

    private void StopSparks()
    {
        _capsuleCollider.enabled = false;
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

