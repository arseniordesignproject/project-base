using UnityEngine;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class HandManager : MonoBehaviour
{
    public static HandManager instance = null;
    #region Vars
    [Tooltip("Cursor movement needs to be outside of this radius for it be registered as an independent movement")]
    public float stabilityRadius = 0.15f;
    [Tooltip("Max interaction distance for cursor")]
    public float maxDistance = 15.0f;
    // Object capable of retreiving the position of the hand
    private InteractionSourceLocation handLocation;
    // Object capable of retreiving the properties of the hand
    private InteractionSourceProperties handProperties;
    // Current hand registered to control cursor
    private uint controllingID;
    #endregion

    #region Props
    public bool HandDetected
    { get; private set; }
    public GameObject HandObject
    { get; private set; }
    public Vector3 HandPosition
    { get; private set; }
    public Vector3 CollidePoint
    { get; private set; }
    public Vector3 CollideNormal
    { get; private set; }
    #endregion

    #region Func
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        HUDLogController.QuickMessage("Hand Manager Checking In");
        HandDetected = false;
        HandPosition = new Vector3(0, 0, 0);
    }

    // Use this for initialization
    void Start()
    {
        InteractionManager.SourceDetected += InteractionManager_SourceDetected;
        InteractionManager.SourceLost += InteractionManager_SourceLost;
        InteractionManager.SourceUpdated += InteractionManager_SourceUpdated;
    }

    private void InteractionManager_SourceUpdated(InteractionSourceState state)
    {
        // Ensure only updating on an active driver
        if (HandDetected && controllingID == state.source.id)
        {
            Vector3 handPosition;
            if (state.properties.location.TryGetPosition(out handPosition))
            {
                HandPosition = handPosition;
            }
        }
    }

    private void InteractionManager_SourceLost(InteractionSourceState state)
    {
        // Ensure only the driver cancel detection
        if (controllingID == state.source.id)
        {
            HandDetected = false;
            HUDLogController.QuickMessage("Hand Lost");
        }
    }

    private void InteractionManager_SourceDetected(InteractionSourceState state)
    {
        // Ensure only one hand is driving
        if (!HandDetected)
        {
            HUDLogController.QuickMessage("Hand Detected");
            HandDetected = true;
            controllingID = state.source.id;
            Vector3 handPosition;
            if (state.properties.location.TryGetPosition(out handPosition))
            {
                HandPosition = handPosition;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HandDetected)
        {
            Vector3 headPositoin = Camera.current.transform.position;
            RaycastHit handHit;
            if (Physics.Raycast(headPositoin, HandPosition, out handHit, maxDistance))
            {
                HandObject = handHit.collider.gameObject;
                CollidePoint = handHit.point;
                CollideNormal = handHit.normal;
            }
        }
    }

    private void OnDestroy()
    {
        InteractionManager.SourceDetected -= InteractionManager_SourceDetected;
        InteractionManager.SourceLost -= InteractionManager_SourceLost;
        InteractionManager.SourceUpdated -= InteractionManager_SourceUpdated;
    }
    #endregion
}
