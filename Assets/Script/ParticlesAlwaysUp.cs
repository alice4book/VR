using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesAlwaysUp : MonoBehaviour
{
    void LateUpdate()
    {
        // Resetuj globalny obr�t obiektu do pocz�tkowego
        transform.rotation = Quaternion.identity;
    }

}
