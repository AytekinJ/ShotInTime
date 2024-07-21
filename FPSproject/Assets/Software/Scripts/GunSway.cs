using System.Collections;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float swaySmoothFactor = 4f;

    private Vector3 initialPosition;
    //public CameraController CameraScript;
    //Camera cam;

    float mouseY, mouseX;

    void Start()
    {
        initialPosition = transform.localPosition;
        //cam = Camera.main;
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");

        //float rotationX = cam.transform.eulerAngles.x;
        //if (rotationX < CameraScript.PositiveClamp - 1 || rotationX > CameraScript.NegativeClamp - 1)
        //{
            mouseY = Input.GetAxis("Mouse Y");
        //}

        float moveX = Mathf.Clamp(-mouseX * swayAmount, -maxSwayAmount, maxSwayAmount);
        float moveY = Mathf.Clamp(-mouseY * swayAmount, -maxSwayAmount, maxSwayAmount);

        Vector3 targetPosition = new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition + initialPosition, Time.deltaTime * swaySmoothFactor);
    }
}
