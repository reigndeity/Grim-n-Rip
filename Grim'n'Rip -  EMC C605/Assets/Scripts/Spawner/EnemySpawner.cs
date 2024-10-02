using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] enemyPoints;
    public GameObject[] enemyPrefabs;
    [SerializeField] bool isEnemySpawning;
    [SerializeField] float enemySpawnRate;

    public float enemiesRemaining;



    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if (isEnemySpawning == false && enemiesRemaining > 0)
        {
            int randomPoint = Random.Range(0, 4);
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], enemyPoints[randomPoint]);
            isEnemySpawning = true;
            enemiesRemaining--;
            Invoke("ResetSpawnRate", enemySpawnRate);
        }

    }
    void ResetSpawnRate()
    {
        isEnemySpawning = false;
    }
}
