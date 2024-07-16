using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public TimeScaleLerp TimeLerpScript;
    public Transform player;
    public float sensitivity = 2.0f;
    public float smoothSpeed = 15.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    //[SerializeField] float TimeScaleValue = 0.1f;
    //[SerializeField] float ScaledSmoothSpeed;
    [SerializeField] float ScaledSensitivity;

    void Start()
    {
        //Time.timeScale = TimeScaleValue;
        ScaledSensitivity = sensitivity * Time.timeScale;
        //ScaledSmoothSpeed = smoothSpeed / Time.timeScale;
        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            TimeLerpScript.StartTimeScale();
        //Time.timeScale = TimeScaleValue;
        ScaledSensitivity = sensitivity * Time.timeScale;
        //ScaledSmoothSpeed = smoothSpeed / Time.timeScale;
        
        HandleMouseInput();
        RotatePlayer();
        RotateCamera();
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void HandleMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * ScaledSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * ScaledSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
    }

    void RotatePlayer()
    {
        Quaternion playerRotation = Quaternion.Euler(0, rotationY, 0);
        player.rotation = Quaternion.Slerp(player.rotation, playerRotation, smoothSpeed * Time.deltaTime);
    }

    void RotateCamera()
    {
        Quaternion targetRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoothSpeed * Time.deltaTime);
    }
}
