using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] EnemyStats enemyStatsScript;
    [SerializeField] PlayerStats playerStatsScript;

    [Header("Enemy Properties")]
    [SerializeField] float enemyHitChance;
    
    void Awake()
    {
        enemyStatsScript = GetComponentInParent<EnemyStats>();
        playerStatsScript = FindObjectOfType<PlayerStats>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Attack();
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
    }


}
