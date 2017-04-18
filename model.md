---
title: Instructions
layout: default
---

# Model

## Code

For the model code, we modeled a small lego man in a major modeling program and saved the file as a `.stl` from the program.

Unity works well with `.obj` files, so we used a third-party program, [Meshlab](http://www.meshlab.net) to clean up the models and export them as `.obj` files.

We then imported the models into the Unity scene and added code to animate each individual step. Each scene includes several models, but it is possible to interact with only one of them.

We took the model that is the subject of the current step and added a Mesh Collider in order to make it possible for the user to drag the model around the workspace and added a script for the animation.

The full code for an example object is shown below.

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class step8_animation : MonoBehaviour {

  private Vector3 origin;
  private Vector3 startPosition;

	// Use this for initialization
	void Start () {
    // Get the current position and set origin to the initial position of the object.
    Vector3 pnt = transform.position;
    origin = pnt;

    // Set the starting position to the initial position away from the origin.
    startPosition = new Vector3(pnt.x - 1f, pnt.y - 5f, pnt.z + 5f);
    transform.position = startPosition;
	}

	// Update is called once per frame
	void Update () {
    // Check if the current position is the origin. If it is, call the reset method.
    if ((transform.position.x >= origin.x) && (transform.position.y >= origin.y) && (transform.position.z <= origin.z)) {
      transform.position = startPosition;
    }

    // Move the object closer to the origin.
    float step = Time.deltaTime;
    transform.position = Vector3.MoveTowards(transform.position, origin, step);
	}

}
```

## Code Explained

To start, we needed to include a way to animate the object for the user to show them how a particular piece fits inside the assembly.

We need to set some initial variables for the animation in the `Start` method. We get the original position of the model in Unity, which is where the child model is supposed to fit into the overall model. We save this position in a class variable called `origin`.

```C#
private Vector3 origin;
private Vector3 startPosition;

// Use this for initialization
void Start () {
  // Get the current position and set origin to the initial position of the object.
  Vector3 pnt = transform.position;
  origin = pnt;
```

From there, we need to move the child model a short distance away from the origin, so that we can drift it toward the origin over time. This position is called `startPosition`.

```C#
// Set the starting position to the initial position away from the origin.
startPosition = new Vector3(pnt.x - 1f, pnt.y - 5f, pnt.z + 5f);
transform.position = startPosition;
```

Now that we have the initial variables set up, we can focus on the `Update` method. This method is what will do the actual animation. We need to check if the object has drifted to the origin, in which case, we reset the position back to the `startPosition`. This way, the animation loops and the user can see a loop of how the child object fits into the parent.

```C#
// Check if the current position is the origin. If it is, call the reset method.
if ((transform.position.x >= origin.x) && (transform.position.y >= origin.y) && (transform.position.z <= origin.z)) {
  transform.position = startPosition;
}
```

The last step is to slowly drift the object toward the origin over time, despite its current position.

```C#
// Move the object closer to the origin.
float step = Time.deltaTime;
transform.position = Vector3.MoveTowards(transform.position, origin, step);
```
