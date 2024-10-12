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

    [Header("Score Properties")]
    [SerializeField] TextMeshProUGUI scoreTxt;
    public float scoreValue;

    [Header("Player UI Properties")]
    [SerializeField] Image enemiesRemainingFillImg;
    public bool isEnemiesRemainingFill;
    [SerializeField] float valuesEnemy;


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

        isEnemiesRemainingFill = true;
        // Start the round
        StartCoroutine(BeginRound());
    }

    void Update()
    {  
        // UI Updates
        scoreTxt.text = scoreValue.ToString();
        waveTxt.text = waveManagerScript.currentWave.ToString();
        enemiesValue = waveManagerScript.enemyCount;
        enemiesRemainingTxt.text = enemiesValue.ToString();

        if (isEnemiesRemainingFill == false)
        {
            valuesEnemy = enemiesValue;
            isEnemiesRemainingFill = true;
        }
        enemiesRemainingFillImg.fillAmount = enemiesValue / valuesEnemy;


        // After the spawning of objects are done
        if (isDoneSpawningObjects == true)
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
            if (isRoundResetting == false)
            {
                isDoneSpawningObjects = false;
                isRoundResetting = true;
                isRoundStart = false;
                Invoke("StartRound", 3f);
                
            }
        }
    }

    void StartRound()
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
    }

    void RoundResetBool()
    {
        isRoundResetting = false;
    }
}
