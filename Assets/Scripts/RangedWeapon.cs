using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField]
    private int maxAmmo;
    private int currentAmmo;

    /// <summary>
    /// Amount of bullets shot at once
    /// </summary>
    [SerializeField]
    private int bulletsPerShot;

    /// <summary>
    /// Amount of bullets that can be shot per second
    /// </summary>
    [SerializeField]
    private float fireRate;

    /// <summary>
    /// Angle in which bullets are shot, can be set to simulate recoil and also shotgun spread adding more bulletsPerShot
    /// </summary>
    [SerializeField]
    private float spreadAngle;

    /// <summary>
    /// Whether the weapon shotos automatically when holding click or not
    /// </summary>
    [SerializeField]
    private bool automatic;

    public int GetBulletsPerShot()
    {
        return bulletsPerShot;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetSpreadAngle()
    {
        return spreadAngle;
    }

    public bool IsAutomatic()
    {
        return automatic;
    }
}
