using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] enemyPoints;
    public GameObject[] enemyPrefabs;
    [SerializeField] bool isEnemySpawning;
    [SerializeField] float enemySpawnRate;

    public float enemiesToSpawn;
    public int maxEnemiesOnScreen = 50;  // Max number of enemies allowed on the screen at once

    [Header("Script References")]
    public WaveManager waveManagerScript;

    void Start()
    {
        waveManagerScript = FindObjectOfType<WaveManager>();
    }

    void Update()
    {
        if (waveManagerScript.allowSpawn == true)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        // Check the current number of enemies in the scene
        int currentEnemyCount = FindObjectsOfType<EnemyStats>().Length;  // Assuming Enemy is your enemy class

        // Only spawn if the limit has not been reached and there are still enemies to spawn
        if (isEnemySpawning == false && enemiesToSpawn > 0 && currentEnemyCount < maxEnemiesOnScreen)
        {
            int randomPoint = Random.Range(0, enemyPoints.Length);
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], enemyPoints[randomPoint]);
            isEnemySpawning = true;
            enemiesToSpawn--;  // Decrement the number of enemies to spawn
            Invoke("ResetSpawnRate", enemySpawnRate);

            if (enemiesToSpawn == 0)
            {
                waveManagerScript.allowSpawn = false;  
            }
        }
    }

    void ResetSpawnRate()
    {
        isEnemySpawning = false;
    }
}