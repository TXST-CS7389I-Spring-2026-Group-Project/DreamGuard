# Ts Renderdoc For Oculus

**Documentation Index:** Learn about ts renderdoc for oculus in this documentation.

---

---
title: "Use RenderDoc Meta Fork for GPU Profiling"
description: "GPU debugging and frame analysis tool for Meta Quest with render stage tracing and shader inspection."
last_updated: "2024-11-27"
---

RenderDoc is a graphics debugging tool that supports multiple graphics APIs and development platforms. It is used for frame capture and analysis during development. RenderDoc shows how a running application's engine determined a scene should be rendered on Meta Quest hardware for a single frame. This information can often be used to find optimization opportunities and potential sources of performance issues.

Meta now maintains its own fork of RenderDoc. This fork provides access to low-level GPU profiling data from Meta Quest's Snapdragon 835, Meta Quest 2's Snapdragon XR2, Meta Quest Pro's Snapdragon XR2+, and the Snapdragon XR2 Gen 2 in Meta Quest 3 and Meta Quest 3S [TODO: verify Quest 3 and Quest 3S support with RenderDoc team before publishing], specifically information from the GPU's tile renderer.

To install RenderDoc Meta Fork, the installer for Windows and Mac is available on our [Downloads](/downloads/package/renderdoc-oculus/) page.

<oc-devui-note type="important" heading="UNINSTALL PREVIOUS VERSION">Developers using a version of RenderDoc for Oculus must manually uninstall before installing RenderDoc Meta Fork.</oc-devui-note>

## Use Cases

RenderDoc Meta Fork can perform a tile-level render stage trace for a single frame of an app on a connected Meta Quest. After completing a successful trace, the results can be viewed in the new **Tile Timeline** view, which can be found at **Window > Tile Timeline**. This timeline shows each surface rendered sequentially during the frame, as well as the render stages for each tile on a given surface, with accurate timing information.

RenderDoc Meta Fork can also perform a draw call trace that collects up to 48 low-level metrics pertaining to each individual draw call in the capture. To get started, go to **Window** > **Performance Counter Viewer**. For a list of these metrics, see [Draw Call Metrics](/documentation/unity/ts-draw-call-metrics/).

RenderDoc Meta Fork offers access to Meta Quest shader stats through the Vulkan extension `KHR_pipeline_executable_properties`. For more information on retrieving shader stats and interpreting them, see [Accessing Vulkan Shader Stats](/documentation/unity/ts-renderdoc-shaderstats/).

RenderDoc Meta Fork can validate Vulkan apps for issues when loading a capture.

RenderDoc Meta Fork supports both OpenGL and Vulkan apps, except in the case of Vulkan shader stats and API validation. Note that apps used with this tool must be development builds.

A capture is necessary to perform render stage traces, draw call traces, and to retrieve Vulkan shader stats. Read [Taking and Loading a Capture](/documentation/unity/ts-renderdoc-capture/) before going through other sections of the documentation.

### Render Stage Trace and the Tile Timeline

The Tile Timeline is a new UI element that presents the results of a render stage trace. Due to the Meta Quest GPU's tiled architecture, rendering of a surface is divided into bins and then executed in stages. Using the Tile Timeline view, developers can identify hidden states such as the rendering mode of a surface or the number of bins executed.

### Draw Call Trace and the Performance Counter Viewer

The Performance Counter Viewer is a new UI element that presents the results of a draw call trace. It displays user-selected metrics pertaining to each draw call. See [Draw Call Metrics](/documentation/unity/ts-draw-call-metrics/) for information on available metrics.

### Vulkan Shader Stats

Vulkan shader stats are accessed by loading a capture and selecting an individual draw call. To retrieve shader stats, select a value from the **Pipeline State** panel (available under **Window**), and then click **View**. For more information, see [Accessing Vulkan Shader Stats](/documentation/unity/ts-renderdoc-shaderstats/).

### Vulkan Validation

Vulkan validation errors can negatively affect app performance. RenderDoc Meta Fork can enumerate these errors when opening a Vulkan capture. Enable API validation on replay when opening your capture to see any errors. See [Taking and Loading a Capture](/documentation/unity/ts-renderdoc-capture/) for more information.

## Using RenderDoc Meta Fork

See the following topics that detail the use of RenderDoc Meta Fork:

* [Taking and Loading a Capture](/documentation/unity/ts-renderdoc-capture/)
* [Performing a Render Stage Trace](/documentation/unity/ts-renderdoc-renderstage/)
* [Performing a Draw Call Trace](/documentation/unity/ts-renderdoc-drawcall/)
* [Draw Call Metrics](/documentation/unity/ts-draw-call-metrics/)
* [Accessing Vulkan Shader Stats](/documentation/unity/ts-renderdoc-shaderstats/)
* [Recommended Settings](/documentation/unity/ts-renderdoc-settings/)

See the following guides for information on using RenderDoc Meta Fork to optimize your apps:

* [Using RenderDoc Meta Fork to Optimize Your App - Part 1](/documentation/unity/po-renderdoc-optimizations-1/)
* [Using RenderDoc Meta Fork to Optimize Your App - Part 2](/documentation/unity/po-renderdoc-optimizations-2/)

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)