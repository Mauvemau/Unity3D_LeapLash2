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

    public void HealPlayer()
    {
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
