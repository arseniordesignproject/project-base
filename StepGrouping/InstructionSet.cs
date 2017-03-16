using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstructionSet : MonoBehaviour
{

    public List<GameObject> stepObjects;
    private List<StepController> stepControllers;

    public GameObject completedConstruction;
    public string setName = "[UNSET]";

    public StepController[] GetControllers(out int actualCount, out bool failState)
    {
        StepController[] list = new StepController[stepObjects.Count];
        actualCount = 0;
        failState = false;

        try
        {
            foreach (GameObject step in stepObjects)
            {
                StepController controller = step.GetComponent<StepController>();
                if (controller != null)
                {
                    list[actualCount++] = controller;
                }
            }
        }
        catch
        {
            failState = true;
        }

        return list;
    }
        
}
