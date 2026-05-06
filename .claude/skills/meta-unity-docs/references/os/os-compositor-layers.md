# Os Compositor Layers

**Documentation Index:** Learn about os compositor layers in this documentation.

---

---
title: "Compositor layers"
description: "Render textures with higher clarity by bypassing eye buffer resampling and submitting them directly to the compositor."
last_updated: "2025-03-11"
---

<oc-devui-note type="note">
This guide explains the technical details behind compositor layers, and how they are implemented in the Quest operating system. To learn how to implement compositor layers in your application, go to the <a href="/documentation/unity/unity-ovroverlay/">Unity</a>, <a href="/documentation/spatial-sdk/spatial-sdk-2dpanel-layers/">Spatial SDK</a> , <a href="/documentation/unreal/unreal-overlay/">Unreal</a> or documentation pages.
</oc-devui-note>

Compositor layers are a tool provided by [the compositor](/documentation/unity/os-compositor/) which allow you to render crisper, easier-to-read textures at the frame rate of the compositor — which can be faster, and is never lower, than the frame rate of your application. By using compositor layers for rendering text and user interfaces, you can achieve high clarity even if using smaller fonts and UI elements.

Use compositor layers for:
 - text, user interfaces, and videos. Sharper textures allow your app to use smaller font sizes and UI elements, while still remaining readable by the user. (Note: You can use slices of cylinder shapes to create large, curved UIs.)
 - gaze cursors, crosshairs, and other textures intended to be the focal objects in your scene.
 - loading screens or other scenarios where you expect poor application performance. Compositor layer elements in load screens always appear to update at full frame rate because the compositor runs at framerate even if the application fails to submit frames on time.

## Why compositor layers are sharper

To understand how compositor layers provide sharper rendering at independent framerates, it is necessary to understand the path an application takes in rendering to a VR headset.

### Application rendering

As discussed in [The compositor: distorting frames](/documentation/unity/os-compositor/#distorting-frames), game engines such as Unity and Unreal render the contents of their application's scene into two separate, non-distorted eyebuffer textures. Then, the compositor distorts these textures to negate the distortion caused by the lenses on the VR headset.

Throughout this pipeline, an input texture that is rendered in-world is sampled twice:
1. Input texture is sampled, to render it in the in-world scene for the eyebuffer texture
1. Eyebuffer texture is sampled, to render it into the distorted screen output

**Note:** Each round of texture sampling adds blur artifacts, as texel sizes on source textures don't exactly line up with the output texture. Although it is possible to reduce the amount of blur by increasing your eyebuffer size, additional rounds of sampling will always reduce the crispness of the original image or text.

## Compositor layer rendering

As a solution to reduce the number of rounds of sampling, we allow game engines to submit additional textures to the compositor _to be rendered directly to the screen, pre-distorted_.

These additional textures are not rendered by the application. Instead, the compositor renders the selected shape using a special vertex shader that handles distortion correction at vertex shader time. This results in the input texture only having to be sampled once: from source texture into distorted output texture.

There are some restrictions to this approach:
 - You can only select from a pre-defined list of shapes such as quad, cylinder, cubemap, and equirect that have pre-distorted vertex shaders. You are allowed to transform these shapes arbitrarily.
 - You cannot control the pixel shader used. Your texture is rendered with no lighting or effects. However, you can bake lighting and effects into your texture (and change your texture per-frame) if desired.

This pipeline provides multiple benefits to your application:
 - Textures appear sharper due to skipping a layer of sampling.
 - Textures are rendered at the framerate of the compositor, which is always greater than or equal to the framerate of your application.

To learn more about how to implement compositor layers in your engine, see:
- [Use VR compositor layers (Unity)](/documentation/unity/unity-ovroverlay/)
- [Layers and UI quality (Spatial SDK)](/documentation/spatial-sdk/spatial-sdk-2dpanel-layers/)
- [VR compositor layers (Unreal)](/documentation/unreal/unreal-overlay)

## Profiling and debugging compositor layers

Compositor layers have the following discrete performance costs:

- The cost of your app rendering a texture to draw as a compositor layer

   Because this is done entirely within application-space, the profiler tools built into most game engines can give you detailed information on the costs of generating the texture. Costs can be reduced by rendering to a smaller texture, or rendering only every other frame.

 - A flat per-layer cost

   On a Meta Quest 2 at CPU/GPU level 4, every additional compositor layer costs about 0.1ms. Note that the compositor additionally merges headlocked `FIXED_TO_VIEW` layers using the `QUAD` shape into 1 layer. Merged layers have no additional per-layer cost.

 - An additional per-layer cost based on the number of pixels touched by that layer

   This cost is proportional to the number of on-screen pixels covered by that layer. This cost exists even if the layer doesn't render to those pixels. For example, if the layer is an underlay and a higher layer renders an opaque object to that pixel, or if the layer shows captions but there is nothing to caption at the moment.

   On a Meta Quest 2 at CPU/GPU level 4, a fullscreen compositor layer costs about 0.6ms.

It is common for app developers to create compositor layers, and supply them with a 0-alpha texture rather than destroy the compositor layer. Note that you will continue to pay the costs for rendering a compositor layer in this case.

Meta Quest supports up to 16 compositor layers per frame; any layers beyond this limit will not be rendered.

To determine the cost of compositor layers, and to identify "hidden" compositor layers that are still rendering, Meta offers the following tools:

 - [Meta Quest Developer Hub](#mqdh)
 - [VrApi logs](#vrapi-logs)
 - [Compositor layer logs](#layer-logs)
 - [Compositor layer properties log](#layer-prop-logs)
 - [Compositor layer overlays](#layer-overlays)

### Meta Quest Developer Hub {#mqdh}

The "Performance Analyzer" tab of Meta Quest Developer Hub contains controls to toggle visibility of each compositor layer, and display information contained in [Compositor layer property logs](#layer-prop-logs) without using a command prompt.

For more information, see [Layer Visibility Control](/documentation/unity/ts-mqdh-compositor-layer/)

### VrApi logs {#vrapi-logs}

As covered in [VrApi stats definition guide](/documentation/unity/ts-logcat-stats/), you can get detailed realtime performance stats on a Quest application by running the following command in a terminal with a connected `adb` instance.

**Note:** Although the VrApi development API was deprecated in August 2022, the `VrApi` logcat tag continues to emit runtime stats for OpenXR-based apps. For more information, see [VrApi to OpenXR migration](/documentation/unity/os-openxr-vrapi/).

```
adb logcat -s VrApi
```

The following segment of the VrApi stats output is of interest to developers profiling their compositor layers:
```
TW=2.80ms,App=1.11ms,GD=0.23ms,CPU&GPU=6.41ms,LCnt=5(DR14,LM2)
```
 - `TW=#` lists the time spent by the compositor (the name refers to `TimeWarp`, one of the compositor's duties). The cost of your overlay layers are included in this number.
 - `LCnt=#` lists the number of separate layers being combined by the compositor. There is always at least one layer, for your app's frame buffer. Additional layers can come from your app, or from other services (such as notifications).
 - `LM#` lists the number of layers that are merged together, saving compositor time. This number is either 0 (no merges occurred) or 2+ (the number of layers reduced to 1 layer).

### Compositor layer logs {#layer-logs}

If the number of layers reported by `VrApi` logs is unexpected, you can obtain realtime logs which list the compositor layers drawn per-frame, using the following method:

 - In a terminal, run: `adb shell setprop debug.oculus.logLayers 1`
 - Launch your app, and reach a point in your app where you'd like to profile the compositor layers drawn per-frame.
 - In a terminal, run: `adb logcat -s CompositorClient`

Once the above steps are done, for every frame rendered, you will see output in your terminal like the following:
```
LogLayers: Client 12 (com.Sample.UnityAppSpaceWarp:11432)
  Layer 0:
    Type QUAD
    Flags CLIP_TO_TEXTURE_RECT DIRECT_PROJECTION VIEW_FRUSTUM_CULLING
    Quad translation (0.4137005, -0.091204815, -0.9783828)
    Quad rotation (-0.997699, -9.291795e-10, 0.06780008, -6.314374e-11)
    Quad anchor translation (0, 0, 0)
    Quad anchor rotation (1, 0, 0, 0)
    Quad anchor id 0 version 0
    Quad size (0.4, 0.40000007)
    Corners not rounded
    Corner Rect size (0, 0)
    Corner Rect offset (0, 0)
    ApertureId 134bc4800000000c
    Placement 80
    ColorScale (1, 1, 1, 1)
    ColorOffset (0, 0, 0, 0)
    Src Blend Color BLEND_SRC_ALPHA
    Dst Blend Color BLEND_ONE_MINUS_SRC_ALPHA
    Src Blend Alpha BLEND_SRC_ALPHA
    Dst Blend Alpha BLEND_ONE_MINUS_SRC_ALPHA
    SwapChain[0] : TEXTURE2D : 333 : image 0x7024a248f8 (500 x 500) array index 0
    TextureRect[0] : (0.002, 0.002, 0.996, 0.996)
    SwapChain[1] : TEXTURE2D : 333 : image 0x7024a248f8 (500 x 500) array index 0
    TextureRect[1] : (0.002, 0.002, 0.996, 0.996)
  Layer 1:
    Type PROJECTION
    Flags CLIP_TO_TEXTURE_RECT APP_SPACE_WARP PREMULTIPLIED_WITH_VALID_ALPHA
    Content UNKNOWN id 0
    fov[0] (l:-54, r:40, b:-55, t:44)
    fov[1] (l:-40, r:54, b:-55, t:44)
    ApertureId 134bc4800000000c
    Placement 80
    ColorScale (1, 1, 1, 1)
    ColorOffset (0, 0, 0, 0)
    Src Blend Color BLEND_ONE
    Dst Blend Color BLEND_ONE_MINUS_SRC_ALPHA
    Src Blend Alpha BLEND_ONE
    Dst Blend Alpha BLEND_ONE_MINUS_SRC_ALPHA
    SwapChain[0] : TEXTURE2D_ARRAY : 309 : image 0x7024a24a48 (2016 x 1760) array index 0
    TextureRect[0] : (0, 0, 0.80505955, 1)
    SwapChain[1] : TEXTURE2D_ARRAY : 309 : image 0x7024a24a48 (2016 x 1760) array index 1
    TextureRect[1] : (0.19494048, 0, 0.80505955, 1)
LogLayers: Client 9 (com.oculus.ovrmonitormetricsservice:8465)
  Layer 0:
    Type QUAD
    Flags FIXED_TO_VIEW DISABLE_ALPHA_WRITE NO_AR_CLIP
    Quad translation (-1.4901161e-08, 0.34202012, -0.9396927)
    Quad rotation (1, 0, 0, 0)
    Quad anchor translation (0, 0, 0)
    Quad anchor rotation (1, 0, 0, 0)
    Quad anchor id 0 version 0
    Quad size (0.279375, 0.120000005)
    Corners not rounded
    Corner Rect size (0, 0)
    Corner Rect offset (0, 0)
    ApertureId a469a2e400000009
    Placement ffffffff
    ColorScale (1, 1, 1, 1)
    ColorOffset (0, 0, 0, 0)
    Src Blend Color BLEND_ONE
    Dst Blend Color BLEND_ONE_MINUS_SRC_ALPHA
    Src Blend Alpha BLEND_ONE
    Dst Blend Alpha BLEND_ONE_MINUS_SRC_ALPHA
    SwapChain[0] : TEXTURE2D : 241 : image 0x6f833cd4f8 (298 x 128) array index 0
    TextureRect[0] : (0, 0, 1, 1)
    SwapChain[1] : TEXTURE2D : 241 : image 0x6f833cd4f8 (298 x 128) array index 0
    TextureRect[1] : (0, 0, 1, 1)
```

The output lists the Overlay layers in the order they are drawn, from front to back. This sample output indicates:
 - First, `com.Sample.UnityAppSpaceWarp` is rendering a 500x500 quad. This corresponds to an `OVROverlayCanvas` element placed in-scene, with a `Max Texture Size` of 500.
- Then, `com.Sample.UnityAppSpaceWarp` is rendering a 2016x1760 eyebuffer (`PROJECTION`) -- this is the main layer, where your app's main render pass is displayed. This layer will also be processed using App SpaceWarp-specific paths (`APP_SPACE_WARP`).
 - Finally, `com.oculus.ovrmonitormetricsservice` is rendering a 298 x 128 headlocked (`FIXED_TO_VIEW`) texture to a quad. (This is the [Metrics HUD](/documentation/unity/ts-ovrmetricstool/)).

### Compositor layer properties logs {#layer-prop-logs}

Compositor overlays can still appear blurry if the input texture is low-resolution. You can get realtime information about the texture resolutions of your compositor layers, and recommended changes, via the following method:

 - In a terminal with a connected `adb` instance, run: `adb shell setprop debug.oculus.sysPropDebug 1`
 - Double-tap your headset's power button to put the headset to sleep and re-awake it. This restarts some services, and is necessary for the previous prop to take hold.
 - Run: `adb shell setprop debug.oculus.layerProperties 1`
 - Launch your app, and reach a point in your app where you'd like to profile the compositor layers drawn per-frame.
 - Run: `adb logcat -s CompositorVR`

Once the above steps are done, every second, you will see output in your terminal like the following:
```
CLP: Eye:Right LayerType:Quad PanelVisiblity:Visible DevicePPD:20.67 LayerRenderedPPD:23.71 Texture Resolution:512.00x128.00 Recommended Texture Resolution:446.43x111.61 Recommend Apply:None
CLP: Eye:Left LayerType:Quad PanelVisiblity:Visible DevicePPD:20.67 LayerRenderedPPD:24.81 Texture Resolution:512.00x128.00 Recommended Texture Resolution:426.48x106.62 Recommend Apply:None
CLP: Eye:Right LayerType:Quad PanelVisiblity:Visible DevicePPD:20.67 LayerRenderedPPD:10.97 Texture Resolution:300.00x300.00 Recommended Texture Resolution:565.27x565.27 Recommend Apply:Sharpening
CLP: Eye:Left LayerType:Quad PanelVisiblity:Visible DevicePPD:20.67 LayerRenderedPPD:10.89 Texture Resolution:300.00x300.00 Recommended Texture Resolution:569.17x569.17 Recommend Apply:Sharpening
```

This output indicates...
 - `CLP`: Compositor layer properties. This is an arbitrary string to search for in your output.
 - `Eye`: One of `Left` or `Right`. Indicates which eyebuffer is rendering at the time of this output.
 - `DevicePPD`: The pixels per degree of the VR headset. The Meta Quest 2 has 20.67 PPD, indicating a roughly 89x93 degree FOv across its 1832x1920 per-eye display.
 - `LayerRenderedPPD`: The pixels (of this compositor layer's input texture) per degree of your compositor layer shape, as rendered in-world.
 - `Texture Resolution`: The resolution of this compositor layer's input texture
 - `Recommended Texture Resolution`: The ideal resolution for this compositor layer's input texture. At this resolution, when rendering this compositor layer at its current shape & transform, each pixel in the input texture corresponds to exactly 1 rendered pixel in the VR headset.
 - `Recommend Apply`: Either `Sharpening`, `SuperSampling`, or `None`. See [Composition Layer Filtering](/documentation/native/android/mobile-openxr-composition-layer-filtering/#enable-layer-filtering) for details on these algorithms. Although sharpening and supersampling can improve the visual quality of too-small and too-large textures respectively, it is always better to instead supply an input texture at recommended resolution, if possible.

### Compositor layer overlays {#layer-overlays}

You can activate a built-in overlay to visualize the realtime location of each compositor layer with the following command. (This can be a [Custom Command in Meta Quest Developer Hub](/documentation/native/android/ts-mqdh-custom-commands/) for one-click application.)
```
adb shell setprop debug.oculus.sysPropDebug 1 && adb shell input keyevent "KEYCODE_POWER" && adb shell input keyevent "KEYCODE_POWER" && adb shell setprop debug.oculus.visualizeLayers 1
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
              <td><code>setprop debug.oculus.sysPropDebug 1</code></td>
              <td>Enables debug pathways for system tools.</td>
          </tr>
          <tr>
              <td><code>input keyevent "KEYCODE_POWER"</code></td>
              <td>Same as pressing the power button on your Meta Quest headset. Doing this again re-awakes the headset, allowing the OS to configure new buffers to display the overlay.</td>
          </tr>
          <tr>
              <td><code>setprop debug.oculus.visualizeLayers 1</code></td>
              <td>Enables a system overlay that renders compositor layers with contrasting colors.</td>
          </tr>
      </tbody>
  </table>
</oc-devui-collapsible-card>

Once the above steps are done, you will see colored overlays like the following in-headset:

Every compositor layer will have its bounds marked with a different color in the half-opacity overlay rendered over your app. This example image shows an app with 3 layers: the orange main layer, the green underlay, and the light blue overlay. The colors used per-layer are arbitrary, and don't represent anything.

This tool is best used for identifying large compositor layers that aren't being actively used (such as subtitles or menus that aren't currently displayed), which can be significant performance losses.

When you are done debugging, run the following command to stop rendering the motion vector buffer. (This can also be a Custom Command in Meta Quest Developer Hub.)

```
adb shell setprop debug.oculus.sysPropDebug 0 && adb shell setprop debug.oculus.visualizeLayers 0 && adb shell input keyevent "KEYCODE_POWER" && adb shell input keyevent "KEYCODE_POWER"
```