using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5f; // How often to update the FPS display
    private float lastInterval;
    private int frames = 0;
    private float fps;

    private void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    private void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(10, 10, 100, 50); // Position and size of the FPS display

        style.fontSize = 16;
        style.normal.textColor = Color.green;

        int fpsInt = Mathf.RoundToInt(fps); // Round the FPS to the nearest integer
        GUI.Label(rect, "FPS: " + fpsInt, style);
    }
}
