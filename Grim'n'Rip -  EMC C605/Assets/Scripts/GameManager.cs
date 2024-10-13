using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



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
    public PlayerValues playerValuesScript;
    public WaveUpgrades waveUpgradesScript;
    
    [Header("Game Properties")]
    public bool isDoneSpawningObjects;
    public bool canShoot;
    public bool canMove;
    public bool canStartWave;
    [Header("Player Properties")]
    public GameObject playerObj;

    [Header("Wave Properties")]
    [SerializeField] TextMeshProUGUI waveTxt;
    [SerializeField] TextMeshProUGUI enemiesRemainingTxt;
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
        isEnemiesRemainingFill = true;
        PlayerPrefs.SetFloat("temporaryProjectileDamageAmount", 0);
        PlayerPrefs.SetFloat("temporaryMovementSpeedAmount", 0);
        PlayerPrefs.SetFloat("temporaryProjectileDamageAmount", 0);
        // Start the round
        StartCoroutine(BeginRound());
    }

    void Update()
    {  
        // UI Updates ===================================
        scoreTxt.text = scoreValue.ToString();
        waveTxt.text = waveManagerScript.currentWave.ToString();
        enemiesValue = waveManagerScript.enemyCount;
        enemiesRemainingTxt.text = enemiesValue.ToString();

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
                //Invoke("StartRound", 3f);
                
            }
        }
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
        yield return new WaitForSeconds(0.5f);
        objectAreaScript.SpawnArea();
        yield return new WaitForSeconds(0.1f);
        objectSpawnerScript = FindObjectOfType<ObjectSpawner>();
        yield return new WaitForSeconds(0.1f);
        objectSpawnerScript.SpawnObject();

        yield return new WaitForSeconds(3f);
        currentWaveObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        currentWaveObj.SetActive(false);
    }

    void RoundResetBool()
    {
        isRoundResetting = false;
    }
}
