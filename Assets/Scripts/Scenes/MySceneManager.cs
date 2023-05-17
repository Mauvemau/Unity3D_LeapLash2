using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviourSingleton<MySceneManager>
{
    [Header("Event Listeners")]
    [SerializeField] private StringEventChannel loadSceneChannel;
    [SerializeField] private VoidEventChannel closeGameChannel;

    private void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

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
        closeGameChannel.OnEventRaised += CloseGame;
    }

    private void OnDisable()
    {
        loadSceneChannel.OnEventRaised -= LoadScene;
        closeGameChannel.OnEventRaised -= CloseGame;
    }
}
