using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    List<Transform> spawnPositions;
    public float spawnDelay = 2f;
    public int maxItems = 10;
    public int increaseRate = 5;
    public float increaseInterval = 30f;

    private int currentItems = 0;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // spawnPositions get all children of the spawner
        spawnPositions = new List<Transform>();
        foreach (Transform child in transform)
        {
            spawnPositions.Add(child);
        }
        StartCoroutine(SpawnRoutine());
        StartCoroutine(IncreaseSpawnRate());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            Debug.Log("Spawning item");
            if (currentItems < maxItems)
            {
                SpawnItem();
            }
        }
    }

    void SpawnItem()
    {
        if (spawnPositions.Count == 0) return;

        Transform randomPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
        GameObject spawnedItem = Instantiate(objectToSpawn, randomPosition.position, randomPosition.rotation);
        spawnedObjects.Add(spawnedItem);
        currentItems++;

        // Listen for item destruction
        spawnedItem.GetComponent<Destructible>().OnDestroyed += () =>
        {
            currentItems--;
            spawnedObjects.Remove(spawnedItem);
        };
    }

    IEnumerator IncreaseSpawnRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseInterval);
            maxItems += increaseRate;
        }
    }
}


