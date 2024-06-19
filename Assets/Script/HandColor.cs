using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColor : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] Burnable burnable;
    Color color1 = new Color (1.0f, 1.0f, 1.0f, 1.0f);
    Color color2= new Color(1.0f, 0.0f, 0.0f, 1.0f);
    public void ChangeColor()
    {
        if(burnable._isBurning)
        material.color = color2;
    }

    public void RevertColor()
    {
        material.color = color1;
    }
}
