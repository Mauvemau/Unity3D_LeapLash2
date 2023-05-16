using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// String Channel for Events
/// </summary>
[CreateAssetMenu(menuName = "Events/String Channel")]
public class StringEventChannel : ScriptableObject
{
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string value)
    {
        OnEventRaised?.Invoke(value);
    }
}

