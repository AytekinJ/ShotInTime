using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float TimeToDestroy = 3f;
    public bool DestroyOnCollision = false;
    public bool FadeAway = false;
    public float FadeRate = 10f;
    GameObject TrailTransformSetObject;
    MeshRenderer meshRenderer;

    void Start()
    {
        if (FadeAway)
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            if (meshRenderer != null)
            {
                StartCoroutine(FadeTheMaterial());
            }
        }

        Destroy(gameObject, TimeToDestroy);
        TrailTransformSetObject = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnCollisionEnter(Collision collision)
    {
        TrailRenderer trail = GetComponentInChildren<TrailRenderer>();

        if (trail != null)
        {
            trail.transform.parent = TrailTransformSetObject.transform;
        }

        if (DestroyOnCollision)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FadeTheMaterial()
    {
        yield return new WaitForSeconds(TimeToDestroy - 2);
        float startAlpha = meshRenderer.material.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / 2f);
            Color newColor = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, alpha);
            meshRenderer.material.color = newColor;
            yield return null;
        }
    }
}
