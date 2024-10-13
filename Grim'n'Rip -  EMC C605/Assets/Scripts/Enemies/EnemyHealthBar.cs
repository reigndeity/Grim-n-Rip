using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] EnemyStats enemyStatsScript;
    [SerializeField] Camera cameraMain;
    [SerializeField] Transform target;

    private bool isEnemyHealthFill;
    private float enemyHealthValue;

    void Awake()
    {
        enemyStatsScript = GetComponentInParent<EnemyStats>();
        cameraMain = FindObjectOfType<Camera>();
    }


    void Update()
    {
        
        if (isEnemyHealthFill == false) 
        {
            enemyHealthValue = enemyStatsScript.health;
            isEnemyHealthFill = true;
        }
        slider.value = enemyStatsScript.health / enemyHealthValue;

        transform.rotation = cameraMain.transform.rotation;
    }
}
