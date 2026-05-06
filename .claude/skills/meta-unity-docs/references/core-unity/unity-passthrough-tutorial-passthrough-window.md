# Unity Passthrough Tutorial Passthrough Window

**Documentation Index:** Learn about unity passthrough tutorial passthrough window in this documentation.

---

---
title: "Passthrough Window Tutorial"
description: "Build a passthrough window effect using custom shaders and layered planes in a Unity scene."
last_updated: "2024-06-26"
---

In this tutorial, you will modify the [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial/) to implement a simple passthrough window. Here are the steps:

- [Create a new scene](#create-a-new-scene) containing just the **Directional Light** and **OVRCameraRig**.
- [Create a large solid plane](#create-a-solid-plane) to use as a backdrop.
- [Create a smaller plane](#create-a-passthrough-plane) in front of the larger one that will allow the passthrough.
- [Create a shader](#create-a-new-shader) that will define how the colors in the passthrough are rendered, based on the scene lighting. (Refer to [Materials, Shaders, and Textures](https://docs.unity3d.com/560/Documentation/Manual/Shaders.html) in the Unity documentation.)
- [Create a material](#create-and-assign-a-shader-material) for attaching the shader.
- [Create a short script](#create-the-passthroughplane-script) that will set the `OVRManager.eyeFovPremultipliedAlphaModeEnabled` property to `false`, and assign the script to the **OVRCameraRig**.

**Note**: In this tutorial, we have both the [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/) and the new script components attached to the [`OVRCameraRig`](/reference/unity/latest/class_o_v_r_camera_rig/). (The `OVRPassthroughLayer` is added and configured in the [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial/)).

## Before You Begin

You should first do the [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial/). Once you complete that, you will be ready to do the Passthrough Window tutorial.

## Create a New Scene

1. In the **Project** tab, under the **Assets** folder, create a new folder named **Passthrough Tutorial**, and then select it to make it the current folder for new objects.
2. In the **Project Hierarchy**, right click **SampleScene**, and choose **Save Scene As**. Give the new scene a unique name, such as **PWScene**. This becomes the active scene in the Hierarchy.
3. Remove any game objects from the scene, except for the **Directional Light** and **OVRCameraRig**.

## Create a Solid Plane

1. On the top menu, go to **GameObject** > **3D Object** > **Plane**, and name it **SolidPlane**.
2. Set **Position** to 10, 0, 10.
3. Set **Scale** to 20, 1, 20. This will cause it to fill the view at runtime.
4. Set **Rotation** to 0, 90, -90 so that the plane faces the **OVRCameraRig** view.
5. Drag the BallMaterial you used in the basic tutorial to **SolidPlane**.

{:width="550px"}

## Create a Passthrough Plane

1. On the top menu, go to **GameObject** > **3D Object** > **Plane**. Name it **PassthroughPlane**.
2. Set **Position** to 10, 0, 5. This will place **PassthroughPlane** in front of **SolidPlane**.
3. Set **Scale** to 1, 1, 1. This makes it smaller than **SolidPlane**.
4. Set **Rotation** to 0, 90, -90 so that the plane faces the **OVRCameraRig** view.

At this point **PassthroughPlane** is between the **OvrCameraRig** and **SolidPlane**.

{:width="550px"}

## Create a New Shader

1. On the top menu, go to **Assets** > **Create** > **Shader** > **Unlit Shader**, and name it **PassthroughPlaneShader**.
2. Double-click the **PassthroughPlaneShader** to open it in your code editor.
3. Immediately above `CGPROGRAM` add
   ```
      BlendOp RevSub
      Blend One Zero, Zero Zero
   ```
4. Adjust `struct v2f` to remove `UNITY_FOG_COORDS(1)`:
   ```
   struct v2f
   {
      float2 uv : TEXCOORD0;
      // UNITY_FOG_COORDS(1) // delete or comment
      float4 vertex : SV_POSITION;
   };
   ```
5. Adjust `struct v2f_vert` to remove `UNITY_TRANSFER_FOG(o,o.vertex);`:
   ```
   v2f vert (appdata v)
   {
      v2f o;
      o.vertex = UnityObjectToClipPos(v.vertex);
      o.uv = TRANSFORM_TEX(v.uv, _MainTex);
      // UNITY_TRANSFER_FOG(o,o.vertex); // delete or comment
      return o;
   }
   ```
6. Adjust `fixed4 frag (v2f i)` so that it just returns the `float4`:
   ```
     fixed4 frag (v2f i) : SV_Target
      {
         return float4(0, 0, 0, 0);
      }
   ```
7. Save the file.

{:width="550px"}

## Create and Assign a Shader Material

1. On the top menu, go to **Assets** > **Create** > **Material**, and name it **PassthroughShaderMaterial**.
2. Click **PassthroughShaderMaterial** to select it. Then, in the **Inspector**, set the **Shader** property to  **PassthroughPlaneShader**.
3. In the **Project** pane, drag the **PassthroughShaderMaterial** to the **PassthroughPlane**.

{:width="250px"}

## Create the PassthroughPlane Script

1. On the top menu, go to **Assets** > **Create** > **Scripting** > **Empty C# Script**, and name it **PassthroughPlaneScript**.
2. Double-click the new script to open it.
3. Adjust the `Start` script as follows:
```
      void Start()
      {
   #if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_ANDROID
         OVRManager.eyeFovPremultipliedAlphaModeEnabled = false;
   #endif
       }
```
4. Save the script.

{:width="550px"}

## Assign the Script to **OvrCameraRig**

1. In the **Hierarchy** window, click **OvrCameraRig**.
2. In the **Inspector**, scroll to the bottom and choose **Add Component**.
3. Search for the **PassthroughPlaneScript** and add it.

{:width="550px"}

## Save, Build, and Run the Project
1. On the top menu, go to **File** > **Save**, and then go to **File** > **Save Project**.
2. On the top menu, go to **File** > **Build Profiles** to open the build window.
3. Remove the previous scenes from the **Scenes in Build** pane. Then click **Add Open Scenes**.
4. Make sure that the **Run Device** is set to Meta Quest headset that is connected via USB cable.
5. Click **Build and Run**.
6. Save the .APK file.
7. Put on the headset to test the passthrough window. You should see a small window of passthrough surrounded by a larger opaque field.

   {:width="550px"}

## Reference: The Full Shader

For your reference, here is the full shader file. For more information on Shaders, refer to [Materials, Shaders, and Textures](https://docs.unity3d.com/560/Documentation/Manual/Shaders.html) in the Unity documentation.

```
Shader "Unlit/PassthroughTutorialShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
           BlendOp RevSub
           Blend One Zero, Zero Zero

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return float4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}

```