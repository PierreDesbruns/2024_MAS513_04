using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update the position of the UI canvas to always have it in front of user's eyes.
/// </summary>
public class UIPoseUpdater : MonoBehaviour
{
    [Tooltip("Drag and drop center eye from OVRCameraRig.")]
    [SerializeField] Camera worldCamera;

    // Start is called before the first frame update
    void Start()
    {
        worldCamera = GetComponent<Canvas>().worldCamera;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = worldCamera.transform.position + worldCamera.transform.forward * 0.4f + worldCamera.transform.up * 0.1f;
        transform.rotation = worldCamera.transform.rotation;
    }
}
