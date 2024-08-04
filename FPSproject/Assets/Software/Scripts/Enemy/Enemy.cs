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
                public static void Move(Vector3 playerPos, Vector3 thisPosition, NavMeshAgent agent, float minFollowDistance, float maxFollowDistance, float speed)
                {
                    agent.speed = speed;
                    float distance = Vector3.Distance(playerPos, thisPosition);

                    if (distance < minFollowDistance)
                    {
                        agent.isStopped = false;
                        Vector3 direction = (thisPosition - playerPos).normalized;
                        agent.destination = thisPosition + direction; // Move away from the player
                    }
                    else if (distance > maxFollowDistance)
                    {
                        agent.isStopped = false;
                        agent.destination = playerPos; // Move towards the player
                    }
                    else
                    {
                        agent.isStopped = true; // Stop if within the range
                    }
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
    namespace Stats
    {
        public class EHealth : MonoBehaviour
        {
            public static float GetDamage(float currentHealth, float damageAmount,GameObject enemyObj)
            {
                currentHealth -= damageAmount;
                if (currentHealth <= 0)
                {
                    Destroy(enemyObj);
                }
                return currentHealth;
            }
        }
    }
}
