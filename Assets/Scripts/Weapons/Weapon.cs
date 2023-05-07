using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    /// <summary>
    /// Name of the weapon
    /// </summary>
    [SerializeField]
    protected string weaponName;

    [SerializeField]
    protected float damage;

    /// <summary>
    /// Will attack from an origin position, towards a target position.
    /// </summary>
    /// <param name="origin"> The position from which the weapon is attacking </param>
    /// <param name="target"> The targetted position </param>
    public abstract void Attack(Vector3 origin, Vector3 target);
}
