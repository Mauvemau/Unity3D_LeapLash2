using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    protected Rigidbody rb;
    protected SphereCollider coll;

    [Header("Stats")]
    [SerializeField] protected float maxHealthPoints;
    protected float healthPoints;

    [Header("Movement")]
    [SerializeField] protected float speed;
    protected Vector3 _currentMovement;

    private void HandleDeath()
    {
        // Corpses don't move
        rb.isKinematic = true;
        rb.detectCollisions = false;
        // So it doesn't block bullets or the player.
        coll.enabled = false;
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

    private void FixedUpdate()
    {
        if (rb)
        {
            transform.Translate(speed * Time.deltaTime * _currentMovement);
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
        if (speed == 0)
        {
            Debug.LogWarning($"{name}: Speed is set to 0!");
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
