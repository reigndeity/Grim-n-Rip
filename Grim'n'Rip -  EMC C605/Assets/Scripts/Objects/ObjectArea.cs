using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class ObjectArea : MonoBehaviour
{
    [Header("Object Area Properties")]
    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject[] areaPrefabs;

    [Header("Object Intro Properties")]
    public float moveSpeed; // Speed at which the object moves
    private Vector3 targetPosition = new Vector3(0, 0, 0); // Target position
    private Vector3 startPosition; // Start position
    private bool isMoving = false;

    void Start()
    {
        startPosition = new Vector3(0, -10, 0); // Set the starting position
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
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (transform.position == targetPosition)
            {
                isMoving = false; // Stop moving
                GameManager.instance.isDoneSpawningObjects = true;
            }
        }
    }

}
