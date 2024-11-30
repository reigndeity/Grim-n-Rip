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

    // Phase 2 spawn chances (declare at class level)
    public float commonPhase2Chance;
    public float uncommonPhase2Chance;
    public float rarePhase2Chance;
    public float epicPhase2Chance;

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

        // Phase 1 spawn chances (decrease over time)
        float phaseOutWave = 150f; // The wave at which phase 1 enemies no longer spawn

        if (waveFactor >= phaseOutWave)
        {
            commonSpawnChance = 0f;
            uncommonSpawnChance = 0f;
            rareSpawnChance = 0f;
            epicSpawnChance = 0f;
        }
        else
        {
            commonSpawnChance = 100f - (0.5f * waveFactor);
            uncommonSpawnChance = 5f + (0.7f * waveFactor);
            rareSpawnChance = 0f + (0.2f * waveFactor);
            epicSpawnChance = 0f + (0.1f * waveFactor);
        }

        // Phase 2 spawn chances (start at specific wave thresholds)
        float phase2Factor = Mathf.Max(0, waveFactor - 50);
        float phase2UncommonFactor = Mathf.Max(0, waveFactor - 65);
        float phase2RareFactor = Mathf.Max(0, waveFactor - 80);
        float phase2EpicFactor = Mathf.Max(0, waveFactor - 105);

        commonPhase2Chance = Mathf.Min(50f, phase2Factor * 0.4f);
        uncommonPhase2Chance = Mathf.Min(50f, phase2UncommonFactor * 0.5f);
        rarePhase2Chance = Mathf.Min(50f, phase2RareFactor * 0.6f);
        epicPhase2Chance = Mathf.Min(50f, phase2EpicFactor * 0.7f);

        // Combine the chances for phase 1 and phase 2
        commonSpawnChance += commonPhase2Chance;
        uncommonSpawnChance += uncommonPhase2Chance;
        rareSpawnChance += rarePhase2Chance;
        epicSpawnChance += epicPhase2Chance;

        // Ensure spawn chances are within bounds
        commonSpawnChance = Mathf.Max(0f, commonSpawnChance);
        uncommonSpawnChance = Mathf.Min(50f, uncommonSpawnChance);
        rareSpawnChance = Mathf.Min(50f, rareSpawnChance);
        epicSpawnChance = Mathf.Min(50f, epicSpawnChance);
    }

    private GameObject GetRandomEnemy()
    {
        float totalChance = commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance;

        // Add phase 2 chances to total
        totalChance += (commonPhase2Chance + uncommonPhase2Chance + rarePhase2Chance + epicPhase2Chance);

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
        else if (randomValue < commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance + commonPhase2Chance)
        {
            return enemyPrefabs[4]; // Common Phase 2
        }
        else if (randomValue < commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance + commonPhase2Chance + uncommonPhase2Chance)
        {
            return enemyPrefabs[5]; // Uncommon Phase 2
        }
        else if (randomValue < commonSpawnChance + uncommonSpawnChance + rareSpawnChance + epicSpawnChance + commonPhase2Chance + uncommonPhase2Chance + rarePhase2Chance)
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
