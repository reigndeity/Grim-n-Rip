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

    // Check if upgrades should be disabled based on PlayerPrefs values
    if (PlayerPrefs.GetInt("movementSpeedUpgradeCount") >= 9)
    {
        // Disable movement speed upgrade (waveUpgrades[1])
        availableUpgrades[1] = null;
    }
    if (PlayerPrefs.GetInt("dodgeRateUpgradeCount") >= 30)
    {
        // Disable dodge rate upgrade (waveUpgrades[3])
        availableUpgrades[3] = null;
    }
    if (PlayerPrefs.GetInt("projectileSpeedUpgradeCount") >= 4)
    {
        // Disable projectile speed upgrade (waveUpgrades[4])
        availableUpgrades[4] = null;
    }
    if (PlayerPrefs.GetInt("fireRateUpgradeCount") >= 13)
    {
        // Disable fire rate upgrade (waveUpgrades[5])
        availableUpgrades[5] = null;
    }

    // Filter out the null entries (disabled upgrades) to get the actual list of available upgrades
    availableUpgrades.RemoveAll(upgrade => upgrade == null);

    // Ensure we spawn the remaining valid upgrades, whether it's 3 or 2
    int upgradesToSpawn = Mathf.Min(3, availableUpgrades.Count);

    // Spawn the determined number of upgrades
    for (int i = 0; i < upgradesToSpawn; i++) 
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
