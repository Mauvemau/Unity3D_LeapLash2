using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedCamera : MonoBehaviour
{
    [Header("Camera Target")]
    [SerializeField] private Transform target;
    [Header("Mouse Camera Input Settings")]
    /// <summary>
    /// True if you want the mouse position to alter the camera, false if you don't.
    /// </summary>
    [SerializeField] private bool mouseInput;
    /// <summary>
    /// Defines how much distance the mouse can move the camera.
    /// </summary>
    [Range(0.0f, 1.0f)]
    [SerializeField] private float mouseInputStrength = 1.0f;
    [Header("Camera Settings")]
    /// <summary>
    /// Defines how much smoothing to add to the camera; 1f = no smoothing, 0.1f = very smooth.
    /// </summary>
    [Range(0.01f, 1.0f)]
    [SerializeField] private float smoothSpeed = 1.0f;

    private Vector3 offset;
    private Vector3 cameraPos;

    private void OnValidate()
    {
        if (target)
        {
            offset = transform.position - target.position;
        }
    }

    private void Awake()
    {
        transform.LookAt(target);
    }

    private void LateUpdate()
    {
        if(target && smoothSpeed == 1.0f)
        {
            cameraPos = target.position + offset;
            if (mouseInput)
            {
                Vector3 mousePosition = new Vector3(cameraPos.x + MouseUtils.GetNormalizedMousePosition().x, cameraPos.y, cameraPos.z + MouseUtils.GetNormalizedMousePosition().y);
                cameraPos = Vector3.Lerp(cameraPos, mousePosition, .5f);
            }
            transform.position = cameraPos;
        }
    }

    private void FixedUpdate()
    {
        if (target && smoothSpeed < 1.0f)
        {
            cameraPos = target.position + offset;
            if (mouseInput)
            {
                Vector3 mousePosition = new Vector3(cameraPos.x + MouseUtils.GetNormalizedMousePosition().x, cameraPos.y, cameraPos.z + MouseUtils.GetNormalizedMousePosition().y);
                cameraPos = Vector3.Lerp(cameraPos, mousePosition, mouseInputStrength);
            }
            Vector3 smoothedFollow = Vector3.Lerp(transform.position, cameraPos, smoothSpeed);
            transform.position = smoothedFollow;
        }
    }
}
