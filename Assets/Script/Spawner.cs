using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    int randNum;
    public Transform spawnDest1;
    public float spawnTime;
    public bool spawningfalg;
    

    private void Start()
    {
        StartCoroutine(spawning());
    }
    public IEnumerator spawning()
    {
        Quaternion stickrotation = new Quaternion();
        stickrotation.SetEulerAngles(80,0,0);
            while (spawningfalg)
            {
                randNum = Random.Range(1, 4);
                yield return new WaitForSeconds(spawnTime);
                switch (randNum) {
                    case 1:
                        Instantiate(obj1, spawnDest1.position, stickrotation);
                        break;
                    case 2:
                        Instantiate(obj2, spawnDest1.position, stickrotation);
                        break;
                    case 3:
                        Instantiate(obj3, spawnDest1.position, stickrotation);
                        break;
                }
            }
    }
}
