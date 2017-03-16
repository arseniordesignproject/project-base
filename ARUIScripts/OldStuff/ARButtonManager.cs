using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

namespace HoloInstructions
{

    public class ARButtonManager : MonoBehaviour
    {
        public delegate void OnPressedHandler(object source, ARButtonEventArgs e);
        public delegate void OnEnteredHandler(object source, ARButtonEventArgs e);
        public delegate void OnExitedHandler(object source, ARButtonEventArgs e);


        public class ARButtonEventArgs : EventArgs
        {
            public bool HandFocused { get; set; }
            public bool GazeFocused { get; set; }
            public bool PriorityFocused { get; set; }
        }


        private SpriteRenderer _Renderer;

        public float OutlineWidth = 10.0f;
        public event OnPressedHandler OnPressed;
        public event OnEnteredHandler OnEntered;
        public event OnExitedHandler OnExited;

        // Use this for initialization
        void Start()
        {
            _Renderer = GetComponent<SpriteRenderer>();
            _Renderer.enabled = true;
            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            _Renderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_Outline", 0);
            _Renderer.SetPropertyBlock(mpb);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnGazeEnter()
        {
            HUDLogController.QuickError(name + " entered");

            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            _Renderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_Outline", OutlineWidth);
            _Renderer.SetPropertyBlock(mpb);

            ARButtonEventArgs args = new ARButtonEventArgs()
            {
                GazeFocused = false,
                HandFocused = false,
                PriorityFocused = true
            };

            OnEnteredHandler handler = OnEntered;
            if(handler != null)
            {
                handler(this, args);
            }
        }

        void OnGazeExit()
        {
            HUDLogController.QuickError(name + " exited");

            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            _Renderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_Outline", 0);
            _Renderer.SetPropertyBlock(mpb);

            ARButtonEventArgs args = new ARButtonEventArgs()
            {
                GazeFocused = false,
                HandFocused = false,
                PriorityFocused = true
            };

            OnExitedHandler handler = OnExited;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        void OnTapped(object sender)
        {
            HUDLogController.instance.AddToLog("Tap Detected " + gameObject.name, HUDLogController.LogMessageTypes.Message, false);
            ARButtonEventArgs args = new ARButtonEventArgs()
            {
                GazeFocused = false,
                HandFocused = false,
                PriorityFocused = true
            };

            OnPressedHandler handler = OnPressed;
            if (handler != null)
            {
                handler(this, args);
            }
            
        }
    }
}
