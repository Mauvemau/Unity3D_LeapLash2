using UnityEngine;

/// <summary>
/// Container for beam data, mainly used to send values between the weapon and the beam
/// </summary>
public class BeamContainer : ScriptableObject
{
    /// <summary>
    /// Direction in which the beam is being shot
    /// </summary>
    public Vector3 direction;
    /// <summary>
    /// Damage the beam will deal to it's target
    /// </summary>
    public float damage;
    /// <summary>
    /// Amount of time the beam stays active after being shot
    /// </summary>
    public float lifeTime;
    /// <summary>
    /// Length of the beam
    /// </summary>
    public float range;
}
