using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 _currentMovement;

    [Header("Equipment")]
    [SerializeField] private Weapon equipedWeapon;

    [Header("Event Listeners")]
    [SerializeField] private Vector3EventChannel movementChannel;
    [SerializeField] private VoidEventChannel attackChannel;

    private void OnMove(Vector3 direction)
    {
        _currentMovement.x = direction.x;
        _currentMovement.z = direction.z;
    }

    private void OnAttack()
    {
        equipedWeapon.Attack(transform.position, Vector3.zero);
    }

    private void FixedUpdate()
    {
        if (GetComponent<Rigidbody>())
        {
            transform.Translate(speed * Time.deltaTime * _currentMovement);
        }
    }

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
            Debug.LogError($"{name}: {nameof(rb)} is null!");
        if (speed == 0)
            Debug.LogWarning($"{name}: Speed is set to 0!");
    }

    private void OnValidate()
    {
        rb ??= GetComponent<Rigidbody>();
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
