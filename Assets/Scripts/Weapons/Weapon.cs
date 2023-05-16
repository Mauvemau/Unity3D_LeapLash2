using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] protected SphereCollider coll;

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

    /// <summary>
    /// Virtual function so we don't overwrite it on children
    /// </summary>
    protected virtual void _Awake() {}

    private void Awake()
    {
        if (!GetComponent<SphereCollider>())
        {
            Debug.LogError($"{name}: {nameof(coll)} is null!");
        }
        _Awake();
    }

    /// <summary>
    /// Virtual function so we don't overwrite it on children
    /// </summary>
    protected virtual void _OnValidate() {}

    private void OnValidate()
    {
        coll ??= GetComponent<SphereCollider>();
        _OnValidate();
    }
}
