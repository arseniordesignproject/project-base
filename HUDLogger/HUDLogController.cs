using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class HUDLogController : MonoBehaviour
{
    public static HUDLogController instance = null;
    public Color messageColor = Color.gray;

    public enum LogMessageTypes
    {
        Message,
        Warning,
        Error
    }

    private Text logText;
    private int msgCount = 0;
    private int wrnCount = 0;
    private int errCount = 0;
    private int entryCount = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        logText = GetComponent<Text>();
        logText.color = messageColor;
    }

    //Use this for initialization
    void Start()
    {
        //logText = GetComponent<Text>();
        AddToLog("Beginning logger", LogMessageTypes.Message, false);
    }

    public void AddToLog(string content, LogMessageTypes msgType, bool withTimeStamp)
    {
        if (logText == null)
            return;

        entryCount++;
        if (entryCount > 1)
        {
            logText.text += Environment.NewLine;
        }

        string msg = string.Empty;
        if (withTimeStamp)
        {
            string time = DateTime.Now.ToString();
            msg = time + " ";
        }

        switch (msgType)
        {
            case LogMessageTypes.Message:
                msg += "[MSG " + (++msgCount).ToString() + "] " + content;
                break;
            case LogMessageTypes.Warning:
                msg += "[WRN " + (++wrnCount).ToString() + "] " + content;
                break;
            case LogMessageTypes.Error:
                msg += "[ERR " + (++errCount).ToString() + "] " + content;
                break;
        }
        logText.text += msg;
    }

    public static void QuickMessage(string content)
    {
        if (instance != null)
            instance.AddToLog(content, LogMessageTypes.Message, false);
    }

    public static void QuickWarning(string content)
    {
        if (instance != null)
            instance.AddToLog(content, LogMessageTypes.Warning, false);
    }

    public static void QuickError(string content)
    {
        if (instance != null)
            instance.AddToLog(content, LogMessageTypes.Error, false);
    }

}

