using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Object Spawner Properties")]
    [SerializeField] Transform[] spawnPos;
    [SerializeField] GameObject[] objectPrefabs;

    public void SpawnObject()
    {
        foreach (Transform spawnPosition in spawnPos)
        {
            int randomObject = Random.Range(0,objectPrefabs.Length);
            Instantiate(objectPrefabs[randomObject], spawnPosition);
        }

    }

}
