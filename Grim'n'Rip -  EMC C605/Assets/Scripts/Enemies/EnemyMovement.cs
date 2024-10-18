using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Script Reference")]
    public EnemyStats enemyStatsScript;
    public EnemyValues enemyValuesScript;
    public Animator enemyAnimator;
    public EnemyProjectileType enemyProjectileTypeScript;

    [Header("Enemy Components")]
    [SerializeField] NavMeshAgent enemyAgent;
    public Transform playerTarget;

    [Header("EnemyType")]
    [SerializeField] int enemyType;

    [Header("Melee Type Enemy")]
    [SerializeField] bool isAttacking;
    [SerializeField] bool isPlayerWithinArea;
    [SerializeField] GameObject meleeAttack;
    [SerializeField] bool animatorTransition;

    [Header("Projectile Type Enemy")]
    [SerializeField] bool isProjectileType;
    //[SerializeField] Transform enemyRotation; // only for the fucked up model of the projectile

    

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindWithTag("Player").transform; // finds the player tag within hierarchy
        enemyStatsScript = GetComponent<EnemyStats>();
        enemyValuesScript = GetComponent<EnemyValues>();
        enemyAnimator = GetComponent<Animator>();
        enemyProjectileTypeScript = GetComponent<EnemyProjectileType>();

        
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
                
                if (enemyType == 0)
                {
                    // Make the enemy look at the player target
                    Vector3 directionToPlayer = playerTarget.position - transform.position;
                    directionToPlayer.y = 0; // Keep the rotation only on the Y-axis
                    Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation

                    if (distanceToPlayer < 1.5f)
                    {
                        enemyAgent.velocity = Vector3.zero;
                        if (isAttacking == false)
                        {
                            StartCoroutine(BlazeAttack());
                        }
                    }
                    if (distanceToPlayer >= 1.5f)
                    {
                        isPlayerWithinArea = false;
                        if (isPlayerWithinArea == false && isAttacking == false)
                        {
                            enemyAgent.SetDestination(playerTarget.position);
                            enemyAnimator.SetInteger("animState", 0);
                        }
                    }
                }

                if (enemyType == 2)
                {
                    // Make the enemy look at the player target
                    Vector3 directionToPlayer = playerTarget.position - transform.position;
                    directionToPlayer.y = 0; // Keep the rotation only on the Y-axis
                    Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation

                    if (distanceToPlayer < 2.0f)
                    {
                        enemyAgent.velocity = Vector3.zero;
                        if (isAttacking == false)
                        {
                            StartCoroutine(VainedAttack());
                        }
                    }
                    if (distanceToPlayer >= 2.0f)
                    {
                        isPlayerWithinArea = false;
                        if (isPlayerWithinArea == false && isAttacking == false)
                        {
                            enemyAgent.SetDestination(playerTarget.position);
                            enemyAnimator.SetInteger("animState", 0);
                            animatorTransition = false;
                        }
                    }
                }
             
        }
        // Range Type Enemy Movement
        else
        {
            // Calculate direction towards the player
            Vector3 directionToPlayer = playerTarget.position - transform.position;
            directionToPlayer.y = 0; // Keep the rotation on the Y-axis only

            // Rotate towards the player
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation

            // Adjust the enemy's rotation to keep 90 degrees on X-axis cuz this model is shit
            // Transform enemyRotation = transform;
            // enemyRotation.eulerAngles = new Vector3(90f, enemyRotation.eulerAngles.y, enemyRotation.eulerAngles.z);

            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            
            if (distanceToPlayer < 8)
            {
                Debug.Log("Enemy Attacking Area Reached!");
                enemyAgent.velocity = Vector3.zero;
                if (isAttacking == false)
                {
                    StartCoroutine(RangeAttack());
                    
                }
            }

            if (distanceToPlayer >= 8)
            {
                isPlayerWithinArea = false;
                if (isPlayerWithinArea == false && isAttacking == false)
                {
                    enemyAgent.SetDestination(playerTarget.position);
                    enemyAnimator.SetInteger("animState", 0);
                }
            }
        }

    }

    IEnumerator BlazeAttack()
    {
        isAttacking = true;
        enemyAgent.ResetPath();
        enemyAnimator.SetInteger("animState", 1);
        if (animatorTransition == false)
        {
            yield return new WaitForSeconds(1.25f);
            animatorTransition = true;
        }
        yield return new WaitForSeconds(0.10f);
        meleeAttack.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        meleeAttack.SetActive(false);
        yield return new WaitForSeconds(1.05f); // Adjust base on animation
        isAttacking = false;
    }
    IEnumerator VainedAttack()
    {
        isAttacking = true;
        enemyAgent.ResetPath();
        enemyAnimator.SetInteger("animState", 1);
        yield return new WaitForSeconds(1f);
        meleeAttack.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        meleeAttack.SetActive(false);
        yield return new WaitForSeconds(0.75f); // Adjust base on animation
        isAttacking = false;
    }

    IEnumerator RangeAttack()
    {
        isAttacking = true;
        enemyAgent.ResetPath();
        enemyAnimator.SetInteger("animState", 1);
        yield return new WaitForSeconds(0.15f); // Adjust base on animation
        enemyProjectileTypeScript.FireProjectile();
        yield return new WaitForSeconds(1.52f);
        isAttacking = false;
    }
}
