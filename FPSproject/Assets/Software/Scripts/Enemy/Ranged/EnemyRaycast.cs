using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyRaycast : MonoBehaviour
{
    [SerializeField] float range;
    void Update()
    {
        Debug.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y,transform.position.z + range));
    }

}
