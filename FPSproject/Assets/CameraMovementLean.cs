using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementLean : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] float rotationSpeed = 5.0f;
    float targetRotationSpeed;

    void Start()
    {
        targetRotationSpeed = rotationSpeed;
    }

    void Update()
    {
        checkIfRunning();
        float targetRotationZ = -movement.horizontal;
        Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, targetRotationZ);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * targetRotationSpeed);
    }

    void checkIfRunning()
    {
        if (movement.isRunning)
        {
            targetRotationSpeed = rotationSpeed * 2;
        }
        else if (!movement.isRunning)
        {
            targetRotationSpeed = rotationSpeed;
        }
            
    }
}
