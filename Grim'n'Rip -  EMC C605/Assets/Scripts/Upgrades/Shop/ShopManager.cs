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
    [Header("Upgrade Cost Properties")]
    [SerializeField] TextMeshProUGUI healthCostTxt;
    [SerializeField] TextMeshProUGUI movementSpeedCostTxt;
    [SerializeField] TextMeshProUGUI dodgeRateCostTxt;
    [SerializeField] TextMeshProUGUI luckRateCostTxt;
    [SerializeField] TextMeshProUGUI projectileDamageCostTxt;
    [SerializeField] TextMeshProUGUI projectileSpeedCostTxt;
    [SerializeField] TextMeshProUGUI weaponFireRateCostTxt;


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
            PlayerPrefs.SetInt("healthUpgradeCost", 40);
            PlayerPrefs.SetInt("movementSpeedUpgradeCost", 35);
            PlayerPrefs.SetInt("dodgeRateUpgradeCost", 30);
            PlayerPrefs.SetInt("luckRateUpgradeCost", 30);
            PlayerPrefs.SetInt("projectileDamageUpgradeCost", 25);
            PlayerPrefs.SetInt("projectileSpeedUpgradeCost", 20);
            PlayerPrefs.SetInt("weaponFireRateUpgradeCost", 30);
        }
    }


    void Update()
    {
        totalCoinsTxt.text = "Coins: " + Mathf.Ceil(PlayerPrefs.GetInt("totalCoins")).ToString();

        healthValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("healthAmount")).ToString();
        movementSpeedValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("movementSpeedAmount")).ToString();
        dodgeRateValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("dodgeRateAmount")).ToString();
        luckRateValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("luckRateAmount")).ToString();
        projectileDamageValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("projectileDamageAmount")).ToString();
        projectileSpeedValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("projectileSpeedAmount")).ToString();
        weaponFireRateValueTxt.text = PlayerPrefs.GetFloat("weaponFireRateAmount").ToString();
        

        healthCostTxt.text = "COST\n" + PlayerPrefs.GetInt("healthUpgradeCost").ToString();
        movementSpeedCostTxt.text = "COST\n" + PlayerPrefs.GetInt("movementSpeedUpgradeCost").ToString();
        dodgeRateCostTxt.text = "COST\n" + PlayerPrefs.GetInt("dodgeRateUpgradeCost").ToString();
        luckRateCostTxt.text ="COST\n" +  PlayerPrefs.GetInt("luckRateUpgradeCost").ToString();
        projectileDamageCostTxt.text ="COST\n" +  PlayerPrefs.GetInt("projectileDamageUpgradeCost").ToString();
        projectileSpeedCostTxt.text = "COST\n" + PlayerPrefs.GetInt("projectileSpeedUpgradeCost").ToString();
        weaponFireRateCostTxt.text = "COST\n" + PlayerPrefs.GetInt("weaponFireRateUpgradeCost").ToString();
    }

    public void HealthUpgrade()
    {
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

            // Increase the upgrade cost by 10% for next time and save it
            float priceCost = 1.1f;
            upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost); 
            PlayerPrefs.SetInt("healthUpgradeCost", upgradeCost);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade health!");
        }
    }

    public void MovementSpeedUpgrade()
    {
        int upgradeCost = PlayerPrefs.GetInt("movementSpeedUpgradeCost");
        int currentCoins = PlayerPrefs.GetInt("totalCoins");

        if (currentCoins >= upgradeCost)
        {
           
            currentCoins -= upgradeCost;
            PlayerPrefs.SetInt("totalCoins", currentCoins);

            float movementSpeedUpgrade = 1;
            float permanentMovementSpeed = PlayerPrefs.GetFloat("movementSpeedAmount");
            permanentMovementSpeed += movementSpeedUpgrade;
            PlayerPrefs.SetFloat("movementSpeedAmount", permanentMovementSpeed);
           
            float priceCost = 1.1f;
            upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost); 
            PlayerPrefs.SetInt("movementSpeedUpgradeCost", upgradeCost);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade movement speed!");
        }
    }
    public void DodgeRateUpgrade()
    {
        int upgradeCost = PlayerPrefs.GetInt("dodgeRateUpgradeCost");
        int currentCoins = PlayerPrefs.GetInt("totalCoins");

        if (currentCoins >= upgradeCost)
        {
           
            currentCoins -= upgradeCost;
            PlayerPrefs.SetInt("totalCoins", currentCoins);

            float dodgeRateUpgrade = 2.75f;
            float permanentDodgeRate = PlayerPrefs.GetFloat("dodgeRateAmount");
            permanentDodgeRate += dodgeRateUpgrade;
            PlayerPrefs.SetFloat("dodgeRateAmount", permanentDodgeRate);
           
            float priceCost = 1.1f;
            upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost); 
            PlayerPrefs.SetInt("dodgeRateUpgradeCost", upgradeCost);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade dodge rate!");
        }
    }

    public void LuckUpgrade()
    {
        int upgradeCost = PlayerPrefs.GetInt("luckRateUpgradeCost");
        int currentCoins = PlayerPrefs.GetInt("totalCoins");

        if (currentCoins >= upgradeCost)
        {
           
            currentCoins -= upgradeCost;
            PlayerPrefs.SetInt("totalCoins", currentCoins);

            float luckUpgrade = 1.5f;
            float permanentLuck = PlayerPrefs.GetFloat("luckRateAmount");
            permanentLuck += luckUpgrade;
            PlayerPrefs.SetFloat("luckRateAmount", permanentLuck);
           
            float priceCost = 1.1f;
            upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost); 
            PlayerPrefs.SetInt("luckRateUpgradeCost", upgradeCost);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade luck rate!");
        }
    }


    public void ProjectileDamageUpgrade()
    {
        int upgradeCost = PlayerPrefs.GetInt("projectileDamageUpgradeCost");
        int currentCoins = PlayerPrefs.GetInt("totalCoins");

        if (currentCoins >= upgradeCost)
        {
           
            currentCoins -= upgradeCost;
            PlayerPrefs.SetInt("totalCoins", currentCoins);

            float projectileDamageUpgrade = 15;
            float permanentProjectileDamage = PlayerPrefs.GetFloat("projectileDamageAmount");
            permanentProjectileDamage += projectileDamageUpgrade;
            PlayerPrefs.SetFloat("projectileDamageAmount", permanentProjectileDamage);
           
            float priceCost = 1.1f;
            upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost); 
            PlayerPrefs.SetInt("projectileDamageUpgradeCost", upgradeCost);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade projectile damage!");
        }
    }
    public void ProjectileSpeedUpgrade()
    {
        int upgradeCost = PlayerPrefs.GetInt("projectileSpeedUpgradeCost");
        int currentCoins = PlayerPrefs.GetInt("totalCoins");

        if (currentCoins >= upgradeCost)
        {
            currentCoins -= upgradeCost;
            PlayerPrefs.SetInt("totalCoins", currentCoins);

            float projectileSpeedUpgrade = 3f;
            float permanentProjectileSpeed = PlayerPrefs.GetFloat("projectileSpeedAmount");
            permanentProjectileSpeed += projectileSpeedUpgrade;
            PlayerPrefs.SetFloat("projectileSpeedAmount", permanentProjectileSpeed);
           
            float priceCost = 1.1f;
            upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost); 
            PlayerPrefs.SetInt("projectileSpeedUpgradeCost", upgradeCost);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade projectile speed!");
        }
    }
    public void WeaponFireRateUpgrade()
    {
        int upgradeCost = PlayerPrefs.GetInt("weaponFireRateUpgradeCost");
        int currentCoins = PlayerPrefs.GetInt("totalCoins");

        if (currentCoins >= upgradeCost)
        {
            currentCoins -= upgradeCost;
            PlayerPrefs.SetInt("totalCoins", currentCoins);

            float weaponFireRateUpgrade = 0.1f;
            float permanentWeaponFireRate = PlayerPrefs.GetFloat("weaponFireRateAmount");
            permanentWeaponFireRate -= weaponFireRateUpgrade;
            PlayerPrefs.SetFloat("weaponFireRateAmount", permanentWeaponFireRate);
           
            float priceCost = 1.1f;
            upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost); 
            PlayerPrefs.SetInt("weaponFireRateUpgradeCost", upgradeCost);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade weapon fire rate!");
        }
    }


}
