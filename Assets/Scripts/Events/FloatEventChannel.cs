using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Float Channel for Events
/// </summary>
[CreateAssetMenu(menuName = "Events/Float Channel")]
public class FloatEventChannel : ScriptableObject
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float value)
    {
        OnEventRaised?.Invoke(value);
    }
}
