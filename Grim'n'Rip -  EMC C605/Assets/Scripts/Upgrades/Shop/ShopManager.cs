using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("First-time Boot-Up")]
    [SerializeField] int isFirstBoot; // 0 is false, 1 is true
    [Header("Shop Properties")]
    [SerializeField] TextMeshProUGUI totalCoinsTxt;
    [SerializeField] int totalCoins;
    public GameObject confirmationUpgradePanel;
    public GameObject[] upgradeImages;
    public TextMeshProUGUI statUpgradeValueTxt;
    public Button confirmButton;
    public TextMeshProUGUI confirmTxt;
    public int selectedUpgrade;

    [Header("Shop UI Properties")]
    // Player Stat Values
    [SerializeField] TextMeshProUGUI healthValueTxt;
    [SerializeField] TextMeshProUGUI movementSpeedValueTxt;
    [SerializeField] TextMeshProUGUI dodgeRateValueTxt;
    //[SerializeField] TextMeshProUGUI luckRateValueTxt;
    [SerializeField] TextMeshProUGUI projectileDamageValueTxt;
    [SerializeField] TextMeshProUGUI projectileSpeedValueTxt;
    [SerializeField] TextMeshProUGUI weaponFireRateValueTxt;
    [Header("Upgrade Cost Properties")]
    [SerializeField] TextMeshProUGUI healthCostTxt;
    [SerializeField] TextMeshProUGUI movementSpeedCostTxt;
    [SerializeField] TextMeshProUGUI dodgeRateCostTxt;
    //[SerializeField] TextMeshProUGUI luckRateCostTxt;
    [SerializeField] TextMeshProUGUI projectileDamageCostTxt;
    [SerializeField] TextMeshProUGUI projectileSpeedCostTxt;
    public TextMeshProUGUI weaponFireRateCostTxt;
    [Header("Upgrade Button Properties")]
    public bool isMaxFireRate;
    public Button fireRateBut;


    void Start()
    {
        Time.timeScale = 1;
        isFirstBoot = PlayerPrefs.GetInt("isFirstBoot", 0);
        if (isFirstBoot == 0)
        {
            // Player Base
            PlayerPrefs.SetFloat("healthAmount", 100f); 
            PlayerPrefs.SetFloat("movementSpeedAmount", 5f);
            PlayerPrefs.SetFloat("dodgeRateAmount", 5f);
            //PlayerPrefs.SetFloat("luckRateAmount", 5); //5f orig
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
            //PlayerPrefs.SetInt("luckRateUpgradeCost", 30);
            PlayerPrefs.SetInt("projectileDamageUpgradeCost", 25);
            PlayerPrefs.SetInt("projectileSpeedUpgradeCost", 20);
            PlayerPrefs.SetInt("weaponFireRateUpgradeCost", 30);

            PlayerPrefs.SetInt("postProcess", 0);
        }
    }


    void Update()
    {
        totalCoinsTxt.text = Mathf.Ceil(PlayerPrefs.GetInt("totalCoins")).ToString();

        healthValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("healthAmount")).ToString();
        movementSpeedValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("movementSpeedAmount")).ToString();
        dodgeRateValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("dodgeRateAmount")).ToString();
        //luckRateValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("luckRateAmount")).ToString();
        projectileDamageValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("projectileDamageAmount")).ToString();
        projectileSpeedValueTxt.text = Mathf.Ceil(PlayerPrefs.GetFloat("projectileSpeedAmount")).ToString();
        weaponFireRateValueTxt.text = PlayerPrefs.GetFloat("weaponFireRateAmount").ToString();
        

        healthCostTxt.text = "COST: " + PlayerPrefs.GetInt("healthUpgradeCost").ToString();
        movementSpeedCostTxt.text = "COST: " + PlayerPrefs.GetInt("movementSpeedUpgradeCost").ToString();
        dodgeRateCostTxt.text = "COST: " + PlayerPrefs.GetInt("dodgeRateUpgradeCost").ToString();
        //luckRateCostTxt.text ="COST\n" +  PlayerPrefs.GetInt("luckRateUpgradeCost").ToString();
        projectileDamageCostTxt.text ="COST: " +  PlayerPrefs.GetInt("projectileDamageUpgradeCost").ToString();
        projectileSpeedCostTxt.text = "COST: " + PlayerPrefs.GetInt("projectileSpeedUpgradeCost").ToString();
        weaponFireRateCostTxt.text = "COST: " + PlayerPrefs.GetInt("weaponFireRateUpgradeCost").ToString();

        if (selectedUpgrade == 0)
        {
            float currentDodgeRate = PlayerPrefs.GetFloat("dodgeRateAmount");
            float projectedValue = Mathf.Ceil(currentDodgeRate + 2.75f); // Add the upgrade value
            statUpgradeValueTxt.text = Mathf.Ceil(currentDodgeRate).ToString() + " > " + projectedValue.ToString();
        }
        if (selectedUpgrade == 1)
        {
            float currentHealth = PlayerPrefs.GetFloat("healthAmount");
            float projectedValue = Mathf.Ceil(currentHealth + 30f); // Health upgrade adds 30
            statUpgradeValueTxt.text = Mathf.Ceil(currentHealth).ToString() + " > " + projectedValue.ToString();
        }
        if (selectedUpgrade == 2)
        {
            float currentMovementSpeed = PlayerPrefs.GetFloat("movementSpeedAmount");
            float projectedValue = Mathf.Ceil(currentMovementSpeed + 1f); // Movement speed upgrade adds 1
            statUpgradeValueTxt.text = Mathf.Ceil(currentMovementSpeed).ToString() + " > " + projectedValue.ToString();
        }
        if (selectedUpgrade == 3 && isMaxFireRate == false)
        {
            float currentWeaponFireRate = PlayerPrefs.GetFloat("weaponFireRateAmount");
            float projectedValue = (currentWeaponFireRate - 0.1f); // Weapon fire rate upgrade reduces by 0.1
            statUpgradeValueTxt.text = (currentWeaponFireRate).ToString("F1") + " > " + projectedValue.ToString("F1");
        }
        if (selectedUpgrade == 3 && isMaxFireRate == true)
        {
            statUpgradeValueTxt.text = "MAXED OUT!";
        }

        if (selectedUpgrade == 4)
        {
            float currentProjectileDamage = PlayerPrefs.GetFloat("projectileDamageAmount");
            float projectedValue = Mathf.Ceil(currentProjectileDamage + 15f); // Projectile damage upgrade adds 15
            statUpgradeValueTxt.text = Mathf.Ceil(currentProjectileDamage).ToString() + " > " + projectedValue.ToString();
        }
        if (selectedUpgrade == 5)
        {
            float currentProjectileSpeed = PlayerPrefs.GetFloat("projectileSpeedAmount");
            float projectedValue = Mathf.Ceil(currentProjectileSpeed + 3f); // Projectile speed upgrade adds 3
            statUpgradeValueTxt.text = Mathf.Ceil(currentProjectileSpeed).ToString() + " > " + projectedValue.ToString();
        }
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

        // Get the number of times movement speed has been upgraded
        int movementSpeedUpgradeCount = PlayerPrefs.GetInt("movementSpeedUpgradeCount", 0);

        // Set the maximum number of upgrades allowed
        int maxMovementSpeedUpgrades = 10;

        // Check if the upgrade count is less than the max limit
        if (movementSpeedUpgradeCount < maxMovementSpeedUpgrades)
        {
            if (currentCoins >= upgradeCost)
            {
                currentCoins -= upgradeCost;
                PlayerPrefs.SetInt("totalCoins", currentCoins);

                // Apply the movement speed upgrade
                float movementSpeedUpgrade = 1f;
                float permanentMovementSpeed = PlayerPrefs.GetFloat("movementSpeedAmount");
                permanentMovementSpeed += movementSpeedUpgrade;
                PlayerPrefs.SetFloat("movementSpeedAmount", permanentMovementSpeed);

                // Increment the upgrade count
                movementSpeedUpgradeCount++;
                PlayerPrefs.SetInt("movementSpeedUpgradeCount", movementSpeedUpgradeCount);

                // Increase the upgrade cost by 10% for the next upgrade
                float priceCost = 1.1f;
                upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost);
                PlayerPrefs.SetInt("movementSpeedUpgradeCost", upgradeCost);
            }
            else
            {
                Debug.Log("Not enough coins to upgrade movement speed!");
            }
        }
        else
        {
            Debug.Log("Maximum upgrades reached for movement speed!");
        }
    }
    public void DodgeRateUpgrade()
    {
        int upgradeCost = PlayerPrefs.GetInt("dodgeRateUpgradeCost");
        int currentCoins = PlayerPrefs.GetInt("totalCoins");

        // Get the number of times dodge rate has been upgraded
        int dodgeRateUpgradeCount = PlayerPrefs.GetInt("dodgeRateUpgradeCount", 0);

        // Set the maximum number of upgrades allowed
        int maxDodgeRateUpgrades = 30;

        // Check if the upgrade count is less than the max limit
        if (dodgeRateUpgradeCount < maxDodgeRateUpgrades)
        {
            if (currentCoins >= upgradeCost)
            {
                currentCoins -= upgradeCost;
                PlayerPrefs.SetInt("totalCoins", currentCoins);

                // Apply the dodge rate upgrade
                float dodgeRateUpgrade = 2.75f;
                float permanentDodgeRate = PlayerPrefs.GetFloat("dodgeRateAmount");
                permanentDodgeRate += dodgeRateUpgrade;
                PlayerPrefs.SetFloat("dodgeRateAmount", permanentDodgeRate);

                // Increment the upgrade count
                dodgeRateUpgradeCount++;
                PlayerPrefs.SetInt("dodgeRateUpgradeCount", dodgeRateUpgradeCount);

                // Increase the upgrade cost by 10% for the next upgrade
                float priceCost = 1.1f;
                upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost);
                PlayerPrefs.SetInt("dodgeRateUpgradeCost", upgradeCost);
            }
            else
            {
                Debug.Log("Not enough coins to upgrade dodge rate!");
            }
        }
        else
        {
            Debug.Log("Maximum upgrades reached for dodge rate!");
        }
    }

    // public void LuckUpgrade()
    // {
    //     int upgradeCost = PlayerPrefs.GetInt("luckRateUpgradeCost");
    //     int currentCoins = PlayerPrefs.GetInt("totalCoins");

    //     if (currentCoins >= upgradeCost)
    //     {
           
    //         currentCoins -= upgradeCost;
    //         PlayerPrefs.SetInt("totalCoins", currentCoins);

    //         float luckUpgrade = 1.5f;
    //         float permanentLuck = PlayerPrefs.GetFloat("luckRateAmount");
    //         permanentLuck += luckUpgrade;
    //         PlayerPrefs.SetFloat("luckRateAmount", permanentLuck);
           
    //         float priceCost = 1.1f;
    //         upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost); 
    //         PlayerPrefs.SetInt("luckRateUpgradeCost", upgradeCost);
    //     }
    //     else
    //     {
    //         Debug.Log("Not enough coins to upgrade luck rate!");
    //     }
    // }


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

        // Get the number of times projectile speed has been upgraded
        int projectileSpeedUpgradeCount = PlayerPrefs.GetInt("projectileSpeedUpgradeCount", 0);

        // Set the maximum number of upgrades allowed
        int maxProjectileSpeedUpgrades = 5;

        // Check if the upgrade count is less than the max limit
        if (projectileSpeedUpgradeCount < maxProjectileSpeedUpgrades)
        {
            if (currentCoins >= upgradeCost)
            {
                currentCoins -= upgradeCost;
                PlayerPrefs.SetInt("totalCoins", currentCoins);

                // Apply the projectile speed upgrade
                float projectileSpeedUpgrade = 3f;
                float permanentProjectileSpeed = PlayerPrefs.GetFloat("projectileSpeedAmount");
                permanentProjectileSpeed += projectileSpeedUpgrade;
                PlayerPrefs.SetFloat("projectileSpeedAmount", permanentProjectileSpeed);

                // Increment the upgrade count
                projectileSpeedUpgradeCount++;
                PlayerPrefs.SetInt("projectileSpeedUpgradeCount", projectileSpeedUpgradeCount);

                // Increase the upgrade cost by 10% for the next upgrade
                float priceCost = 1.1f;
                upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost);
                PlayerPrefs.SetInt("projectileSpeedUpgradeCost", upgradeCost);
            }
            else
            {
                Debug.Log("Not enough coins to upgrade projectile speed!");
            }
        }
        else
        {
            Debug.Log("Maximum upgrades reached for projectile speed!");
        }
    }
    public void WeaponFireRateUpgrade()
    {
        int upgradeCost = PlayerPrefs.GetInt("weaponFireRateUpgradeCost");
        int currentCoins = PlayerPrefs.GetInt("totalCoins");

        // Get the number of times fire rate has been upgraded
        int fireRateUpgradeCount = PlayerPrefs.GetInt("fireRateUpgradeCount", 0);

        // Set the maximum number of upgrades allowed
        int maxUpgrades = 14;

        // Check if the upgrade count is less than the max limit
        if (fireRateUpgradeCount < maxUpgrades)
        {
            if (currentCoins >= upgradeCost)
            {
                currentCoins -= upgradeCost;
                PlayerPrefs.SetInt("totalCoins", currentCoins);

                // Apply the fire rate upgrade
                float weaponFireRateUpgrade = 0.1f;
                float permanentWeaponFireRate = PlayerPrefs.GetFloat("weaponFireRateAmount");
                permanentWeaponFireRate -= weaponFireRateUpgrade;
                PlayerPrefs.SetFloat("weaponFireRateAmount", permanentWeaponFireRate);

                // Increment the upgrade count
                fireRateUpgradeCount++;
                PlayerPrefs.SetInt("fireRateUpgradeCount", fireRateUpgradeCount);

                // Increase the upgrade cost by 10% for the next upgrade
                float priceCost = 1.1f;
                upgradeCost = Mathf.CeilToInt(upgradeCost * priceCost);
                PlayerPrefs.SetInt("weaponFireRateUpgradeCost", upgradeCost);
            }
            else
            {
                Debug.Log("Not enough coins to upgrade weapon fire rate!");
            }
        }
        else
        {
            Debug.Log("Maximum upgrades reached for fire rate!");
            isMaxFireRate = true;
        }
    }


}
