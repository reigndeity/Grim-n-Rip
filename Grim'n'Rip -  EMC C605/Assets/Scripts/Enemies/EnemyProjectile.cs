using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] EnemyStats enemyStatsScript;
    [SerializeField] PlayerStats playerStatsScript;
    [SerializeField] Rigidbody rb;
    public Transform playerTarget;
    public float speed = 10f;
    private Vector3 initialDirection;
    [Header("Enemy Properties")]
    [SerializeField] float enemyHitChance;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("DestroyProjectile", 8f);
        playerTarget = GameObject.FindWithTag("Player").transform;
        enemyStatsScript = GetComponentInParent<EnemyStats>();
        playerStatsScript = FindObjectOfType<PlayerStats>();

        // Calculate the direction towards the player ONCE when the projectile is instantiated
        initialDirection = (playerTarget.position - transform.position).normalized;
    }

    void Start()
    {
        // Set the velocity towards the player based on the initial direction
        rb.velocity = initialDirection * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Attack();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("ProjectileDestroyer"))
        {
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        CalculateHitChance(enemyStatsScript.damage);
    }

    public void CalculateHitChance(float damageAmount)
    {
        float playerDodgeRate = playerStatsScript.dodgeRate;
        enemyHitChance = enemyStatsScript.hitChance - playerDodgeRate;
        float hitChance = Random.Range(1f, 101f);
        if (hitChance <= enemyHitChance)
        {
            playerStatsScript.health -= damageAmount;
            playerStatsScript.health = Mathf.Ceil(playerStatsScript.health);
        }
        else
        {
            GameManager.instance.SpawnFloatingText();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
