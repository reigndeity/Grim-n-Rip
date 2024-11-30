using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperMode : MonoBehaviour
{
    [Header("Debug Properties")]
    public PlayerStats playerStats;
    public WaveManager waveManager;
    [SerializeField] GameObject debugPanel;
    [SerializeField] TextMeshProUGUI opModeTxt;
    [SerializeField] TextMeshProUGUI currentWaveTxt;
    public bool isClick;
    public bool isOpMode;
    
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

    // Game Debug Stick
    public void OnClickDebugStick()
    {
        debugPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnClickCloseDebugStick()
    {
        debugPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClickOpMode()
    {
        if (isClick == false)
        {
            opModeTxt.color = Color.green;
            isClick = true;
            playerStats.health = 100000f;
            PlayerPrefs.SetFloat("movementSpeedAmount", 15f);
            PlayerPrefs.SetFloat("dodgeRateAmount", 70f);
            PlayerPrefs.SetFloat("luckRateAmount", 40); //5f orig
            PlayerPrefs.SetFloat("projectileDamageAmount", 10000f); 
            PlayerPrefs.SetFloat("projectileSpeedAmount",50f);
            PlayerPrefs.SetFloat("weaponFireRateAmount", 0.1f); //1f orig
        }
        else
        {
            opModeTxt.color = Color.red;
            isClick = false;
            playerStats.health = 100;
            PlayerPrefs.SetFloat("healthAmount", 100f); 
            PlayerPrefs.SetFloat("movementSpeedAmount", 5f);
            PlayerPrefs.SetFloat("dodgeRateAmount", 5f);
            PlayerPrefs.SetFloat("luckRateAmount", 5); //5f orig
            PlayerPrefs.SetFloat("projectileDamageAmount", 30f); 
            PlayerPrefs.SetFloat("projectileSpeedAmount", 10f);
            PlayerPrefs.SetFloat("weaponFireRateAmount", 1.5f); //1f orig
        }
    }

    public void IncreaseWaveOne()
    {
        int addedWave = 1;
        int currentWave = PlayerPrefs.GetInt("debugWave");

        currentWave += addedWave;
        PlayerPrefs.SetInt("debugWave",currentWave);
        currentWaveTxt.text = "Current Wave: " + currentWave.ToString();

    }
    public void DecreaseWaveOne()
    {
        int addedWave = 1;
        int currentWave = PlayerPrefs.GetInt("debugWave");
        if (currentWave > 0)
        {
            currentWave = Mathf.Max(0, currentWave - addedWave); // Ensure it does not go below zero
            PlayerPrefs.SetInt("debugWave", currentWave);
            currentWaveTxt.text = "Current Wave: " + currentWave.ToString();
        }
    }
        public void IncreaseWaveTen()
    {
        int addedWave = 10;
        int currentWave = PlayerPrefs.GetInt("debugWave");
        currentWave += addedWave;
        PlayerPrefs.SetInt("debugWave",currentWave);
        currentWaveTxt.text = "Current Wave: " + currentWave.ToString();
    }
    public void DecreaseWaveTen()
    {
        int addedWave = 10;
        int currentWave = PlayerPrefs.GetInt("debugWave");
        if (currentWave > 0)
        {
            currentWave = Mathf.Max(0, currentWave - addedWave); // Ensure it does not go below zero
            PlayerPrefs.SetInt("debugWave", currentWave);
            currentWaveTxt.text = "Current Wave: " + currentWave.ToString();
        }
    }
    public void ResetWave()
    {
        int currentWave = PlayerPrefs.GetInt("debugWave");
        PlayerPrefs.SetInt("debugWave", 0);
        currentWaveTxt.text = "Current Wave: " + currentWave.ToString();
    }
}
