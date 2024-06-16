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
    private ParticleSystem _particleSystem;

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
            _triesCount++;
            _igniteChance = Random.Range(_minChance, 101);
            if (_igniteChance > 75)
            {
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
    }

    private void StopSparks()
    {
        _capsuleCollider.enabled = false;
    }
}

