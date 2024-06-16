using UnityEngine;

public class FireSize : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The projectile creating transparent part")]
    private ParticleSystem _alpha;
    [SerializeField]
    [Tooltip("The projectile creating opaque part")]
    private ParticleSystem _add;
    [SerializeField]
    [Tooltip("The projectile creating glowing part")]
    private ParticleSystem _glow;

    [SerializeField] 
    private AudioSource _crackingFireNoise;

    [Tooltip("Scal whole bonfire")]
    [Range(1.0f, 10.0f)]
    public float size;
    [Tooltip("The variable handles the size of the surface from which the particles are emitted - X Axies")]
    [Range(1.0f, 100.0f)]
    public float lenght_x;
    [Tooltip("The variable handles the size of the surface from which the particles are emitted - Y Axies")]
    [Range(1.0f, 100.0f)]
    public float lenght_y;
    [Tooltip("The variable handles the size of the surface from which the particles are emitted - Z Axies")]
    [Range(1.0f, 100.0f)]
    public float lenght_z;

    [SerializeField]
    [Tooltip("Basis for emsision rate")]
    private float _startingEmissionRate;
    private float _oldSize;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            // Get the ParticleSystem component from the child
            ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
            _oldSize = 1.0f;
            lenght_x = 1.0f;
            lenght_y = 1.0f;
            lenght_z = 1.0f;
            transform.localScale = new Vector3(lenght_x, lenght_y, lenght_z);
            // Check if the child has a ParticleSystem component
            if (particleSystem != null)
            {
                // Assign the ParticleSystem to the appropriate field
                if (child.name.Contains("Alpha"))
                {
                    _alpha = particleSystem;
                    _alpha.Stop();
                }
                else if (child.name.Contains("Add"))
                {
                    _add = particleSystem;
                    _add.Stop();
                }
                else if (child.name.Contains("Glow"))
                {
                    _glow = particleSystem;
                    _glow.Stop();
                }
            }
        }
        _crackingFireNoise = GetComponent<AudioSource>();

        UpdateValues();
    }

    private void OnValidate()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        if (_alpha != null)
        {
            _alpha.startSize = size * _alpha.startSize / _oldSize;
            _alpha.startLifetime = size * _alpha.startLifetime / _oldSize;
            _alpha.startSpeed = _oldSize * _alpha.startSpeed / size;


            var shape = _alpha.shape;
            shape.scale = new Vector3(lenght_x, lenght_y, lenght_z);
            _alpha.emissionRate = _startingEmissionRate + lenght_y * lenght_x * 3.0f;
        }
        if (_add != null)
        {
            _add.startSize = size * _add.startSize / _oldSize;
            _add.startLifetime = size * _add.startLifetime / _oldSize;
            _add.startSpeed = _oldSize * _add.startSpeed / size;


            var shape = _add.shape;
            shape.scale = new Vector3(lenght_x, lenght_y, lenght_z);
            _add.emissionRate = _startingEmissionRate + lenght_y * lenght_x * 3.0f;
        }
        if (_glow != null)
        {
            _glow.startSize = size * _glow.startSize / _oldSize;
            _glow.startLifetime = size * _glow.startLifetime / _oldSize;
            _glow.startSpeed = _oldSize * _glow.startSpeed / size;

            var shape = _glow.shape;
            shape.scale = new Vector3(lenght_x, lenght_y, lenght_z);
            _glow.emissionRate = _startingEmissionRate + lenght_y * lenght_x * 3.0f;
        }
        _oldSize = size;
    }

    public void ResetValues()
    {
        size = 1.0f;
        lenght_x = 1.0f;
        lenght_y = 1.0f;
        lenght_z = 1.0f;
    }

    public void StartAll()
    {
        if (_alpha != null)
            _alpha.Play();
        if (_glow != null)
            _glow.Play();
        if (_add != null)
            _add.Play();
        if(_crackingFireNoise)
            _crackingFireNoise.Play();
    }

    public void StopAll()
    {
        if (_alpha != null)
            _alpha.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        if (_glow != null)
            _glow.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        if (_add != null)
            _add.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        if ( _crackingFireNoise)
            _crackingFireNoise.Stop();
    }
}
