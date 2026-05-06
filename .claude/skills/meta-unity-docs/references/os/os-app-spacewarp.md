# Os App Spacewarp

**Documentation Index:** Learn about os app spacewarp in this documentation.

---

---
title: "App Spacewarp"
description: "Render your app at half framerate while the compositor synthesizes full-rate output using motion vectors and depth."
last_updated: "2024-08-16"
---

<oc-devui-note type="note">
This guide explains the technical details behind Application Spacewarp, and how it is implemented in the Quest operating system. To learn how to implement AppSW in your application, go to the <a href="/documentation/unreal/unreal-asw/">Unreal</a>, <a href="/documentation/unity/unity-asw/">Unity</a>, or <a href="/documentation/native/android/mobile-asw/">Native</a> documentation pages.
</oc-devui-note>

Application SpaceWarp (AppSW) is a feature that achieves a step function improvement in both performance and latency at a significant magnitude. It's one of the most substantial optimizations shipped to Quest developers. In our initial testing, it gave apps up to 70 percent additional compute, potentially with little to no perceptible artifacts.

It is an impactful optimization but requires detailed understanding about the technical considerations and tradeoffs to implement it appropriately. To help you use AppSW optimally, we have created this guide.

## Introduction

The guide is broken into the following sections:

* **Tech Overview** - Provides a detailed overview of AppSW
* **API and Integration Considerations** - Describes key considerations worth knowing for both engine and native app developers prior to starting development.
* **Memory Footprint** - Describes memory considerations when using AppSW.
* **Troubleshooting** - Contains testings and debugging info, along with info on known issues and upcoming features.
* **Best Practices** - Describes best practices when implementing AppSW.

## Tech overview

AppSW allows an app to render at half-rate (e.g., 36 FPS vs 72 FPS) by rendering a lower-resolution motion vector buffer and depth buffer that the system uses to synthesize new frames, which results in an output of 72 FPS to the display.

AppSW is designed to only run on one Compositor layer on your app (normally the layer that renders your in-game camera). For applications that render UI on a separate compositor layer, you may need to render your 3d environment at half-framerate with AppSW, and your UI on a separate layer at full framerate, without AppSW.
The tech has two key components: motion vector generation and frame synthesis. In the next sections, we will dive deeper into each component.

### Motion vector generation

With AppSW, it is the app's responsibility to generate motion vector and depth buffer data. The motion vector buffer, also known as velocity buffer, is widely used in 3D rendering for graphics features such as motion blur and temporal anti-aliasing. Apps have significant flexibility to decide how to generate motion vectors.

AppSW defines motion vector as the NDC space position difference between the current frame and previous frame for corresponding pixels. This basically tracks how much the pixel moved in screen space and the depth buffer. To calculate this difference, the app must do the following:

1. The vertex shader transforms each vertex in every object to clip space, for both the current frame, and also the previous frame.
2. The fragment shader receives the previous frame and current frame positions, and converts them into NDC space. The motion vector is simply the difference of the two values.

The following code snippet explains this process more accurately:

```
 /// <vertex shader>
out vec4 currentClipPos;
out vec4 prevClipPos;
void main()
{
  ...
  currentClipPos = ViewProjectionMatrix[viewId] * (LocalToWorld * localVertPos);
  prevClipPos = PrevViewProjectionMatrix[viewId]* (PrevLocalToWorld * prevVertPos);
  gl_Position = currentClipPos;
}

 /// <fragment shader>
in vec4 currentClipPos ;
in vec4 prevClipPos;
out highp vec4 outMotionVector;
void main()
{
   vec3 CurrentNDC = currentClipPos.xyz / currentClipPos.w;
   vec3 PrevNDC = prevClipPos.xyz / prevClipPos.w;
   outMotionVector.xyz =  (CurrentNDC  - PrevNDC);
   outMotionVector.w = 0.0f;
}
```

In the above code snippet, the motion vector is a 3D vector rather than a 2D one, because we take the depth motion into consideration, which further improves frame extrapolation/reprojection accuracy.

Our Unity Meta XR SDKs and Unreal integration introduce a dedicated motion vector pass to render motion vectors into a lower resolution buffer. The reason is, we actually don't need the motion vectors to be full-res. At the time of this writing, the default eye buffer resolution on Quest 2 is 1440x1584, but the default motion vector texture is only 368x400. This is instrumental because it saves GPU cost: a full-res MV buffer would cause an order of magnitude more GPU than it does today! This makes the motion vector pass __almost free__ in GPU cost (almost, but not fully).

We also collect the depth buffer of the motion vector pass, which is the same resolution as the motion vectors, and pass this depth buffer to our compositor so that the compositor can use the buffer (see discussion about Positional TimeWarp down below).

**Note**: Rendering motion vectors in a dedicated pass is just one method. Developers often generate motion vectors for other features and have the freedom to choose their own creative methods to achieve the same goal.

### Frame synthesis

Once the motion vector/depth buffer and eye texture rendering are done, apps can submit them through the [OpenXR API](https://www.khronos.org/registry/OpenXR/specs/1.0/html/xrspec.html#XR_FB_space_warp). Note that if you're a Unity/Unreal developer, this is fully handled for you.

Understanding the two-step process helps you fully understand the technology's boundaries, even though the runtime almost entirely handles this step:

1. Frame extrapolation - With motion vector data, the runtime can predict where the pixel will be in the next frame. The runtime performs this work in the compositor as part of the composition process by moving pixels to their predicted location in the synthesis frame.
2. Depth-based reprojection (Positional TimeWarp) - TimeWarp has been available on Quest since its inception. TimeWarp reduces head rotation latency by reprojecting the eye buffer with the latest HMD pose at the compositing stage. TimeWarp is one of the most important technologies in the compositing software stack and is enabled for every Quest app automatically. However, since apps were not submitting depth, TimeWarp did not know how far away the pixels were, so the reprojection was limited to only rotation correction. Meaning HMD translation does not get corrected in the compositing stage. Now that depth buffers are submitted by the app, we can do depth-based reprojection to reduce HMD translation latency, as well. This results in a more advanced version of TimeWarp called Positional TimeWarp. Since the app is rendering at half rate under AppSW, the inherited HMD rendering latency tends to be larger than when the app runs at full FPS. With Positional TimeWarp, we can largely reduce HMD rendering latency to a level where it is even better than latency on a full FPS app without AppSW.

## Metrics

### Memory footprint

To enable AppSW, the system needs to pre-allocate some extra memory resources, and the app need to create lower resolution motion vector and depth swap chains. Here is a rough estimation of total memory footprint.

| HMD Type | System (pre-allocated once OpenXR extension is enabled) | Motion vector + depth swap chain (allocated by the app) | Total |
|---|---|---|---|
| Quest 3 (1680x1760) | ~5.5MB | ~13.5MB | 18MB |
| Quest 2 (1440x1584) | ~4MB | ~10MB | 14MB |

### Frames per second

Once AppSW is enabled successfully in the app, you can observe the following from `adb logcat -s VrApi`:

```
09-13 22:11:30.350  4270  4365 I VrApi   : FPS=36/72,Prd=39ms,Tear=0,Early=0,Stale=0,VSnc=0,Lat=-3,Fov=0D,CPU4/GPU=3/3,1382/490MHz,OC=FF,TA=0/0/0,SP=N/N/N,Mem=1353MHz,Free=2868MB,PLS=1,Temp=44.5C/0.0C,TW=1.86ms,App=11.42ms,GD=0.00ms,CPU&GPU=20.90ms,LCnt=1,GPU%=0.55,CPU%=0.18(W0.25),DSF=1.00
09-13 22:11:30.351  4270  4365 I VrApi   : ASW=72, Type=App E=-0.000/0.250,D=0.000/0.000
```

You can see a complete breakdown of these stats in the [VrApi Stats Definition Guide](/documentation/native/android/ts-logcat-stats/). A summary of the AppSW-specific stats is listed below.

* **FPS=36/72** - Indicates the app is running at half FPS mode.
* **ASW=72, Type=App** - Indicates that AppSW is enabled and that there are 72 ASW frames in the past second.

You can also track these stats in-headset using   [OVRMetricsTool](/documentation/unity/ts-ovrmetricstool/) . Examine the following stats:

* **FPS** - The frames per second submitted by the app, __not including ASW frames__. In a 72 FPS app with ASW, this is expected to read `36`.
* **ASW FPS** - The frames per second submitted by the app, __including ASW frames__. In a 72 FPS app with ASW, this is expected to read `72`. In an app without ASW enabled, this is expected to read `0`.
* **ASW TYPE** - ASW is enabled if this value is not `0`.

## Debugging

While using AppSW, you may notice elements of your app stuttering, rippling, or stretching.

|  |  |

This is caused by a mismatch between the player's expectations of how an object should move, and the motion vector data.

- Objects with materials that don't render motion vectors will appear to stutter, as they won't move in AppSW-generated frames.
- Transparent objects are difficult with AppSW. The AppSW algorithm only supports each pixel moving in one direction, but a pixel that contains a transparent object and the opaque object behind it can be expected to "move" in two directions.

You can find more information on AppSW artifacts, and how to fix them, at the [Unity AppSpaceWarp Sample on GitHub](https://github.com/oculus-samples/unity-appspacewarp).

The most efficient way to discover which objects are creating AppSW artifacts is to compare the frame output with the motion vector buffer. There are two ways to do this.

- You can do offline analysis on captured frames with [RenderDoc for Oculus](/documentation/unity/ts-renderdoc-for-oculus/). This allows you to step through every draw call in your motion vector buffer to analyze individual missing calls or unexpected values.
- You can render the motion vector buffer to the screen in real-time with the following command. (This can be a [Custom Command in Meta Quest Developer Hub](/documentation/native/android/ts-mqdh-custom-commands/) for one-click application.)
  ```
  adb shell setprop debug.oculus.spaceWarpDebug 1 && adb shell setprop debug.oculus.MVOverlay 4 && adb shell setprop debug.oculus.MVOverlay.Alpha 0.8 && adb shell input keyevent "KEYCODE_POWER" && adb shell input keyevent "KEYCODE_POWER"
  ```
  <oc-devui-collapsible-card heading="Explanation of commands">
    <table>
        <thead>
            <tr>
                <th>Command</th>
                <th>Behavior</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td><code>setprop debug.oculus.spaceWarpDebug 1</code></td>
                <td>Enables debug-rendering App SpaceWarp data.</td>
            </tr>
            <tr>
                <td><code>setprop debug.oculus.MVOverlay 4</code></td>
                <td>Selects which data/style to render in the overlay. See next table for other possible values.</td>
            </tr>
            <tr>
                <td><code>setprop debug.oculus.MVOverlay.Alpha 0.8</code></td>
                <td>Sets opacity of the App SpaceWarp data overlay. Modify as desired.</td>
            </tr>
            <tr>
                <td><code>input keyevent "KEYCODE_POWER"</code></td>
                <td>Same as pressing the power button on your Meta Quest headset. Doing this again re-awakes the headset, allowing the OS to configure new buffers to display the overlay.</td>
            </tr>
        </tbody>
    </table>
    <b>Valid MVOverlay Values</b>
    <table>
        <thead>
            <tr>
                <th>#</th>
                <th>Output</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>1</td>
                <td><img src="/images/asw-mvoverlay-1.png" alt="MVOverlay 1" width="150" height="150" /></td>
                <td>Renders the MotionVector buffer directly. Note that motion in negative axes will not be visible in this view.</td>
            </tr>
            <tr>
                <td>2</td>
                <td><img src="/images/asw-mvoverlay-2.png" alt="MVOverlay 2" width="150" height="150" /></td>
                <td>Render the depth buffer. AppSW uses the depth buffer to identify edges.</td>
            </tr>
            <tr>
                <td>3</td>
                <td><img src="/images/asw-mvoverlay-3.png" alt="MVOverlay 3" width="150" height="150" /></td>
                <td>Renders the MotionVector buffer, multiplied for higher contrast to more easily identify small movement. Note that motion in negative axes will not be visible in this view.</td>
            </tr>
            <tr>
                <td>4</td>
                <td><img src="/images/asw-mvoverlay-4.png" alt="MVOverlay 4" width="150" height="150" /></td>
                <td>Renders the MotionVector buffer, multiplied and moved so zero motion renders gray. This is intended to make all non-zero motion easily visible.</td>
            </tr>
        </tbody>
    </table>
  </oc-devui-collapsible-card>
  While the overlay is enabled, translate the player camera around your scene. Objects should "light up" as the camera translates, or as they move in-world; objects closer to the camera will "light up" with more saturated colors. If an object doesn't "light up", or the overlay does not match its silhouette, that denotes an issue with its AppSW rendering behavior.

  When you are done debugging, run the following command to stop rendering the motion vector buffer. (This can also be a Custom Command in Meta Quest Developer Hub.)
  ```
  adb shell setprop debug.oculus.spaceWarpDebug 0 && adb shell input keyevent "KEYCODE_POWER" && adb shell input keyevent "KEYCODE_POWER"
  ```

## Best practices

### Compositor layer SpaceWarp

Developers often use independent-rendered compositor layers to handle some in-game UIs (e.g. HUDs). Compositor Layer SpaceWarp makes compositor layer movement much smoother regardless of the framerate. Using a compositor layer for game UI is also a general good practice for AppSW apps. Since the layer is rendered independently, it is a way to handle your transparency UI without worrying about AppSW's transparency limitations.

AppSW is a very powerful feature for developers, but we want to make it clear that AppSW may not be suitable for every type of app. It is your responsibility as a developer to decide how and when to use this tool and to thoroughly test your app to confirm there are no graphics glitches or regressions. To understand how to wield the power of AppSW effectively, developers must understand the scenarios where AppSW performs well, as well as the less optimal scenarios.

The following are some scenarios to watch out for as you develop with AppSW.

### Transparency rendering

When you understand the technical details of AppSW, it is clear that rendering transparency will be challenging when using it. For AppSW, we can only have one single value for the motion vector and for depth. The basic principle of transparency is allowing two overlapping pixels at different depths to combine and have their color blended together. Imagine a case where you have a transparent object moving left, on top of an opaque object moving right. For a pixel that contains both objects, the motion vector will be ambiguous, since the pixels are moving in both directions. This is why we usually don't recommend rendering transparent objects into the motion vector pass.

Yet, the magnitude of the issue varies depending on the situation. For example, when the transparency surface is far away from the camera, the result is usually fine because the actual projected motion is very small from frame to frame. Also, for particle effects, a big use case of transparency rendering, the small amount of motion jitter is usually less noticeable because the effect is often combined with fast animations (for example, explosions).

Transparent near-field fast moving objects may result in issues with AppSW. For instance, rendered controllers, which are usually very close to the near plane and may be moving fast, may be problematic. In this scenario, consider designing your controllers, and any child objects of the controllers, to be AppSW friendly.

### Background distortion artifacts

AppSW is a frame extrapolation technique; meaning it moves pixels around. The process has been tuned to reduce potential artifacts but it may introduce some image distortion, particularly in the background. Most video games have backgrounds with rich texture patterns and most users will never notice the distortion (e.g. the image above on the left). However, human eyes are very sensitive to subtle distortions on simple and clean backgrounds like the image above on the right. If you noticed this issue in your app, you may consider modifying your background to be more AppSW friendly. The level of acceptable distortion is a subjective metric, and we recommend testing and tuning until you reach a quality bar that is acceptable for your app.

### Very fast object rotation

If a scene contains some game objects that are rotating very fast, users may see pixel distortion artifacts around the object. For example, imagine a scene where a cube is rotating around at an extremely fast rate, like 100 rotations per second. In this case, the cube's orientation from a given frame to the next frame will seem more or less random. Because the cube moves so fast, the motion vectors cannot accurately be constructed. The motion vector might even point in the opposite direction.

Tuning down the cube's rotation speed can help mitigate the issue. 

This solution allows us to still utilize Positional TimeWarp (head motion will be smooth) while ignoring the object motion vector. In test scenarios, we have seen this solution work very well. Lastly, at some point, if your object is rotating like crazy, the object is unlikely to be noticed as the brain expects to see a lot of object jitter already.

### What if your motion vector is hard to calculate

Theoretically, you can always calculate your game object's motion vector because your app has knowledge about where each vertex of your game objects was in the previous frame. However,  if you have some very complex vertex animation in your vertex shader, it might be hard to calculate the motion vector. In this scenario, you can run the animation twice in the motion vector shader, once for the previous frame and once for the current frame. This may be more trouble than it is worth and avoiding such materials may be easier. If that is not possible, a quick trade-off is to give up the vertex animation's contribution on motion vectors just like we mentioned in the previous section.

Instead of
```
   currentClipPos = ViewProjectionMatrix[viewId] * (LocalToWorld * currAnimatedPos);
  prevClipPos = PrevViewProjectionMatrix[viewId]* (PrevLocalToWorld * prevAnimatedPos);
```
Use
```
   currentClipPos = ViewProjectionMatrix[viewId] * (LocalToWorld * currAnimatedPos);
  prevClipPos = PrevViewProjectionMatrix[viewId]* (PrevLocalToWorld * currAnimatedPos);
```

The vertex animation itself might judder a bit, but head motion and whole object motion will still be smooth. To be very clear, we recommend you generate correct motion vectors for all cases,  however for some edge cases, the above is a workaround to reduce the impact of wrong motion vectors.

### Latency sensitive apps

As we mentioned in the Meta Connect talk, we have done significant work to optimize AppSW latency. Phase Sync and Positional TimeWarp are automatically enabled in OpenXR AppSW apps, and we highly recommend enabling Late Latching in your apps.

When all these techniques are combined, HMD latency results are nearly as good as, if not better than non-AppSW apps. However, controller input latency may still be higher than non-AppSW apps. If your apps has a very high requirement for controller latency,  you may want to consider whether the trade off is appropriate for your app.

## Conclusion

When utilized correctly, AppSW is a powerful feature that can give apps significant additional compute, potentially with little-to-no perceptible artifacts. There may be corner cases or parts of your app that result in other artifacts than what we listed above. Developers can choose to mitigate them by changing content or finding your own workarounds.

We stated above that AppSW may not be for all apps, but developers can mitigate many artifacts through creativity. For example, we called out transparency as a top issue, but trying different approaches, such as rendering a proxy mesh into the motion vector pass so the motion vector can be generated for opaque surfaces, might help you work around it. In some cases, the jitter may not be noticeable at all. Another example of where developers can be creative is rendering the motion vector texture in a dedicated pass. While we recommend this, there are other options such as using MRT or reconstructing the static object's motion vector from depth. There are so many different ways to achieve the same goal. Remember, developers are the owners of the motion vector and you can change it however you want.

In the end, developers have full control on how to leverage AppSW, because it can be enabled and disabled in any frame. It is best practice to take advantage of the benefits of AppSW when you can and to turn it off when the trade off does not meet your quality bar.