using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class InstructionManager : MonoBehaviour
{
    public enum InstructionStates
    {
        Unseen,
        Starting,
        Using
    }

    #region Vars
    public static InstructionManager instance = null;

    private GameObject PresentationSpace;
    private GameObject ControlsCanvas;
    private ControlsCanvasManager CanvasManager;

    private GameObject InstructionPresenter;
    private GameObject StartButton;
    private GameObject CloseButton;
    private GameObject NextButton;
    private GameObject BackButton;

    private Transform NextTransform;
    private Transform BackTransform;

    //private ARButtonManager StartManager;
    //private ARButtonManager CloseManager;
    //private ARButtonManager NextManager;
    //private ARButtonManager BackManager;

    private Vector3 extents;
    public Vector3 minExtents;
    //public InstructionsController Instructions;
    public List<GameObject> StepPrefabs = new List<GameObject>();
    private int StepIndex = 0;
    private GameObject[] StepArray;

    public float InverseMoveTime = 5.0f;
    #endregion

    #region Props
    public StepController CurrentController { get; private set; }
    public GameObject CurrentStep { get; private set; }


    public List<StepController> stepControllers = new List<StepController>();
    public List<StepController> StepControllers
    {
        get
        {
            return stepControllers;
        }
        set
        {
            stepControllers = value;
            StepArray = new GameObject[value.Count];
        }
    }
    #endregion

    #region Funcs
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        InstructionPresenter = GameObject.Find("InstructionPresenter");
        PresentationSpace = GameObject.Find("PresentationSpace");
        ControlsCanvas = GameObject.Find("ControlsCanvas");
        CanvasManager = ControlsCanvas.GetComponent<ControlsCanvasManager>();

        StartButton = GameObject.Find("StartButton");
        NextButton = GameObject.Find("NextButton");
        BackButton = GameObject.Find("BackButton");
        CloseButton = GameObject.Find("CloseButton");

        NextTransform = NextButton.GetComponent<RectTransform>();
        BackTransform = BackButton.GetComponent<RectTransform>();

        SetInstructionState(InstructionStates.Unseen);
    }

    public void OnStartPressed()
    {
        HUDLogController.QuickMessage("StepControllers Count " + StepControllers.Count.ToString());
        if (StepControllers.Count > 0)
        {
            StepIndex = 0;
            GameObject obj = Instantiate(StepControllers[StepIndex].gameObject, PresentationSpace.transform) as GameObject;
            StepArray[StepIndex] = obj.gameObject;
            CurrentStep = obj.gameObject;
            obj.transform.position += PresentationSpace.transform.position;
            StepIndex++;
            SetInstructionState(InstructionStates.Using);
        }
    }
        
    public void OnClosePressed()
    {
        HUDLogController.QuickMessage("Close Pressed");
    }

    public void OnBackPressed()
    {
        if (StepIndex > 0)
        {
            DestroyImmediate(StepArray[--StepIndex]);
            CurrentStep = StepIndex > 0 ? StepArray[StepIndex - 1] : null;
            UpdateBounds();
        }
    }
        

    public void OnNextPressed()
    {
        if (StepIndex < StepControllers.Count)
        {
            HUDLogController.QuickMessage("Next Pressed");
            GameObject obj = Instantiate(StepControllers[StepIndex].gameObject, PresentationSpace.transform) as GameObject;
            StepArray[StepIndex] = obj.gameObject;
            CurrentStep = obj.gameObject;
            obj.transform.position += PresentationSpace.transform.position;
            StepIndex++;
        }
    }
        
    public void UpdateBounds()
    {
        HUDLogController.QuickMessage("UpdateBounds Called");
        Bounds b = new Bounds();
        Vector3 newExtents = minExtents;
        //Create a bounds structure with extents that include all insta
        try
        {
            foreach (GameObject obj in StepArray)
            {
                MeshFilter filter = obj.GetComponent<MeshFilter>();
                if (filter != null)
                {
                    Vector3 max = filter.mesh.bounds.max;
                    if (Mathf.Abs(max.x) > Mathf.Abs(newExtents.x))
                        newExtents.x = Mathf.Abs(max.x);

                    if (Mathf.Abs(max.y) > Mathf.Abs(newExtents.y))
                        newExtents.y = Mathf.Abs(max.y);

                    if (Mathf.Abs(max.z) > Mathf.Abs(newExtents.z))
                        newExtents.z = Mathf.Abs(max.z);
                }
            }
            b.extents = newExtents;
            b.center = new Vector3(0, 0, 0);
            HUDLogController.instance.AddToLog("x: " + newExtents.x + " y: " + newExtents.y + " z: " + newExtents.z,
                HUDLogController.LogMessageTypes.Message, false);
            CanvasManager.ResizeCanvas(b);
        } 
        catch(UnityException e)
        {
            HUDLogController.QuickMessage("ProblemWithUpdateBOunds");
            HUDLogController.QuickError(e.ToString());
        }
    }

    public void SetInstructionState(InstructionStates state)
    {
        switch (state)
        {
            case InstructionStates.Unseen:
                NextButton.SetActive(false);
                BackButton.SetActive(false);
                CloseButton.SetActive(false);
                StartButton.SetActive(false);
                HUDLogController.QuickMessage("Set to Placing");
                break;
            case InstructionStates.Starting:
                NextButton.SetActive(false);
                BackButton.SetActive(false);
                CloseButton.SetActive(false);
                StartButton.SetActive(true);
                HUDLogController.QuickMessage("Set to Starting");
                break;
            case InstructionStates.Using:
                NextButton.SetActive(true);
                BackButton.SetActive(true);
                CloseButton.SetActive(true);
                StartButton.SetActive(false);
                HUDLogController.QuickMessage("Set to Using");
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != PresentationSpace.transform.position)
        {
            PresentationSpace.transform.position = transform.position;
        }
    }

    #endregion
}
