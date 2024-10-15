using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Script References")]
    [SerializeField] EnemyValues enemyValuesScript;
    [SerializeField] Weapon weaponScript;
    

    void Awake()
    {
        weaponScript = FindObjectOfType<Weapon>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyValues enemyValuesScript = other.gameObject.GetComponent<EnemyValues>();
            enemyValuesScript.TakeDamage(weaponScript.projectileDamage);
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
