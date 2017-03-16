using UnityEngine;
using System.Collections;

public class RotateTask : MonoBehaviour
{
    [Tooltip("1 over rotate time")]
    public float inverseRotateTime = 0.15f;
    [Tooltip("Time delay before starting rotation")]
    public float delayTime = 0f;
    [Tooltip("Time wait after ending rotation")]
    public float waitTime = 0f;
    [Tooltip("Starting rotation position (if left null, transform rotation will be used instead)")]
    public Quaternion startRotations;
    [Tooltip("Ending rotation positoin (if left null, transform rotation will be used instead")]
    public Quaternion endRotatoin;
    [Tooltip("Repeate rotation after reaching end point")]
    public bool repeatOnComplete = true;

    private bool running = false;
    private Quaternion initRotate;

    public Coroutine CurrentCoroutine
    { get; private set; }

    private void Awake()
    {
        if (startRotations != null)
        {
            startRotations = transform.rotation;
        }
        if (endRotatoin != null)
        {
            endRotatoin = transform.rotation;
        }
    }

    // Not using WaitForSec() because I want the delay cancelable
    private IEnumerator DelayCoroutine()
    {
        float delay = delayTime;
        while(running && delay > float.Epsilon)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        CurrentCoroutine = StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        float distX = (endRotatoin.x - startRotations.x);
        float distY = (endRotatoin.y - startRotations.y);
        float distZ = (endRotatoin.z - startRotations.z);
        float remainingX = distX;
        float remainingY = distY;
        float remainingZ = distZ;
        while (running && remainingX > float.Epsilon && 
            remainingY > float.Epsilon && remainingZ > float.Epsilon)
        {
            remainingX -= distX * inverseRotateTime * Time.deltaTime;
            remainingY -= distY * inverseRotateTime * Time.deltaTime;
            remainingZ -= distZ * inverseRotateTime * Time.deltaTime;
            transform.rotation = new Quaternion(remainingX, remainingY, remainingZ, initRotate.w);
            yield return null;
        }
        CurrentCoroutine = StartCoroutine(RotateCoroutine());
    }

    // Not using WaitForSec() because I want the wait cancelable
    private IEnumerator WaitCoroutine()
    {
        float wait = waitTime;
        while (running && wait > float.Epsilon)
        {
            wait -= Time.deltaTime;
            yield return null;
        }

        if (running && repeatOnComplete)
        {
            CurrentCoroutine = StartCoroutine(DelayCoroutine());
        }
        else
        {
            running = false;
            CurrentCoroutine = null;
        }
    }

    public void BeginRotateTask()
    {
        running = true;
        CurrentCoroutine = StartCoroutine(DelayCoroutine());
    }

    public void StopRotateTask()
    {
        running = false;
        StopCoroutine(CurrentCoroutine);
        transform.rotation = initRotate;
    }

    public void StopRotateTask(Quaternion stopRotation)
    {
        running = false;
        StopCoroutine(CurrentCoroutine);
        transform.rotation = stopRotation;
    }


}
