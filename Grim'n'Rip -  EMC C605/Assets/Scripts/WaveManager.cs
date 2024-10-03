using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float currentWave;
    public float baseEnemyCount = 5;  // The starting number of enemies for the first wave
    public float enemyCount;          // The number of enemies for the current wave
    public bool allowSpawn;

    [Header("Script References")]
    public EnemySpawner enemySpawnerScript;

    void Start()
    {
        enemySpawnerScript = FindObjectOfType<EnemySpawner>();
        
    }

    void Update()
    {
        if (GameManager.instance.canStartWave == true)
        {
            allowSpawn = true;
            currentWave++;  // Increment the wave count
            enemyCount = Mathf.Ceil(baseEnemyCount + Mathf.Pow(1.07f, currentWave - 1));
            enemySpawnerScript.enemiesToSpawn = enemyCount;
            GameManager.instance.canStartWave = false; 
        }
    }

//     void StartWave()
//     {
//         beginWave = true;
//         currentWave++;  // Increment the wave count
//         enemyCount = Mathf.Ceil(baseEnemyCount + Mathf.Pow(1.3f, currentWave));  // Increase by 30% each wave
//         enemySpawnerScript.enemiesToSpawn = enemyCount;
//         GameManager.instance.canStartWave = false;
//     }
// 
}
