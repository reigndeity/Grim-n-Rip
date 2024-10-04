using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Object Spawner Properties")]
    [SerializeField] Transform[] spawnPos;
    [SerializeField] GameObject[] objectPrefabs;
    
    private List<GameObject> spawnedObjects = new List<GameObject>();

    // Function to spawn objects
    public void SpawnObject()
    {
        foreach (Transform spawnPosition in spawnPos)
        {
            int randomObject = Random.Range(0, objectPrefabs.Length);
            GameObject spawnedObject = Instantiate(objectPrefabs[randomObject], spawnPosition);
            spawnedObjects.Add(spawnedObject);
        }
    }

    // Function to destroy all spawned objects
    public void DestroyObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear(); // Clear the list after destroying
    }

}
