using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    namespace AI
    {
        namespace Ranged
        {
            public class MovementRanged : MonoBehaviour
            {
                public static void RangedMovement(Vector3 playerPos,Vector3 thisPosition,NavMeshAgent agent,float followDistance, float speed)
                {
                    float distance = Vector3.Distance(playerPos, thisPosition);
                    if (distance < followDistance) 
                    {

                    }

                    agent.destination = playerPos;
                }
            }
        }
        namespace NonRanged
        {
            public class Movement : MonoBehaviour
            {

            }
        }
    }
}
