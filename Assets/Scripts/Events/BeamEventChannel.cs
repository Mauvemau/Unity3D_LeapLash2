using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event Channel for beams
/// </summary>
[CreateAssetMenu(menuName = "Events/Beam Channel")]
//TODO: OOP - Should inherit from a generic class EventChannel<T> and receive the type by the inheritor
public class BeamEventChannel : ScriptableObject
{
    public UnityAction<BeamContainer> OnEventRaised;

    public void RaiseEvent(BeamContainer beamToBeShot)
    {
        OnEventRaised?.Invoke(beamToBeShot);
    }
}