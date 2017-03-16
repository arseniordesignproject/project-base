using UnityEngine;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class GestureManager : MonoBehaviour
{
    public static GestureManager instance = null;

    private bool manipulationStarted = false;
    private bool navigationStarted = false;

    private GameObject manipulationObject = null;
    private GameObject navigationObject = null;

    private GestureRecognizer recognizer;
    private InstructionManager manager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        recognizer = new GestureRecognizer();
    }

    // Use this for initialization
    void Start ()
    {
        recognizer.TappedEvent += Recognizer_TappedEvent;

        recognizer.ManipulationCanceledEvent += Recognizer_ManipulationCanceledEvent;
        recognizer.ManipulationCompletedEvent += Recognizer_ManipulationCompletedEvent;
        recognizer.ManipulationStartedEvent += Recognizer_ManipulationStartedEvent;
        recognizer.ManipulationUpdatedEvent += Recognizer_ManipulationUpdatedEvent;

        recognizer.NavigationCanceledEvent += Recognizer_NavigationCanceledEvent;
        recognizer.NavigationCompletedEvent += Recognizer_NavigationCompletedEvent;
        recognizer.NavigationStartedEvent += Recognizer_NavigationStartedEvent;
        recognizer.NavigationUpdatedEvent += Recognizer_NavigationUpdatedEvent;

        InteractionManager.SourcePressed += InteractionManager_SourcePressed;
        InteractionManager.SourceReleased += InteractionManager_SourceReleased;

        recognizer.StartCapturingGestures();
	}

    private void Recognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        if (navigationObject != null)
        {
            SmartCursor.instance.FocusedObject.SendMessage("OnFocusDrag", normalizedOffset, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Recognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        if (SmartCursor.instance.FocusedObject != null)
        {
            navigationStarted = true;
            navigationObject = SmartCursor.instance.FocusedObject;
            navigationObject.SendMessage("OnFocusDragStart", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Recognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        navigationStarted = false;
        if (navigationObject != null)
        {
            navigationObject.SendMessage("OnFocusDragStop", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Recognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        navigationStarted = false;
        if (navigationObject != null)
        {
            navigationObject.SendMessage("OnFocusDragStop", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Recognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        if(manipulationObject != null)
        {
            SmartCursor.instance.FocusedObject.SendMessage("OnFocusScroll", cumulativeDelta, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Recognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        if (SmartCursor.instance.FocusedObject != null)
        {
            manipulationStarted = true;
            manipulationObject = SmartCursor.instance.FocusedObject;
            manipulationObject.SendMessage("OnFocusScrollStart", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Recognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        manipulationStarted = false;
        if (manipulationObject != null)
        {
            manipulationObject.SendMessage("OnFocusScrollStop", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Recognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        manipulationStarted = false;
        if (manipulationObject != null)
        {
            manipulationObject.SendMessage("OnFocusScrollStop", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void InteractionManager_SourceReleased(InteractionSourceState state)
    {
        if (SmartCursor.instance.FocusedObject != null)
        {
            SmartCursor.instance.FocusedObject.SendMessage("OnFocusUp", this, SendMessageOptions.DontRequireReceiver);
        }
        else if (SmartCursor.instance.DraggedObject != null)
        {
            SmartCursor.instance.DraggedObject = null;
        }
    }

    private void InteractionManager_SourcePressed(InteractionSourceState state)
    {
        if (SmartCursor.instance.FocusedObject != null)
        {
            SmartCursor.instance.FocusedObject.SendMessage("OnFocusDown", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (SmartCursor.instance.FocusedObject != null)
        {
            SmartCursor.instance.FocusedObject.SendMessage("OnFocusTapped", tapCount, SendMessageOptions.DontRequireReceiver);
        }
        else if (SmartCursor.instance.DraggedObject != null)
        {
            SmartCursor.instance.DraggedObject = null;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    private void OnDestroy()
    {
        recognizer.TappedEvent -= Recognizer_TappedEvent;

        recognizer.ManipulationCanceledEvent -= Recognizer_ManipulationCanceledEvent;
        recognizer.ManipulationCompletedEvent -= Recognizer_ManipulationCompletedEvent;
        recognizer.ManipulationStartedEvent -= Recognizer_ManipulationStartedEvent;
        recognizer.ManipulationUpdatedEvent -= Recognizer_ManipulationUpdatedEvent;

        recognizer.NavigationCanceledEvent -= Recognizer_NavigationCanceledEvent;
        recognizer.NavigationCompletedEvent -= Recognizer_NavigationCompletedEvent;
        recognizer.NavigationStartedEvent -= Recognizer_NavigationStartedEvent;
        recognizer.NavigationUpdatedEvent -= Recognizer_NavigationUpdatedEvent;

        InteractionManager.SourcePressed -= InteractionManager_SourcePressed;
        InteractionManager.SourceReleased -= InteractionManager_SourceReleased;

        recognizer.StopCapturingGestures();
    }
}
