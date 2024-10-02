using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValues : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] EnemyStats enemyStatsScript;

    [Header("Enemy Properties")]
    [SerializeField] int enemyType; // 0 - blaze | 1 - sinister seer | 2 - vained | 3 - tormented soul


    void Start()
    {
        enemyStatsScript = GetComponent<EnemyStats>();
        switch(enemyType)
        {
            case 0: // BLAZE Base Values
                enemyStatsScript.health = 80f;
                enemyStatsScript.damage = 20f;
                enemyStatsScript.movementSpeed = 4f;
                enemyStatsScript.hitChance = 100f;
            break;
            case 1: // SINISTER SEER Base Values
                enemyStatsScript.health = 60f;
                enemyStatsScript.damage = 15f;
                enemyStatsScript.movementSpeed = 3;
                enemyStatsScript.hitChance = 50f;
            break;
            case 2: // Vained Base Values
                enemyStatsScript.health = 120f;
                enemyStatsScript.damage = 30f;
                enemyStatsScript.movementSpeed = 2f;
                enemyStatsScript.hitChance = 40f;
            break;
            case 3: // Tormented Soul Base Values
                enemyStatsScript.health = 200;
                enemyStatsScript.damage = 25f;
                enemyStatsScript.movementSpeed = 2.5f;
                enemyStatsScript.hitChance = 45f;
            break;
        }        
    }
}
