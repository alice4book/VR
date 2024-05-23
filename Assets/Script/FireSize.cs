using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSize : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _alpha;
    [SerializeField]
    private ParticleSystem _add;
    [SerializeField]
    private ParticleSystem _glow;

    [Range(1.0f, 10.0f)]
    public float size;
    private float _oldSize;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            // Get the ParticleSystem component from the child
            ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
            size = 1.0f;
            _oldSize = size;
            // Check if the child has a ParticleSystem component
            if (particleSystem != null)
            {
                // Assign the ParticleSystem to the appropriate field
                if (child.name.Contains("Alpha"))
                {
                    _alpha = particleSystem;
                }
                else if (child.name.Contains("Add"))
                {
                    _add = particleSystem;
                }
                else if (child.name.Contains("Glow"))
                {
                    _glow = particleSystem;
                }
            }
        }
    }

    private void Start()
    {
        if (_alpha != null)
        {
            _alpha.startSize = size * _alpha.startSize / _oldSize;
            _alpha.startLifetime = size * _alpha.startLifetime / _oldSize;
            _alpha.startSpeed = _oldSize * _alpha.startSpeed / size;
        }
        if (_add != null)
        {
            _add.startSize = size * _add.startSize / _oldSize;
            _add.startLifetime = size * _add.startLifetime / _oldSize;
            _add.startSpeed = _oldSize * _add.startSpeed / size;
        }
        if (_glow != null)
        {
            _glow.startSize = size * _glow.startSize / _oldSize;
            _glow.startLifetime = size * _glow.startLifetime / _oldSize;
            _glow.startSpeed = _oldSize * _glow.startSpeed / size;
        }
        _oldSize = size;
    }

    private void OnValidate()
    {
        if (_alpha != null)
        {
            _alpha.startSize = size * _alpha.startSize / _oldSize;
            _alpha.startLifetime = size * _alpha.startLifetime / _oldSize;
            _alpha.startSpeed = _oldSize * _alpha.startSpeed / size;
        }
        if (_add != null)
        {
            _add.startSize = size * _add.startSize / _oldSize;
            _add.startLifetime = size * _add.startLifetime / _oldSize;
            _add.startSpeed = _oldSize * _add.startSpeed / size;
        }
        if (_glow != null)
        {
            _glow.startSize = size * _glow.startSize / _oldSize;
            _glow.startLifetime = size * _glow.startLifetime / _oldSize;
            _glow.startSpeed = _oldSize * _glow.startSpeed / size;
        }
        _oldSize = size;
    }

}
