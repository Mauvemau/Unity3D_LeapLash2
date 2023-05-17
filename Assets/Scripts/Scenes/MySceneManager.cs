using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviourSingleton<MySceneManager>
{
    [Header("Event Listeners")]
    [SerializeField] private StringEventChannel loadSceneChannel;

    private LightingSettings CreateDefaultLightingSettings()
    {
        LightingSettings lightingSettings = new LightingSettings();

        // Baking settings
        lightingSettings.bakedGI = true;
        lightingSettings.realtimeGI = false;

        // Indirect lighting settings
        lightingSettings.indirectResolution = 2;
        lightingSettings.lightmapResolution = 10;

        // Ambient lighting settings
        lightingSettings.aoExponentIndirect = 1.2f;
        lightingSettings.aoExponentDirect = 1.5f;
        lightingSettings.aoMaxDistance = 5f;

        return lightingSettings;
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
    }

    private void OnDisable()
    {
        loadSceneChannel.OnEventRaised -= LoadScene;
    }
}
