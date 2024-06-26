using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _match;

    public void SpawnMatch()
    {
        if (_match != null)
        {
            GameObject spawnedMatch = Instantiate(_match, Vector3.zero, Quaternion.identity);
        }
    }



}
