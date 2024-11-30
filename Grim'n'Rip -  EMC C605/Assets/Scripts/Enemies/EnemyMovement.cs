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
    public AudioManager _audioManager;

    [Header("Enemy Components")]
    [SerializeField] NavMeshAgent enemyAgent;
    public Transform playerTarget;

    [Header("EnemyType")]
    [SerializeField] int enemyType;

    [Header("Melee Type Enemy")]
    [SerializeField] bool isAttacking;
    [SerializeField] bool isPlayerWithinArea;
    [SerializeField] bool animatorTransition;

    [Header("Projectile Type Enemy")]
    [SerializeField] bool isProjectileType;

    

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindWithTag("Player").transform; // finds the player tag within hierarchy
        enemyStatsScript = GetComponent<EnemyStats>();
        enemyValuesScript = GetComponent<EnemyValues>();
        enemyAnimator = GetComponent<Animator>();
        enemyProjectileTypeScript = GetComponent<EnemyProjectileType>();
        _audioManager = FindObjectOfType<AudioManager>();
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
            // Calculate direction towards the player
            Vector3 directionToPlayer = playerTarget.position - transform.position;
            directionToPlayer.y = 0; // Keep the rotation on the Y-axis only

            // Rotate towards the player
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation
                if (enemyType == 0)
                {
                    if (distanceToPlayer < 2f)
                    {
                        enemyAgent.velocity = Vector3.zero;
                        if (isAttacking == false)
                        {
                            StartCoroutine(BlazeAttack());
                        }
                    }
                    if (distanceToPlayer >= 2f)
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
                    if (distanceToPlayer < 2.5f)
                    {
                        enemyAgent.velocity = Vector3.zero;
                        if (isAttacking == false)
                        {
                            StartCoroutine(VainedAttack());
                        }
                    }
                    if (distanceToPlayer >= 2.5f)
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

                if (enemyType == 3)
                {
                    if (distanceToPlayer < 3.0f)
                    {
                        enemyAgent.velocity = Vector3.zero;
                        if (isAttacking == false)
                        {
                            StartCoroutine(TormentorAttack());
                        }
                    }
                    if (distanceToPlayer >= 3.0f)
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
        yield return new WaitForSeconds(1.250f); // Adjust base on animation
        isAttacking = false;
    }
    IEnumerator VainedAttack()
    {
        isAttacking = true;
        enemyAgent.ResetPath();
        enemyAnimator.SetInteger("animState", 1);
        yield return new WaitForSeconds(1.875f); // Adjust base on animation
        isAttacking = false;
    }

    IEnumerator TormentorAttack()
    {
        isAttacking = true;
        enemyAgent.ResetPath();
        enemyAnimator.SetInteger("animState", 1);
        yield return new WaitForSeconds(1.583f);
        isAttacking = false;
    }

    IEnumerator RangeAttack()
    {
        isAttacking = true;
        enemyAgent.ResetPath();
        enemyAnimator.SetInteger("animState", 1);
        yield return new WaitForSeconds(1.667f); // Adjust base on animation
        isAttacking = false;
    }

    // Event System
    public void BlazeAttackSound()
    {
        _audioManager.PlayBlazeAttackSound();
    }
    public void SinisterSeerAttackSound()
    {
        _audioManager.PlaySinisterSeerAttackSound();
    }
    public void VainedAttackSound()
    {
        _audioManager.PlayVainedAttackSound();
    }
    public void TormentorAttackSound()
    {
        _audioManager.PlayTormentorAttackSound();
    }
}
