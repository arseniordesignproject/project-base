using UnityEngine;
using System.Collections;

public class ControlsCanvasManager : MonoBehaviour
{
    //The instance
    public static ControlsCanvasManager instance = null;

    public bool enabledOnStart = false;
    public float inverseMoveTime = 0.5f;

    private RectTransform rect;
    private Vector2 minSize;
    private Vector3 scale;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        if (!enabledOnStart)
            enabled = false;

        rect = GetComponent<RectTransform>();
        scale = rect.localScale;
        minSize = new Vector3(rect.sizeDelta.x * scale.x,
            rect.sizeDelta.y * scale.y);
    }
    // Use this for initialization
    void Start () {
        HUDLogController.QuickMessage("Something");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void CanvasPlaced()
    {

    }

    public void ResizeCanvas(Bounds bounds)
    {
        Vector3 newExtents = bounds.extents;
        Vector2 newSize = new Vector2(minSize.x + (newExtents.x / scale.x),
            minSize.y + (newExtents.y / scale.y));
        HUDLogController.instance.AddToLog("x: " + newSize.x + " y: " + newSize.y, HUDLogController.LogMessageTypes.Message, false);
        rect.sizeDelta = newSize;
        transform.position = new Vector3(transform.position.x, transform.position.y, newExtents.z);
    }


   
}
