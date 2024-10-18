using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperMode : MonoBehaviour
{
    [Header("Debug Properties")]
    [SerializeField] GameObject debugPanel;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            debugPanel.SetActive(true);
        }
        else
        {
            debugPanel.SetActive(false);
        }
        
    }
    public void ResetPlayerStats()
    {
        PlayerPrefs.SetInt("isFirstBoot", 0);
        
        PlayerPrefs.SetFloat("healthAmount", 100f); 
        PlayerPrefs.SetFloat("movementSpeedAmount", 5f);
        PlayerPrefs.SetFloat("dodgeRateAmount", 5f);
        PlayerPrefs.SetFloat("luckRateAmount", 5); //5f orig
        PlayerPrefs.SetFloat("projectileDamageAmount", 30f); 
        PlayerPrefs.SetFloat("projectileSpeedAmount", 10f);
        PlayerPrefs.SetFloat("weaponFireRateAmount", 1.5f); //1f orig

        
    }

    public void ResetShopPrices()
    {
        PlayerPrefs.SetInt("healthUpgradeCost", 20);
        PlayerPrefs.SetInt("movementSpeedUpgradeCost", 20);
        PlayerPrefs.SetInt("dodgeRateUpgradeCost", 20);
        PlayerPrefs.SetInt("luckRateUpgradeCost", 20);
        PlayerPrefs.SetInt("projectileDamageUpgradeCost", 20);
        PlayerPrefs.SetInt("projectileSpeedUpgradeCost", 20);
        PlayerPrefs.SetInt("weaponFireRateUpgradeCost", 20);
    }
    public void ResetMoney()
    {
        PlayerPrefs.SetInt("totalCoins", 0);
    }

    public void GainMoney()
    {
        int totalCoins = PlayerPrefs.GetInt("totalCoins");
        int coinAmount = 5000;

        totalCoins += coinAmount;

        PlayerPrefs.SetInt("totalCoins", totalCoins);
    }

}
