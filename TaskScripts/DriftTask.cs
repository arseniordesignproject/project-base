using UnityEngine;
using System.Collections;

public class DriftTask : MonoBehaviour
{

    [Tooltip("1 over move time")]
    public float inverseMoveTime = 0.15f;
    [Tooltip("Time delay before starting movement")]
    public float delayTime = 0f;
    [Tooltip("Time wait after ending movement")]
    public float waitTime = 0f;
    [Tooltip("Point to drift to (if left null, transform position will be used instead)")]
    public Vector3 endPoint;
    [Tooltip("Point to drift from (if left null, transform position will be used instead)")]
    public Vector3 startPoint;
    [Tooltip("Repeate drift after reaching end point")]
    public bool repeatOnComplete = true;
    // Initial position of drifting object
    private Vector3 initPoint;
    private bool running = false;

    public Coroutine CurrentCoroutine
    { get; private set; }

    private void Awake()
    {
        initPoint = transform.position;
        if (startPoint != null)
        {
            startPoint = transform.position;
        }
        if (endPoint != null)
        {
            endPoint = transform.position;
        }
    }

    // Not using WaitForSec() because I want the delay cancelable
    IEnumerator DelayCoroutine()
    {
        float delay = delayTime;
        while (running && delay > float.Epsilon)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        CurrentCoroutine = StartCoroutine(DriftCoroutine());
    }

    IEnumerator DriftCoroutine()
    {
        float remainingDistance = (endPoint - startPoint).sqrMagnitude;
        transform.position = startPoint;
        while (running && remainingDistance > float.Epsilon)
        {
            Vector3 newPoint = Vector3.MoveTowards(transform.position, endPoint, inverseMoveTime * Time.deltaTime);
            transform.position = newPoint;
            remainingDistance = (endPoint - newPoint).sqrMagnitude;
            yield return null;
        }
        CurrentCoroutine = StartCoroutine(WaitCoroutine());
    }

    // Not using WaitForSec() because I want the wait cancelable
    IEnumerator WaitCoroutine()
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

    public void BeginDriftTask()
    {
        running = true;
        CurrentCoroutine = StartCoroutine(DelayCoroutine());
    }

    public void StopDriftTask()
    {
        running = false;
        StopCoroutine(CurrentCoroutine);
        transform.position = initPoint;
    }

    public void StopDriftTask(Vector3 stopPosition)
    {
        running = false;
        StopCoroutine(CurrentCoroutine);
        transform.position = stopPosition;
    }
}
