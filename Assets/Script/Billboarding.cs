using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    Vector3 _cameraDir;

    // Update is called once per frame
    void Update()
    {
        // Get the camera's forward direction
        Vector3 _cameraDir = Camera.main.transform.forward;

        // Flatten the camera's direction vector to the horizontal plane
        _cameraDir.y = 0;
        _cameraDir.Normalize();

        // Get the parent's forward direction and rotation
        Vector3 parentForward = transform.parent.forward;
        Quaternion parentRotation = transform.parent.rotation;

        // Create the target rotation to face the camera
        Quaternion targetRotation = Quaternion.LookRotation(_cameraDir);

        // Combine the target rotation with the parent's rotation around the forward axis
        Quaternion combinedRotation = Quaternion.LookRotation(_cameraDir, parentRotation * Vector3.up);

        // Apply the final rotation to the object
        transform.rotation = combinedRotation;
    }
}
