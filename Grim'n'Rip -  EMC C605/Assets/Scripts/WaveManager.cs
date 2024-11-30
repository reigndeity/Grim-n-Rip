using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float currentWave = 1;   // Starting at wave 1
    public float baseEnemyCount;
    public float newWaveCount;
    public float enemyCount;
    public bool allowSpawn;
    [Header("Script References")]
    public EnemySpawner enemySpawnerScript;

    void Start()
    {
        enemySpawnerScript = FindObjectOfType<EnemySpawner>();
        CalculateEnemyCount();
        
        int debugWave = PlayerPrefs.GetInt("debugWave");
        currentWave = debugWave;
    }

    void Update()
    {
        if (GameManager.instance.canStartWave)
        {
            StartNewWave();  // Explicitly call to start a new wave
            GameManager.instance.canStartWave = false;
        }
    }

    // Function to start a new wave
    public void StartNewWave()
    {
        currentWave++;
        CalculateEnemyCount();
        enemySpawnerScript.enemiesToSpawn = enemyCount;
        allowSpawn = true;
        // Debugging logs to check if values update correctly
        Debug.Log("New wave started: " + currentWave);
        Debug.Log("Enemies to spawn: " + enemyCount);
        GameManager.instance.isEnemiesRemainingFill = false; // Enemies Remaining Fill Update
    }

    // Function to calculate the enemy count
    private void CalculateEnemyCount()
    {
        baseEnemyCount = 3 + Mathf.Ceil(currentWave * 1.30f);
        newWaveCount = Mathf.Ceil(baseEnemyCount * 1.30f);
        enemyCount = newWaveCount;
    }
}
