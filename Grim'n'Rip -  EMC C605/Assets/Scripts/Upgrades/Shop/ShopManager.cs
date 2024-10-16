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

    [Header("Shop UI Properties")]
    // Player Stat Values
    [SerializeField] TextMeshProUGUI healthValueTxt;
    [SerializeField] TextMeshProUGUI movementSpeedValueTxt;
    [SerializeField] TextMeshProUGUI dodgeRateValueTxt;
    [SerializeField] TextMeshProUGUI luckRateValueTxt;
    [SerializeField] TextMeshProUGUI projectileDamageValueTxt;
    [SerializeField] TextMeshProUGUI projectileSpeedValueTxt;
    [SerializeField] TextMeshProUGUI weaponFireRateValueTxt;
    // Player Cost
    [SerializeField] TextMeshProUGUI healthCostTxt;
    [SerializeField] TextMeshProUGUI movementSpeedCostTxt;


    void Start()
    {
        isFirstBoot = PlayerPrefs.GetInt("isFirstBoot", 0);
        if (isFirstBoot == 0)
        {
            // Player Base
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

            // Upgrade Cost
            PlayerPrefs.SetInt("healthUpgradeCost", 20);
            PlayerPrefs.SetInt("movementSpeedCost", 15);

            }
    }


    void Update()
    {
        totalCoinsTxt.text = Mathf.Ceil(PlayerPrefs.GetInt("totalCoins")).ToString();

        healthValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("healthAmount")).ToString();
        movementSpeedValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("movementSpeedAmount")).ToString();
        dodgeRateValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("dodgeRateAmount")).ToString();
        luckRateValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("luckRateAmount")).ToString();
        projectileDamageValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("projectileDamageAmount")).ToString();
        projectileSpeedValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("projectileSpeedAmount")).ToString();
        weaponFireRateValueTxt.text = PlayerPrefs.GetFloat("weaponFireRateAmount").ToString();
        

        healthCostTxt.text = PlayerPrefs.GetInt("healthUpgradeCost").ToString();
    }

    public void HealthUpgrade()
    {
        // Retrieve the current upgrade cost from PlayerPrefs, default to 50 if not set
        int upgradeCost = PlayerPrefs.GetInt("healthUpgradeCost");
        int currentCoins = PlayerPrefs.GetInt("totalCoins");

        // Check if the player has enough coins
        if (currentCoins >= upgradeCost)
        {
            // Deduct the cost
            currentCoins -= upgradeCost;
            PlayerPrefs.SetInt("totalCoins", currentCoins);

            // Apply the health upgrade
            float healthUpgrade = 30;
            float permanentHealthUpgrade = PlayerPrefs.GetFloat("healthAmount");
            permanentHealthUpgrade += healthUpgrade;
            PlayerPrefs.SetFloat("healthAmount", permanentHealthUpgrade);

            // Increase the upgrade cost for next time and save it
            int randomNumber = Random.Range(20, 30);
            upgradeCost += randomNumber; // Increment the cost (adjust as needed)
            PlayerPrefs.SetInt("healthUpgradeCost", upgradeCost);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade health!");
        }
    }

    public void MovementSpeedUpgrade()
    {
        float movementSpeedUpgrade = 1;
        float permanentMovementSpeed = PlayerPrefs.GetFloat("movementSpeedAmount");
        permanentMovementSpeed += movementSpeedUpgrade;
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
    public void ProjectileSpeedUpgrade()
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
