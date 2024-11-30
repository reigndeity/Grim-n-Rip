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

    // Spawn chances for Phase 1
    [Header("Phase 1 Enemy Spawn Chances")]
    [SerializeField] float commonSpawnChance = 100f;
    [SerializeField] float uncommonSpawnChance = 5f;
    [SerializeField] float rareSpawnChance = 0f;
    [SerializeField] float epicSpawnChance = 0f;

    // Spawn chances for Phase 2
    [Header("Phase 2 Enemy Spawn Chances")]
    [SerializeField] float common2SpawnChance = 0f;
    [SerializeField] float uncommon2SpawnChance = 0f;
    [SerializeField] float rare2SpawnChance = 0f;
    [SerializeField] float epic2SpawnChance = 0f;

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
        float waveFactor = waveManagerScript.currentWave;

        // Update Phase 1 spawn chances (Phase 1 becomes inactive at wave 150)
        if (waveFactor >= 150)
        {
            commonSpawnChance = 0f;
            uncommonSpawnChance = 0f;
            rareSpawnChance = 0f;
            epicSpawnChance = 0f;
        }
        else
        {
            commonSpawnChance = Mathf.Max(0f, 100f - (0.5f * waveFactor));
            uncommonSpawnChance = Mathf.Max(0f, 5f + (0.7f * waveFactor));
            rareSpawnChance = Mathf.Max(0f, 0.2f * waveFactor);
            epicSpawnChance = Mathf.Max(0f, 0.1f * waveFactor);
        }

        // Update Phase 2 spawn chances (Phase 2 starts at wave 50)
        common2SpawnChance = Mathf.Min(50f, Mathf.Max(0, (waveFactor - 50) * 0.4f));
        uncommon2SpawnChance = Mathf.Min(50f, Mathf.Max(0, (waveFactor - 75) * 0.5f));
        rare2SpawnChance = Mathf.Min(50f, Mathf.Max(0, (waveFactor - 100) * 0.6f));
        epic2SpawnChance = Mathf.Min(50f, Mathf.Max(0, (waveFactor - 125) * 0.7f));
    }

    private GameObject GetRandomEnemy()
    {
        // Combine spawn chances for Phase 1 and Phase 2
        float totalChance = commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance +
                            common2SpawnChance + uncommon2SpawnChance + rare2SpawnChance + epic2SpawnChance;

        float randomValue = Random.Range(0, totalChance);

        // Phase 1 enemies
        if (randomValue < commonSpawnChance)
        {
            return enemyPrefabs[0]; // Common Phase 1
        }
        else if (randomValue < commonSpawnChance + uncommonSpawnChance)
        {
            return enemyPrefabs[1]; // Uncommon Phase 1
        }
        else if (randomValue < commonSpawnChance + uncommonSpawnChance + rareSpawnChance)
        {
            return enemyPrefabs[2]; // Rare Phase 1
        }
        else if (randomValue < commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance)
        {
            return enemyPrefabs[3]; // Epic Phase 1
        }

        // Phase 2 enemies
        else if (randomValue < commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance + common2SpawnChance)
        {
            return enemyPrefabs[4]; // Common Phase 2
        }
        else if (randomValue < commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance + common2SpawnChance + uncommon2SpawnChance)
        {
            return enemyPrefabs[5]; // Uncommon Phase 2
        }
        else if (randomValue < commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance + common2SpawnChance + uncommon2SpawnChance + rare2SpawnChance)
        {
            return enemyPrefabs[6]; // Rare Phase 2
        }
        else
        {
            return enemyPrefabs[7]; // Epic Phase 2
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
            GameObject enemyToSpawn = GetRandomEnemy(); // Get a random enemy based on chances
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
