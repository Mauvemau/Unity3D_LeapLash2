using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [Header("Debug")]
    [SerializeField] private List<Level> levels;
    [SerializeField] private int currentLevel;



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

    /// <summary>
    /// Initializes the first level of the game.
    /// </summary>
    private void Initialize()
    {
        currentLevel = 0;
        levels[currentLevel].Initialize();
    }

    protected override void SingletonAwakened()
    {
        if (levels.Count < 1)
        {
            Debug.LogWarning($"{name}: There is no levels in the scene!");
        }
        else
        {
            Initialize();
        }
    }

    private void OnValidate()
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
    }
}
