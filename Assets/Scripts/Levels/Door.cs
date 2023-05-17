using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private BoxCollider coll;

    private enum DoorTypes
    {
        top = 0,
        left,
        bottom,
        right
    }

    [Header("Settings")]
    [SerializeField] DoorTypes doorType = DoorTypes.top;

    [Header("Debug")]
    [SerializeField] Vector3 spawnOffset;

    [Header("Event Listeners")]
    [SerializeField] private BoolEventChannel lockDoorsChannel;

    /// <summary>
    /// Locks the door.
    /// </summary>
    private void SetLocked(bool locked)
    {
        if (locked)
        {
            coll.isTrigger = false;
        }
        else
        {
            coll.isTrigger = true;
        }
    }

    /// <summary>
    /// Where the player is supposed to be teleported at.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetOffsetPosition()
    {
        return (transform.position + spawnOffset);
    }

    private void Awake()
    {
        if (!GetComponent<BoxCollider>())
        {
            Debug.LogError($"{name}: {nameof(coll)} is null!");
        }
        coll.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            LevelManager.Instance.AdvanceStage();
        }
    }

    private void OnValidate()
    {
        coll = GetComponent<BoxCollider>();

        switch (doorType)
        {
            case DoorTypes.top:
                spawnOffset = new Vector3(0f, 1f, -2f);
                break;
            case DoorTypes.left:
                spawnOffset = new Vector3(2f, 1f, 0f);
                break;
            case DoorTypes.bottom:
                spawnOffset = new Vector3(0f, 1f, 2f);
                break;
            case DoorTypes.right:
                spawnOffset = new Vector3(-2f, 1f, 0f);
                break;
            default:
                spawnOffset = new Vector3(0f, 0f, 0f);
                break;
        }
    }

    private void OnEnable()
    {
        if (lockDoorsChannel != null)
            lockDoorsChannel.OnEventRaised += SetLocked;
    }

    private void OnDisable()
    {
        if (lockDoorsChannel != null)
            lockDoorsChannel.OnEventRaised -= SetLocked;
    }
}
