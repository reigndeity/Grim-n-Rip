using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryUpgrades : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] PlayerStats playerStatsScript;
    [SerializeField] WaveUpgrades waveUpgradesScript;

    void Awake()
    {
        playerStatsScript = FindObjectOfType<PlayerStats>();
        waveUpgradesScript = FindObjectOfType<WaveUpgrades>();
    }

    // Upgrades Health
    public void HealthUpgradeWave()
    {
        float maxHealth = GameManager.instance.healthValue;
        GameManager.instance.healthValue += Mathf.Ceil(maxHealth * 0.1f);
        playerStatsScript.health = GameManager.instance.healthValue;
        if (playerStatsScript.health >= GameManager.instance.healthValue)
        {
            GameManager.instance.isHealthRemainingFill = false;
        }
        StartTheRound();
    }

    public void MovementSpeedUpgrade()
    {
        float baseMovementSpeed = PlayerPrefs.GetFloat("movementSpeedAmount");
        float temporaryMovementSpeed = PlayerPrefs.GetFloat("temporaryMovementSpeedAmount");
        temporaryMovementSpeed += baseMovementSpeed * 0.1f;
        PlayerPrefs.SetFloat("temporaryMovementSpeedAmount", temporaryMovementSpeed);
        StartTheRound();
    }

    public void DamageUpgradeWave()
    {
        float baseDamage = PlayerPrefs.GetFloat("projectileDamageAmount");
        float damageUpgrade = baseDamage * 0.1f;
        float temporaryDamage = PlayerPrefs.GetFloat("temporaryProjectileDamageAmount");
        temporaryDamage += damageUpgrade;
        PlayerPrefs.SetFloat("temporaryProjectileDamageAmount", temporaryDamage);
        StartTheRound();
    }




    void StartTheRound()
    {
        GameManager.instance.waveUpgradeObj.SetActive(false);
        GameManager.instance.StartRound();
        waveUpgradesScript.DestroySpawnedUpgrades();
    }
}
