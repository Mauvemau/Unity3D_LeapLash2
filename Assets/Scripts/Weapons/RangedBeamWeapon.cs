using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBeamWeapon : RangedWeapon
{
    /// <summary>
    /// The amount of time the beam stays active.
    /// </summary>
    [SerializeField]
    private float lifetime;
    /// <summary>
    /// Lenght of the beam
    /// </summary>
    [SerializeField]
    private float range;

    [Header("Event Broadcasters")]
    [SerializeField] private Vector3EventChannel shootBeamChannel;

    public override void Attack(Vector3 origin, Vector3 target)
    {
        if (shootBeamChannel)
        {
            // Allow attack respecting the attack rate
            if (nextAttack < Time.fixedTime)
            {
                // We redefine when the next attack will be available
                nextAttack = Time.fixedTime + attackRate;

                // For each bullet per shot we do the following:
                for (int i = 0; i < projectilesPerShot; i++)
                {
                    // We request the pool to activate a bullet
                    PoolManager.Instance.CreateObject("PlayerBeams", origin, Vector3.zero, new Vector3(.25f, .25f, .25f));

                    // We take aim
                    Vector3 direction = GetProjectileDirection(origin, target);

                    // BOOM!
                    shootBeamChannel.RaiseEvent(direction);
                }
            }
        }
        else
        {
            Debug.LogWarning("No event channel set for [" + GetType().Name + "]" + weaponName + ".");
        }
    }
}
