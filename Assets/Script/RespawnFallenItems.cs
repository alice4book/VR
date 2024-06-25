using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFallenItems : MonoBehaviour
{
    [SerializeField] Collider spawnArea;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = GetRandomPosition();
    }


    private Vector3 GetRandomPosition()
    {
        Vector3 point = new Vector3(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
            );

        return point;
    }
}
