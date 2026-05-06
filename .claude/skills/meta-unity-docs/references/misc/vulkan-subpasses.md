# Vulkan Subpasses

**Documentation Index:** Learn about vulkan subpasses in this documentation.

---

---
title: "Vulkan subpasses in Unity"
description: "Add performant post-processing effects and depth input to shaders using Vulkan subpasses on Meta Quest."
last_updated: "2025-5-2"
---

In the Vulkan API, a single render pass consists of one or more subpasses.  Instead of writing the rendering result from GPU memory to system memory between each rendering operation, subpasses conserve memory bandwidth by utilizing the contents of the framebuffers from previous subpasses within the same render pass. For the Meta Quest's tile-based GPUs, subpasses can improve performance for certain rendering operations like post-processing effects and depth lookup.

## Prerequisites

To use subpasses in Meta Horizon apps, there are a few requirements:

- You must be on Unity 2022.3.42f1+ or Unity 6000.0.23f1+
- Vulkan API must be enabled for Android. See the [Unity Graphic APIs documentation](https://docs.unity3d.com/2022.3/Documentation/Manual/configure-graphicsAPIs.html) for more info.
- You must use the correct branch of Meta’s URP fork, which contains some code changes needed to run subpasses. See the setup instructions below for your version to apply the corresponding changes.

## Project setup

<box padding-top="8">
<oc-devui-collapsible-card heading="Unity 6000.0.40+">
    <ol>
        <li>Add the following lines to your project’s <a href="https://docs.unity3d.com/Manual/upm-manifestPrj.html">manifest.json</a> file, removing any lines with the same keys:</li>
        <pre><code>
"com.unity.render-pipelines.core": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.core#6000.0/17.0.4-subpass",
"com.unity.render-pipelines.universal": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.universal#6000.0/17.0.4-subpass",
"com.unity.shadergraph": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.shadergraph#6000.0/17.0.4-subpass"
        </code></pre>
        <li>Navigate to <b>Edit</b> > <b>Project Settings</b> > <b>Graphics</b>. Under <b>Pipeline Specific Settings</b>, scroll down to the bottom and make sure <b>Compatibility Mode (Render Graph Disabled)</b> is <i>unchecked</i>.</li>
    </ol>
</oc-devui-collapsible-card>
<oc-devui-collapsible-card heading="Unity 6000.0.23 - 6000.0.39">
    <ol>
        <li>Add the following lines to your project’s <a href="https://docs.unity3d.com/Manual/upm-manifestPrj.html">manifest.json</a> file, removing any lines with the same keys:</li>
        <pre><code>
"com.unity.render-pipelines.core": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.core#6000.0/17.0.3-subpass",
"com.unity.render-pipelines.universal": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.universal#6000.0/17.0.3-subpass",
"com.unity.shadergraph": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.shadergraph#6000.0/17.0.3-subpass"
        </code></pre>
        <li>Navigate to <b>Edit</b> > <b>Project Settings</b> > <b>Graphics</b>. Under <b>Pipeline Specific Settings</b>, scroll down to the bottom and make sure <b>Compatibility Mode (Render Graph Disabled)</b> is <i>unchecked</i>.</li>
    </ol>
</oc-devui-collapsible-card>
<oc-devui-collapsible-card heading="Unity 2022 LTS">
    <ol>
        <li>Add the following lines to your project’s <a href="https://docs.unity3d.com/Manual/upm-manifestPrj.html">manifest.json</a> file, removing any lines with the same keys:</li>
        <pre><code>
"com.unity.render-pipelines.core": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.core#2022.3/staging-subpass",
"com.unity.render-pipelines.universal": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.render-pipelines.universal#2022.3/staging-subpass",
"com.unity.shadergraph": "git+https://github.com/Oculus-VR/Unity-Graphics.git?path=/Packages/com.unity.shadergraph#2022.3/staging-subpass"
        </code></pre>
        <li>In the <a href="https://docs.unity3d.com/6000.0/Documentation/Manual/urp/urp-universal-renderer.html">URP Renderer</a> used by your <a href="https://docs.unity3d.com/6000.0/Documentation/Manual/urp/universalrp-asset.html">URP Asset</a>, make sure that <b>Native RenderPass</b> is <i>checked</i>.</li>
    </ol>
</oc-devui-collapsible-card>
</box>

## Details

Instead of rendering the entire buffer and storing the result to system memory for every render pass, using subpasses will keep the result of a previous subpass in tile memory so that it can be reused for subsequent subpasses. Since our Meta Quest devices are equipped with mobile GPUs (using tile-based rendering), this results in less transfer between the GPU memory (tile memory) and RAM (system memory), which in turn means better GPU performance.

There are some limitations with subpasses, notably that the frame buffers of each subpass need to have the same dimensions. Another limitation is that only the current pixel coordinates can be used to sample the result of previous subpasses. Because of this, effects like bloom or blurs cannot be implemented using subpasses.

## Use post-processing effects

Post-processing effects that are tile compatible (Channel Mixer, Color Adjustments, Color Curves, Color Lookup, Film Grain, Lift Gamma Gain, Shadows Midtones Highlights, Split Toning, Tonemapping, Vignette, WhiteBalance) can be done more efficiently with subpasses. To use post-processing:

1. In the [URP Renderer](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/urp-universal-renderer.html), check the **Post-processing** > **Enabled** checkbox.
2. In the Camera settings, check the **Rendering** > **Post Processing** checkbox.

This [Unity 2022 sample project](https://github.com/Oculus-VR/Unity-Graphics/tree/2022.3/staging-subpass/TestProjects/PostProcessSubpassSample) and this [Unity 6 sample project](https://github.com/Oculus-VR/Unity-Graphics/tree/6000.0/17.0.4-subpass/TestProjects/PostProcessSubpassSample) use Vulkan subpasses to implement several post-processing effects.

## Use depth input

Subpasses are useful for shaders that require depth sampling. By dividing the object rendering process into two subpasses, you can handle all opaque objects in the first subpass and then render objects that require depth in the second subpass. To achieve this, you will need to properly configure the URP passes and make several modifications to the shader graph / shader code.

### Configure depth input pass

1. Add a new layer called “DepthInputSubpass”. Set this layer on any GameObjects that you want to use a depth lookup shader.
2. In the [URP Asset](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/universalrp-asset.html), make sure **Depth Texture** and **Opaque Texture** are unchecked.
    * Usually these options would need to be enabled to supply shaders with depth information. In the case of Vulkan subpasses, however, enabling these options will create an unwanted copy pass, preventing the opaque and depth input subpasses from merging together and breaking rendering.
3. In the [URP Renderer](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/urp-universal-renderer.html):
    * Under the **Filtering** section, expand the **Opaque Layer Mask** dropdown and uncheck the “DepthInputSubpass” layer that was added in step 1. Do the same for **Transparent Layer Mask**.
    * Set **Compatibility** > **Intermediate Texture** to “Auto”.
    * At the bottom, select **Add Renderer Feature** > **Render Objects**. Set up the feature as follows:
        - Set **Event** to either “AfterRenderingTransparents” or “AfterRenderingOpaques”.
        - Set **Layer Mask** to the “DepthInputSubpass” layer that was added in step 1.
        - Under **Overrides**, check **Depth** and **Depth Input**. Make sure that **Write Depth** is *unchecked*.

### Use depth input in shader graph

To use depth input in a shader graph, you will need to add a special keyword so that the Scene Depth node will read from an input attachment instead of doing texture fetching:

1. Within your shader graph, click the plus icon and select **Keyword** > **Boolean**.
2. Name the new keyword DEPTH_INPUT_ATTACHMENT. Do not change the **Reference** – it must be _DEPTH_INPUT_ATTACHMENT.
3. Set **Definition** to “MultiCompile”.
4. If using Unity 2022 LTS, set **Scope** to “Global”.

This [Unity 2022 sample project](https://github.com/Oculus-VR/Unity-Graphics/tree/2022.3/staging-subpass/TestProjects/DepthInputSubpassSample) and this [Unity 6 sample project](https://github.com/Oculus-VR/Unity-Graphics/tree/6000.0/17.0.4-subpass/TestProjects/DepthInputSubpassSample) use Vulkan subpasses to implement depth input in a shader graph.

### Depth input in HLSL

You can also define the depth input attachment and then load from it in HLSL. When using the render object pass, the attachment index for depth will be set to 0. Define the input attachment inside a shader with the following:

```
#if defined(_DEPTH_INPUT_ATTACHMENT)
    #define depth_input 0
    FRAMEBUFFER_INPUT_FLOAT_MS(depth_input);
#endif
```

Later in the fragment shader, use LOAD_FRAMEBUFFER_INPUT_MS to load the depth from the input attachment:

```
float depth =  LOAD_FRAMEBUFFER_INPUT_MS(depth_input, 0, float2(0,0));
```

This [Unity 2022 sample project](https://github.com/Oculus-VR/Unity-Graphics/tree/2022.3/staging-subpass/TestProjects/DepthInputSubpassSample) and this [Unity 6 sample project](https://github.com/Oculus-VR/Unity-Graphics/tree/6000.0/staging-subpass/TestProjects/DepthInputSubpassSample) use Vulkan subpasses to implement depth input in an HLSL shader.

## Debugging

### Using RenderDoc with subpasses

[Renderdoc Meta Fork](/documentation/unity/ts-renderdoc-for-oculus) can be a valuable tool for verifying if URP passes are merged as subpasses or not. For instance, when using post-processing with subpasses, you can confirm that the subpasses have been merged correctly by checking if they are part of the same Vulkan render pass and by observing that  `vkCmdNextSubpass()` is called between subpasses.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Renderdoc's render stage tracing, which shows subpasses being merged as a single render pass."
      src="/images/rendering-renderdoc-subpass-merging.jpg"
      border-radius="12px"/>
</box>

### Using Render Graph Viewer with Meta XR Simulator

In Unity 6, the new [Render Graph Viewer](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/render-graph-view.html) can also be used for inspecting URP subpasses. This requires using the Unity OpenXR Plugin, [Meta XR Simulator](/documentation/unity/xrsim-intro), and [setting Vulkan as the graphics API](https://docs.unity3d.com/6000.0/Documentation/Manual/configure-graphicsAPIs.html) for Windows/Mac/Linux. When using XR Sim in editor, you can check if the render passes are merged or not. In the image below, the blue bar under the render passes indicates that those have been merged into a single render pass as subpasses. The graph also shows useful information about the access and use of pipeline resources (textures, attachments, etc.).

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Unity 6's Render Graph Viewer, which shows subpasses being merged as a single render pass."
      src="/images/rendering-render-graph-viewer-subpass-merging.jpg"
      border-radius="12px"/>
</box>

## Limitations

* `OnRenderObjectCallbackPass` will prevent the post processing pass from being merged with the draw object pass. In the changes within Meta’s URP fork, this pass is commented out.
* The **Depth Texture** and **Opaque Texture** settings in the URP Asset will trigger a depth/color copy pass and break the rendering. When using Vulkan subpasses, these should always be *unchecked*.
* Due to the limitation that subpasses can only sample the current pixel, the following post-processing effects can’t be implemented with subpasses: Bloom, Chromatic Aberration, Depth of Field, Lens Distortion, Motion Blur, and Panini Projection.
* Due to the requirement that render targets be the same size, [Dynamic Resolution](/documentation/unity/dynamic-resolution-unity) is currently incompatible with the use of subpasses.

## Known Issues

* When using post processing with subpass, the “Use Recommended MSAA Level” setting under OVR Manager will cause a glitch in the first frame. It’s recommended to uncheck it.
* In Unity 6, when playing in the editor with Vulkan as the graphics API and using the Unity OpenXR Plugin, MSAA will cause the rendered result to appear black. To resolve this issue, it is recommended to disable MSAA while playing in the editor.