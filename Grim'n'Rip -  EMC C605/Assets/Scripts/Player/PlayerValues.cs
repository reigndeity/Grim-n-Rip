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
        playerStatsScript.health = PlayerPrefs.GetFloat("healthAmount");
        playerStatsScript.movementSpeed = PlayerPrefs.GetFloat("movementSpeedAmount");
        playerStatsScript.dodgeRate = PlayerPrefs.GetFloat("dodgeRateAmount");
        playerStatsScript.luck = PlayerPrefs.GetFloat("luckRateAmount");
        playerStatsScript.projectileDamage = PlayerPrefs.GetFloat("projectileDamageAmount");
        playerStatsScript.projectileSpeed = PlayerPrefs.GetFloat("projectileSpeedAmount");
        playerStatsScript.fireRate = PlayerPrefs.GetFloat("weaponFireRateAmount");
    }

    

}
