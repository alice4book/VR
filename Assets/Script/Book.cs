using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] Burnable burnable;

    public void Thunder()
    {
        if (burnable._isBurning)
        {
            Debug.Log("Thunder!!!");
            //Thunder
        }
    }
}
