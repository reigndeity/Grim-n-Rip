using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Upgradables")]
    public float projectileSpeed;
    public float fireRate;

    [Header("Weapon Properties")]
    [SerializeField] Transform firingPos;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] bool isShoot;


    void Update()
    {
        if (isShoot == false)
        {
            var bullet = Instantiate(projectilePrefab, firingPos.position, firingPos.rotation);
            bullet.GetComponent<Rigidbody>().velocity =  firingPos.forward * projectileSpeed;
            isShoot = true;
            Invoke("ResetShoot", fireRate);
        }

    }
    void ResetShoot()
    {
        isShoot = false;
    }
}
