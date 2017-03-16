using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public interface ARButtonInterface
{
    void OnFocusEnter();
    void OnFocusExit();
    void OnFocusTap(int tapCount);
}

public abstract class ARInteractionBase : MonoBehaviour
{
    [System.Serializable]
    public class Vector3Event : UnityEvent<Vector3> { }
    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }

    #region Vars
    public UnityEvent onFocusEntered = new UnityEvent();
    public UnityEvent onFocusExited = new UnityEvent();
    public UnityEvent onFocusOver = new UnityEvent();

    public UnityEvent onFocusDown = new UnityEvent();
    public UnityEvent onFocusUp = new UnityEvent();
    public IntEvent onFocusTapped;

    public UnityEvent onFocusDragStarted = new UnityEvent();
    public Vector3Event onFocusDragged;
    public UnityEvent onFocusDragStopped = new UnityEvent();

    public UnityEvent onFocusScrollStarted = new UnityEvent();
    public Vector3Event onFocusScrolled;
    public UnityEvent onFocusScrollStopped = new UnityEvent();
    #endregion

    #region Funcs
    public virtual void OnFocusEnter()
    {
        if (onFocusEntered != null)
        {
            onFocusEntered.Invoke();
        }
    }

    public virtual void OnFocusExit()
    {
        if (onFocusExited != null)
        {
            onFocusExited.Invoke();
        }
    }

    public virtual void OnFocusOver()
    {
        if (onFocusOver != null)
        {
            onFocusOver.Invoke();
        }
    }

    public virtual void OnFocusDown()
    {
        if (onFocusDown != null)
        {
            onFocusDown.Invoke();
        }
    }

    public virtual void OnFocusUp()
    {
        if (onFocusUp != null)
        {
            onFocusUp.Invoke();
        }
    }

    public virtual void OnFocusTap(int tapCount)
    {
        if (onFocusTapped != null)
        {
            onFocusTapped.Invoke(tapCount);
        }
    }

    public virtual void OnFocusDragStart()
    {
        if (onFocusDragStarted != null)
        {
            onFocusDragStarted.Invoke();
        }
    }

    public virtual void OnFocusDrag(Vector3 dragDistance)
    {
        if (onFocusDragged != null)
        {
            onFocusDragged.Invoke(dragDistance);
        }
    }

    public virtual void OnFocusDragStop()
    {
        if (onFocusDragStopped != null)
        {
            onFocusDragStopped.Invoke();
        }
    }

    public virtual void OnFocusScrollStart()
    {
        if (onFocusScrollStarted != null)
        {
            onFocusScrollStarted.Invoke();
        }
    }

    public virtual void OnFocusScroll(Vector3 scrollDistance)
    {
        if (onFocusScrolled != null)
        {
            onFocusScrolled.Invoke(scrollDistance);
        }
    }

    public virtual void OnFocusScrollStop()
    {
        if (onFocusScrollStopped != null)
        {
            onFocusScrollStopped.Invoke();
        }
    }
    #endregion

}
