using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviourSingleton<MySceneManager>
{
    [Header("Event Listeners")]
    [SerializeField] private StringEventChannel loadSceneChannel;

    private void LoadScene(string sceneName)
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            LevelManager.Instance.Reset();
            SpawnManager.Instance.Reset();
        }
        SceneManager.LoadScene(sceneName);
    }

    private void OnEnable()
    {
        loadSceneChannel.OnEventRaised += LoadScene;
    }

    private void OnDisable()
    {
        loadSceneChannel.OnEventRaised -= LoadScene;
    }
}
