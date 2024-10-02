using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Script References")]
    public ObjectArea objectAreaScript;
    public ObjectSpawner objectSpawnerScript;


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
        StartCoroutine(BeginRound());
        
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator BeginRound()
    {
        objectAreaScript = FindObjectOfType<ObjectArea>();
        yield return new WaitForSeconds(0.1f);
        objectAreaScript.SpawnArea();
        yield return new WaitForSeconds(0.5f);
        objectSpawnerScript = FindObjectOfType<ObjectSpawner>();
        yield return new WaitForSeconds(0.1f);
        objectSpawnerScript.SpawnObject();
    }
}
