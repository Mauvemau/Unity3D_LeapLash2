using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    [Header("Equipment")]
    [SerializeField] private Weapon equippedWeapon;

    [Header("Event Listeners")]
    [SerializeField] private Vector3EventChannel movementChannel;
    [SerializeField] private Vector2EventChannel attackChannel;

    [Header("Movement")]
    [SerializeField] protected float speed;
    protected Vector3 _currentMovement;

    protected override void HandleDeathEffect()
    {
        // Todo
    }

    private void OnMove(Vector3 direction)
    {
        _currentMovement.x = direction.x;
        _currentMovement.z = direction.z;
    }

    private void OnAttack(Vector2 direction)
    {
        if (equippedWeapon != null)
        {
            Vector3 attackDirection = new Vector3(direction.x, 0f, direction.y);
            equippedWeapon.Attack(transform.position, attackDirection);
        }
    }

    private void FixedUpdate()
    {
        if (rb)
        {
            transform.Translate(speed * Time.deltaTime * _currentMovement);
        }
    }

    private void OnEnable()
    {
        if (movementChannel != null)
            movementChannel.OnEventRaised += OnMove;
        if(attackChannel != null)
            attackChannel.OnEventRaised += OnAttack;
    }

    private void OnDisable()
    {
        if (movementChannel != null)
            movementChannel.OnEventRaised -= OnMove;
        if (attackChannel != null)
            attackChannel.OnEventRaised -= OnAttack;
    }
}
