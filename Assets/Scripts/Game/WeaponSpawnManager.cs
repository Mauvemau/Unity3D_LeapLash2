using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnManager : MonoBehaviour
{
    [Header("Weapon Spawns")]
    [SerializeField] private List<Weapon> weaponPool;

    private void Start()
    {
        if (weaponPool.Count > 0)
        {
            int childCount = transform.childCount;
            Vector3[] spawnPoints = new Vector3[childCount];

            for (int i = 0; i < childCount; i++)
            {
                spawnPoints[i] = transform.GetChild(i).position;
            }

            foreach (Vector3 spawnPoint in spawnPoints)
            {
                LootManager.Instance.CreateWeaponDrop(weaponPool[Random.Range(0, weaponPool.Count)], spawnPoint);
            }
        }
    }
}
