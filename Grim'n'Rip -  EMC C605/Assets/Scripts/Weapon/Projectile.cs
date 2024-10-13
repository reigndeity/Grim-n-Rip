using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Upgradables")]
    [SerializeField] float projectileDamage;

    [Header("Script References")]
    [SerializeField] EnemyValues enemyValuesScript;
    



    void Start()
    {
        projectileDamage = PlayerPrefs.GetFloat("projectileDamageAmount") + PlayerPrefs.GetFloat("temporaryProjectileDamageAmount");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyValues enemyValuesScript = other.gameObject.GetComponent<EnemyValues>();
            enemyValuesScript.TakeDamage(projectileDamage);
            Destroy(gameObject); // Destroy Bullet Upon Collision With Enemy
        }

        if (other.gameObject.CompareTag("ProjectileDestroyer"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (GameManager.instance.isDoneSpawningObjects == false)
        {
            Destroy(gameObject);
        }
    }
}
