using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviourSingleton<LootManager>
{
    [Header("Loot Maker")]
    [SerializeField] private GameObject weaponDropTemplate;

    [Header("Loot Scanner")]
    [SerializeField] private List<GameObject> nearbyLoot;

    [Header("Event Broadcasters")]
    [SerializeField] private BoolEventChannel togglePromptChannel;
    [SerializeField] private StringEventChannel setTextChannel;
    [SerializeField] private WeaponEventChannel equipWeaponChannel;

    public void CreateWeaponDrop(Weapon weapon, Vector3 position)
    {
        GameObject obj = (GameObject)GameObject.Instantiate(weaponDropTemplate);
        obj.transform.position = position;
        obj.GetComponent<WeaponDrop>().SetWeapon(weapon);
    }

    public void LootItem()
    {
        if(nearbyLoot.Count > 0)
        {
            WeaponDrop weapon = nearbyLoot[0].GetComponent<WeaponDrop>();
            if(weapon != null && equipWeaponChannel != null)
            {
                equipWeaponChannel.RaiseEvent(weapon.getWeapon());
            }

            GameObject objectReference = nearbyLoot[0];
            RemoveLoot(nearbyLoot[0]);
            Destroy(objectReference);
        }
    }

    public bool CanLoot()
    {
        return (nearbyLoot.Count > 0);
    }

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
        if(setTextChannel != null)
            setTextChannel.RaiseEvent(nearbyLoot[0].GetComponent<WeaponDrop>().getWeapon().name);
    }

    /// <summary>
    /// A new item is added to the loot scanner.
    /// </summary>
    /// <param name="obj"> Item to add </param>
    public void AddLoot(GameObject obj)
    {
        nearbyLoot.Add(obj);
        if(togglePromptChannel != null)
            togglePromptChannel.RaiseEvent(true);
        if(setTextChannel != null)
            setTextChannel.RaiseEvent(nearbyLoot[0].GetComponent<WeaponDrop>().getWeapon().name);
    }

    /// <summary>
    /// An item is removed from the loot scanner.
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveLoot(GameObject obj)
    {
        if(nearbyLoot.Contains(obj))
            nearbyLoot.Remove(obj);
        if (togglePromptChannel != null && nearbyLoot.Count == 0)
            togglePromptChannel.RaiseEvent(false);
    }

    private void Update()
    {
        // expensive
        if (nearbyLoot.Count > 1)
            SortObjectsByDistance();
    }
}
