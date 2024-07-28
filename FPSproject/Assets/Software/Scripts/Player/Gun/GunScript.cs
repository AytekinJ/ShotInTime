using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Animator animator;
    public Movement movementScript;
    [SerializeField] WeaponTepmeScript TepmeScript;
    [SerializeField] int CurrentAmmo;
    public int MaxAmmo;
    [SerializeField] float ReloadTime = 1f;
    [SerializeField] float FireRate = 0.15f;
    public Transform GunMuzzle;
    public GameObject BulletPrefab;
    public Transform BulletCaseTransform;
    public GameObject BulletCasePrefab;
    public bool CanAim;
    bool CanShoot = true;
    public bool IsAiming;
    public bool EffectSignal;
    [SerializeField] bool IsAuto = false;
    public float MaxRayDistance;
    public LineRenderer lineRenderer;

    void Start()
    {
        CurrentAmmo = MaxAmmo + 1;
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (IsAuto)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                TryShoot();
            }
        }
        else if (!IsAuto)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TryShoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CurrentAmmo < MaxAmmo && CanShoot)
                StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) /*&& CanAim*/)
        {
            IsAiming = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) /*&& CanAim*/)
        {
            IsAiming = false;
        }
    }

    void TryShoot()
    {
        if (CanShoot && CurrentAmmo > 0)
        {
            EffectSignal = true;
            StartCoroutine(ShootWithDelay());
        }
    }

    IEnumerator ShootWithDelay()
    {
        CanShoot = false;
        Shoot();
        yield return new WaitForSeconds(FireRate);
        CanShoot = true;
    }

    void Shoot()
    {
        CurrentAmmo--;

        var bullet = Instantiate(BulletPrefab, GunMuzzle.position, GunMuzzle.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.right * 300, ForceMode.Impulse);

        RayShoot();

        var Casing = Instantiate(BulletCasePrefab, BulletCaseTransform.position, BulletCaseTransform.rotation, null);
        Rigidbody CasingRb = Casing.GetComponent<Rigidbody>();
        if (movementScript.IsMovingRight())
            CasingRb.AddForce(CasingRb.transform.right * Random.Range(0.6f, 0.9f), ForceMode.Impulse);
        else
            CasingRb.AddForce(CasingRb.transform.right * Random.Range(0.3f, 0.6f), ForceMode.Impulse);

        TepmeScript.ApplyRecoil();
        animator.SetTrigger("GlockShoot");
    }

    void RayShoot()
    {
        Vector3 direction = GunMuzzle.forward;
        Physics.Raycast(GunMuzzle.position, direction, out RaycastHit hitInfo, MaxRayDistance);

        Debug.DrawRay(GunMuzzle.position, direction * MaxRayDistance, Color.red, 0.1f);

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, GunMuzzle.position);
            if (hitInfo.collider != null)
            {
                lineRenderer.SetPosition(1, hitInfo.point);
                print(hitInfo.collider.transform.name);
            }
            else
            {
                lineRenderer.SetPosition(1, GunMuzzle.position + direction * MaxRayDistance);
            }
        }
    }

    IEnumerator Reload()
    {
        CanShoot = false;
        yield return new WaitForSeconds(ReloadTime);
        if (CurrentAmmo > 0)
        {
            CurrentAmmo = MaxAmmo + 1;
        }
        else
        {
            CurrentAmmo = MaxAmmo;
        }
        CanShoot = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (GunMuzzle != null)
        {
            Gizmos.DrawRay(GunMuzzle.position, GunMuzzle.forward * MaxRayDistance);
        }
    }
}
