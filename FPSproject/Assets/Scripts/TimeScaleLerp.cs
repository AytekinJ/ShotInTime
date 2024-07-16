using System.Collections;
using UnityEngine;

public class TimeScaleLerp : MonoBehaviour
{
    public float targetTimeScale = 0.5f;
    public float slerpDuration = 2.0f;
    public float holdDuration = 5.0f;

    private bool isSlerping = false;

    public void StartTimeScale()
    {
        StartCoroutine(SlerpTimeScale(targetTimeScale, slerpDuration, holdDuration));
    }

    IEnumerator SlerpTimeScale(float target, float slerpDuration, float holdDuration)
    {
        if (isSlerping) yield break;
        isSlerping = true;

        float start = Time.timeScale;
        float elapsed = 0f;

        while (elapsed < slerpDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(start, target, elapsed / slerpDuration);
            yield return null;
        }
        Time.timeScale = target;

        yield return new WaitForSecondsRealtime(holdDuration);

        start = Time.timeScale;
        elapsed = 0f;
        while (elapsed < slerpDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(start, 1.0f, elapsed / slerpDuration);
            yield return null;
        }
        Time.timeScale = 1.0f;

        isSlerping = false;
    }
}