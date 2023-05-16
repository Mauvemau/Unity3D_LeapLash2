using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudTextDisplay : MonoBehaviour
{
    [Header("Event Listeners")]
    [SerializeField] private StringEventChannel setTextChannel;

    /// <summary>
    /// If current class is a Text mesh pro text, then it sets it's text.
    /// </summary>
    /// <param name="text"> Text to be set </param>
    private void SetText(string text)
    {
        TextMeshProUGUI tmpText = gameObject.GetComponent<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.text = text;
        }
    }

    private void OnEnable()
    {
        // If inside a prompt panel, message must be sent after toggling the prompt.
        if (setTextChannel != null)
            setTextChannel.OnEventRaised += SetText;
    }

    private void OnDisable()
    {
        if (setTextChannel != null)
            setTextChannel.OnEventRaised -= SetText;
    }
}
