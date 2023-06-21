using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private enum LevelProgression
    {
        inOrder = 0,
        random
    }

    [Header("Settings")]
    [SerializeField] private LevelProgression levelProgressionType;

    [Header("Random Progression Settings")]
    [Range(1, 10)]
    [SerializeField] private int amountOfStages = 1;

    [Header("Level Order")]
    [SerializeField] private List<Room> rooms;

    [Header("Debug")]
    [SerializeField] private int currentRoom;

    /// <summary>
    /// Returns the current room/stage and the amount of rooms/stages.
    /// </summary>
    /// <returns></returns>
    public (int currentRoom, int amountRooms) GetProgress()
    { 
        return (currentRoom, amountOfStages - 1);
    }

    /// <summary>
    /// Advances to the next room/stage of the level.
    /// </summary>
    public void AdvanceRoom()
    {
        currentRoom++;
        if (currentRoom > amountOfStages - 1) 
        {
            Debug.LogError($"{name}: There is no next room!");
        }
        else
        {
            //TODO: TP2 - Strategy
            if (levelProgressionType == LevelProgression.inOrder)
            {
                rooms[currentRoom].Initialize();
            }
            else
            {
                rooms[Random.Range(0, rooms.Count - 1)].Initialize();
            }
        }
    }

    /// <summary>
    /// Teleports the player to the first room of the level and starts it.
    /// </summary>
    public void Initialize()
    {
        if (rooms.Count < 1)
        {
            Debug.LogError($"{name}: There is no rooms in the level!");
        }
        else
        {
            Debug.Log("Initializing level...");
            currentRoom = 0;
            rooms[currentRoom].Initialize();
        }
    }

    private void Awake()
    {
        if (rooms.Count < 1)
        {
            Debug.LogWarning($"{name}: There is no rooms in the level!");
        }
    }

    private void OnValidate()
    {
        Room[] roomsDetected = GetComponentsInChildren<Room>();

        foreach (Room room in roomsDetected)
        {
            if (room.GetComponent<Room>() != null && !rooms.Contains(room))
            {
                rooms.Add(room);
            }
        }

        if(levelProgressionType == LevelProgression.inOrder)
        {
            amountOfStages = rooms.Count;
        }
    }
}
