using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event Channel for weapons
/// </summary>
[CreateAssetMenu(menuName = "Events/Weapon Channel")]
public class WeaponEventChannel : ScriptableObject
{
    public UnityAction<Weapon> OnEventRaised;

    public void RaiseEvent(Weapon weapon)
    {
        OnEventRaised?.Invoke(weapon);
    }
}
