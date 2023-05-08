using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBulletWeapon : RangedWeapon
{
    [Header("Event Broadcasters")]
    [SerializeField] private Vector3EventChannel shootBulletChannel;

    public override void Attack(Vector3 origin, Vector3 target)
    {
        if (shootBulletChannel)
        {
            // Debug.Log(nextAttack +"/"+ Time.fixedTime);
            // Allow attack respecting the attack rate
            if (nextAttack < Time.fixedTime)
            {
                // We redefine when the next attack will be available
                nextAttack = Time.fixedTime + attackRate;

                // We request the pool to activate a bullet
                PoolManager.Instance.CreateObject("PlayerBullets", origin, Vector3.zero, new Vector3(.25f, .25f, .25f));

                // We take aim

                // BOOM!
                shootBulletChannel.RaiseEvent(target);
            }
        }
        else
        {
            Debug.LogWarning("No event channel set for ["+ GetType().Name +"]"+  weaponName +".");
        }
    }
}
