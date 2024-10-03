using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;

public class ObjectArea : MonoBehaviour
{
    [Header("Object Area Properties")]
    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject[] areaPrefabs;

    [Header("Object Intro Properties")]
    public float moveSpeed; // Speed at which the object moves
    private Vector3 risePosition = new Vector3(0, 0, 0); // Rise position
    private Vector3 descentPosition = new Vector3(0, -20, 0); // Rise position
    private Vector3 startPosition; // Start position
    private bool isMoving = false;

    //[Header("Nav Mesh Area Properties")]
    public NavMeshSurface navMeshSurface;
    
    void Start()
    {
        startPosition = new Vector3(0, -20, 0); // Set the starting position
        transform.position = startPosition; // Set the GameObject's initial position
        isMoving = true; // Start moving
    }
    public void SpawnArea()
    {
        int randomArea = Random.Range(0,2);
        Instantiate(areaPrefabs[randomArea], spawnPos);
    }

    // Object Intro Intro
    void Update()
    {
        if (GameManager.instance.isResetRound == false)
        {
            RiseObjects();
        }
        else
        {
            DescentObjects();
        }   
    }
    
    public void RiseObjects()
    {
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, risePosition, moveSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (transform.position == risePosition)
            {
                isMoving = false; // Stop moving
                navMeshSurface.BuildNavMesh(); // bake
                GameManager.instance.isDoneSpawningObjects = true;
                GameManager.instance.canStartWave = true;
            }
        }
    }
    public void DescentObjects()
    {
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, descentPosition, moveSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (transform.position == descentPosition)
            {
                isMoving = false; // Stop moving
            }
        }
    }

}
