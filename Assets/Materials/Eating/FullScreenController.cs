using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering;

public class FullScreenController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float _dispalyTransitionSeconds;
    [SerializeField]
    private float _dispalyShowSeconds;

    [Header("References")]
    [SerializeField] private ScriptableRendererFeature _fullScreen;
    [SerializeField] private Material _material;

    [SerializeField] bool _bMushroomEffectOn;

    private void Start()
    {
        _fullScreen.SetActive(false);
        _bMushroomEffectOn = false;
    }

    private void OnValidate()
    {
        if (_bMushroomEffectOn)
            StartCoroutine(Mushroom());
    }

    public void StartEffect()
    {
        if(!_bMushroomEffectOn)
            StartCoroutine(Mushroom());
    }

    private IEnumerator Mushroom()
    {
        _fullScreen.SetActive(true);
        _bMushroomEffectOn = true;
        bool up = true;
        float elapsedTime = 0.0f;
        float lerpedIntensity = 0.0f;
        while (elapsedTime < _dispalyTransitionSeconds)
        {
            elapsedTime += Time.deltaTime;

            if(up)
                lerpedIntensity = Mathf.Lerp(0.0f, 1.0f, elapsedTime / (_dispalyTransitionSeconds * 0.5f));
            else
                lerpedIntensity = Mathf.Lerp(1.0f, 0.0f, elapsedTime / (_dispalyTransitionSeconds * 0.5f));
            if (lerpedIntensity >= 1.0f)
            {
                yield return new WaitForSeconds(_dispalyShowSeconds);
                lerpedIntensity = 1.0f;
                up = false;
                elapsedTime = 0.0f; // Reset elapsed time for decreasing phase
            }
            // Set the material's float property to change its intensity
            _material.SetFloat("_Intensity", lerpedIntensity);

            yield return new WaitForFixedUpdate();
        }

        _bMushroomEffectOn = false;
        _fullScreen.SetActive(false);

    }
}
