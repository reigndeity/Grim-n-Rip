using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



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
    public bool isRoundFinished;
    public float enemiesValue;

    [Header("Score Properties")]
    [SerializeField] TextMeshProUGUI scoreTxt;
    public float scoreValue;


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
        StartCoroutine(BeginRound());

        waveManagerScript = FindObjectOfType<WaveManager>();
        enemySpawnerScript = FindObjectOfType<EnemySpawner>();
        objectAreaScript = FindObjectOfType<ObjectArea>();

    }

    // Update is called once per frame
    void Update()
    {
        // Allow the player to move and shoot after spawning the objects     
        if (isDoneSpawningObjects == true)
        {
            canMove = true; // Player can now move
            canShoot = true; // Player can now shoot 
        }

        
        //UI Updates
        scoreTxt.text = scoreValue.ToString();
        waveTxt.text = waveManagerScript.currentWave.ToString();
        enemiesValue = waveManagerScript.enemyCount;
        enemiesRemainingTxt.text = enemiesValue.ToString();

        // If there are no enemies left
        if (enemiesValue <= 0 && isRoundFinished == false)
        {
            playerObj.transform.position = new Vector3(0,1,0); // Go back to the center of the map
            isRoundFinished = true;
        }

        if (isRoundFinished == true)
        {
            StartCoroutine(BeginRound());
            isDoneSpawningObjects = false;
        }

    }

    IEnumerator BeginRound()
    {
        isRoundFinished = false;
        if (isDoneSpawningObjects == false)
        {    
            canMove = false; // Player can't move yet
            canShoot = false; // Player can't shoot yet
            yield return new WaitForSeconds(0.1f);
            objectAreaScript.SpawnArea();
            objectSpawnerScript = FindObjectOfType<ObjectSpawner>();
            yield return new WaitForSeconds(0.5f);
            objectSpawnerScript.SpawnObject();
        }
    }



    IEnumerator EndRound()
    {
        Debug.Log("Round End");
        canMove = false; // Player can't move yet
        canShoot = false; // Player can't shoot yet
        
        yield return new WaitForSeconds(1.0f);
        isDoneSpawningObjects = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(BeginRound());
    }
}
