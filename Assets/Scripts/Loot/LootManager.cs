using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviourSingleton<LootManager>
{
    [Header("Loot Scanner")]
    [SerializeField] private List<GameObject> nearbyLoot;

    /// <summary>
    /// Compares the distance between objects and player.
    /// </summary>
    /// <param name="a"> Element a to compare </param>
    /// <param name="b"> Element b to compare </param>
    /// <returns> Which of the two is closest to player </returns>
    private int CompareDistance(GameObject a, GameObject b)
    {
        Transform playerTransform = MyGameManager.Instance.getPlayerTransform();

        float distanceA = (a.transform.position - playerTransform.position).sqrMagnitude;
        float distanceB = (b.transform.position - playerTransform.position).sqrMagnitude;

        return distanceA.CompareTo(distanceB);
    }

    /// <summary>
    /// Sorts the scanner based on distance between the player and objects from closest to farthest.
    /// </summary>
    private void SortObjectsByDistance()
    {
        nearbyLoot.Sort(CompareDistance);
    }

    /// <summary>
    /// A new item is added to the loot scanner.
    /// </summary>
    /// <param name="obj"> Item to add </param>
    public void AddLoot(GameObject obj)
    {
        nearbyLoot.Add(obj);
    }

    /// <summary>
    /// An item is removed from the loot scanner.
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveLoot(GameObject obj)
    {
        if(nearbyLoot.Contains(obj))
            nearbyLoot.Remove(obj);
    }

    private void Update()
    {
        if (nearbyLoot.Count > 1)
            SortObjectsByDistance();
    }
}
