using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    /// <summary>
    /// Different types of movement for the enemy, done this way in case i wanted to make an enemy that changes behaviour.
    /// </summary>
    private enum MovementTypes
    {
        followPlayer = 0,
        leaping,
        stationary
    }

    [Header("Objective")]
    [SerializeField] private Vector3 target;

    [Header("Equipment")]
    [SerializeField] private Weapon equippedWeapon;

    [Header("Movement")]
    [SerializeField] MovementTypes movementType;

    [Header("Follow Movement Settings")]
    [SerializeField] private float speed;

    [Header("Leap Movement Settings")]
    [SerializeField] private float leapImpulse;
    [SerializeField] private float leapInterval;
    [SerializeField] private float nextLeap = 0;

    protected override void HandleDeathEffect()
    {
        movementType = MovementTypes.stationary; // We don't want it to keep moving
    }

    private void AttackTarget()
    {
        Vector3 attackDirection = target - transform.position;
        attackDirection.y = 0;
        equippedWeapon.Attack(transform.position, attackDirection);
    }

    private int GetRandomSign()
    {
        return Random.Range(0, 2) == 0 ? -1 : 1;
    }

    private void HandleFollowMovement()
    {
        Vector3 direction = target - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void HandleLeapingMovement()
    {
        // Allow leap respecting the leap interval
        if (nextLeap < Time.fixedTime)
        {
            // We redefine when the next leap will be available
            nextLeap = Time.fixedTime + leapInterval;
            Vector3 leapDirection = new Vector3(GetRandomSign(), 0, GetRandomSign());
            rb.AddForce(leapDirection.normalized * leapImpulse, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (rb && movementType == MovementTypes.followPlayer)
            HandleFollowMovement();
    }

    private void Update()
    {
        target = FindObjectOfType<PlayerCharacter>().gameObject.transform.position;

        if (rb && movementType == MovementTypes.leaping)
            HandleLeapingMovement();

        if (!IsDead() && equippedWeapon && target.magnitude > 0)
            AttackTarget();
    }
}
