using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    /// <summary>
    /// Amount of bullets shot at once
    /// </summary>
    [SerializeField]
    protected int bulletsPerShot;

    /// <summary>
    /// Angle in which bullets are shot, can be set to simulate recoil and also shotgun spread adding more bulletsPerShot
    /// </summary>
    [SerializeField]
    protected float spreadAngle;

    public override abstract void Attack(Vector3 origin, Vector3 target);
}
