using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {

    [SerializeField] private Color mushroomColor;

    private ParticleSystem[] particleSystems;

    void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject);
    }

    void CheckCollision(GameObject collidedObject)
    {
        Transform bonfireTransform = collidedObject.transform.Find("Bonfire");

        if (bonfireTransform != null)
        {
            Burnable burnable = bonfireTransform.GetComponent<Burnable>();

            if (burnable != null && burnable._isBurning)
            {
                //Debug.Log("burning");

                particleSystems = bonfireTransform.GetComponentsInChildren<ParticleSystem>();

                Debug.Log(particleSystems.Length);

                
                for(int i = 0; i < particleSystems.Length; i++)
                {
                    ParticleSystem particleSystem = particleSystems[i];

                    var colorOverLifetime = particleSystem.colorOverLifetime;
                    colorOverLifetime.enabled = true;

                    Gradient gradient = new Gradient();
                    GradientColorKey[] gradientColorKeys = {new GradientColorKey(mushroomColor, 0.0f), new GradientColorKey(mushroomColor, 1.0f)};
                    GradientAlphaKey[] gradientAlphaKeys = {new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f)};
                    gradient.SetKeys(gradientColorKeys, gradientAlphaKeys);

                    colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
            
                }               
            }
        }
    }
}
