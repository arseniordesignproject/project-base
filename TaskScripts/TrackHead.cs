using UnityEngine;
using System.Collections;

public class TrackHead : MonoBehaviour {

    // Update is called once per frame
    void Update ()
    {
        Vector3 headPosition = Camera.main.transform.position;
        Vector3 groundedPosition = new Vector3(headPosition.x, transform.position.y, headPosition.z);
        transform.LookAt(groundedPosition);
	}
}
