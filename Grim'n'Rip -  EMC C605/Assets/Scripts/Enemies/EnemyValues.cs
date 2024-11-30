using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyValues : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] EnemyStats enemyStatsScript;
    [SerializeField] WaveManager waveManagerScript;
    [SerializeField] PlayerStats playerStatsScript;
    [SerializeField] EnemyMovement enemyMovementScript;
    public AudioManager _audioManager;
    
    [Header("Enemy Properties")]
    [SerializeField] int enemyType; // 0 - blaze | 1 - sinister seer | 2 - vained | 3 - tormented soul
    [SerializeField] bool isDoneUpgrade;
    [SerializeField] NavMeshAgent enemyAgent;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] CapsuleCollider enemyCollider;
    [SerializeField] GameObject healthBar;
    [SerializeField] ParticleSystem bloodSplat;
    [SerializeField] GameObject[] thisModel;
    
   

    void Awake()
    {
        enemyStatsScript = GetComponent<EnemyStats>();
        waveManagerScript = FindObjectOfType<WaveManager>();
        playerStatsScript = FindObjectOfType<PlayerStats>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyMovementScript = GetComponent<EnemyMovement>();
        enemyCollider = GetComponent<CapsuleCollider>();
        healthBar = transform.Find("Enemy Health Display Canvas").gameObject;
        bloodSplat = GetComponentInChildren<ParticleSystem>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        switch(enemyType)
        {
            case 0: // BLAZE Base Values
                enemyStatsScript.health = 80f;
                enemyStatsScript.damage = 10f;
                enemyStatsScript.movementSpeed = 4f;
                enemyStatsScript.hitChance = 100f;
                enemyStatsScript.enemyScore = 50f;               
            break;
            case 1: // SINISTER SEER Base Values
                enemyStatsScript.health = 60f;
                enemyStatsScript.damage = 15f;
                enemyStatsScript.movementSpeed = 3;
                enemyStatsScript.hitChance = 60f;
                enemyStatsScript.enemyScore = 75f;                
            break;
            case 2: // VAINED Base Values
                enemyStatsScript.health = 120f;
                enemyStatsScript.damage = 30f;
                enemyStatsScript.movementSpeed = 2f;
                enemyStatsScript.hitChance = 60f;
                enemyStatsScript.enemyScore = 100f;
            break;
            case 3: // TORMENTED SOUL Base Values
                enemyStatsScript.health = 200;
                enemyStatsScript.damage = 25f;
                enemyStatsScript.movementSpeed = 2.5f;
                enemyStatsScript.hitChance = 60f;
                enemyStatsScript.enemyScore = 200f;
            break;
        }        
    }

    // Receiving Damage
    public void TakeDamage(float damageAmount)
    {
        enemyStatsScript.health -= damageAmount;
        if (enemyStatsScript.health <= 0)
        {
            enemyAnimator.SetInteger("animState",2);
            Destroy(enemyMovementScript);
            Destroy(healthBar);
            enemyAgent.speed = 0;
            enemyCollider.enabled = false;
            GameManager.instance.scoreValue += enemyStatsScript.enemyScore;
            waveManagerScript.enemyCount--;
        }
    }

    void Update()
    {
        // Enemy Difficulty Progression
        if (isDoneUpgrade == false)
        {
            enemyStatsScript.health = enemyStatsScript.health + (enemyStatsScript.health * 0.1f * waveManagerScript.currentWave);
            enemyStatsScript.damage = enemyStatsScript.damage + (enemyStatsScript.damage * 0.06f * waveManagerScript.currentWave);
            enemyStatsScript.movementSpeed = enemyStatsScript.movementSpeed + (enemyStatsScript.movementSpeed * 0.05f * waveManagerScript.currentWave);
            enemyStatsScript.hitChance = enemyStatsScript.hitChance + (enemyStatsScript.hitChance * 0.02f * waveManagerScript.currentWave);
            isDoneUpgrade = true;
        }

        // Capping The Enemy Stats
        if (enemyStatsScript.hitChance > 100f)
        {
            enemyStatsScript.hitChance = 100f;
        }
        if (enemyStatsScript.movementSpeed > 8f && enemyType == 0)
        {
            enemyStatsScript.movementSpeed = 8;
        }
        if (enemyStatsScript.movementSpeed > 5 && enemyType == 1)
        {
            enemyStatsScript.movementSpeed = 5;
        }
        if (enemyStatsScript.movementSpeed > 6.5f && enemyType == 2)
        {
            enemyStatsScript.movementSpeed = 6.5f;
        }
        if (enemyStatsScript.movementSpeed > 5 && enemyType == 3)
        {
            enemyStatsScript.movementSpeed = 5;
        }

    }

    
    public void DestroyMyself()
    {
        bloodSplat.Play();
        for (int i = 0; i < thisModel.Length; i++)
        {
            thisModel[i].SetActive(false);
        }
        Invoke("ActuallyDestroyMyself", 1f);
        if (_audioManager.playOnce == 0)
        {
            _audioManager.PlayEnemyDeathSound();
            _audioManager.playOnce = 1;
        }
    }
    public void ActuallyDestroyMyself()
    {
        _audioManager.playOnce = 0;
        Destroy(gameObject);
    }

}
