using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviourSingleton<SpawnManager>
{
    [Header("Settings")]
    /// <summary>
    /// Time to wait before it starts spawning enemies
    /// </summary>
    [SerializeField] private float startDelay;
    [SerializeField] private List<string> spawnPools;

    [Header("Debug")]
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private float spawnRate;
    [SerializeField] private int amountToSpawn;
    /// <summary>
    /// Complementary to amount to spawn, defines how many were spawned so far
    /// </summary>
    [SerializeField] private int amountSpawned = 0;

    /// <summary>
    /// Complementary to spawnrate, defines when it can spawn
    /// </summary>
    private bool canSpawn = true;

    private IEnumerator HandleSpawnRate()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnRate);
        canSpawn = true;
    }

    private IEnumerator HandleStartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        canSpawn = true;
    }

    /// <summary>
    /// Begins spawning enemies.
    /// </summary>
    public void StartSpawning(int _amountToSpawn, float _spawnRate)
    {
        amountToSpawn = _amountToSpawn;
        spawnRate = _spawnRate;
        StartCoroutine(HandleStartDelay());
    }

    /// <summary>
    /// Sets up all spawn locations in the current room
    /// </summary>
    /// <param name="room"> A reference of the room must be received </param>
    public void SetUpSpawnPoints(GameObject room)
    {
        int childCount = room.transform.childCount;
        spawnPoints = new Vector3[childCount];

        for (int i = 0; i < childCount; i++)
        {
            // If the child is a door, we get the offset position instead of the actual position.
            Door door = room.transform.GetChild(i).GetComponent<Door>();
            if (door != null)
            {
                spawnPoints[i] = door.GetOffsetPosition();
            }
            else
            {
                spawnPoints[i] = room.transform.GetChild(i).position;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPools.Count > 0 && spawnPoints.Length > 0)
        {
            PoolManager.Instance.CreateObject(
                spawnPools[Random.Range(0, spawnPools.Count - 1)],
                spawnPoints[Random.Range(0, spawnPoints.Length - 1)],
                new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
        }
        amountSpawned++;
    }

    private void Update()
    {
        if (canSpawn)
        {
            if(amountSpawned < amountToSpawn)
            {
                StartCoroutine(HandleSpawnRate());
                SpawnEnemy();
            }
            else
            {
                canSpawn = false;
                amountSpawned = 0;
            }
        }
    }
}
