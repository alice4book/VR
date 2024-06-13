using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The projectile that's created")]
    GameObject m_ProjectilePrefab = null;

    [SerializeField]
    ParticleSystem extinguisherCloud;

    [SerializeField]
    [Tooltip("The point that the project is created")]
    Transform m_StartPoint = null;

    [SerializeField]
    [Tooltip("The speed at which the projectile is launched")]
    float m_LaunchSpeed = 1.0f;

    bool fireSpawned = false;
    GameObject newObject;

    [SerializeField]
    bool debug;

    private ExtinguisherAnimations animations;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            // Get the ParticleSystem component from the child
            ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
            // Check if the child has a ParticleSystem component
            if (particleSystem != null)
            {
                // Assign the ParticleSystem to the appropriate field
                if (child.name.Contains("ExtinguisherCloud"))
                {
                    extinguisherCloud = particleSystem;
                    extinguisherCloud.Stop();
                }
            }
        }
    }


    public void Fire()
    {
        if(!fireSpawned) {
            animations.PressHandle();
            fireSpawned = true;
            //newObject = Instantiate(m_ProjectilePrefab, m_StartPoint.position, m_StartPoint.rotation, m_StartPoint);

            //if (newObject.TryGetComponent(out Rigidbody rigidBody))
                //ApplyForce(rigidBody);
            if(extinguisherCloud != null)
            {
                extinguisherCloud.Play();
            }
        }
    }

    private void OnValidate()
    {
        if (debug)
        {
            Fire();
        }
        else
        {
            FireOff();
        }
    }


    public void FireOff()
    {
        if(fireSpawned) {
            animations.ReleaseHandle();
            //Destroy(newObject);
            if(extinguisherCloud != null)
            {
                extinguisherCloud.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            fireSpawned = false;
        }
    }

    void ApplyForce(Rigidbody rigidBody)
    {
        Vector3 force = m_StartPoint.forward * m_LaunchSpeed;
        rigidBody.AddForce(force);
    }

    void Start() {

        animations = GetComponent<ExtinguisherAnimations>();

    }
}
