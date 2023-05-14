using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Event Listeners")]
    [SerializeField] private BulletEventChannel shootBulletChannel;

    [Header("Debug")]
    [SerializeField] private Rigidbody rb;
    private float timeShot;

    IEnumerator HandleLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    protected virtual void OnShot(BulletContainer settings)
    {
        if (rb != null)
        {
            Vector3 velocity = settings.direction * settings.speed;
            velocity.y = rb.velocity.y;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }
        // 0 = Will not destroy with time
        if(settings.lifeTime > 0)
            StartCoroutine(HandleLifeTime(settings.lifeTime));

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
