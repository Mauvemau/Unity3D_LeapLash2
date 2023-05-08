using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float lifeTime;
    [SerializeField] protected float speed;
    protected float timeShot;

    [Header("Event Listeners")]
    [SerializeField] private Vector3EventChannel shootBulletChannel;

    [Header("Debug")]
    [SerializeField] private Rigidbody rb;

    IEnumerator HandleLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    protected virtual void OnShot(Vector3 direction)
    {
        if (rb != null)
        {
            Vector3 velocity = direction * speed;
            velocity.y = rb.velocity.y;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

            StartCoroutine(HandleLifeTime());
        }

        // For a normal bullet, once it's been shot, we unsubscribe from the event.
        if (shootBulletChannel != null)
            shootBulletChannel.OnEventRaised -= OnShot;
    }

    private void OnValidate()
    {
        rb ??= GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
            Debug.LogError($"{name}: {nameof(rb)} is null!");
        if (speed == 0)
            Debug.LogWarning($"{name}: Speed is set to 0!");
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
