using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    [SerializeField]
    private int maxAmmo;
    private int currentAmmo;

    /// <summary>
    /// Amount of bullets shot at once
    /// </summary>
    [SerializeField]
    protected int bulletsPerShot;

    /// <summary>
    /// Amount of bullets that can be shot per second
    /// </summary>
    [SerializeField]
    protected float fireRate;

    /// <summary>
    /// Angle in which bullets are shot, can be set to simulate recoil and also shotgun spread adding more bulletsPerShot
    /// </summary>
    [SerializeField]
    protected float spreadAngle;

    /// <summary>
    /// Whether the weapon shotos automatically when holding click or not
    /// </summary>
    [SerializeField]
    protected bool automatic;

    public override abstract void Attack(Vector3 origin, Vector3 target);
}
