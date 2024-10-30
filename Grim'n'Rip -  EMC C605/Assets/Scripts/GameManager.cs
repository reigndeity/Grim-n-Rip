using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Script References")]
    public ObjectArea objectAreaScript;
    public ObjectSpawner objectSpawnerScript;
    public WaveManager waveManagerScript;
    public EnemySpawner enemySpawnerScript;
    public PlayerMovement playerMovementScript;
    public PlayerStats playerStatsScipt;
    public WaveUpgrades waveUpgradesScript;
    public Weapon weaponScript;
    
    [Header("Game Properties")]
    public bool isDoneSpawningObjects;
    public bool canShoot;
    public bool canMove;
    public bool canStartWave;

    [Header("Player Properties")]
    public GameObject playerObj;
    [SerializeField] Transform floatingTextPos;
    [SerializeField] GameObject floatingTextPrefab;

    [Header("Wave Properties")]
    [SerializeField] TextMeshProUGUI waveTxt;
    [SerializeField] TextMeshProUGUI enemiesRemainingTxt;
    [SerializeField] GameObject enemiesRemainingObj;
    public bool isRoundStart;
    public bool isRoundResetting;
    public float enemiesValue;
    public GameObject waveUpgradeObj;

    [Header("Score Properties")]
    [SerializeField] TextMeshProUGUI scoreTxt;
    public float scoreValue;

    [Header("Player UI Properties")]
    [SerializeField] Image enemiesRemainingFillImg;
    public bool isEnemiesRemainingFill;
    [SerializeField] float valuesEnemy;
    [SerializeField] GameObject currentWaveObj;
    [SerializeField] Image healthRemainingFill;
    [SerializeField] TextMeshProUGUI healthRemainingTxt;
    [SerializeField] TextMeshProUGUI healthValueTxt;
    public bool isHealthRemainingFill;
    public float healthValue;
    [SerializeField] GameObject gameOverPanelObj;
    [SerializeField] TextMeshProUGUI highScoreTxt;
    [SerializeField] TextMeshProUGUI currentScoreTxt;
    [SerializeField] TextMeshProUGUI coinsEarnedTxt;
    public bool isCalculateCoinsEarned;

    [Header("Stat Holder Properties")]
    [SerializeField] TextMeshProUGUI movementSpeedTxt;
    [SerializeField] TextMeshProUGUI luckTxt;
    [SerializeField] TextMeshProUGUI dodgeRateTxt;
    [SerializeField] TextMeshProUGUI projectileDamageTxt;
    [SerializeField] TextMeshProUGUI projectileSpeedTxt;
    [SerializeField] TextMeshProUGUI weaponFireRateTxt;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        // Display the current high score at the start
        Debug.Log("High Score: " + PlayerPrefs.GetFloat("HighScore", 0));
    }

    void Start()
    {
        Application.targetFrameRate = 90;
        waveManagerScript = FindObjectOfType<WaveManager>();
        enemySpawnerScript = FindObjectOfType<EnemySpawner>();
        objectAreaScript = FindObjectOfType<ObjectArea>();
        playerStatsScipt = FindObjectOfType<PlayerStats>();
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        waveUpgradesScript = FindObjectOfType<WaveUpgrades>();
        weaponScript = FindObjectOfType<Weapon>();
        isEnemiesRemainingFill = true;
        ResetTemporaryUpgrades();
        // Start the round
        Time.timeScale = 1;
        StartCoroutine(BeginRound());
    }

    void Update()
    {  
        // UI Updates ===================================
        scoreTxt.text = "SCORE:\n" + scoreValue.ToString();
        waveTxt.text = waveManagerScript.currentWave.ToString();
        enemiesValue = waveManagerScript.enemyCount;
        enemiesRemainingTxt.text = "ENEMIES REMAINING: " + enemiesValue.ToString();
        // Enemies Remaining Fill
        if (isEnemiesRemainingFill == false) 
        {
            valuesEnemy = enemiesValue;
            isEnemiesRemainingFill = true;
        }
        enemiesRemainingFillImg.fillAmount = enemiesValue / valuesEnemy;

        // Player Health Fill
        if (isHealthRemainingFill == false) 
        {
            healthValue = playerStatsScipt.health;
            isHealthRemainingFill = true;
        }
        healthRemainingTxt.text = playerStatsScipt.health.ToString();
        healthRemainingFill.fillAmount = playerStatsScipt.health / healthValue;
        healthValueTxt.text = healthValue.ToString();



        // In Game Mechanics ===================================
        if (isDoneSpawningObjects == true)  // After the spawning of objects are done
        {
            canMove = true; // Player can now move
            canShoot = true; // Player can now shoot 
            enemiesRemainingObj.SetActive(true);
        }
        else
        {
            canMove = false; // Player cannot move
            canShoot = false; // Player cannot shoot
            waveManagerScript.allowSpawn = true;
        }


        // If there are no enemies left
        if (enemiesValue <= 0 && isRoundStart == true)
        {
            playerObj.transform.position = new Vector3(0,1,0); // Go back to the center of the map
            playerMovementScript.playerRb.velocity = Vector3.zero;
            if (isRoundResetting == false)
            {
                isDoneSpawningObjects = false;       
                isRoundResetting = true;
                isRoundStart = false;
                waveUpgradeObj.SetActive(true);
                waveUpgradesScript.SpawnWaveUpgrades();
                enemiesRemainingObj.SetActive(false);
                //Invoke("StartRound", 3f);
                playerObj.transform.position = new Vector3(0,1,0); // Go back to the center of the map
                playerMovementScript.playerRb.velocity = Vector3.zero;
            }
        }

        if (playerStatsScipt.health <= 0)
        {
            PlayerDefeat();
        }

        // Stat Holder View
        movementSpeedTxt.text = Mathf.Ceil(playerStatsScipt.movementSpeed).ToString();
        luckTxt.text = Mathf.Ceil(playerStatsScipt.luck).ToString();
        dodgeRateTxt.text = Mathf.Ceil(playerStatsScipt.dodgeRate).ToString();
        projectileDamageTxt.text = Mathf.Ceil(playerStatsScipt.projectileDamage).ToString();
        projectileSpeedTxt.text = Mathf.Ceil(playerStatsScipt.projectileSpeed).ToString();
        weaponFireRateTxt.text = playerStatsScipt.fireRate.ToString("F1");
    }

    public void StartRound()
    {
        StartCoroutine(BeginRound());
        objectSpawnerScript.DestroyObjects();
        Invoke("RoundResetBool", 3f);
    }

    IEnumerator BeginRound()
    {
        isRoundStart = true;
        canMove = false;
        canShoot = false; 
        objectAreaScript.isMoving = true;
        yield return new WaitForSecondsRealtime(0.5f);
        objectAreaScript.SpawnArea();
        yield return new WaitForSecondsRealtime(0.1f);
        objectSpawnerScript = FindObjectOfType<ObjectSpawner>();
        yield return new WaitForSecondsRealtime(0.1f);
        objectSpawnerScript.SpawnObject();
        yield return new WaitForSecondsRealtime(3f);
        currentWaveObj.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        currentWaveObj.SetActive(false);
        
    }

    void RoundResetBool()
    {
        isRoundResetting = false;
    }

    void ResetTemporaryUpgrades()
    {
        PlayerPrefs.SetFloat("temporaryMovementSpeedAmount", 0);
        PlayerPrefs.SetFloat("temporaryLuckRateAmount", 0);
        PlayerPrefs.SetFloat("temporaryDodgeRateAmount", 0);
        PlayerPrefs.SetFloat("temporaryProjectileDamageAmount", 0);
        PlayerPrefs.SetFloat("temporaryProjectileSpeedAmount", 0);
        PlayerPrefs.SetFloat("temporaryWeaponFireRateAmount", 0);
    }

    public void PlayerDefeat()
    {
        Time.timeScale = 0;
        gameOverPanelObj.SetActive(true);

        // Display the current score
        currentScoreTxt.text = scoreValue.ToString();

        // Check for and display the correct high score
        CheckHighScore();
        highScoreTxt.text = PlayerPrefs.GetFloat("highScore", 0).ToString();  // Use GetFloat for high score

        if (isCalculateCoinsEarned == false)
        {
            CalculateCoinsEarned();
            isCalculateCoinsEarned = true;
        }
    }

    public void CheckHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("highScore", 0);  // Get the stored high score as a float, or 0 if not set

        // If the current score is greater than the saved high score, update it
        if (scoreValue > highScore)
        {
            PlayerPrefs.SetFloat("highScore", scoreValue);  // Save the new high score as a float
            PlayerPrefs.Save();  // Optional, to force save to disk
            Debug.Log("New High Score: " + scoreValue);
        }
    }

    public void CalculateCoinsEarned()
    {
        int coinsEarned = (int)(scoreValue / 50);
        int totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
        PlayerPrefs.Save();

        totalCoins += coinsEarned;

        PlayerPrefs.SetInt("totalCoins", totalCoins);
        coinsEarnedTxt.text = coinsEarned.ToString();
    }

    public void SpawnFloatingText()
    {
        Instantiate(floatingTextPrefab,floatingTextPos);
    }
}
