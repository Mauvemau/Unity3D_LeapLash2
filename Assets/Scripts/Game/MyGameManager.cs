using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviourSingleton<MyGameManager>
{
    [Header("Debug")]
    [SerializeField] private PlayerCharacter player;

    /// <summary>
    /// Returns the current altitude of the player.
    /// </summary>
    /// <returns></returns>
    public float getPlayerAltitude()
    {
        return player.transform.position.y;
    }

    /// <summary>
    /// Teleports the player to specific position.
    /// </summary>
    /// <param name="position"></param>
    public void TeleportPlayer(Vector3 position)
    {
        player.Teleport(position);
    }

    private void OnValidate()
    {
        player ??= FindObjectOfType<PlayerCharacter>();
    }
}
