using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] GameObject spawnSpot;
    [SerializeField] GameObject parentOfMushrooms;
    [SerializeField] float delay = 20f;

    Transform spawnPosition;

    GameObject spawnedMushroom;

    bool currentlySpawning = false;



    // Start is called before the first frame update
    void Start()
    {
        SpawnMushroom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnMushroom()
    {
        //Debug.Log("Spawn");
        spawnedMushroom = Instantiate(objectPrefab, spawnSpot.transform.position, spawnSpot.transform.rotation, parentOfMushrooms.transform);
        currentlySpawning = false;
    }

    private IEnumerator SpawnMushroomAfterDelay()
    {
        //Debug.Log("Waiting");
        yield return new WaitForSeconds(delay);

        SpawnMushroom();
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("OnTriggerExit");
        if (currentlySpawning == false) {
            if (other.gameObject == spawnedMushroom)
            {
                currentlySpawning = true;
                StartCoroutine(SpawnMushroomAfterDelay());
            }
        }
    }
}
