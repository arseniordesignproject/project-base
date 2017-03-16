using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class StepController : MonoBehaviour
{
    public delegate void OnStepCompletedHandler(object sender, EventArgs e);
    public event OnStepCompletedHandler OnStepCompleted;

    public string stepName = "[UNSET]";
    public string stepDescription = "[UNSET]";
}
