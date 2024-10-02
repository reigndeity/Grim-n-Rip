using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectArea : MonoBehaviour
{
    [SerializeField] Transform introPos;
    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject[] areaPrefabs;

    public void SpawnArea()
    {
        int randomArea = Random.Range(0,2);
        Instantiate(areaPrefabs[randomArea], spawnPos);
    }



}
