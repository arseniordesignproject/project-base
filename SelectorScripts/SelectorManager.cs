using UnityEngine;
using System.Collections;

public class SelectorManager : MonoBehaviour
{
    public static SelectorManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnLaunchClick()
    {
        HUDLogController.QuickMessage("Launch Pressed");
    }
}
