using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviourSingleton<MyGameManager>
{
    [Header("Debug")]
    [SerializeField] private PlayerCharacter player;

    public void HealPlayer(float amount)
    {
        player.TakeDamage(-amount);
    }

    //TODO: Fix - Unclear name
    public void HealPlayer()
    {
        //TODO: OOP - Player Should have a method to fully heal
        player.TakeDamage(-999);
    }

    /// <summary>
    /// Returns the current altitude of the player.
    /// </summary>
    /// <returns></returns>
    public Transform getPlayerTransform()
    {
        return player.transform;
    }

    /// <summary>
    /// Teleports the player to specific position.
    /// </summary>
    /// <param name="position"></param>
    public void TeleportPlayer(Vector3 position)
    {
        player.Teleport(position);
    }

    public int GetActiveEnemiesCount()
    {
        //TODO: Fix - Too under-performant. List should be cached and be updated via events
        return FindObjectsOfType<EnemyCharacter>().Length;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game")
            player = FindObjectOfType<PlayerCharacter>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
