# Unity Asw

**Documentation Index:** Learn about unity asw in this documentation.

---

---
title: "Application SpaceWarp Developer Guide"
description: "Implement Application SpaceWarp (AppSW) in Unity to halve frame rendering costs on Meta Quest."
last_updated: "2026-05-04"
---

<oc-devui-note type="note">

This guide covers how to implement AppSW in your Unity application. To learn how AppSW works, and how to debug it, go <a href="/documentation/unity/os-app-spacewarp/">here</a>.

</oc-devui-note>

Application SpaceWarp (AppSW) is a feature that achieves a step function improvement in both performance and latency at a significant magnitude. It's one of the most substantial optimizations shipped to Quest developers. In our initial testing, it gave apps up to 70 percent additional compute, potentially with little to no perceptible artifacts.

However, enabling AppSW is a serious technical commitment. It **requires modifying your app's materials and render pipeline**; any materials that have not been modified to support AppSW will produce artifacts when running with AppSW.

To help you use AppSW optimally, we have created this guide to discuss the technical considerations and tradeoffs to implement it appropriately.

## API and integration considerations

At the native API level, AppSW is enabled in Quest apps through the OpenXR extension [XR_FB_space_warp](https://www.khronos.org/registry/OpenXR/specs/1.0/html/xrspec.html#XR_FB_space_warp). Our game engine integration handles that for developers, but it is good knowledge for curious developers to understand how it works. Please check our [native developer guide](/documentation/native/android/mobile-asw/) and sample project XrSpaceWarp in the [OpenXR SDK package](/downloads/package/oculus-openxr-mobile-sdk/) to learn more.

## How to Enable AppSW in App

### Prerequisites

AppSW under Unity requires:
 * OpenXR OVRPlugin v34 or higher.
 * The latest release of the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) (recommended for projects on Meta XR SDKs v74+), or the [Oculus XR Plugin](/documentation/unity/unity-xr-plugin/#oculus-xr-plugin).

  <oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

 * URP with SpaceWarp support. For instructions for installing an Oculus-maintained fork of URP, which adds SpaceWarp support, expand the section below that matches your Unity version.
 * Unity 2022.3.15f1 or higher, or Unity 6000.0.9f1 or higher (recommended)
    * **Note:** It is a good idea to always get the latest version of a stable Unity release. So even though 2022.3.24 may be the minimum version for your target URP version, you should always get the latest stable version, whatever minor version that may be.

Although it is possible to implement SpaceWarp support manually in any custom scriptable rendering pipeline, the simplest way is to use an Oculus-maintained fork of URP, which adds SpaceWarp support. To use them, expand the section below that matches your Unity version and follow the instructions within.
<oc-devui-collapsible-card heading="Unity 2022.3.11 - 2022.3.17">
  <p>Add the following lines to your manifest file, removing any lines with the same keys. This uses the <code>2022.3/14.0.8-oculus-app-spacewarp</code> branch (<a href="https://github.com/Oculus-VR/Unity-Graphics/tree/2022.3/14.0.8-oculus-app-spacewarp">GitHub</a>).</p>
  <pre class="prettyprinted"><code>
"com.unity.render-pipelines.core": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.core#2022.3/14.0.8-oculus-app-spacewarp",
"com.unity.render-pipelines.universal": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.universal#2022.3/14.0.8-oculus-app-spacewarp",
"com.unity.shadergraph": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.shadergraph#2022.3/14.0.8-oculus-app-spacewarp"
  </code></pre>
</oc-devui-collapsible-card>
<oc-devui-collapsible-card heading="Unity 2022.3.18 - 2022.3.23">
  <p>Add the following lines to your manifest file, removing any lines with the same keys. This uses the <code>2022.3/14.0.9-oculus-app-spacewarp</code> branch (<a href="https://github.com/Oculus-VR/Unity-Graphics/tree/2022.3/14.0.9-oculus-app-spacewarp">GitHub</a>).</p>
  <pre class="prettyprinted"><code>
"com.unity.render-pipelines.core": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.core#2022.3/14.0.9-oculus-app-spacewarp",
"com.unity.render-pipelines.universal": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.universal#2022.3/14.0.9-oculus-app-spacewarp",
"com.unity.shadergraph": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.shadergraph#2022.3/14.0.9-oculus-app-spacewarp"
  </code></pre>
</oc-devui-collapsible-card>
<oc-devui-collapsible-card heading="Unity 2022.3.24+">
  <p>Add the following lines to your manifest file, removing any lines with the same keys. This uses the <code>2022.3/14.0.10-oculus-app-spacewarp</code> branch (<a href="https://github.com/Oculus-VR/Unity-Graphics/tree/2022.3/14.0.10-oculus-app-spacewarp">GitHub</a>).</p>
  <pre class="prettyprinted"><code>
"com.unity.render-pipelines.core": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.core#2022.3/14.0.10-oculus-app-spacewarp",
"com.unity.render-pipelines.universal": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.universal#2022.3/14.0.10-oculus-app-spacewarp",
"com.unity.shadergraph": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.shadergraph#2022.3/14.0.10-oculus-app-spacewarp"
  </code></pre>
</oc-devui-collapsible-card>
<oc-devui-collapsible-card heading="Unity 6000.0.9+">
  <p>Starting with Unity 6000.0.9f1, URP includes support for SpaceWarp by default when using RenderGraph. We also maintain a fork of URP with legacy render pipeline support, if needed.</p>
  <p>For legacy render pipeline support, add the following lines to your manifest file, removing any lines with the same keys. This uses the <code>6000.0/oculus-app-spacewarp</code> branch (<a href="https://github.com/Oculus-VR/Unity-Graphics/tree/6000.0/oculus-app-spacewarp">GitHub</a>).</p>
  <pre class="prettyprinted"><code>
"com.unity.render-pipelines.core": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.core#6000.0/oculus-app-spacewarp",
"com.unity.render-pipelines.universal": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.universal#6000.0/oculus-app-spacewarp",
"com.unity.shadergraph": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.shadergraph#6000.0/oculus-app-spacewarp"
  </code></pre>
</oc-devui-collapsible-card>

**Note**: The URP fork with support for SpaceWarp is just a reference. If you’re using the stock URP with no modifications, then feel free to simply grab our fork. However, if you’ve either made local modifications to the URP, or if your project’s render pipeline is a customized SRP, then you’ll likely be better off simply adding the code yourself as an addition to your current SRP. To do so, it will be helpful to look at the changes that the branch has, by viewing [this commit which exposes the required changes made to the URP](https://github.com/Oculus-VR/Unity-Graphics/commit/cab44411c3a4f5095c6eae6a56442633831727e3).

### Enabling AppSW

1. In **Project Settings** > **Player** > **Android** > **Other Settings** > **Graphics APIs**, ensure your only Graphics API is **Vulkan**. AppSW is not supported under any other Graphics API.
2. Enable Application SpaceWarp in your project's OpenXR plugin.
 * If using the [recommended Unity OpenXR plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin), in **Project Settings** > **XR Plug-in Management** > **OpenXR** > **All Features**, ensure **Meta XR Space Warp** is enabled.
 * If using the [deprecated Oculus XR plugin](/documentation/unity/unity-xr-plugin/), in **Project Settings** > **XR Plug-in Management** > **Oculus** > **Android** > **Experimental**, ensure **Application SpaceWarp (Vulkan)** is enabled.

3. To enable AppSW rendering for a given frame, use the API in the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/), `OVRManager.SetSpaceWarp`. This can be toggled on and off at any time during runtime. If you’d like to turn SpaceWarp on and never turn it off, then calling this API on the first frame will be sufficient. However, if your main camera changes at runtime, it is important to call `OVRManager.SetSpaceWarp` whenever this happens to ensure that position changes to the main camera are transmitted to the runtime.

### Adding Motion Vector Passes to Shaders

Using a URP fork with support for SpaceWarp will cause all built-in URP materials (such as `Universal Render Pipeline/Lit`) to automatically support motion vector passes, where possible. However, you will need to manually add motion vector passes to any custom shaders your app uses.

See the [Unity-AppSpaceWarp Sample](https://github.com/oculus-samples/unity-appspacewarp) for examples of [how to add motion vector passes to custom shaders](https://github.com/oculus-samples/unity-appspacewarp?tab=readme-ov-file#custom-shaders), and [handling transparency with App SpaceWarp](https://github.com/oculus-samples/unity-appspacewarp?tab=readme-ov-file#transparent-objects).

## How to Use the SRP to Generate Motion Vectors

The most important part of the Unity integration consists of how to generate correct motion vectors. After all, the quality of your AppSW apps will only be as good as the quality of the motion vectors that you generate. This means that if you generate incorrect motion vectors, you will often see undesirable artifacts that should be avoided.

If you’re using the stock/unmodified URP, then we would recommend you use the Oculus URP fork and use the branch to get the URP version that supports rendering the correct motion vectors. If you’re using a modified URP or custom SRP from scratch, like a lot of developers use, then you should probably check out the fork branch to see how we’re doing it, so you can understand how to apply it to your apps as well.

There are a few things to keep in mind, including:
1. The motion vector pass, MotionVecPass
2. How the pass is enqueued, including specific properties of the pass (view-projection matrices, culling results, etc.)
3. The shaders that correspond to the motion vector pass

These ideas come together to allow the SRP to add a new pass, the motion vector pass, and populate it based on motion in the scene. Let’s look at each individually.

### OculusMotionVectorPass

The [OculusMotionVectorPass](https://github.com/Oculus-VR/Unity-Graphics/blob/2022.3/14.0.7-oculus-app-spacewarp/Packages/com.unity.render-pipelines.universal/Runtime/Passes/OculusMotionVectorPass.cs) is a standard scriptable render pass, and looks similar to many of the other scriptable render passes in the URP. Mostly, it just boils down to using [ScriptableRenderContext.DrawRenderers](https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.DrawRenderers.html) API. This API essentially checks every renderer in the scene, and based on a large set of criteria such as culling results, whether the renderer’s shader matches a ShaderTagId, decides whether or not to include each object in the drawing pass, as well as which shader pass to use.

This is where the motion vector shader comes in. The shader itself is described in detail below, but the key idea inside `OculusMotionVectorPass` is that we only want to include renderers in the pass whose shaders explicitly have a `LightMode` of `MotionVectors`. This is a handy way to include specific shaders that line up with objects we want to include in the motion vector pass (nearly all opaque objects should be here, as an example), while excluding shaders who we don’t want in the MV pass, such as transparent objects. You can see how, in `OculusMotionVectorPass`, we use `CreateDrawingSettings` to filter by this `ShaderTagId`.

Once we set the appropriate `DrawingSettings` and `FilteringSettings`, we’re good to call `DrawRenderers`, and then Unity performs rendering work for the motion vector pass.

#### How the Pass Is Set Up and Enqueued with the Right Properties

Check the code from the branch inside ForwardRenderer to see how we’re creating and enqueuing the motion vector pass. Most of that code is fairly straightforward and follows the mold of other render passes.

You’ll notice that we have a specific check for: `“if(cameraData.xr.motionVectorRenderTargetValid)”`

This is at several places in the URP changes. You should always use this API to verify whether the motion vector render target is valid and ready to be used for AppSW on a given frame. If this is false, then AppSW will not occur on the given frame, which can happen for a variety of reasons, such as:

1. You called `OVRManager.EnableSpaceWarp(false)`, which disables AppSW until you enable it again. Then, `motionVectorRenderTargetValid` will return false.
2. Any other reason that means the texture is not valid. For example, you may have forgotten to check the AppSW checkbox in Unity.

Once the pass is enqueued, it will go through the standard per-frame, per-pass logic that occurs in the Unity URP(and probably in your custom SRP if you’re using one). This includes:

1. Setting the render target attachments.
 * This ultimately means that the URP call to [SetRenderTarget](https://docs.unity3d.com/ScriptReference/Rendering.CommandBuffer.SetRenderTarget.html), which occurs right before we perform rendering work, will set these motion vector color+depth attachments.
 * We are writing the 3D motion vector data to the R, G, and B channels in the color texture, and the depth buffer data is simply recorded through ordinary use of the depth buffer in this render pass.
2. Setting certain uniform buffer properties, such as those related to the camera(see next section below), via the C# API [SetGlobalMatrixArray](https://docs.unity.cn/ScriptReference/Rendering.CommandBuffer.SetGlobalMatrixArray.html).
3. Finally, pushing rendering work onto the render thread, which will actually perform the rendering for the motion vector pass.

#### Camera Properties

One of the most important details about the motion vector pass is that it must share the exact same camera properties as the traditional eye texture forward pass which is always rendered. After all, the motion vectors are very much a 1:1 mapping. The whole point of the motion vectors are to describe the motion of the eye textures, from frame n-1 to frame n, in NDC space.

So, it’s **extremely crucial** to ensure that your motion vector pass has the same camera parameters as the eye texture pass. This includes:

1. View-projection matrix (and other camera matrices) must be identical.
 * If they are different, then the projection will be different and then the AppSW algorithm will not apply the motion vector properly.
2. Late-latching on/off. This is kind of a subset of 1, since late-latching just affects view-projection and objectToWorld matrices, but ultimately if one of your passes is using late latching, the other must use it also.
 * To understand why, imagine what would happen if you enable late-latching for your eye texture render pass, but not for the motion vector render pass. Then, the eye texture render pass would ultimately render with a late-latched, lower latency view-projection matrix, but the motion vector render pass would render with a higher latency, non-late-latched matrix. This would mean the motion vector pass would not in fact line up with the eye texture pass, which would make the motion vectors incorrect.
 * To avoid this, just ensure that the end-resulting matrices in uniform buffers are always the same in your motion vector pass as they are in your eye texture pass.

#### Mesh Renderer Setup

In order to ensure motion vectors are rendered for an object, you need to set the [Motion Vector Generation Mode](https://docs.unity3d.com/ScriptReference/Renderer-motionVectorGenerationMode.html) for each Mesh Renderer in your scene. For Unity 2022.2 and earlier, this mode should be set to [`Per Object Motion`](https://docs.unity3d.com/ScriptReference/MotionVectorGenerationMode.Object.html) for all opaque objects in your scene. This setting can be found under 'Additional Settings' in every Mesh Renderer.

In Unity 2022.3, we additionally added support for the [`Camera Motion Only`](https://docs.unity3d.com/ScriptReference/MotionVectorGenerationMode.Camera.html) mode. This mode should be specified for all static objects in your scene (i.e., environment, buildings, etc.). Using the `Camera Motion Only` mode opens the option of reusing the depth information generated when the scene was initially rendered, which can make the Motion Vector pass much faster.

To enable this mode, you will need to turn on depth submission for the XR plugin that you are using.
- If you are using the Unity OpenXR Plugin, set the depth submission mode to 16-bit or 24-bit depth in the Unity OpenXR Plugin menu.
- For the Oculus XR Plugin, you should check the 'Depth Submission (Vulkan)' checkbox in the Oculus XR Plugin menu.

**Note**: It is very important for performance to ensure that the `Optimize Buffer Discards (Vulkan)` checkbox is checked for either XR plugin. This option can be found in the main Oculus XR Plugin settings menu, and for OpenXR inside the Meta Quest options menu.

#### Shader Motion Vector Changes

Finally, let’s talk about shader changes required. The bulk of the shaders can be found in [`OculusMotionVectorCore.hlsl`](https://github.com/Oculus-VR/Unity-Graphics/blob/2022.3/14.0.7-oculus-app-spacewarp/Packages/com.unity.render-pipelines.universal/ShaderLibrary/OculusMotionVectorCore.hlsl). These contain a standard vertex and fragment shader. A simple summary of each is that the vertex shader transforms both the current vertex position and the previous vertex position down to clip space, then rasterization occurs, and in the fragment shader, the NDC space positions of both the previous frame and current frame are calculated, and finally, their difference is the resulting motion vector.

If your project consists of fairly simple shaders, or of just the default URP shaders such as Lit and SimpleLit, then this will either be supported out of the box, or you can follow the [model](https://github.com/Oculus-VR/Unity-Graphics/blob/2022.3/14.0.7-oculus-app-spacewarp/Packages/com.unity.render-pipelines.universal/Shaders/Lit.shader#L470-L487) that we used to add support to Lit / SimpleLit.

Specifically, you must add a Pass of `LightMode="MotionVectors"` into every single shader that you use in the project for which you’d like to generate the motion vectors. We don’t feel that this is too large of a burden, since most of the work will consist purely of copy-pasting the exact pass of type MotionVectors that you can see in the branch in the Lit or SimpleLit shaders. Keep in mind that if you don’t add this pass to every shader, then motion vectors will not be rendered, for the reason above about the ShaderTagId of MotionVectors being applied in MotionVecPass. This is a filter which will only render objects of a given LightMode.

For Opaque shaders that use Alpha Clip, it's important to use a custom Motion Vector shader that performs the same logic as your Forward pass to calculate the alpha value that is used to clip. After that, the fragment shader should use the same calculation found in OculusMotionVectorCore.hlsl to determing the resulting color value.

You likely will only want to add the Pass of `LightMode=”MotionVectors”` in your opaque objects’ shaders, not your transparent ones because of the aforementioned possible transparency issues with AppSW. However, there are a few exceptions where you will want to generate motion vectors for transparent shaders. For UI shaders that are mostly opaque content, but use alpha for rounded edge, or for text that has fully opaque letters, you will want to add a motion vector pass that uses Alpha Clip instead of Alpha Blend. Additionally, for mostly opaque but semi transparent objects, you may get better results from SpaceWarp if you do generate motion vectors (still using Alpha Clip, as motion vectors cannot correctly be blended between objects).

In order to enable Motion Vectors for objects in the transparent Queue, you will need to make a modification to the URP fork: Where OculusMotionVectorPass is created in [Universal Renderer](https://github.com/Oculus-VR/Unity-Graphics/blob/2022.3/14.0.7-oculus-app-spacewarp/Packages/com.unity.render-pipelines.universal/Runtime/UniversalRenderer.cs#L284), change `RenderQueueRange.opaque` to `RenderQueueRange.all`. You may additionally need to change `data.opaqueLayerMask` to `data.opaqueLayerMask | data.transparentLayerMask` depending on your application setup.

##### Custom Shaders

One call-out that we’d like to make is regarding use of highly customized shaders. One use case we’ve seen is around avatar rendering. Consider an example case whereby the vertex buffer (traditional inputs to the vertex shader) do not contain vertex positions, and instead these vertex positions are accessed by the vertex shader sampling from a texture which is an input to the shader.

In this case, to properly support motion vectors, the texture which provides the vertex positions will also need to be augmented to add previous vertex positions that can be accessed by the motion vector vertex shader. Then, the motion vector vertex shader will have enough information to transform both the current and the previous vertex positions down to NDC space, from which the final motion vector is constructed.

If your use case matches this, it can still be fully supported, but it just needs a little more extra work to pass the previous vertex positions so that they can be accessed by your customized shader.

### Conclusion

A high level summary of using AppSW with the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) is as follows:

1. Make sure to grab all of the right plugin/Engine/URP versions for your Unity version.
2. You can toggle SpaceWarp on/off per-frame by using the `OVRManager.SetSpaceWarp` API from the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/). A rule of thumb is that you essentially want to be calling this API to set SpaceWarp on right before entering a scene or region when SpaceWarp is enabled, and turn it off when you’re leaving the scene or want to disable SpaceWarp. This API is helpful as it will then percolate down to the URP, in the value `if(cameraData.xr.motionVectorRenderTargetValid)`, and this value will be true on any frame where SpaceWarp is enabled, and false on any frame where it’s disabled. This way, you can gate all of your URP-side rendering work, and only perform it if SpaceWarp is on.
3. If you're using the URP without modifying shaders, you can likely proceed by simply adding the forked branch to your project. If you’re using the URP and have written your own shaders, you can follow [the CustomShaders scene in the Unity-AppSpaceWarp sample project](https://github.com/oculus-samples/unity-appspacewarp?tab=readme-ov-file#custom-shaders) to modify your shaders to support AppSW. If you're using a customized SRP, review the changes above to create your motion vector pass. Use the correct shaders and set the proper matrices to align with your eye texture pass.
4. The more custom that your shaders are, the more work you’ll have to do to enable SpaceWarp. But if you understand what the concept of the motion vector rendering is, which is to take the difference of the previous frame’s vertex position transformed down to clip space in the frag shader, and that of the current frame, then you can extend nearly any shader with motion vector support, even custom use cases that you may have.

## Step by Step Unity Guide and Sample Project

We also prepared a sample Unity project in the fork in order to demonstrate exactly how AppSW works, in an already working end-to-end state. This will make the integration of the above steps easier, since you will have an already working project that has all of these components.

1. Navigate to the project in the same branch of the fork repo, which lives at the path `TestProjects/OculusAppSpaceWarpSample`. Switch the project to the “Android” platform, like normal when building Quest apps.

2. You can import the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) from the Asset store or get it bundled in the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/).

    When you choose which folders to import, only choose the **VR** folder for now, since this sample project doesn’t have dependencies on the other folders.

    When the import occurs, follow the update prompt, and be sure to press **YES** for OpenXR, since we are only supporting AppSW with the OpenXR backend. You’ll be prompted to restart Unity.

    **Note**: The Oculus XR Plugin uses the OpenXR backend.

3. As a sanity check, your in-Unity directory structure should look exactly like this, now. If it doesn’t, you made a mistake above:

    

4. Now, it’s time to double click that `ScenePackage.unitypackage`, the 5th icon from the left. Double click that, and import the scene+script combination.

5. Finally, open up the scene `ScenePackage/SpaceWarpScene.unity`.
 Your player settings region should look like this, and your Oculus settings in the XR Plugin region should look like the next image. If it does, you are ready to build the APK:

 <box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Player Settings"
      src="/images/asw-sample4.png"
      border-radius="12px"/>
</box>

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Player Settings 2"
      src="/images/asw-sample5.png"
      border-radius="12px"/>
</box>

  Now, simply build and run the APK like normal.

### Testing SpaceWarp Using the App

Now that SpaceWarp is working in this sample app that you just built, you can toggle SpaceWarp on/off by merely hitting the right controller “B” button. There are 2 ways to test that the feature is working on your end:

1. Simply open the app normally, and toggle B on/off. Keep a tab running `adb logcat -s VrApi` in the background, or alternatively, you can use OVRMetricsTool for this if you choose. When SpaceWarp is on, you will see the per-frame VrApi log line have an `ASW=<FPS>` log line, which is how you know that SpaceWarp is running.
In this case, to see that SpaceWarp is working, the key visual details to notice will be the fact that there are little to no perceptible artifacts in the scene, yet you can see from the `FPS=` line in the VrApi output that the FPS will only be half rate (36 FPS, but can be 45 in your actual production app if you’re on a 45/90 cadence). So, this is a great test to verify that SpaceWarp works because you can see rendering working with a high quality bar, at a half frame rate.
2. (Recommended) Close the app, and before opening it, run:

```
adb shell setprop debug.oculus.sysPropDebug 1
adb shell setprop debug.oculus.swapInterval 2
adb shell input keyevent 26
```

Then wait for a second, and run

```
adb shell input keyevent 26
```

Now you’re ready to open the app. The app will be explicitly in 36 FPS mode right now, by default. This is so that you can compare the artifacts of “normal 36 FPS mode” with how SpaceWarp looks. Simply like before, toggle the “B” button. If SpaceWarp works on your end, you will see the artifacts all pretty much disappear. However, the key here is that both versions of the app are running at 36 FPS. That’s the magic of SpaceWarp: the app is still running at half rate, but the quality can rival a full-rate experience.

#### Important Final Step: Understand What's Going On

Now that everything is working, the key step is to figure out exactly what this very simple sample project is doing, down to the details of the shaders that it’s using (just Lit and SimpleLit), how we added the motion vector pass to those, and how one might add the same pass to your own custom shaders.

RenderDoc is highly recommended, since you will want to trace the execution of the additional motion vector render pass and figure out exactly what the contents of that graphics pipeline are. What texture attachments are being rendered to (a 368x400 color+depth buffer pair), how the opaque draw calls are writing the motion vector into the R, G, & B channels, etc. Once you feel like you understand the execution through RenderDoc, you will be well-equipped to actually implement the same tech in your own projects, with your own custom shaders.