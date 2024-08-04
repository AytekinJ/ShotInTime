using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.AI.Ranged;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;

    void Update()
    {
        MovementRanged.Move(player.position,transform.position,agent,5,10,10);
    }
}
