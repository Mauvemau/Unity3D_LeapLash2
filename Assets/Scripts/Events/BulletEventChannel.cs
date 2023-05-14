using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event Channel for bullets
/// </summary>
[CreateAssetMenu(menuName = "Events/Bullet Channel")]
public class BulletEventChannel : ScriptableObject
{
    public UnityAction<BulletContainer> OnEventRaised;

    public void RaiseEvent(BulletContainer bulletToBeShot)
    {
        OnEventRaised?.Invoke(bulletToBeShot);
    }
}
