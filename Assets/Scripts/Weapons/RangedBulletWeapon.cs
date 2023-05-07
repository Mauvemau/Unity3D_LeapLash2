using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBulletWeapon : RangedWeapon
{
    /// <summary>
    /// Speed in which the bullets will be shot
    /// </summary>
    [SerializeField]
    private float bulletSpeed;

    [Header("Event Broadcasters")]
    [SerializeField] private Vector3EventChannel shootBulletChannel;

    public override void Attack(Vector3 origin, Vector3 target)
    {
        if (shootBulletChannel)
        {
            // We request the pool to activate a bullet.
            PoolManager.Instance.CreateObject("PlayerBullets", origin, Vector3.zero, new Vector3(.25f, .25f, .25f));

            // We take aim


            // BOOM!
            shootBulletChannel.RaiseEvent(Vector3.zero);
        }
        else
        {
            Debug.LogWarning("No event channel set for ["+ GetType().Name +"]"+  weaponName +".");
        }
    }
}
