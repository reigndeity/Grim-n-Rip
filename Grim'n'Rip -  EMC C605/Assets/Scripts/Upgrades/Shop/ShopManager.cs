using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] int isFirstBoot; // 0 is false, 1 is true
    void Start()
    {
        if (isFirstBoot == 0)
        {
            PlayerPrefs.SetFloat("healthAmount", 100f);
            PlayerPrefs.SetFloat("movementSpeedAmount", 5f);
            PlayerPrefs.SetFloat("dodgeRateAmount", 5f);
            PlayerPrefs.SetFloat("luckRateAmount", 5f);
            PlayerPrefs.SetFloat("projectileDamageAmount", 30f);
            PlayerPrefs.SetFloat("projectileSpeedAmount", 10f);
            PlayerPrefs.SetFloat("weaponFireRateAmount", 1f);

            PlayerPrefs.SetInt("isFirstBoot", 1);
            isFirstBoot = PlayerPrefs.GetInt("isFirstBoot");
        }
    }

}
