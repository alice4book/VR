using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obj;
    int randNum;
    public Transform spawnDest1;
    public bool spawningfalg = true;
    public float spawnTime;

    private void Start()
    {
        StartCoroutine(spawning());
    }
    IEnumerator spawning()
    {
        while (spawningfalg)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(obj, spawnDest1.position, spawnDest1.rotation);
        }
    }
}
