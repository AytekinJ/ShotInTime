using System.Collections;
using UnityEngine;
using Enemy.Stats;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] GameObject MetalImpactParticle;
    [SerializeField] GameObject WoodImpactParticle;
    [SerializeField] GameObject ImpactSmokeParticle;
    [Space(10)]
    public float TimeToDestroy = 3f;
    public bool DestroyOnCollision = false;
    public bool FadeAway = false;
    public bool IsBullet = false;
    public float FadeRate = 10f;
    GameObject TrailTransformSetObject;
    MeshRenderer meshRenderer;
    [Space(10)]
    [SerializeField] float Damage;

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
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.HealthValue = EHealth.GetDamage(enemyHealth.HealthValue, Damage, collision.gameObject);
        }

        if (IsBullet)
            ParticleEffects(collision);

        if (trail != null)
        {
            trail.transform.parent = TrailTransformSetObject.transform;
        }

        if (DestroyOnCollision)
        {
            Destroy(gameObject);
        }
    }

    void ParticleEffects(Collision collision)
    {
        if (collision.transform.tag == "MetalObject")
        {
            var impact = Instantiate(MetalImpactParticle, transform.position, transform.rotation, collision.transform);
            impact.transform.Rotate(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
            Destroy(impact, 1f);
        }
        else if (collision.transform.tag == "WoodObject")
        {
            var impact = Instantiate(WoodImpactParticle, transform.position, transform.rotation, collision.transform);
            impact.transform.Rotate(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
            Destroy(impact, 1f);

        }
        var smoke = Instantiate(ImpactSmokeParticle, transform.position, transform.rotation, collision.transform);
        smoke.transform.Rotate(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
        Destroy(smoke, 1f);
        
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
