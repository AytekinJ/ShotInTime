using UnityEngine;

public class ManualCameraRenderer : MonoBehaviour
{
    public Camera targetCamera;
    public int targetFrameRate = 60;

    private float frameInterval;
    private float nextFrameTime;

    private void Start()
    {
        if (targetCamera == null)
        {
            return;
        }

        Application.targetFrameRate = 10;

        frameInterval = 1f / targetFrameRate;
        nextFrameTime = Time.time + frameInterval;
    }

    private void Update()
    {
        if (Time.time >= nextFrameTime)
        {
            RenderCamera();
            nextFrameTime += frameInterval;
        }
    }

    private void RenderCamera()
    {
        targetCamera.Render();
    }
}
