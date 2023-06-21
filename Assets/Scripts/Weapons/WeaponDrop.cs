using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponDrop : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private SphereCollider coll;

    //TODO: Fix - Should be native Setter/Getter
    public void SetWeapon(Weapon _weapon)
    {
        weapon = _weapon;
    }

    //TODO: Fix - Should be native Setter/Getter
    /// <summary>
    /// Returns the weapon scriptableObject.
    /// </summary>
    /// <returns></returns>
    public Weapon getWeapon()
    {
        return weapon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCharacter>())
        {
            LootManager.Instance.AddLoot(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerCharacter>())
        {
            LootManager.Instance.RemoveLoot(gameObject);
        }
    }

    private void Awake()
    {
        if (!GetComponent<SphereCollider>())
        {
            Debug.LogError($"{name}: {nameof(coll)} is null!");
        }
        else
        {
            coll.isTrigger = true;
        }
    }

    private void OnValidate()
    {
        coll = GetComponent<SphereCollider>();
    }
}
