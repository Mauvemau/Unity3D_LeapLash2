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
    [SerializeField] private Transform target;

    [Header("Equipment")]
    [SerializeField] private Weapon equippedWeapon;

    [Header("Movement")]
    [SerializeField] MovementTypes movementType;
    private MovementTypes aux;

    [Header("Follow Movement Settings")]
    [SerializeField] private float speed;

    [Header("Leap Movement Settings")]
    [SerializeField] private float leapImpulse;
    [SerializeField] private float leapInterval;
    [SerializeField] private float nextLeap = 0;

    [Header("Misc")]
    [SerializeField] private float corpseDisappearDelay;

    // Attacking
    private bool canAttack = true;

    private IEnumerator HandleAttackRate()
    {
        canAttack = false;
        yield return new WaitForSeconds(equippedWeapon.GetAttackRate());
        canAttack = true;
    }
    //

    private IEnumerator HandleCorpseCleaning()
    {
        yield return new WaitForSeconds(corpseDisappearDelay);
        gameObject.SetActive(false);
    }

    protected override void HandleDeathEffect()
    {
        movementType = MovementTypes.stationary; // We don't want it to keep moving
        StartCoroutine(HandleCorpseCleaning());
    }

    /// <summary>
    /// Attacks towards the target with the equipped weapon.
    /// </summary>
    private void AttackTarget()
    {
        if (canAttack)
        {
            StartCoroutine(HandleAttackRate());
            Vector3 attackDirection = target.position - transform.position;
            attackDirection.y = 0;
            equippedWeapon.Attack(transform.position, attackDirection);
        }
    }

    /// <summary>
    /// Returns either +1 or -1.
    /// </summary>
    /// <returns></returns>
    private int GetRandomSign()
    {
        return Random.Range(0, 2) == 0 ? -1 : 1;
    }

    /// <summary>
    /// Handles follow movement behaviour.
    /// </summary>
    private void HandleFollowMovement()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    /// <summary>
    /// Handles the leap movement behaviour.
    /// </summary>
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
        if (rb != null && movementType == MovementTypes.followPlayer)
            HandleFollowMovement();
    }

    private void Update()
    {
        if (rb != null && movementType == MovementTypes.leaping)
            HandleLeapingMovement();

        if (!IsDead() && equippedWeapon)
            AttackTarget();
    }

    protected override void _Awake()
    {
        target = MyGameManager.Instance.getPlayerTransform();
        aux = movementType;
    }

    protected override void _OnEnable()
    {
        target = MyGameManager.Instance.getPlayerTransform();
        nextLeap = 0;
        canAttack = true;
        movementType = aux;
    }
}
