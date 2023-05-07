using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUtils : MonoBehaviour
{
    /// <summary>
    /// Returns the normalized position of the mouse relative to the window
    /// </summary>
    /// <returns></returns>
    static public Vector2 GetNormalizedMousePosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 windowPosition = new Vector2(Screen.width, Screen.height);
        Vector2 normalizedPosition = (mousePosition / windowPosition) * 2 - new Vector2(1, 1);
        normalizedPosition.x = Mathf.Clamp(normalizedPosition.x, -1f, 1f);
        normalizedPosition.y = Mathf.Clamp(normalizedPosition.y, -1f, 1f);
        return normalizedPosition;
    }

    /// <summary>
    /// Simply returns if the mouse is inside the window
    /// </summary>
    /// <returns>True if the mouse is in the window, false if not</returns>
    static public bool IsMouseInWindow()
    {
        return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y);
    }
}
