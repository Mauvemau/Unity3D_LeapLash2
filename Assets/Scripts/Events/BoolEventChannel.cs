using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Bool Channel for Events
/// </summary>
[CreateAssetMenu(menuName = "Events/Bool Channel")]
public class BoolEventChannel : ScriptableObject
{
    public UnityAction<bool> OnEventRaised;

    public void RaiseEvent(bool value)
    {
        OnEventRaised?.Invoke(value);
    }
}
