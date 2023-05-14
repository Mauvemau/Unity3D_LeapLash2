using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event Channel for beams
/// </summary>
[CreateAssetMenu(menuName = "Events/Beam Channel")]
public class BeamEventChannel : ScriptableObject
{
    public UnityAction<BeamContainer> OnEventRaised;

    public void RaiseEvent(BeamContainer beamToBeShot)
    {
        OnEventRaised?.Invoke(beamToBeShot);
    }
}