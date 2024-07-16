using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    public Vector3 RestPos;
    public Vector3 AimPos;
    public GunScript gunScript;
    public float AimSpeed = 10f;
    public Camera PlayerCamera;
    public Camera FakeCam;
    public float DesiredAimFov;
    public float FovSpeed = 10f;
    float startFov;
    float fakeCamStartFov;

    void Start()
    {
        startFov = PlayerCamera.fieldOfView;
        fakeCamStartFov = FakeCam.fieldOfView;
    }

    void Update()
    {
        if (gunScript.IsAiming)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, AimPos, AimSpeed * Time.deltaTime);
            PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView ,DesiredAimFov, FovSpeed * Time.deltaTime);
            FakeCam.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, DesiredAimFov, FovSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, RestPos, AimSpeed * 2 * Time.deltaTime);
            PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, startFov, FovSpeed * 2 * Time.deltaTime);
            FakeCam.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, fakeCamStartFov, FovSpeed * 2 * Time.deltaTime);
        }

        
    }


}
