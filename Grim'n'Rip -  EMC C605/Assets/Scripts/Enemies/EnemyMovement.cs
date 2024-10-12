using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Script Reference")]
    public EnemyStats enemyStatsScript;
    public EnemyValues enemyValuesScript;

    [Header("Enemy Components")]
    [SerializeField] NavMeshAgent enemyAgent;
    public Transform playerTarget;

    [Header("Melee Type Enemy")]
    [SerializeField] bool isAttacking;
    [SerializeField] bool isPlayerWithinArea;
    [SerializeField] GameObject meleeAttack;

    [Header("Projectile Type Enemy")]
    [SerializeField] bool isProjectileType;

    

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindWithTag("Player").transform; // finds the player tag within hierarchy
        enemyStatsScript = GetComponent<EnemyStats>();
        enemyValuesScript = GetComponent<EnemyValues>();
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
            if (distanceToPlayer < 2.0f)
            {
                if (isAttacking == false)
                {
                    StartCoroutine(MeleeAttack());
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
            if (distanceToPlayer < 10.0f)
            {
                Debug.Log("Enemy Attacking Area Reached!");
                
                if (isAttacking == false)
                {
                    StartCoroutine(RangeAttack());
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

    IEnumerator MeleeAttack()
    {
        isAttacking = true;
        enemyAgent.ResetPath();
        meleeAttack.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        meleeAttack.SetActive(false);
        yield return new WaitForSeconds(2f); // Adjust base on animation
        isAttacking = false;
    }

    IEnumerator RangeAttack()
    {
        isAttacking = true;
        enemyAgent.ResetPath();
        yield return new WaitForSeconds(2f); // Adjust base on animation
        isAttacking = false;
    }
}
