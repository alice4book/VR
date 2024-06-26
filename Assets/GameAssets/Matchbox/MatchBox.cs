using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBox : MonoBehaviour
{
    [SerializeField] private GameObject _match;
    [SerializeField] private bool _used = false;
    [SerializeField] private bool _isBurning = false;

    private Coroutine fireCorutine;

    [Header("Colors")]
    [SerializeField] private Renderer _objectRenderer;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private float _durationOfBurning = 15.0f;


    private void OnTriggerExit(Collider other)
    {
        if (!_isBurning && !_used && other.gameObject.tag == "MatchBox")
        {
            _match.SetActive(true);
            _used = true;
            fireCorutine = StartCoroutine(FireMatch());
        }else
        if (!_used && !_isBurning)
        {
            Burnable otherBurnable = other.gameObject.GetComponent<Burnable>();
            Lighter lighter = other.gameObject.GetComponent<Lighter>();
            if ((lighter != null && lighter.fireSpawned) || (otherBurnable != null && otherBurnable._isBurning) || (other.gameObject.tag == "FireStarter"))
            {
                _match.SetActive(true);
                _used = true;
                fireCorutine = StartCoroutine(FireMatch());
            }
        }
        else
        if (_isBurning)
        {
            if (other.transform.gameObject.tag == "ExtinguisherClouds")
            {
                StopCoroutine(fireCorutine);
                _match.SetActive(false);
            }
        }
    }

    IEnumerator FireMatch()
    {
        float timeElapsed = 0f;
        _isBurning = true;
        while (timeElapsed < _durationOfBurning && _isBurning)
        {
            // Lerp color based on time elapsed
            _objectRenderer.materials[1].color = Color.Lerp(_startColor, _endColor, timeElapsed / _durationOfBurning);
            // Increment the time elapsed
            timeElapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }
        _isBurning = false;
        _match.SetActive(false);
    }
}
