using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Upgradables")]
    [SerializeField] float projectileDamage;



    // public Health enemyHealth;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Health enemyHealth = other.gameObject.GetComponent<Health>();
            // if (enemyHealth != null)
            // {
            //     enemyHealth.Damage(bulletDamage);
            // }
            // Destroy(gameObject); // Destroy Bullet Upon Collision With Enemy
        }

        if (other.gameObject.CompareTag("ProjectileDestroyer"))
        {
            Destroy(gameObject);
        }
    }
}
