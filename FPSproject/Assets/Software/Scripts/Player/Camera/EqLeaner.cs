using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqLeaner : MonoBehaviour
{
    [SerializeField] KeyCode RightKeyCode = KeyCode.E;

    [SerializeField] KeyCode LeftKeyCode = KeyCode.Q;

    [SerializeField] float LeanAngle = 15f;

    [SerializeField] float LeanSpeed = 5f;

    float LeanValue;

    float TheTargetLeanValue;

    
    

    void Update()
    {
        if (Input.GetKey(LeftKeyCode))
        {
            TheTargetLeanValue = LeanAngle;
        }
        else if (Input.GetKey(RightKeyCode))
        {
            TheTargetLeanValue = -LeanAngle;
        }
        else
        {
            TheTargetLeanValue = 0f;
        }

        Lean();
    }

    void Lean()
    {
        LeanValue = Mathf.LerpAngle(LeanValue, TheTargetLeanValue, LeanSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0f, 0f, LeanValue);
    }

}
