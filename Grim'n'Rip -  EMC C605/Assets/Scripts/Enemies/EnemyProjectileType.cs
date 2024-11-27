using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileType : MonoBehaviour
{
    [SerializeField] Transform firingPos;
    [SerializeField] GameObject projectilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firingPos.position, firingPos.rotation, firingPos);
    }
    
}
