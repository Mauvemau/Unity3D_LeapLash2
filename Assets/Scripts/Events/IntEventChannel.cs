using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Int Channel for Events
/// </summary>
[CreateAssetMenu(menuName = "Events/Int Channel")]
public class IntEventChannel : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}
