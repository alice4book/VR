using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesAlwaysUp : MonoBehaviour
{
    void LateUpdate()
    {
        // Resetuj globalny obrót obiektu do pocz¹tkowego
        transform.rotation = Quaternion.identity;
    }

}
