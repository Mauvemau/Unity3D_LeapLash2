using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Settings")]
    /// <summary>
    /// Name of the weapon
    /// </summary>
    [SerializeField]
    protected string weaponName;

    /// <summary>
    /// Hit damage of each attack
    /// </summary>
    [SerializeField]
    protected float damage;

    /// <summary>
    /// Amount of attacks performed per second
    /// </summary>
    [SerializeField]
    protected float attackRate;
    /// <summary>
    /// Complementary to attackrate; defines when you can attack next
    /// </summary>
    protected float nextAttack;

    /// <summary>
    /// Will launch an attack from an origin position, towards a target position
    /// </summary>
    /// <param name="origin"> The position from which the weapon is attacking </param>
    /// <param name="target"> The targetted position </param>
    public abstract void Attack(Vector3 origin, Vector3 target);
}
