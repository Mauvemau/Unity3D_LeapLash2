using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Beam Weapon")]
public class RangedBeamWeapon : RangedWeapon
{
    /// <summary>
    /// The amount of time the beam stays active after being shot
    /// </summary>
    [SerializeField]
    private float lifetime;
    /// <summary>
    /// Length of the beam
    /// </summary>
    [SerializeField]
    private float range;

    [Header("Event Broadcasters")]
    [SerializeField] private BeamEventChannel shootBeamChannel;

    /// <summary>
    /// Handles the activation of beam as well as providing them with the required data for them to be shot
    /// </summary>
    /// <param name="origin"> Position from where the beam starts </param>
    /// <param name="target"> Direction where the beam is casted towards </param>
    public override void Attack(Vector3 origin, Vector3 target)
    {
        if (shootBeamChannel)
        {
            // For each beam per shot we do the following:
            for (int i = 0; i < projectilesPerShot; i++)
            {
                // We request the pool to activate a beam
                PoolManager.Instance.CreateObject(poolToRequest, origin, Vector3.zero, new Vector3(.25f, .25f, .25f));

                // We take aim
                Vector3 direction = GetProjectileDirection(target);

                // We create a beam setting to send
                BeamContainer beamSettings = ScriptableObject.CreateInstance<BeamContainer>();
                beamSettings.direction = direction;
                beamSettings.damage = damage;
                beamSettings.lifeTime = lifetime;
                beamSettings.range = range;

                shootBeamChannel.RaiseEvent(beamSettings);
            }
        }
        else
        {
            Debug.LogWarning("No event channel set for [" + GetType().Name + "]" + weaponName + ".");
        }
    }
}
