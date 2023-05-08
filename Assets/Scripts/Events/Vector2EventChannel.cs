using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Vector2 Channel for Events
/// </summary>
[CreateAssetMenu(menuName = "Events/Vector2 Channel")]
public class Vector2EventChannel : ScriptableObject
{
    public UnityAction<Vector2> OnEventRaised;

    public void RaiseEvent(Vector2 value)
    {
        OnEventRaised?.Invoke(value);
    }
}
