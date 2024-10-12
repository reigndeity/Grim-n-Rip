using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] EnemyStats enemyStatsScript;
    [SerializeField] PlayerStats playerStatsScript;
    
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
        enemyStatsScript.hitChance = enemyStatsScript.hitChance - playerStatsScript.dodgeRate;
        float hitChance = Random.Range(1f, 101f);

        if (hitChance <= enemyStatsScript.hitChance)
        {
            playerStatsScript.health -= damageAmount;
        }
    }


}
