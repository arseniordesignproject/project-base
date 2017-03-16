using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ARButton : ARInteractionBase, ARButtonInterface
{
    [Tooltip("Drag button graphic here to extract any shader on it")]
    public Image image;
    [Tooltip("Drag button graphic here to extract any shader on it")]
    public SpriteRenderer renderer;
    // Shader (should be an outline shader) extracted from graphic
    private Material material;
    [Tooltip("Variable in shader used to make it visible")]
    public string visibilityVariable;
    [Tooltip("Value to set visibility variable to be visible")]
    public float enableVariable;
    [Tooltip("Value to set visibility variable to be invisible")]
    public float disableVariable;

    public int NumberOfTaps
    { get; private set; }

    private void Awake()
    {
        // No going to be using these so setting to null to supress invokes
        onFocusOver = null;
        onFocusDown = null;
        onFocusUp = null;
        onFocusTapped = null;
        onFocusDragStarted = null;
        onFocusDragged = null;
        onFocusDragStopped = null;
        onFocusScrollStarted = null;
        onFocusScrolled = null;
        onFocusScrollStopped = null;

        // Extract shader and initialized to invisible
        if (image != null)
        {
            material = image.materialForRendering;
            material.SetFloat(visibilityVariable, disableVariable);
        }
        else if (renderer != null)
        {
            material = renderer.material;
            material.SetFloat(visibilityVariable, disableVariable);
        }
    }

    public override void OnFocusEnter()
    {
        if (material != null)
        {
            // Turn outline (shader) on
            material.SetFloat(visibilityVariable, enableVariable);
        }

        base.OnFocusEnter();
    }

    public override void OnFocusExit()
    {
        if (material != null)
        {
            // Turn outline (shader) off
            material.SetFloat(visibilityVariable, disableVariable);
        }

        base.OnFocusExit();
    }

    public override void OnFocusTap(int tapCount)
    {
        NumberOfTaps++;
        base.OnFocusTap(tapCount);
    }
}
