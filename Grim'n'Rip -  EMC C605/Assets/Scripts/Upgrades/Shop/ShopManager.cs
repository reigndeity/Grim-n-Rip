using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("First-time Boot-Up")]
    [SerializeField] int isFirstBoot; // 0 is false, 1 is true
    [Header("Shop Properties")]
    [SerializeField] TextMeshProUGUI totalCoinsTxt;
    [SerializeField] int totalCoins;

    void Awake()
    {

    }

    void Start()
    {
        isFirstBoot = PlayerPrefs.GetInt("isFirstBoot", 0);
        if (isFirstBoot == 0)
        {
            PlayerPrefs.SetFloat("healthAmount", 100f); 
            PlayerPrefs.SetFloat("movementSpeedAmount", 5f);
            PlayerPrefs.SetFloat("dodgeRateAmount", 5f);
            PlayerPrefs.SetFloat("luckRateAmount", 5); //5f orig
            PlayerPrefs.SetFloat("projectileDamageAmount", 30f); 
            PlayerPrefs.SetFloat("projectileSpeedAmount", 10f);
            PlayerPrefs.SetFloat("weaponFireRateAmount", 1.5f); //1f orig

            // Only set totalCoins to 0 if it hasn't been initialized yet
            if (!PlayerPrefs.HasKey("totalCoins"))
            {
                PlayerPrefs.SetInt("totalCoins", 0);
            }

            PlayerPrefs.SetInt("isFirstBoot", 1); // Mark as initialized
            PlayerPrefs.Save();
            }
    }

    void Update()
    {
        totalCoinsTxt.text = PlayerPrefs.GetInt("totalCoins").ToString();
    }

    public void HealthUpgradeWave()
    {
        float healthUpgrade = 30;
        float permanentHealthUpgrade = PlayerPrefs.GetFloat("healthAmount");
        permanentHealthUpgrade += healthUpgrade * 0.1f;
        PlayerPrefs.SetFloat("healthAmount", permanentHealthUpgrade);
    }

    public void MovementSpeedU()
    {
        float movementSpeedUpgrade = 1;
        float permanentMovementSpeed = PlayerPrefs.GetFloat("movementSpeedAmount");
        permanentMovementSpeed += movementSpeedUpgrade * 0.1f;
        PlayerPrefs.SetFloat("movementSpeedAmount", permanentMovementSpeed);
    }
    public void DodgeRateUpgrade()
    {
        float dodgeRateUpgrade = 2.75f;
        float permanentDodgeRate = PlayerPrefs.GetFloat("dodgeRateAmount");
        permanentDodgeRate += dodgeRateUpgrade;
        PlayerPrefs.SetFloat("dodgeRateAmount", permanentDodgeRate);
    }

    public void LuckUpgrade()
    {
        float luckUpgrade = 1.5f;
        float permanentLuck = PlayerPrefs.GetFloat("luckRateAmount");
        permanentLuck += luckUpgrade;
        PlayerPrefs.SetFloat("luckRateAmount", permanentLuck);
    }


    public void ProjectileDamageUpgrade()
    {
        float projectileDamageUpgrade = 15;
        float permanentProjectileDamage = PlayerPrefs.GetFloat("projectileDamageAmount");
        permanentProjectileDamage += projectileDamageUpgrade;
        PlayerPrefs.SetFloat("projectileDamageAmount", permanentProjectileDamage);
    }
    public void ProjectileSpeedUpgradeW()
    {
        float projectileSpeedUpgrade = 3f;
        float permanentProjectileSpeed = PlayerPrefs.GetFloat("projectileSpeedAmount");
        permanentProjectileSpeed += projectileSpeedUpgrade;
        PlayerPrefs.SetFloat("projectileSpeedAmount", permanentProjectileSpeed);
    }
    public void WeaponFireRateUpgrade()
    {
        float weaponFireRateUpgrade = 0.1f;
        float permanentWeaponFireRate = PlayerPrefs.GetFloat("weaponFireRateAmount");
        permanentWeaponFireRate -= weaponFireRateUpgrade;
        PlayerPrefs.SetFloat("weaponFireRateAmount", permanentWeaponFireRate);
    }


}
