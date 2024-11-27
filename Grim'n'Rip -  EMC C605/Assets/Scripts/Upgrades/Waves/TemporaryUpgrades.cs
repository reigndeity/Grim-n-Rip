using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryUpgrades : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] PlayerStats playerStatsScript;
    [SerializeField] WaveUpgrades waveUpgradesScript;

    [Header("Player Components")]
    [SerializeField] GameObject playerObj;

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
        float movementSpeedUpgrade = 1;
        float temporaryMovementSpeed = PlayerPrefs.GetFloat("temporaryMovementSpeedAmount");
        temporaryMovementSpeed += movementSpeedUpgrade * 0.1f;
        PlayerPrefs.SetFloat("temporaryMovementSpeedAmount", temporaryMovementSpeed);
        StartTheRound();
    }
    public void DodgeRateUpgradeWave()
    {
        float dodgeRateUpgrade = 2.75f;
        float temporaryDodgeRate = PlayerPrefs.GetFloat("temporaryDodgeRateAmount");
        temporaryDodgeRate += dodgeRateUpgrade;
        PlayerPrefs.SetFloat("temporaryDodgeRateAmount", temporaryDodgeRate);
        StartTheRound();
    }

    public void LuckUpgradeWave()
    {
        float luckUpgrade = 1.5f;
        float temporaryLuck = PlayerPrefs.GetFloat("temporaryLuckRateAmount");
        temporaryLuck += luckUpgrade;
        PlayerPrefs.SetFloat("temporaryLuckRateAmount", temporaryLuck);
        StartTheRound();
    }


    public void ProjectileDamageUpgradeWave()
    {
        
        float projectileDamageUpgrade = 15;
        float temporaryProjectileDamage = PlayerPrefs.GetFloat("temporaryProjectileDamageAmount");
        temporaryProjectileDamage += projectileDamageUpgrade;
        PlayerPrefs.SetFloat("temporaryProjectileDamageAmount", temporaryProjectileDamage);
        StartTheRound();
    }
    public void ProjectileSpeedUpgradeWave()
    {
        float projectileSpeedUpgrade = 3f;
        float temporaryProjectileSpeed = PlayerPrefs.GetFloat("temporaryProjectileSpeedAmount");
        temporaryProjectileSpeed += projectileSpeedUpgrade;
        PlayerPrefs.SetFloat("temporaryProjectileSpeedAmount", temporaryProjectileSpeed);
        StartTheRound();
    }
    public void WeaponFireRateUpgradeWave()
    {
        float weaponFireRateUpgrade = 0.1f;
        float temporaryWeaponFireRate = PlayerPrefs.GetFloat("temporaryWeaponFireRateAmount");
        temporaryWeaponFireRate -= weaponFireRateUpgrade;
        PlayerPrefs.SetFloat("temporaryWeaponFireRateAmount", temporaryWeaponFireRate);
        StartTheRound();
    }

    void StartTheRound()
    {
        GameManager.instance.waveUpgradeObj.SetActive(false);
        GameManager.instance.StartRound();
        waveUpgradesScript.DestroySpawnedUpgrades();

        GameManager.instance.playerObj.transform.position = new Vector3(0,-0.001472749f,0); // Go back to the center of the map
    }
}
