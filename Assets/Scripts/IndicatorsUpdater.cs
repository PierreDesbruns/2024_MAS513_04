using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Update 3D force indicators attached to each finger.
/// </summary>
public class IndicatorsUpdater : MonoBehaviour
{
    [Tooltip("Data receiver object.")]
    [SerializeField] DataReceiver dataReceiver;
    [Tooltip("Right hand object. Drag and drop OVRHandPrefabRight prefab from OVRCameraRig.")]
    [SerializeField] OVRSkeleton rightHand;
    [Header("Indicators")]
    [Tooltip("Cube indicator sticking to thumb. Drag and drop Thumb child.")]
	[SerializeField] GameObject thumbIndicator;
    [Tooltip("Cube indicator sticking to thumb. Drag and drop Index child.")]
	[SerializeField] GameObject indexIndicator;
    [Tooltip("Cube indicator sticking to thumb. Drag and drop Middle child.")]
	[SerializeField] GameObject middleIndicator;

    Renderer thumbIndicatorRenderer;
    Renderer indexIndicatorRenderer;
    Renderer middleIndicatorRenderer;


    // Start is called before the first frame update
    void Start()
    {
        thumbIndicatorRenderer = thumbIndicator.GetComponent<Renderer>();
        indexIndicatorRenderer = indexIndicator.GetComponent<Renderer>();
        middleIndicatorRenderer = middleIndicator.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float thumbForceValue = dataReceiver.thumbForceValue;
        float indexForceValue = dataReceiver.indexForceValue;
        float middleForceValue = dataReceiver.middleForceValue;

        // Updating indicators' color
        thumbIndicatorRenderer.material.color = Color.Lerp(Color.white, Color.red, thumbForceValue);
        indexIndicatorRenderer.material.color = Color.Lerp(Color.white, Color.green, indexForceValue);
        middleIndicatorRenderer.material.color = Color.Lerp(Color.white, Color.blue, middleForceValue);

        // Updating indicators' height
        thumbIndicator.transform.localScale = new Vector3(0.01f, 0.05f * thumbForceValue, 0.01f);
        indexIndicator.transform.localScale = new Vector3(0.01f, 0.05f * indexForceValue, 0.01f);
        middleIndicator.transform.localScale = new Vector3(0.01f, 0.05f * middleForceValue, 0.01f);

        // Updating indicators' position to fingertips' position
        foreach (var bone in rightHand.Bones)
        {
            if (bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip)
            {
                thumbIndicator.transform.position = bone.Transform.position + thumbIndicator.transform.up * 0.05f * thumbForceValue / 2;
            }
            if (bone.Id == OVRSkeleton.BoneId.Hand_IndexTip)
            {
                indexIndicator.transform.position = bone.Transform.position + indexIndicator.transform.up * 0.05f * indexForceValue / 2;
            }
            if (bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip)
            {
                middleIndicator.transform.position = bone.Transform.position + middleIndicator.transform.up * 0.05f * middleForceValue / 2;
            }
        }
    }
}
