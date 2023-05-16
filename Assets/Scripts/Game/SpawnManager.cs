using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviourSingleton<SpawnManager>
{
    [Header("Settings")]
    [SerializeField] private float spawnRate;

    [Header("Debug")]
    [SerializeField] private List<EnemyCharacter> possibleSpawns;
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private bool spawning;


}
