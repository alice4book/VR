using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceClouds : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The projectile that's created")]
    GameObject m_ProjectilePrefab = null;

    [SerializeField]
    [Tooltip("The point that the project is created")]
    Transform m_StartPoint = null;

    [SerializeField]
    [Tooltip("The speed at which the projectile is launched")]
    float m_LaunchSpeed = 1.0f;

    bool fireSpawned = false;
    GameObject newObject;

    public void Fire()
    {
        if(!fireSpawned) {
            fireSpawned = true;
            newObject = Instantiate(m_ProjectilePrefab, m_StartPoint.position, m_StartPoint.rotation, m_StartPoint);

            if (newObject.TryGetComponent(out Rigidbody rigidBody))
                ApplyForce(rigidBody);
        }
    }


    public void FireOff()
    {
        if(fireSpawned) {
            Destroy(newObject);
            fireSpawned = false;
        }
    }

    void ApplyForce(Rigidbody rigidBody)
    {
        Vector3 force = m_StartPoint.forward * m_LaunchSpeed;
        rigidBody.AddForce(force);
    }
}
