using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveUpgrades : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] PlayerStats playerStatsScript;

    [Header("Wave Upgrade Properties")]
    public Transform parentPos;
    public GameObject[] waveUpgrades;

    // List to store spawned objects
    private List<GameObject> spawnedUpgrades = new List<GameObject>();

    void Awake()
    {
        playerStatsScript = GetComponent<PlayerStats>();
    }

    public void SpawnWaveUpgrades()
    {
        // Create a temporary list of upgrades to avoid duplicates
        List<GameObject> availableUpgrades = new List<GameObject>(waveUpgrades);

        // Spawn upgrades 3 times, ensuring no duplicates
        for (int i = 0; i < 3; i++) 
        {
            if (availableUpgrades.Count > 0)
            {
                // Pick a random index from the available upgrades list
                int randomIndex = Random.Range(0, availableUpgrades.Count);

                // Instantiate the selected upgrade
                GameObject spawnedUpgrade = Instantiate(availableUpgrades[randomIndex], parentPos);

                // Add the spawned object to the list
                spawnedUpgrades.Add(spawnedUpgrade);

                // Remove the selected upgrade from the available list
                availableUpgrades.RemoveAt(randomIndex);
            }
        }
    }

    // Function to destroy all spawned upgrades
    public void DestroySpawnedUpgrades()
    {
        // Loop through the list and destroy each upgrade
        foreach (GameObject upgrade in spawnedUpgrades)
        {
            Destroy(upgrade);
        }
        
        // Clear the list after destroying the objects
        spawnedUpgrades.Clear();
    }
}
