using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawner Properties")]
    public Transform[] enemyPoints;
    public GameObject[] enemyPrefabs;
    [SerializeField] bool isEnemySpawning;
    [SerializeField] float enemySpawnRate;
    public float enemiesToSpawn;
    public int maxEnemiesOnScreen = 50;  // Max number of enemies allowed on the screen at once
    [SerializeField] float baseEnemySpawn = 5f;  // Starting number of enemies for the first wave

    // Base spawn chances
    [Header("Enemy Type Spawn Chances")]
    [SerializeField] float commonSpawnChance = 100f; // Base common enemy spawn chance
    [SerializeField] float uncommonSpawnChance = 5f;  // Base uncommon enemy spawn chance
    [SerializeField] float rareSpawnChance = 0f;       // Base rare enemy spawn chance
    [SerializeField] float epicSpawnChance = 0f;       // Base epic enemy spawn chance

    [Header("Script References")]
    public WaveManager waveManagerScript;

    void Start()
    {
        waveManagerScript = FindObjectOfType<WaveManager>();
        UpdateSpawnChances(); // Initial spawn chance update based on the starting wave
    }

    void Update()
    {
        if (waveManagerScript.allowSpawn == true)
        {
            Spawn();
        }
    }

    private void UpdateSpawnChances()
    {
        // Update spawn chances based on the current wave
        float waveFactor = waveManagerScript.currentWave; // Access current wave from WaveManager

        // Example calculations for spawn chances based on the current wave
        commonSpawnChance = 100f - (0.5f * waveFactor); // Decrease common spawn chance
        uncommonSpawnChance = 5f + (0.7f * waveFactor); // Increase uncommon spawn chance
        rareSpawnChance = 0f + (0.2f * waveFactor);     // Increase rare spawn chance
        epicSpawnChance = 0f + (0.1f * waveFactor);     // Increase epic spawn chance
        
        // Ensure that spawn chances remain within reasonable bounds
        commonSpawnChance = Mathf.Max(0f, commonSpawnChance);
        uncommonSpawnChance = Mathf.Min(50f, uncommonSpawnChance);
        rareSpawnChance = Mathf.Min(50f, rareSpawnChance);
        epicSpawnChance = Mathf.Min(50f, epicSpawnChance);
    }

    private GameObject GetRandomEnemy()
    {
        float totalChance = commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance;
        float randomValue = Random.Range(0, totalChance);
        
        if (randomValue < commonSpawnChance)
        {
            return enemyPrefabs[0]; // Assuming index 0 is common enemy
        }
        else if (randomValue < commonSpawnChance + uncommonSpawnChance)
        {
            return enemyPrefabs[1]; // Assuming index 1 is uncommon enemy
        }
        else if (randomValue < commonSpawnChance + uncommonSpawnChance + rareSpawnChance)
        {
            return enemyPrefabs[2]; // Assuming index 2 is rare enemy
        }
        else
        {
            return enemyPrefabs[3]; // Assuming index 3 is epic enemy
        }
    }

    public void Spawn()
    {
        // Update spawn chances each time a new wave starts
        UpdateSpawnChances();

        // Check the current number of enemies in the scene
        int currentEnemyCount = FindObjectsOfType<EnemyStats>().Length;

        // Only spawn if the limit has not been reached and there are still enemies to spawn
        if (!isEnemySpawning && enemiesToSpawn > 0 && currentEnemyCount < maxEnemiesOnScreen)
        {
            int randomPoint = Random.Range(0, enemyPoints.Length);
            GameObject enemyToSpawn = GetRandomEnemy(); // Use the new method to get a random enemy based on rarity
            Instantiate(enemyToSpawn, enemyPoints[randomPoint]);
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
