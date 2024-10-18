using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValues : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] EnemyStats enemyStatsScript;
    [SerializeField] WaveManager waveManagerScript;
    [SerializeField] PlayerStats playerStatsScript;
    
    [Header("Enemy Properties")]
    [SerializeField] int enemyType; // 0 - blaze | 1 - sinister seer | 2 - vained | 3 - tormented soul
    [SerializeField] bool isDoneUpgrade;
    
   

    void Awake()
    {
        enemyStatsScript = GetComponent<EnemyStats>();
        waveManagerScript = FindObjectOfType<WaveManager>();
        playerStatsScript = FindObjectOfType<PlayerStats>();
    }
    void Start()
    {
        switch(enemyType)
        {
            case 0: // BLAZE Base Values
                enemyStatsScript.health = 80f;
                enemyStatsScript.damage = 10f;
                enemyStatsScript.movementSpeed = 4f;
                enemyStatsScript.hitChance = 100f;
                enemyStatsScript.enemyScore = 50f;               
            break;
            case 1: // SINISTER SEER Base Values
                enemyStatsScript.health = 60f;
                enemyStatsScript.damage = 15f;
                enemyStatsScript.movementSpeed = 3;
                enemyStatsScript.hitChance = 60f;
                enemyStatsScript.enemyScore = 75f;                
            break;
            case 2: // VAINED Base Values
                enemyStatsScript.health = 120f;
                enemyStatsScript.damage = 30f;
                enemyStatsScript.movementSpeed = 2f;
                enemyStatsScript.hitChance = 60f;
                enemyStatsScript.enemyScore = 100f;
            break;
            case 3: // TORMENTED SOUL Base Values
                enemyStatsScript.health = 200;
                enemyStatsScript.damage = 25f;
                enemyStatsScript.movementSpeed = 2.5f;
                enemyStatsScript.hitChance = 60f;
                enemyStatsScript.enemyScore = 200f;
            break;
        }        
    }

    // Receiving Damage
    public void TakeDamage(float damageAmount)
    {
        enemyStatsScript.health -= damageAmount;
        if (enemyStatsScript.health <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.scoreValue += enemyStatsScript.enemyScore;
            waveManagerScript.enemyCount--;
        }
    }

    void Update()
    {
        // Enemy Difficulty Progression
        if (isDoneUpgrade == false)
        {
            enemyStatsScript.health = enemyStatsScript.health + (enemyStatsScript.health * 0.1f * waveManagerScript.currentWave);
            enemyStatsScript.damage = enemyStatsScript.damage + (enemyStatsScript.damage * 0.06f * waveManagerScript.currentWave);
            enemyStatsScript.movementSpeed = enemyStatsScript.movementSpeed + (enemyStatsScript.movementSpeed * 0.05f * waveManagerScript.currentWave);
            enemyStatsScript.hitChance = enemyStatsScript.hitChance + (enemyStatsScript.hitChance * 0.02f * waveManagerScript.currentWave);
            isDoneUpgrade = true;
        }

        // Capping The Enemy Stats
        if (enemyStatsScript.hitChance > 100f)
        {
            enemyStatsScript.hitChance = 100f;
        }
        if (enemyStatsScript.movementSpeed > 5f)
        {
            enemyStatsScript.movementSpeed = 5;
        }
    }

}
