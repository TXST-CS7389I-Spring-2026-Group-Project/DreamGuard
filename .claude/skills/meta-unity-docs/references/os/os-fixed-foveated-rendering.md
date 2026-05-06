# Os Fixed Foveated Rendering

**Documentation Index:** Learn about os fixed foveated rendering in this documentation.

---

---
title: "Fixed foveated rendering (FFR)"
description: "Reduce GPU workload by lowering pixel resolution at the edges of the eye buffer where visual acuity is lowest."
last_updated: "2024-12-04"
---

<oc-devui-note type="note">
This guide covers the technical details of fixed foveated rendering (FFR) and its implementation in the Quest operating system. To learn how to implement FFR in your application, go to the <a href="/documentation/unreal/unreal-ffr/">Unreal</a>, <a href="/documentation/unity/unity-fixed-foveated-rendering/">Unity</a>, or <a href="/documentation/native/android/mobile-ffr/">Native</a> documentation pages.
</oc-devui-note>

Meta Quest devices support fixed foveated rendering (FFR). FFR enables the edges of an application-generated frame to be rendered at a lower resolution than the center portion of the frame. This lowers the fidelity of the scene in the viewer's peripheral vision and likely will not be noticed.

This reduction in rendered pixels can provide several benefits:

* Improves framerate in applications with GPU fill bottlenecks.
* Reduces power consumption, and thereby reduces heat and increases battery life.
* Enables applications to increase the resolution of eye textures, which improves the viewing experience, while maintaining performance and power consumption levels.

**Note**: FFR does not rely on eye-tracking. Rather, the high-resolution pixels (the fovea) are fixed in the center of the frame. However, the Meta Quest Pro does have eye-tracking cameras, which are required to place high-resolution pixels where the eye is looking. For more information about this, see [Eye Tracked Foveated Rendering](/documentation/unity/unity-eye-tracked-foveated-rendering/).

FFR has some tradeoffs:

* FFR may not improve performance in applications with simple shaders.
* Applications using FFR should aim to place high-contrast items, such as text, in the center of the frame. Applications that encourage players to look at the edges of the screen (i.e., by placing user interface on your avatar's belt) will cause users to notice the degraded image quality.

## FFR in more detail {#detail}

The image below shows a user's perception of a 135° field of view (hemisphere), with two 20° arcs highlighted. The 2D plane of the screen that renders this view (horizontal line) is overlaid on top. Notice how, when comparing the 20° arc at the edge of the field of view with the 20° arc at the center, the arc at the edge takes up much more of the screen. This distortion is an unavoidable part of rendering a 3D world on a screen.

<image style="width: 400px;" handle="GB_sxgEEfaLq8BoCAAAAAAAAHG4Wbj0JAAAD" src="/images/documentationunreallatestconceptsunreal-ffr-2.png"/>

More pixels are required to create the post-distortion areas at the edge of the FOV than the center of the FOV, resulting in a higher pixel density at the edge of the FOV than in the middle. This is highly counterproductive, since users generally look toward the center of the screen. On top of that, lenses blur the edge of the field of view, so even though many pixels have been rendered in that part of the eye texture, the sharpness of the image is lost. The GPU spends a lot of time rendering pixels at the edge of the FOV that can't be clearly seen. This is very inefficient. FFR reclaims wasted GPU resources by lowering the resolution of these screen portions.

Like most mobile computers, Meta Quest headsets use [tiled rendering](/documentation/unity/gpu-tiled/#tiled-rendering), in which a frame is split into dozens of "tiles" of clustered pixels, and each tile is rendered separately. FFR is implemented by controlling the resolution of individual render tiles on the GPU. When FFR is enabled, tiles that are closer to the edges of the eye buffer are rendered at a lower resolution than tiles closer to the center.

The gains (or losses) provided by FFR typically depend on your application's pixel shader costs. FFR can result in a 25% gain in performance with pixel-intensive applications. On the other hand, applications with very simple shaders, which are not bound on GPU fill, will likely not see a significant improvement from FFR. A highly ALU-bound application will benefit from this, as shown in the graph below that collects GPU utilization on a scene. Given a 16% GPU utilization coming from timewarp (and therefore not affected by FFR), this graph shows a 6.5% performance improvement from the low setting, 11.5% improvement from medium setting, and a 21% improvement from the high setting.

<image style="width: 400px;" handle="GFKBzAGDEzvr8BoCAAAAAAADTCcEbj0JAAAD" src="/images/documentationunreallatestconceptsunreal-ffr-1.png"/>

This demonstrates a best case scenario for using FFR. If you perform the same test on an application that has very simple pixel shaders, it is possible to actually have a net loss on the low setting, due to the fact that the fixed overhead of using FFR can be higher than the rendering savings on a relatively small number of few pixels. In fact, in this situation, you might experience a slight gain with the high setting, but it won’t be worth the image quality loss.

## FFR render examples

The images below show the FFR resolution multiplier map (also called the fragment density map) for the left eye, and an example tilemap for a frame for the left eye, on a Meta Quest 3, at each FFR setting. Note that the positions and sizes of tiles can change depending on your headset and render settings, but will always approximate the FFR resolution multiplier map. The colors represent the following resolution levels:

* White = Full resolution: This is the center of the FOV. Every pixel of the texture is computed independently by the GPU.
* Green = 1/4 resolution: Only one quarter of the pixels are calculated by the GPU.
* Black = no resolution: the GPU does not render anything in this area.

| FFR Setting | Left Eye Resolution Multiplier Map | Example Left Eye Tile Map |
|--|
| Low |  |  |
| Medium |  |  |
| High |  |  |
| High Top (VrApi only) |  |  |

**Note**: The un-calculated (black) pixels on the right edge of the left eye resolution multiplier map are due to rendering with Symmetric Field of View enabled. This is a recommended optimization for Meta Quest devices, in which the left and right eye render at the same field of view, and pixels on the far edge for each eye are not rendered. This results in the same output image as an asymmetric field of view.

**Note**: The resolution multiplier maps for the right eye are just horizontal mirrors of the displayed left-eye maps.

**Note**: High Top is not available when using the OpenXR interface (see [OpenXR, VRAPI, and LibOVR](/documentation/unity/os-openxr-vrapi/)); setting FFR to High Top will be treated like setting to High. In OpenXR, High Top FFR can be generated by using the `verticalOffset` parameter on the High FFR level (see [OpenXR docs](https://registry.khronos.org/OpenXR/specs/1.0/html/xrspec.html#XR_FB_foveation_configuration)).

## Tips and best practices

Many developers simply use the high FFR setting for their entire app as a general solution for performance, which some have found to have a very noticeable impact on visuals. Here are some tips and best practices on better tuning the FFR settings in-game:

* FFR levels can be changed on a per-frame basis and should be changed according to the content being displayed. Starting a new level, opening/closing menus, and entering new map areas are generally good points to consider changing the FFR setting. However, avoid changing FFR levels frequently within the same scene without another transition as the jump between FFR levels can be fairly noticeable.
  * A simple, effective example would be to turn off FFR on menu screens, where there is performance headroom to spare and a lot of text elements, and then turning it on after loading into the game where the performance is needed.
  * Another example would be to set FFR to high on a complex outdoor scene, then turning it off on a simpler indoors level.
* Applications can use FFR for their in-world scene, but preserve the pixel density of user interfaces on the edge of the frame, by placing user interfaces on [Compositor Layers](/documentation/unity/os-compositor-layers/). Compositor layers are not affected by FFR settings.
* The system property `debug.oculus.foveation.level` is a system-wide FFR setting override that can be used to quickly test different FFR settings without changing, reinstalling, or restarting the app, with 0 = Off, 1 = Low, 2 = Medium, 3 = High, 4 = High Top. For example, `adb shell setprop debug.oculus.foveation.level 2` sets the FFR level to medium.
  * When using this system property for debugging, please make sure dynamic foveation is disabled (by `adb shell setprop debug.oculus.foveation.dynamic 0`). Otherwise you won't be able to toggle the foveation level by `debug.oculus.foveation.level` since it is controlling the *maximum* foveation level when dynamic foveation is enabled.
* Use the [VrApi logcat outputs](/documentation/unity/ts-logcat/#collect-meta-quest-vrapi-logs-with-logcat) to determine suitable FFR levels. VrApi generates performance information in logcat every second, including FFR level, GPU rendering times, and average FPS. For instance, a VrApi log that shows an app running at 72 fps (FPS=72), with the app's GPU rendering time at 5.51ms (App=5.51ms), and FFR on high (Fov=3), would indicate more than enough room to turn the FFR level down for a general improvement in the visual quality.
  * When dynamic foveation is enabled, you should see the postfix `D` like `Fov=3D` in the VrApi logcat outputs.