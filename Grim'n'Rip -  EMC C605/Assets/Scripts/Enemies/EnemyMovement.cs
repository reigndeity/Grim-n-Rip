using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Script Reference")]
    public EnemyStats enemyStatsScript;

    [Header("Enemy Components")]
    [SerializeField] NavMeshAgent enemyAgent;
    public Transform playerTarget;

    [Header("Melee Type Enemy")]
    [SerializeField] bool isAttacking;
    [SerializeField] bool isPlayerWithinArea;

    [Header("Projectile Type Enemy")]
    [SerializeField] bool isProjectileType;

    

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindWithTag("Player").transform; // finds the player tag within hierarchy
        enemyStatsScript = GetComponent<EnemyStats>();
    }
    void Update()
    {
        FollowPlayer();
        enemyAgent.speed = enemyStatsScript.movementSpeed;
    }
    void FollowPlayer()
    {
        // Melee Type Enemy
        if (isProjectileType == false)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            Debug.Log("Enemy Distance: " + enemyAgent.velocity);
            Debug.Log("Player Distance: " + distanceToPlayer);

            // Zombie is going to Attack the player
            if (distanceToPlayer < 2.0f)
            {
                Debug.Log("Enemy Attacking Area Reached!");
                isAttacking = true;
                isPlayerWithinArea = true;
                if (isAttacking == true && isPlayerWithinArea == true)
                {
                    enemyAgent.velocity = Vector3.zero;
                    StartCoroutine(MeleeAttackPlayer());
                }
            }
            // Player left the zombie's attack range
            if (distanceToPlayer >= 2)
            {
                isPlayerWithinArea = false;
                if (isPlayerWithinArea == false && isAttacking ==false)
                {
                    enemyAgent.SetDestination(playerTarget.position);
                }
            }  
        }
        // Range Type Enemy Movement
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            Debug.Log("Enemy Distance: " + enemyAgent.velocity);
            Debug.Log("Player Distance: " + distanceToPlayer);

            // Zombie is going to Attack the player
            if (distanceToPlayer < 10.0f)
            {
                Debug.Log("Enemy Attacking Area Reached!");
                isAttacking = true;
                isPlayerWithinArea = true;
                if (isAttacking == true && isPlayerWithinArea == true)
                {
                    enemyAgent.velocity = Vector3.zero;
                    StartCoroutine(RangeAttackPlayer());
                }
            }
            // Player left the zombie's attack range
            if (distanceToPlayer >= 10)
            {
                isPlayerWithinArea = false;
                if (isPlayerWithinArea == false && isAttacking ==false)
                {
                    enemyAgent.SetDestination(playerTarget.position);
                }
            }  
        }

    }
    IEnumerator MeleeAttackPlayer()
    {
        enemyAgent.ResetPath();
        yield return new WaitForSeconds(2.0f); // adjust according to the attack animation of the enemy
        isAttacking = false;
    }

    IEnumerator RangeAttackPlayer()
    {
        enemyAgent.ResetPath();
        yield return new WaitForSeconds(2.0f); // adjust according to the attack animation of the enemy
        isAttacking = false;
    }

}
