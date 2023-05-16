using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBulletWeapon : RangedWeapon
{
    /// <summary>
    /// The amount of time a bullet stays active after being shot.
    /// </summary>
    [SerializeField]
    private float lifetime;
    /// <summary>
    /// The speed of the bullet.
    /// </summary>
    [SerializeField]
    private float bulletSpeed;

    [Header("Event Broadcasters")]
    [SerializeField] private BulletEventChannel shootBulletChannel;

    /// <summary>
    /// Handles the activation of bullet as well as providing them with the required data for them to be shot
    /// </summary>
    /// <param name="origin"> Position to activate the bullet at </param>
    /// <param name="target"> Direction to shoot the bullet at </param>
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
                    PoolManager.Instance.CreateObject(poolToRequest, origin, Vector3.zero, new Vector3(.25f, .25f, .25f));

                    // We take aim
                    Vector3 direction = GetProjectileDirection(target);

                    // We create a bullet setting to send
                    BulletContainer bulletSettings = ScriptableObject.CreateInstance<BulletContainer>();
                    bulletSettings.direction = direction;
                    bulletSettings.damage = damage;
                    bulletSettings.lifeTime = lifetime;
                    bulletSettings.speed = bulletSpeed;

                    // We shoot
                    shootBulletChannel.RaiseEvent(bulletSettings);
                }
            }
        }
        else
        {
            Debug.LogWarning("No event channel set for ["+ GetType().Name +"]"+  weaponName +".");
        }
    }
}
