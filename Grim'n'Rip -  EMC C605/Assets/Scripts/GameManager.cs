using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Script References")]
    public ObjectArea objectAreaScript;
    public ObjectSpawner objectSpawnerScript;
    
    [Header("Game Properties")]
    public bool isDoneSpawningObjects;
    public bool canShoot;
    public bool canMove;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }  
    }

    void Start()
    {
        Application.targetFrameRate = 90;
        StartCoroutine(BeginRound());
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoneSpawningObjects == true)
        {
            canMove = true; // Player can now move
            canShoot = true; // Player can now shoot 
        }
    }

    IEnumerator BeginRound()
    {
        if (isDoneSpawningObjects == false)
        {
            canMove = false; // Player can't move yet
            canShoot = false; // Player can't shoot yet
            objectAreaScript = FindObjectOfType<ObjectArea>();
            yield return new WaitForSeconds(0.1f);
            objectAreaScript.SpawnArea();
            yield return new WaitForSeconds(0.5f);
            objectSpawnerScript = FindObjectOfType<ObjectSpawner>();
            yield return new WaitForSeconds(0.1f);
            objectSpawnerScript.SpawnObject();
        }
    }
}
