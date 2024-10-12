using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] PlayerStats playerStatsScript;
    void Awake()
    {
        playerStatsScript = GetComponent<PlayerStats>();
    }
    void Start()
    {
        playerStatsScript.health = 100f;
        playerStatsScript.movementSpeed = 5f;
        playerStatsScript.dodgeRate = 5f;
        playerStatsScript.luck = 5f;
        playerStatsScript.projectileDamage = 30;
        playerStatsScript.projectileSpeed = 10;
        playerStatsScript.fireRate = 1;
    }

    

}
