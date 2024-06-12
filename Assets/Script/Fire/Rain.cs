using System.Collections;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] private ParticleSystem _rainParticles;
    [SerializeField] private Animator _rainAnimator;
    [SerializeField] private float _rainDuration;
    [SerializeField] private bool _isRaining;

    private void Awake()
    {
        _rainParticles = GetComponent<ParticleSystem>();
        _rainDuration = 5.0f;
    }

    public void StartRain()
    {
        if (!_isRaining)
        {
            _rainParticles.Play();
            if (_rainAnimator != null)
            {
                _rainAnimator.SetBool("isRaining", true);  // Start the animator
            }
            StartCoroutine(StopRain());
        }
    }

    IEnumerator StopRain()
    {
        _isRaining = true;
        yield return new WaitForSeconds(_rainDuration);
        _rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        if (_rainAnimator != null)
        {
            _rainAnimator.SetBool("isRaining", false);  // Stop the animator
        }
        _isRaining = false;
    }

    private void OnValidate()
    {
        if (_isRaining)
        {
            _isRaining = false;
            StartRain();
        }
    }
}
