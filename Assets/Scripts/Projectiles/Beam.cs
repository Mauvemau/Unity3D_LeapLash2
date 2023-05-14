using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    protected float timeShot;

    [Header("Event Listeners")]
    [SerializeField] private BeamEventChannel shootBulletChannel;

    [Header("Debug")]
    [SerializeField] private LineRenderer beamLine;
    [SerializeField] private float damage;

    IEnumerator HandleLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void HandleBeamHit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }

    protected virtual void OnShot(BeamContainer settings)
    {
        if (beamLine != null)
        {
            damage = settings.damage;
            beamLine.SetPosition(0, transform.position);
            RaycastHit hit;

            if(Physics.Raycast(transform.position, settings.direction, out hit, settings.range))
            {
                beamLine.SetPosition(1, hit.point);
                HandleBeamHit(hit.collider);
            }
            else
            {
                beamLine.SetPosition(1, transform.position + (settings.direction * settings.range));
            }
        }
        if(settings.lifeTime > 0)
            StartCoroutine(HandleLifeTime(settings.lifeTime));

        if (shootBulletChannel != null)
            shootBulletChannel.OnEventRaised -= OnShot;
    }

    private void OnValidate()
    {
        beamLine ??= GetComponent<LineRenderer>();
    }

    private void Awake()
    {
        if (!GetComponent<LineRenderer>())
            Debug.LogError($"{name}: {nameof(beamLine)} is null!");
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
