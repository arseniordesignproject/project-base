using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PresenterManager : MonoBehaviour
{
    public enum PresenterStates
    {
        InitState,
        HomePage,
        SelectionPage,
        OperationPage
    }

    public static PresenterManager instance = null;

    public List<GameObject> instructionObjects;
    private InstructionSet[] instructionSets = new InstructionSet[128];
    private int instructionSetIndex = 0; 
    //private List<InstructionSet> instructionSets = new List<InstructionSet>();

    private GameObject NavigationManager;
    private GameObject InstructionSpace;

    private PresenterStates currentState = PresenterStates.InitState;

    private InstructionSet selectedSet = null;
    public InstructionSet SelectedSet {
        get { return selectedSet; }
    }

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
        HUDLogController.QuickMessage("Presenter Starting!!");
            
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    private void PopulateInstructionManager()
    {
        if (selectedSet != null)
        {
            //InstructionManager.instance.StepControllers = selectedSet.StepControllers;
        }
        else
            HUDLogController.QuickMessage("selectedSet is null ");
    }

    private void OnDragStarted()
    {
        HUDLogController.QuickError("Drag started");
        InstructionManager.instance.SetInstructionState(InstructionManager.InstructionStates.Unseen);
    }

    private void OnDragEnded()
    {
        foreach (GameObject obj in instructionObjects)
        {
            InstructionSet set = obj.GetComponent<InstructionSet>();
            if (set != null)
            {
                instructionSets[instructionSetIndex++] = set;
                HUDLogController.QuickMessage("Instruction set found");
            }
        }

        if (instructionSetIndex > 0 && instructionSets[0] != null)
        {
            selectedSet = instructionSets[0];
            bool failState = false;
            int count = 0;
            StepController[] list = instructionSets[0].GetControllers(out count, out failState);
            if (failState)
            {
                HUDLogController.QuickMessage("failed");
            }
            else
            {
                HUDLogController.QuickMessage("Populate manager ");
                InstructionManager.instance.StepControllers = new List<StepController>(list);
            }
        }
        else
            HUDLogController.QuickMessage("Presenter Nope!!");

        InstructionManager.instance.SetInstructionState(InstructionManager.InstructionStates.Starting);
    }

    private void PresenterStateMachine()
    {

    }
}
