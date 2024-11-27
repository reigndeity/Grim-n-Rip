using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Script References")]
    [SerializeField] EnemyValues enemyValuesScript;
    [SerializeField] Weapon weaponScript;
    [SerializeField] ParticleSystem bulletSplash;
    [SerializeField] MeshRenderer thisBullet;
    [SerializeField] SphereCollider bulletCollider;
    

    void Awake()
    {
        weaponScript = FindObjectOfType<Weapon>();
        bulletCollider = GetComponent<SphereCollider>();
        bulletSplash = GetComponentInChildren<ParticleSystem>();
        thisBullet = GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision other)
    {
        bulletSplash.Play();
        thisBullet.enabled = false;
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyValues enemyValuesScript = other.gameObject.GetComponent<EnemyValues>();
            enemyValuesScript.TakeDamage(weaponScript.projectileDamage);
            bulletCollider.enabled = false;
            Invoke("DestroyBullet", 1f);
        }

        if (other.gameObject.CompareTag("ProjectileDestroyer"))
        {
            bulletCollider.enabled = false;
            Invoke("DestroyBullet", 1f);
        }
    }
    
    // void OnTriggerEnter(Collider other)
    // {
    //     bulletSplash.Play();
    //     thisBullet.enabled = false;
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         EnemyValues enemyValuesScript = other.gameObject.GetComponent<EnemyValues>();
    //         enemyValuesScript.TakeDamage(weaponScript.projectileDamage);
    //         bulletCollider.enabled = false;
    //         Invoke("DestroyBullet", 1f);
    //     }

    //     if (other.gameObject.CompareTag("ProjectileDestroyer"))
    //     {
    //         bulletCollider.enabled = false;
    //         Invoke("DestroyBullet", 1f);
    //     }  
    // }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (GameManager.instance.isDoneSpawningObjects == false)
        {
            Destroy(gameObject);
        }
    }
}
