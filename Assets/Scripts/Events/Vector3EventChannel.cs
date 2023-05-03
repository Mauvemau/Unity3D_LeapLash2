using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Vector2 Channel for Events
/// </summary>
[CreateAssetMenu(menuName = "Events/Vector2 Channel for Events")]
public class Vector3EventChannel : ScriptableObject
{
    public UnityAction<Vector3> OnEventRaised;

    public void RaiseEvent(Vector3 value)
    {
        OnEventRaised?.Invoke(value);
    }
}
