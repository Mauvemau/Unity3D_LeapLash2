using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    /// <summary>
    /// Name of the weapon
    /// </summary>
    [SerializeField]
    private string weaponName;

    [SerializeField]
    private float damage;

    /// <summary>
    /// Returns the name of the weapon
    /// </summary>
    public string GetName()
    {
        return weaponName;
    }

    /// <summary>
    /// Returns the damage of the weapon
    /// </summary>
    public float GetDamage()
    {
        return damage;
    }
}
