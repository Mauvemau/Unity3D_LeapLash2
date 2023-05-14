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

    IEnumerator HandleLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    protected virtual void OnShot(BeamContainer settings)
    {
        if (beamLine != null)
        {
            beamLine.SetPosition(0, transform.position);
            RaycastHit hit;

            if(Physics.Raycast(transform.position, settings.direction, out hit, settings.range))
            {
                beamLine.SetPosition(1, hit.point);
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
