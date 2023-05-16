using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudPanelPrompt : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool disabledByDefault;

    [Header("Event Listeners")]
    [SerializeField] private BoolEventChannel togglePromptChannel;

    /// <summary>
    /// Toggles on and off the current panel.
    /// </summary>
    /// <param name="toggle"> Enabled/Disabled </param>
    private void TogglePrompt(bool toggle)
    {
        Image panelImage = GetComponent<Image>();
        TextMeshProUGUI[] children = GetComponentsInChildren<TextMeshProUGUI>(true);
        // Disable the image of the main panel. (We're assuming the script is directly attached to the panel here.)
        if(panelImage != null)
        {
            panelImage.enabled = toggle;
        }
        // Now we disable all children, assuming the text and everything is inside the panel as children.
        foreach(TextMeshProUGUI obj in children)
        {
            obj.enabled = toggle;
        }
    }

    private void Awake()
    {
        if (disabledByDefault)
        {
            TogglePrompt(false);
        }
    }

    private void OnEnable()
    {
        // If inside a prompt panel, message must be sent after toggling the prompt.
        if (togglePromptChannel != null)
            togglePromptChannel.OnEventRaised += TogglePrompt;
    }

    private void OnDisable()
    {
        if (togglePromptChannel != null)
            togglePromptChannel.OnEventRaised -= TogglePrompt;
    }
}
