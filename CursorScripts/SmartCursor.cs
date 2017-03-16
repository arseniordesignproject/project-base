using UnityEngine;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class SmartCursor : MonoBehaviour
{
    public static SmartCursor instance = null;
    // Cursor mesh
    private MeshRenderer mesh;

    #region Enums
    public enum CursorFocusPriority
    {
        Hand,
        Gaze
    }
    #endregion

    #region Vars
    public CursorFocusPriority priority = CursorFocusPriority.Hand;

    private GameObject focusedObject;
    private GameObject oldFocusedObject;

    private GameObject draggedObject;
    private float dragDistance = 0f;

    private SpriteRenderer eyeRenderer;
    private SpriteRenderer handRenderer;
    #endregion

    #region Props
    public GameObject FocusedObject
    {
        get
        {
            if (Dragging)
            {
                return null;
            }
            else
            {
                return focusedObject;
            }
        }
        private set
        {
            focusedObject = value;
        }
    }

    public GameObject DraggedObject
    {
        get
        {
            return draggedObject;
        }
        set
        {
            if (value != null)
            {
                HUDLogController.QuickMessage("Smart Cursor Drag Started");
                value.SendMessage("OnDragStarted", this, SendMessageOptions.DontRequireReceiver);
                Dragging = true;
            }
            else
            {
                HUDLogController.QuickMessage("Smart Cursor Drag Ended");
                draggedObject.transform.position = mesh.transform.position;
                draggedObject.SendMessage("OnDragEnded", this, SendMessageOptions.DontRequireReceiver);
                Dragging = false;
            }

            draggedObject = value;
        }
    }

    public bool Dragging
    { get; private set; }
    public bool LastHandState
    { get; private set; }
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        HUDLogController.QuickMessage("Smart Cursor Checking In");
        mesh = GetComponent<MeshRenderer>();

        GameObject eye = GameObject.Find("EyeIcon");
        eyeRenderer = eye.GetComponent<SpriteRenderer>();

        GameObject hand = GameObject.Find("HandIcon");
        handRenderer = hand.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        FocusedObject = null;

        mesh.enabled = false;
        eyeRenderer.enabled = false;
        handRenderer.enabled = false;

        DraggedObject = PresenterManager.instance.gameObject;
    }

    private void Update()
    {
        CursorUpdate();
    }

    private void CursorUpdate()
    {
        oldFocusedObject = FocusedObject;
        switch (priority)
        {
            case CursorFocusPriority.Hand:
                if (HandManager.instance.HandDetected)
                {
                    HandUpdate();
                }
                else
                {
                    GazeUpdate();
                }
                break;
            case CursorFocusPriority.Gaze:
                GazeUpdate();
                break;
            default:
                break;
        }

        // Message sending should be suppressed while dragging
        if (!Dragging)
        {
            // Send Messages
            if (FocusedObject != oldFocusedObject)
            {
                if (FocusedObject != null)
                {
                    FocusedObject.SendMessage("OnFocusEnter", this, SendMessageOptions.DontRequireReceiver);
                    HUDLogController.QuickMessage("Focus Enter " + FocusedObject.name);
                }
                if (oldFocusedObject != null)
                {
                    oldFocusedObject.SendMessage("OnFocusExit", this, SendMessageOptions.DontRequireReceiver);
                    HUDLogController.QuickMessage("Focus Exit " + oldFocusedObject.name);
                }
            }
            else
            {
                if (FocusedObject != null)
                {
                    FocusedObject.SendMessage("OnFocusOver", this, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        else
        {

            DraggedObject.transform.position = mesh.transform.position;
        }
        
    }

    private void GazeUpdate()
    {

        focusedObject = GazeManager.instance.GazeObject;
        if (focusedObject != null)
        {
            mesh.transform.position = GazeManager.instance.CollidePoint;
            mesh.transform.rotation = Quaternion.FromToRotation(Vector3.forward, GazeManager.instance.CollideNormal);

            if (focusedObject.layer != LayerMask.NameToLayer("NoCursorOnHit"))
            {
                mesh.enabled = true;
                eyeRenderer.enabled = true;
                handRenderer.enabled = false;
            }
            else
            {
                mesh.enabled = false;
                eyeRenderer.enabled = false;
                handRenderer.enabled = false;
            }
        }
    }

    private void HandUpdate()
    {
        focusedObject = HandManager.instance.HandObject;
        if (focusedObject != null)
        {
            mesh.transform.position = HandManager.instance.CollidePoint;
            mesh.transform.rotation = Quaternion.FromToRotation(Vector3.forward, HandManager.instance.CollideNormal);

            if (focusedObject.layer != LayerMask.NameToLayer("NoCursorOnHit"))
            {
                mesh.enabled = true;
                eyeRenderer.enabled = true;
                handRenderer.enabled = false;
            }
            else
            {
                mesh.enabled = false;
                eyeRenderer.enabled = false;
                handRenderer.enabled = false;
            }
        }
    }
}
