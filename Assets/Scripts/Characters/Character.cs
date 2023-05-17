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
    protected virtual void HandleDeathEffect() { }

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
    /// Override from IDamageable.
    /// </summary>
    /// <param name="damageToTake"></param>
    public void TakeDamage(float damageToTake)
    {
        // 0 = Infinite Health
        if(maxHealthPoints != 0)
        {
            healthPoints -= damageToTake;
            if(healthPoints <= 0)
            {
                healthPoints = 0;
                HandleDeath();
            }
            if(healthPoints > maxHealthPoints)
            {
                healthPoints = maxHealthPoints;
            }
            Debug.Log($"{name} took {damageToTake} damage! [Health Points: {healthPoints}/{maxHealthPoints}]");
        }
    }

    /// <summary>
    /// For use in children classes
    /// </summary>
    protected virtual void _Awake() {}

    private void Awake()
    {
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
}
