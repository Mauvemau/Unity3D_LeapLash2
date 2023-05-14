using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float lifeTime;
    [SerializeField] protected float range;
    protected float timeShot;

    [Header("Event Listeners")]
    [SerializeField] private Vector3EventChannel shootBulletChannel;

    [Header("Debug")]
    [SerializeField] private LineRenderer beamLine;

    IEnumerator HandleLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    protected virtual void OnShot(Vector3 direction)
    {
        if (beamLine != null)
        {
            beamLine.SetPosition(0, transform.position);
            RaycastHit hit;

            if(Physics.Raycast(transform.position, direction, out hit, range))
            {
                beamLine.SetPosition(1, hit.point);
            }
            else
            {
                beamLine.SetPosition(1, transform.position + (direction * range));
            }
        }
        StartCoroutine(HandleLifeTime());

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
