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
        //TODO: Fix - Cache value/s
        //TODO: Fix - Add [RequireComponentAttribute]
        Image panelImage = GetComponent<Image>();
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>(true);
        Button[] buttons = GetComponentsInChildren<Button>(true);
        Image[] images = GetComponentsInChildren<Image>(true);

        //TODO: Fix - Remove redundant comments
        // Disable the image of the main panel. (We're assuming the script is directly attached to the panel here.)
        if (panelImage != null)
        {
            panelImage.enabled = toggle;
        }
        // Now we disable all children texts, assuming the text and everything is inside the panel as children.
        foreach(TextMeshProUGUI obj in texts)
        {
            obj.enabled = toggle;
        }
        // Same for buttons.
        foreach(Button obj in buttons)
        {
            obj.enabled = toggle;
        }
        // And images...
        foreach (Image obj in images)
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
