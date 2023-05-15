using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    protected Rigidbody rb;
    protected SphereCollider coll;

    [Header("Stats")]
    [SerializeField] protected float maxHealthPoints;
    protected float healthPoints;

    /// <summary>
    /// Used in case the developer wants to add extra functionality after death, like; Death animation, de-activation, playing death sound, etc
    /// </summary>
    protected abstract void HandleDeathEffect();

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
            Debug.Log($"{name} took {damageToTake} damage! [Health Points: {healthPoints}/{maxHealthPoints}]");
        }
    }

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
        {
            Debug.LogError($"{name}: {nameof(rb)} is null!");
        }
        if (!GetComponent<SphereCollider>())
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
    }

    private void OnValidate()
    {
        rb ??= GetComponent<Rigidbody>();
        coll ??= GetComponent<SphereCollider>();
    }
}
