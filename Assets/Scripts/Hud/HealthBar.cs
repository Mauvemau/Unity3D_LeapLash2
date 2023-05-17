using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Slider slider;

    [Header("Event Listeners")]
    [SerializeField] private FloatEventChannel updateHealthBarChannel;

    private void UpdateValue(float value)
    {
        if(slider != null)
            slider.value = value;
    }

    private void OnValidate()
    {
        slider = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        if (updateHealthBarChannel != null)
            updateHealthBarChannel.OnEventRaised += UpdateValue;
    }

    private void OnDisable()
    {
        if (updateHealthBarChannel != null)
            updateHealthBarChannel.OnEventRaised -= UpdateValue;
    }
}
