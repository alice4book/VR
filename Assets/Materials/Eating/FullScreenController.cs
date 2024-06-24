using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class FullScreenController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float _dispalySeconds;

    [Header("References")]
    [SerializeField] private ScriptableRendererFeature _fullScreen;
    [SerializeField] private Material _material;

    private int _intensity = Shader.PropertyToID("_intensity");

    private void Start()
    {
        _fullScreen.SetActive(false);
    }

    private IEnumerator Mushroom()
    {
        _fullScreen.SetActive(true);
        // set varibles

        yield return new WaitForSeconds(_dispalySeconds);

        float elapsedTime = 0.0f;

        while (elapsedTime < _dispalySeconds)
        {
            elapsedTime += Time.deltaTime;

            // change varibles by time 
            // lerp
            // _material.SetFloat

            yield return null;
        }

        _fullScreen.SetActive(false);
    }
}
