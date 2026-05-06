# Unity Tutorial

**Documentation Index:** Learn about unity tutorial in this documentation.

---

---
title: "Build Your First VR App"
description: "Create a simple VR ball game in Unity to learn 3D objects, components, and build settings for Meta Quest."
---

This tutorial helps you build your first VR app in Unity. It's a basic app, which introduces primary Unity concepts such as 3D objects, components, and build settings. It does not use Oculus Integration package as the objective of this tutorial is to get you started with Unity's basic concepts and interface. At the end, you'll have a VR app ready to run on your computer.

## What's the app about?
It's a simple game! The scene contains a play area surrounded by four walls and a ball that acts as a player. The objective of the game is to keep the ball rolling without colliding with the walls. If it collides with either of the walls, the wall color should change and some text should display on the screen indicating the collision. For input, you need to use your keyboard or Unity-compatible joysticks.

This is what we'll build:

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
    <video-source handle="GKdDfQTQWVXXiPoBAAAAAABMvF9EbosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      <b>Video</b>: Demo of ball movement and wall collision behavior.
  </text>
</box>

## Prerequisites
Before you begin with this tutorial, make sure that you've set up your development environment and performed the necessary settings.

1. Install Unity and create a new project by following [Set up Unity for VR development](/documentation/unity/unity-project-setup/).
1. Complete the [Set up the headset for development](/documentation/unity/unity-env-device-setup/) and guide.
1. [Configure build settings](/documentation/unity/unity-prepare-for-publish#configuration-settings).

## Basic Concepts
This section introduces fundamentals for creating app or gameplay mechanics. We've limited the explanation to core workflow concepts that come handy in building this VR app. To learn more about Unity's concepts and workflows in detail, go to [Unity User Manual](https://docs.unity3d.com/Manual/UnityManual.html).

* **Scene** is a container that holds a variety of game objects.
* **Game Objects** are fundamental objects that represent characters, props, lights, camera, or special effects. In this app, we'll use 3D objects that consist of primitive shapes such as plane, cube, and sphere.
* **Components** define the behavior of the game object. Mainly, the *Transform* component determines the position, rotation, and scale of each game object and the values are represented in form of X, Y, and Z coordinates for each property. By default, the position is set to (0,0,0), which is also known as the origin point from where all the coordinate calculations take place in the scene.
* **Material** adds texture and color to any object. In this app, we'll limit the usage of materials to color the objects and will not go in other technical details.

## Build App

### Step 1. Create material to add colors to game objects.

One of the basic needs in any app design is to add colors and textures. Materials let you define a variety of look-n-feel effects such as colors, shaders, textures, and many more. In this app, we'll limit the usage of materials to add colors.

1. In the **Project** view, in the **Assets** folder, create a new folder to hold materials for different game objects of the app, rename it to *Materials*, and double-click to open the folder.
2. In the menu, go to **Assets** > **Create** > **Material** and rename the material to *floor-color*.
3. In the **Inspector** view, under **Main Maps**, click the Albedo color field to open a color picker and change the color of your choice. In this app, we'll use RGB values of (3,32,70).

   {:width="300px"}
4. Repeat step 2 through step 4 to create materials for walls, player ball, and the change in wall color upon collision. Rename them to *wall-color*, *ball-color*, and *after-collision* and set RGB values to (255,255,255), (240,240,0), and (241,107,8) respectively.

	{:width="400px"}

### Step 2. Build the floor, player ball, and four walls.

The game objects of this app are floor, ball, and four walls and we'll use stock Unity plane, sphere, and cubes to build them.

**Floor**:
1. In the menu, go to **GameObject** > **3D Object** > **Plane**.
2. In the **Hierarchy** view, rename it to *floor*.
3. In the **Inspector** view, under **Transform**, verify the position is set to origin, i.e., (0,0,0). If not, reset it to (0,0,0).
4. Under **Transform**, set the scale to (2,1,2) to enlarge the floor. The plane object is flat and does not have any volume. Therefore, by default, the Y value  is set to one.
5. Drag and drop the *floor-color* material on the plane to add color.

    <image alt="5. Drag and drop the *floor-color* material on the plane to illustration." style="width: 400px;" src="/images/unity-first-vr-plane.png"/>

**Player ball**:
1. In the menu, go to **GameObject** > **3D Object** > **Sphere**.
2. In the **Hierarchy** view, rename it to *player-ball*.
3. From the **Materials** folder, drag and drop *ball-color* on the sphere.
4. In the **Inspector** view, under **Transform**, ensure the position is set to (0,0,0), and set the Y value to 0.5 to let the ball rest on the floor, i.e., (0,0.5,0).

    <image alt="4. In the **Inspector** view, under **Transform**, ensure th illustration." style="width: 400px;" src="/images/unity-first-vr-sphere.png"/>

**Four walls**:
1. In the menu, go to **GameObject** > **3D Object** > **Cube**.
2. From the **Materials** folder, drag and drop *wall-color* on the cube.
3. In the **Inspector** view, under **Transform**, reset the position to (0,0,0), and set the scale to (0.5,2,20.5) to stretch the wall so that it fits the floor edges neatly.
3. In the **Hierarchy** view, right-click the cube, and do the following:

    a. Rename the cube to *first-wall*.

    b. Duplicate the cube three times to add three more walls and rename each of them to *second-wall*, *third-wall*, and *fourth-wall*, respectively.
4. In the **Hierarchy** view, select *third-wall*, and in the **Inspector** view, under **Transform**, set the rotation to (0,90,0). Repeat the step for *fourth-wall*.
5. In the **Hierarchy** view, select *first-wall*, and in the **Inspector** view, under **Transform**, enter the following values to reposition the wall to surround the floor from all directions. Repeat this step for the rest of the walls.

	*first-wall* to (-10,0,0)

	*second-wall* to (10,0,0)

	*third-wall* to (0,0,10)

	*fourth-wall* to (0,0,-10)

    <image alt="fourth-wall illustration" style="width: 300px;" src="/images/unity-first-vr-setup.png"/>

### Step 3: Adjust camera and lighting.

Adjust the main camera and lighting to get a better view of the scene.

1. In the **Hierarchy** view, select *Main Camera*.
2. In the **Inspector** view, under **Transform**, set the position to (0,10,-20) and rotation to (20,0,0).
3. In the **Hierarchy** view, select *Directional Light*.
4. In the **Inspector** view, under **Transform**, set the rotation to (50,60,0).

### Step 4: Add movement to the player ball.
1. In the **Hierarchy** view, select *player-ball*.
2. In the **Inspector** view, do the following:

    a. Click **Add Component** > **Physics** > **Rigidbody**.

	<image alt="Unity Inspector showing the Rigidbody component added to the player-ball GameObject." style="width: 300px;" src="/images/unity-first-vr-rigidbody.png"/>
	b. Click **Add Component** > **New Script**, set the name to *PlayerController*, and click **Create and Add**.

	c. Click the gear icon next to the *PlayerController* script and click **Edit Script** to open it in the code editor.

	<image alt="Unity Inspector with the PlayerController script gear menu open and Edit Script highlighted." style="width: 300px;" src="/images/unity-first-vr-editscript.png"/>
	d. Replace the sample code with the following to grab the input from the keyboard and add forces to move the ball.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Appears in the Inspector view from where you can set the speed
    public float speed;

    // Rigidbody variable to hold the player ball's rigidbody instance
    private Rigidbody rb;

    // Called before the first frame update
    void Start()
    {
        // Assigns the player ball's rigidbody instance to the variable
        rb = GetComponent<Rigidbody>();
    }

    // Called once per frame
    private void Update()
    {
        // The float variables, moveHorizontal and moveVertical, holds the value of the virtual axes, X and Z.

        // It records input from the keyboard.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Vector3 variable, movement, holds 3D positions of the player ball in form of X, Y, and Z axes in the space.
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Adds force to the player ball to move around.
        rb.AddForce(movement * speed * Time.deltaTime);
    }
}
```

3. In the **Inspector** view, under **PlayerController** script, in **Speed**, enter the speed value. For example, *500*. This variable is the same *speed* variable that we have declared in the *PlayerController* script in step 4.2.d.

   {:width="400px"}

4. Click the Play button from the top to preview your app. Press the arrow keys on your keyboard to roll the ball around.

### Step 5. Add text.

1. On the menu, go to **Game Object** > **UI** > **Text**.
2. In the **Hierarchy** view, select *Text*, and rename it to *message*.
3. Switch from the **Scene** view tab to the **Game** view tab to preview how the text will appear to the player.
4. In the **Inspector** view, under **Rect Transform**, reposition the text. You can also change the font color and font size, if needed.

### Step 6. Change the wall color and display text when the ball enters or exits collision.

The collision occurs when the ball hits the wall. You can add effects to highlight the collision. For example, when the ball collides with the wall, the wall color changes and displays text, *Ouch!*, on the screen. On the other hand, when the ball is back to rolling, the wall color changes to the original color and displays text, *Keep Rolling...*, on the screen.

1. In the **Hierarchy**, select *first-wall*.
2. In the **Inspector** view, click **Add Component** > **New Script**, set the name to *ColorController*, and click **Create and Add**.
3. Click the gear icon next to the *ColorController* script and click **Edit Script** to open it in the code editor.
4. Replace the sample code with the following code.

	```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorController : MonoBehaviour
{
    public Material[] wallMaterial;
    Renderer rend;

    // Appears in the Inspector view from where you can assign the textbox
    public Text displayText;
    // Start is called before the first frame update
    void Start()
    {
        // Assigns the component's renderer instance
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        displayText.text = "";
    }
    // Called when the ball collides with the wall
    private void OnCollisionEnter(Collision col)
    {
        // Checks if the player ball has collided with the wall.
        if (col.gameObject.name == "player-ball")
        {
            displayText.text = "Ouch!";
            rend.sharedMaterial = wallMaterial[0];
        }
    }
    // It is called when the ball moves away from the wall
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "player-ball")
        {
            rend.sharedMaterial = wallMaterial[1];
            displayText.text = "Keep Rolling...";
        }
    }
}
    ```

5. In the **Hierarchy** view, select *first-wall*.
6. In the **Inspector** view, under **ColorController** script, do the following:

	a. Expand **Wall Material**, in **Size**, enter 2, and in **Element 0** and **Element 1**, drag and drop *after-collision* and *wall-color*, respectively.

	b. Click the gear icon next to **Display Text** and select **message**. *Display Text* is the same field that we have declared in the ColorController.cs script in step 6.4.

	<image alt="Unity Inspector showing the ColorController script with Wall Material and Display Text fields." style="width: 400px;" src="/images/unity-first-vr-colorcontroller.png"/>
7. In the **Hierarchy** view, select *second-wall*.
8. In **Inspector** view, click **Add Components** > **Scripts**, select *ColorController* from the list, and repeat step 6.
9. Repeat step 7 and step 8 for the remaining walls.
10. Click the Play button from the top to preview your app. Press the arrow keys on your keyboard to roll the ball around.

### Step 7. Build and run your app.

Depending on the target platform you've selected in build settings, Unity builds either .apk file for Android or .exe file for Windows. Since we have not used the Oculus Integration package, the app may not run with Meta Quest controllers.

1. Save your scene.
2. Go to **File** > **Build and Run**.
3. Double-click the file to run the app on your computer. Use your keyboard or Unity-compatible joysticks for input.