using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float movementSpeed;
    public float luck;
    public float dodgeRate;
    public float projectileDamage;
    public float projectileSpeed;
    public float fireRate;

    void Update()
    {
        // Temporary Wave Stats
        movementSpeed = Mathf.Ceil(PlayerPrefs.GetFloat("movementSpeedAmount") + PlayerPrefs.GetFloat("temporaryMovementSpeedAmount"));
        luck = Mathf.Ceil(PlayerPrefs.GetFloat("luckRateAmount") + PlayerPrefs.GetFloat("temporaryLuckRateAmount"));
        dodgeRate = Mathf.Ceil(PlayerPrefs.GetFloat("dodgeRateAmount") + PlayerPrefs.GetFloat("temporaryDodgeRateAmount"));
        projectileSpeed =  Mathf.Ceil(PlayerPrefs.GetFloat("projectileSpeedAmount") + PlayerPrefs.GetFloat("temporaryProjectileSpeedAmount"));
        fireRate = PlayerPrefs.GetFloat("weaponFireRateAmount") + PlayerPrefs.GetFloat("temporaryWeaponFireRateAmount");
        projectileDamage = Mathf.Ceil(PlayerPrefs.GetFloat("projectileDamageAmount") + PlayerPrefs.GetFloat("temporaryProjectileDamageAmount"));
    }
}
