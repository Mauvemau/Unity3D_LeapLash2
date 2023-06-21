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
    [SerializeField] private VoidEventChannel interactChannel;
    [SerializeField] private WeaponEventChannel equipWeaponChannel;

    [Header("Event Broadcasters")]
    [SerializeField] private FloatEventChannel updateHealthBarChannel;
    [SerializeField] private BoolEventChannel toggleGameOverScreenChannel;

    [Header("Movement")]
    [SerializeField] protected float speed;
    protected Vector3 _currentMovement;

    [Header("Debug")]
    [SerializeField] private GameObject model;

    // Attacking
    private bool canAttack = true;

    //TODO: Fix - Repeated code
    private IEnumerator HandleAttackRate()
    {
        canAttack = false;
        yield return new WaitForSeconds(equippedWeapon.GetAttackRate());
        canAttack = true;
    }
    //

    protected override void HandleDeathEffect()
    {
        toggleGameOverScreenChannel.RaiseEvent(true);
    }

    protected override void UpdateHealthbar()
    {
        updateHealthBarChannel.RaiseEvent(healthPoints / maxHealthPoints);
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (equippedWeapon != null)
            LootManager.Instance.CreateWeaponDrop(equippedWeapon, transform.position);
        equippedWeapon = weapon;
    }

    /// <summary>
    /// Teleports towards a position.
    /// </summary>
    /// <param name="position"> Position to teleport towards </param>
    public void Teleport(Vector3 position)
    {
        Debug.Log("Teleporting to " + position);
        transform.position = position;
    }

    private void OnInteract()
    {
        if (LootManager.Instance.CanLoot())
        {
            LootManager.Instance.LootItem();
        }
    }

    private void OnMove(Vector3 direction)
    {
        _currentMovement.x = direction.x;
        _currentMovement.z = direction.z;
    }

    private void OnAttack(Vector2 direction)
    {
        if (canAttack && !IsDead() && equippedWeapon != null)
        {
            StartCoroutine(HandleAttackRate());
            Vector3 attackDirection = new Vector3(direction.x, 0f, direction.y);
            equippedWeapon.Attack(transform.position, attackDirection);
        }
    }

    private void FixedUpdate()
    {
        if (!IsDead() && rb)
        {
            transform.Translate(speed * Time.deltaTime * _currentMovement);
            if (model && _currentMovement.magnitude > 0)
            {
                model.transform.rotation = Quaternion.LookRotation(_currentMovement);
            }
        }
    }

    protected override void _OnValidate()
    {
        model = transform.GetChild(0).gameObject;
    }

    protected override void _OnEnable()
    {
        if (movementChannel != null)
            movementChannel.OnEventRaised += OnMove;
        if(attackChannel != null)
            attackChannel.OnEventRaised += OnAttack;
        if (interactChannel != null)
            interactChannel.OnEventRaised += OnInteract;
        if (equipWeaponChannel != null)
            equipWeaponChannel.OnEventRaised += EquipWeapon;
    }

    protected override void _OnDisable()
    {
        if (movementChannel != null)
            movementChannel.OnEventRaised -= OnMove;
        if (attackChannel != null)
            attackChannel.OnEventRaised -= OnAttack;
        if (interactChannel != null)
            interactChannel.OnEventRaised -= OnInteract;
        if (equipWeaponChannel != null)
            equipWeaponChannel.OnEventRaised -= EquipWeapon;
    }
}
