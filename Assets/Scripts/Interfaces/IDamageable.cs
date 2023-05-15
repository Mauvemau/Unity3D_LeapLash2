using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// Used for objects that take damage
    /// </summary>
    /// <param name="damageToTake"> Amount of health points to subtract </param>
    public void TakeDamage(float damageToTake);
}
