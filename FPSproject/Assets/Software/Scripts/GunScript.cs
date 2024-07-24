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
    bool CanShoot = true;
    public bool IsAiming;
    [SerializeField] bool IsAuto = false;
    public float MaxRayDistance;

    void Start()
    {
        CurrentAmmo = MaxAmmo + 1;
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

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    TryShoot();
        //}

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CurrentAmmo < MaxAmmo && CanShoot)
                StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            IsAiming = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            IsAiming = false;
        }
    }

    void TryShoot()
    {
        if (CanShoot && CurrentAmmo > 0)
        {
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
        Physics.Raycast(GunMuzzle.position, GunMuzzle.forward, out RaycastHit hitInfo, MaxRayDistance);
        if (hitInfo.collider == null)
            return;
        print(hitInfo.collider.transform.name);
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
}