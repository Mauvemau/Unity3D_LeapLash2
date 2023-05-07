using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBeamWeapon : RangedWeapon
{
    /// <summary>
    /// Lenght of the beam
    /// </summary>
    [SerializeField]
    private float range;

    public override void Attack(Vector3 origin, Vector3 target)
    {
        Debug.Log("Attacked");
    }
}
