using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float TimeToDestroy = 3f;
    public bool DestroyOnCollision = false;
    public GameObject TrailTransformSetObject;
    void Start()
    {
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
}
