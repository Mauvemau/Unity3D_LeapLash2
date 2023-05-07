using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float lifeTime;

    [Header("Event Listeners")]
    [SerializeField] private Vector3EventChannel shootBulletChannel;

    protected virtual void OnShot(Vector3 direction)
    {
        Debug.Log("Bullet shot towards "+ direction +".");

        // For a normal bullet, once it's been shot, we unsubscribe from the event.
        if (shootBulletChannel != null)
            shootBulletChannel.OnEventRaised -= OnShot;
    }

    private void OnEnable()
    {
        if (shootBulletChannel != null)
            shootBulletChannel.OnEventRaised += OnShot;
    }

    private void OnDisable()
    {
        if (shootBulletChannel != null)
            shootBulletChannel.OnEventRaised -= OnShot;
    }
}
