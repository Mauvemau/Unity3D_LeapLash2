using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Event Listeners")]
    [SerializeField] private BulletEventChannel shootBulletChannel;

    [Header("Debug")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float damage;
    [SerializeField] private TrailRenderer tr;

    //TODO: Fix - Repeated code
    /// <summary>
    /// De-activates the bullet after a set time
    /// </summary>
    /// <param name="lifeTime"> Bullet will de-activate after this amount of time in seconds </param>
    /// <returns></returns>
    private IEnumerator HandleLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Handles the shooting of the bullet
    /// </summary>
    /// <param name="settings"> Data container, containing the settings of the bullet </param>
    protected virtual void OnShot(BulletContainer settings)
    {
        if (rb != null)
        {
            damage = settings.damage;
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

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
    }

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
            Debug.LogError($"{name}: {nameof(rb)} is null!");
        else
            rb.useGravity = false;
    }

    private void OnEnable()
    {
        if (shootBulletChannel != null)
            shootBulletChannel.OnEventRaised += OnShot;
        if(tr != null)
        {
            tr.Clear();
            tr.emitting = true;
        }
    }

    private void OnDisable()
    {
        if (shootBulletChannel != null)
            shootBulletChannel.OnEventRaised -= OnShot;
        if (tr != null)
        {
            tr.Clear();
            tr.emitting = false;
        }
    }
}
