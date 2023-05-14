using UnityEngine;

/// <summary>
/// Container for bullet data, mainly used to send values between the weapon and the bullet
/// </summary>
public class BulletContainer : ScriptableObject
{
    /// <summary>
    /// Direction in which the bullet is being shot
    /// </summary>
    public Vector3 direction;
    /// <summary>
    /// Damage the bullet will deal to it's target
    /// </summary>
    public float damage;
    /// <summary>
    /// Amount of time the bullet stays active after being shot
    /// </summary>
    public float lifeTime;
    /// <summary>
    /// Speed in which the bullet moves
    /// </summary>
    public float speed;
}
