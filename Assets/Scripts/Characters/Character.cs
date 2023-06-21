using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] protected float maxHealthPoints;

    [Header("Debug")]
    [SerializeField] protected float healthPoints;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected CapsuleCollider coll;

    //TODO: TP2 - Syntax - Fix declaration order
    /// <summary>
    /// Returns if dead or not.
    /// </summary>
    /// <returns></returns>
    protected bool IsDead()
    {
        return (healthPoints <= 0);
    }

    /// <summary>
    /// Used in case the developer wants to add extra functionality after death, like; Death animation, de-activation, playing death sound, etc
    /// </summary>
    //TODO: Fix - Should be event based
    protected virtual void HandleDeathEffect() {}

    /// <summary>
    /// Basic function performed on death
    /// </summary>
    private void HandleDeath()
    {
        // Corpses don't move
        rb.isKinematic = true;
        rb.detectCollisions = false;
        // So it doesn't block bullets or the player
        coll.enabled = false;
        HandleDeathEffect();
    }

    /// <summary>
    /// Used for updating a health bar, if entity has one
    /// </summary>
    //TODO: Fix - Should be event based
    protected virtual void UpdateHealthbar() {}

    /// <summary>
    /// Override from IDamageable.
    /// </summary>
    /// <param name="damageToTake"></param>
    public void TakeDamage(float damageToTake)
    {
        //TODO: Fix - If MaxHP=0 is an error, it should just log it or clamp the value in OnValidate
        //TODO: Fix - MaxHP=0 would just mean the character dies on the first hit, not infinite health
        // 0 = Infinite Health
        if(maxHealthPoints != 0)
        {
            healthPoints -= damageToTake;
            //TODO: Fix - Repeated code - Use IsDead
            if(healthPoints <= 0)
            {
                healthPoints = 0;
                HandleDeath();
            }
            if(healthPoints > maxHealthPoints)
            {
                healthPoints = maxHealthPoints;
            }
        }
        //TODO: TP2 - SOLID
        UpdateHealthbar();
    }

    /// <summary>
    /// For use in children classes
    /// </summary>
    //TODO: OOP - Awake should just be virtual then
    protected virtual void _Awake() {}

    private void Awake()
    {
        //TODO: Fix - TryGetComponent
        if (!GetComponent<Rigidbody>())
        {
            Debug.LogError($"{name}: {nameof(rb)} is null!");
        }
        if (!GetComponent<CapsuleCollider>())
        {
            Debug.LogError($"{name}: {nameof(coll)} is null!");
        }
        if (maxHealthPoints == 0)
        {
            Debug.LogWarning($"{name}: Max Health Points is set to 0!");
        }
        else
        {
            healthPoints = maxHealthPoints;
        }
        _Awake();
    }

    /// <summary>
    /// For use in children classes.
    /// </summary>
    protected virtual void _OnValidate() {}

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        _OnValidate();
    }

    /// <summary>
    /// For use in children classes.
    /// </summary>
    protected virtual void _OnEnable() {}

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
        if (coll != null)
        {
            coll.enabled = true;
        }
        healthPoints = maxHealthPoints;
        _OnEnable();
    }

    protected virtual void _OnDisable() {}

    private void OnDisable()
    {
        _OnDisable();
    }
}
