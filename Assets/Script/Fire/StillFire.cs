using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillFire : MonoBehaviour
{
    [Tooltip("Script controlling fire size")]
    [SerializeField]
    private FireSize fireSize;

    private void Awake()
    {
        fireSize = GetComponent<FireSize>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "ExtinguisherClouds")
        {
            if (fireSize != null)
                fireSize.StopAll();
            Invoke("DestroyFire", 0.2f);
        }
    }

    private void DestroyFire()
    {
        Destroy(gameObject);
    }
}
