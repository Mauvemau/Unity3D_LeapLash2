using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Void channel for Events
/// </summary>
[CreateAssetMenu(menuName = "Events/Void Channel")]
public class VoidEventChannel : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
