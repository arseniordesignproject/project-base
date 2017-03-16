using UnityEngine;
using System.Collections;

public class GazeManager : MonoBehaviour
{
    public static GazeManager instance = null;
    #region Vars
    [Tooltip("Cursor movement needs to be outside of this radius for it be registered as an independent movement")]
    public float stabilityRadius = 0.15f;
    [Tooltip("Max interaction distance for cursor")]
    public float maxDistance = 15.0f;
    #endregion

    #region Props
    public GameObject GazeObject
    { get; private set; }
    public Vector3 CollidePoint
    { get; private set; }
    public Vector3 CollideNormal
    { get; private set; }
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        HUDLogController.QuickMessage("Gaze Manager Checking In");
    }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 headPostion = Camera.main.transform.position;
        Vector3 dirPosition = Camera.main.transform.forward;
        RaycastHit gazeHit;
        if (Physics.Raycast(headPostion, dirPosition, out gazeHit, maxDistance))
        {
            GazeObject = gazeHit.collider.gameObject;
            CollidePoint = gazeHit.point;
            CollideNormal = gazeHit.normal;
        }
	}
}
