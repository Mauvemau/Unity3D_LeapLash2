using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    /// <summary>
    /// In case we need it, the name of the pool we want to request our projectiles from.
    /// </summary>
    [SerializeField]
    protected string poolToRequest;
    /// <summary>
    /// Amount of projectiles shot at once
    /// </summary>
    [SerializeField]
    protected int projectilesPerShot;

    /// <summary>
    /// Angle in which projectiles are shot, can be set to simulate recoil and also shotgun spread adding more projectilesPerShot
    /// </summary>
    [SerializeField]
    protected float spreadAngle;

    protected Vector3 GetProjectileDirection(Vector3 origin, Vector3 target)
    {
        Vector3 direction;
        direction = target.normalized;
    
        if(spreadAngle != 0)
        {
            Quaternion rotationModifier = Quaternion.Euler(0f, Random.Range(-spreadAngle, spreadAngle), 0f);
            direction = rotationModifier * direction;
        }

        return direction;
    }

    public override abstract void Attack(Vector3 origin, Vector3 target);
}
