using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBulletWeapon : RangedWeapon
{
    /// <summary>
    /// The amount of time a bullet stays active after being shot.
    /// </summary>
    [SerializeField]
    private float bulletLifetime;

    [Header("Event Broadcasters")]
    [SerializeField] private Vector3EventChannel shootBulletChannel;

    public override void Attack(Vector3 origin, Vector3 target)
    {
        if (shootBulletChannel)
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
                    PoolManager.Instance.CreateObject("PlayerBullets", origin, Vector3.zero, new Vector3(.25f, .25f, .25f));

                    // We take aim
                    Vector3 direction = GetProjectileDirection(origin, target);

                    // BOOM!
                    shootBulletChannel.RaiseEvent(direction);
                }
            }
        }
        else
        {
            Debug.LogWarning("No event channel set for ["+ GetType().Name +"]"+  weaponName +".");
        }
    }
}
