# Unity Passthrough Tutorial Passthrough Color Lut

**Documentation Index:** Learn about unity passthrough tutorial passthrough color lut in this documentation.

---

---
title: "Passthrough Color LUT Tutorial"
description: "Apply color look-up table effects to passthrough using the OVRPassthroughColorLut class in Unity."
last_updated: "2025-01-04"
---

In this tutorial, you will modify the [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial/) to set up a simple color LUT (color look up table)-enhanced passthrough environment. Here are the steps:

- [Create a new scene](#create-a-new-scene) containing just the **Directional Light** and **OVRCameraRig**.
- [Create a short script](#create-the-passthroughcolorlutcontroller-script) that will set the [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/) color LUT property using an existing `Texture2D` asset. This script uses the [`OVRPassthroughColorLut`](/reference/unity/latest/class_o_v_r_passthrough_color_lut) class to impose the color changes.
- [Set up the scene](#add-the-color-lut-texture2d-to-passthroughcolorlutcontroller) and assemble the parts.

**Note**: In this tutorial, we have both the [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/) and the new script components attached to the [`OVRCameraRig`](/reference/unity/latest/class_o_v_r_camera_rig/). (The `OVRPassthroughLayer` is added and configured in the [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial)).

## Before You Begin

- Review the information about [Color Mapping Techniques](/documentation/unity/unity-customize-passthrough-color-mapping/)
- You should first do the [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial/). Once you complete that, you will be ready to do the Passthrough Color LUT tutorial.

## Create a New Scene

1. In the **Project** tab, under the **Assets** folder, create a new folder named **Passthrough Color LUT Tutorial**, and then select it to make it the current folder for new objects.
2. In the **Project Hierarchy**, right click **SampleScene**, and choose **Save Scene As**. Give the new scene a unique name, such as **PassthroughColorLUTTutorial**. This becomes the active scene in the Hierarchy.
3. Remove any game objects from the scene, except for the **Directional Light** and **OVRCameraRig**.

## Create the PassthroughColorLUTController Script

1. On the top menu, go to **Assets** > **Create** > **Scripting** > **MonoBehaviour Script**, and name it **PassthroughColorLUTController**.
2. Double-click the new script to open it.
3. Create a serialized `Texture2D` field by adding the following code snippet to the `PassthroughColorLUTController` class. Later, we will assign our `Texture2D` asset to it in the **Inspector**.

   ```csharp
   [SerializeField]
   private Texture2D _2dColorLUT;
   public Texture2D ColorLUT2D => _2dColorLUT;
   ```

4. In the `Start` script, we want to instantiate the [`OVRCameraRig`](/reference/unity/latest/class_o_v_r_camera_rig/), the [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/), and the `Texture2D` object.

   ```csharp
   private OVRPassthroughColorLut ovrpcl;
   private OVRPassthroughLayer passthroughLayer;

   void Start()
   {
       OVRCameraRig ovrCameraRig = FindObjectOfType<OVRCameraRig>();
       if (ovrCameraRig == null)
       {
           Debug.LogError("Scene does not contain an OVRCameraRig");
           return;
       }

       passthroughLayer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
       if (passthroughLayer == null)
       {
           Debug.LogError("OVRCameraRig does not contain an OVRPassthroughLayer component");
           return;
       }

       ovrpcl = new OVRPassthroughColorLut(_2dColorLUT, flipY: false);
       passthroughLayer.SetColorLut(ovrpcl, weight: 1);
   }
   ```

   For this tutorial, the relevant action is setting the passed-in LUT image into the passthrough layer. When we create the color LUT object, we specify the passed-in LUT image, and a value of `false` for `flipY` (the image we will use is already flipped -- that is, the solid black portion of the image is to the lower right).

   When we call [`SetColorLut`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a58f56420f1a0dd9655dae90da504b759), we provide the passed-in LUT image, and a weight value of `1`, since we want the colors to be taken fully from the LUT.

5. Save the script.

## Assign the Script to **OVRCameraRig**

1. In the **Hierarchy** window, click **OVRCameraRig**.
2. In the **Inspector**, scroll to the bottom and choose **Add Component**.
3. Search for the **PassthroughColorLUTController** and add it.

## Add the Color LUT Texture2D to PassthroughColorLUTController

1. In the **Inspector**, expand the **Passthrough Color LUT Controller** component.
2. Save the following **inverted-lut** image to your **Assets** folder. This image comes from the [Starter Samples project](/documentation/unity/unity-starter-samples/). Make sure the Compression value of the asset is set to `None`.
   
3. Drag the **inverted-lut** asset to the **Passthrough Color LUT Controller** component **2d Color LUT** field.

Your project should resemble the following figure:

{:width="550px"}

## Alternatively, Assign Color LUT Texture directly to OVRPassthroughLayer

You can achieve the same outcome without utilizing a separate script, PassthroughColorLUTController, by directly assigning the Color LUT texture to the OVRPassthroughLayer component.

1. In the **Hierarchy** window, click **OVRCameraRig**.
2. In the **Inspector**, expand the **OVR Passthrough Layer** component.
3. In the **Color Control** dropdown, choose **Color LUT**.
4. Drag the **inverted-lut** asset to the **OVR Passthrough Layer** component **LUT** field. If prompted, select **Fix** image compression.

## Save, Build, and Run the Project

1. On the top menu, go to **File** > **Save**, and then go to **File** > **Save Project**.
2. On the top menu, go to **File** > **Build Profiles** to open the build window.
3. Remove the previous scenes from the **Scenes in Build** pane. Then click **Add Open Scenes**. Only your new **PassthroughColorLUTTutorial** should be in the build.
4. Make sure that the **Run Device** is set to Meta Quest headset that is connected via USB cable.
5. Click **Build and Run**.
6. Save the .APK file.
7. Put on the headset to test the color effect. Your passthrough environment should resemble the following figure:
   {:width="550px"}

## Reference: The Full Passthrough Color LUT Controller Script

For your reference, here is the full controller script.

```csharp
using UnityEngine;

public class PassthroughColorLUTController : MonoBehaviour
{
    [SerializeField]
    private Texture2D _2dColorLUT;
    public Texture2D ColorLUT2D => _2dColorLUT;
    private OVRPassthroughColorLut ovrpcl;
    private OVRPassthroughLayer passthroughLayer;

    void Start()
    {
        OVRCameraRig ovrCameraRig = FindObjectOfType<OVRCameraRig>();
        if (ovrCameraRig == null)
        {
            Debug.LogError("Scene does not contain an OVRCameraRig");
            return;
        }

        passthroughLayer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
        if (passthroughLayer == null)
        {
            Debug.LogError("OVRCameraRig does not contain an OVRPassthroughLayer component");
            return;
        }

        ovrpcl = new OVRPassthroughColorLut(_2dColorLUT, flipY: false);
        passthroughLayer.SetColorLut(ovrpcl, weight: 1);
    }

    void Update()
    {
    }
}
```