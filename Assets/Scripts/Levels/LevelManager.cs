using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [Header("Debug")]
    [SerializeField] private List<Level> levels;
    [SerializeField] private int currentLevel;

    [Header("Event Broadcasters")]
    [SerializeField] private BoolEventChannel lockDoorsChannel;

    /// <summary>
    /// Called to abort execution, for example when changing scenes.
    /// </summary>
    public void Reset()
    {
        levels.Clear();
        currentLevel = 0;
    }

    /// <summary>
    /// Locks or Unlocks all doors
    /// </summary>
    /// <param name="value"> Lock/Unlock </param>
    public void SetDoorsLocked(bool value)
    {
        if(lockDoorsChannel)
            lockDoorsChannel.RaiseEvent(value);
    }

    /// <summary>
    /// Moves the player to the next stage/room/level.
    /// </summary>
    public void AdvanceStage()
    {
        (int currentRoom, int amountRooms) = levels[currentLevel].GetProgress();
        if(currentRoom < amountRooms)
        {
            levels[currentLevel].AdvanceRoom();
        }
        else
        {
            currentLevel++;
            if (currentLevel > levels.Count - 1)
            {
                Debug.LogError($"{name}: There is no next Level!");
            }
            else
            {
                levels[currentLevel].Initialize();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // The developer can choose the sorting of levels in the unity editor, the level at the top will be the first, and so on.
        // So the sorting doesn't get messed up on validate, we do the following;
        Level[] levelsDetected = FindObjectsOfType<Level>();

        foreach (Level level in levelsDetected)
        {
            if (level.GetComponent<Level>() != null && !levels.Contains(level))
            {
                levels.Add(level);
            }
        }

        currentLevel = 0;
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
