using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTepmeScript : MonoBehaviour
{
    Vector3 CurrentRotation;
    Vector3 TargetRotation;

    [SerializeField] float SnapRate;
    [SerializeField] float FallBackRate;

    [SerializeField] float Xrecoil;
    [SerializeField] float Yrecoil;
    [SerializeField] float Zrecoil;

    void Update()
    {
        TargetRotation = Vector3.Lerp(TargetRotation, Vector3.zero, FallBackRate * Time.deltaTime);
        CurrentRotation = Vector3.Slerp(CurrentRotation, TargetRotation, SnapRate * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(CurrentRotation);
    }

    public void ApplyRecoil()
    {
        TargetRotation += new Vector3(Xrecoil, Random.Range(-Yrecoil, Yrecoil), Random.Range(-Zrecoil, Zrecoil));
    }
}