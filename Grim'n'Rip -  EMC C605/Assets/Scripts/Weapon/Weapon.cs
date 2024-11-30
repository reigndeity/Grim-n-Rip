using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Script References")]
    public PlayerStats playerStatsScript;
    public AudioManager _audioManager;
    [Header("Upgradables")]
    public float projectileSpeed;
    public float fireRate;
    public float projectileDamage;

    [Header("Weapon Properties")]
    [SerializeField] Transform firingPos;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] bool isShoot;

    void Start()
    {
        playerStatsScript = FindObjectOfType<PlayerStats>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        projectileSpeed =  playerStatsScript.projectileSpeed;
        fireRate = playerStatsScript.fireRate;
        projectileDamage = playerStatsScript.projectileDamage;
        if (GameManager.instance.canShoot == true)
        {
            if (isShoot == false)
            {
                _audioManager.PlayPlayerAttack();
                var bullet = Instantiate(projectilePrefab, firingPos.position, firingPos.rotation);
                bullet.GetComponent<Rigidbody>().velocity =  firingPos.forward * projectileSpeed;
                isShoot = true;
                Invoke("ResetShoot", fireRate);
            }
        }
    }
    void ResetShoot()
    {
        isShoot = false;
    }
}
