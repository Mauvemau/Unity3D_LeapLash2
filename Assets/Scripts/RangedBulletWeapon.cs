using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBulletWeapon : RangedWeapon
{
    /// <summary>
    /// Bullet lifetime
    /// </summary>
    [SerializeField]
    private int bulletLifetime;

    /// <summary>
    /// Speed in which the bullets will be shot
    /// </summary>
    [SerializeField]
    private float bulletSpeed;



    public int GetBulletLifetime()
    {
        return bulletLifetime;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

}
