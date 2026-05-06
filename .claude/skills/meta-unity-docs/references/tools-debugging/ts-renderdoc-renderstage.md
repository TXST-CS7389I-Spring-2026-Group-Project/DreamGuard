# Ts Renderdoc Renderstage

**Documentation Index:** Learn about ts renderdoc renderstage in this documentation.

---

---
title: "Performing a Render Stage Trace"
description: "Profile render stage timing and analyze the Tile Timeline in RenderDoc Meta Fork."
last_updated: "2024-11-27"
---

This topic describes how to use RenderDoc Meta Fork to perform a render stage trace, as well as how to read the **Tile Timeline**.

To look at render stage profiling data, load a capture, go to the **Event Browser**, which can be opened by going to **Window** on the menu, and click the  icon to perform a render stage trace. The headset proximity sensor will be briefly disabled so the trace can be performed without the need to cover it.
Ensure that you connect to the device via the [profiling mode](/documentation/unity/ts-renderdoc-capture#loading-a-capture). If you use normal replay mode/context, the Meta button for render stage trace will be grayed out.

After a few seconds, the render stage data appears in the **Tile Timeline** window. If the **Tile Timeline** window is not present, it can be found under the **Window** menu.

The **Tile Timeline** presents a lot of information in a compressed space. Zooming functionality is available with the mouse scrolling wheel to zoom in and out of areas of the timeline.

The top row of the timeline shows each of the surfaces rendered to in succession during the frame. Each block on the first row represents a surface and displays the following information:

* Surface ID
* Dimensions of the surface
* MSAA status
* Bit color and bit depth
* Dimensions of the individual tiles that make up the surface
* Total number of tiles for the surface

The second row of the timeline shows the active bin. To be precise, Render/StoreColor/LoadColor/StoreDS/LoadDS can belong to a bin and will have an additional bin row above them if the surface is rendering with binning mode enabled.

The third row of the timeline lists all the render stages for each tile on a given surface. This provides accurate timings (in microseconds) for each render stage, with a color key and label indicating the specific render stage. Common render stages include the following:

* **Binning** - The Meta Quest's GPU uses a tiled architecture where all draw calls for a frame are executed in two stages. The first stage is the binning phase, where triangle vertex positions for all draw calls are calculated and assigned to bins that correspond to a partition of the drawing surface. A simplified version of vertex shaders are executed during binning for the purpose of finding a triangle's position.
* **Render** -This is the second stage of the draw call execution that began with binning. One chunk of this represents the total cost of all vertex and fragment operations for one bin. The full version of the vertex shaders are executed to compute the interpolants used by the fragment shader.
* **LoadColor** - Loads the color from slow memory into fast memory. This can happen when starting to render into a surface without clearing it.
* **StoreColor** - After an entire bin of pixel and fragment operations are done executing, the calculated color value is copied from fast memory (dedicated for the bin's rendering operations) to slow memory.
* **LoadDS** and **StoreDS** - These serve the same purpose as LoadColor and StoreColor, but for depth reads and writes to the surface.
* **Blit** - This stage indicates copying between slow memory regions. This can happen for various operations, such as mipmap generation and when clearing a surface without rendering anything.
* **Preempt** - The compositor is an OS-level service that executes at regular intervals to present the image submitted by the application to the screen. In order to deliver the image at the proper cadence, the GPU will preempt the application's workload so that the compositor can complete its work on time.

Other render stages include the following:

* **GLAsyncCompute** - For the OpenGL compute shader
* **VKQueue** - For the Vulkan arbitrary start/stop within a queue stage
* **VKRenderClear** - For the Vulkan render pass clear stage
* **VKWorkload** - For the Vulkan workload (subpass) stage
* **VKAsyncCompute** - For the Vulkan compute shader dispatch pass stage
* **VKLoadInput** - For the Vulkan GMEM input attachment load stage

## Surface Information

Left click on a surface time slice to see detailed information regarding the execution of the replay:

* surface id
* start timestamp - in microseconds
* end timestamp - in microseconds
* duration - in microseconds
* width - width of the framebuffer
* height - height of the framebuffer
* msaa - multi-sampling anti-aliasing level
* multiple rendertargets - number of rendertargets bound to the framebuffer
* render mode
* color bits per pixel
* depth bits per pixel
* stencil bits per pixel
* color attachment attributes - Flags set for the color attachment
* depth attachment attributes - Flags set for the depth attachment
* stencil attachment attributes - Flags set for the stencil attachment
* bin count - Number of bins this surface is divided into
* bin width - Width of a single non-merged bin
* bin height - Height of a single non-merged bin
* stage count - Number of render stages executed under this surface
* new frame surface - This will be 1 if the surface is the first surface in a frame. This is used to delimit one frame from another.
* num rendered bins - Number of items in row 2 minus Binning, Blit or Compute
* num surface stages - Number of items in row 3 plus Binning, Blit or Compute
* compute only

## Bin Information

Left click on a bin to see additional information on its execution.

{:width="500px"}

* `#` bins contained X/Y - If you've enabled Fixed Foveated Rendering, bins near the edge of the render texture will be combined. These values will be greater than 1 if this bin maps to multiple bins in the final render texture.
* Fov Scale Factor X/Y [`#`] - If this bin maps to multiple bins in the final render texture, these values will indicate the FOV adjustments so the edges of this bin align with neighboring bins.

## Stage Information

Left click on a render time slice to see the start and end timestamp, duration, and the type of render stage.

## OpenGL Multiview

Due to hardware differences between the Meta Quest headsets, a render stage capture can appear different on an OpenGL app using multiview. The following timeline is from Meta Quest:

Note the two surfaces with the same ID. They are referring to the left eye and right eye slice of a 2D texture array. The same OpenGL app running on Meta Quest 2 will have only one single surface, but double the number of tiles:

Meta Quest 3 and Meta Quest 3S both use the Adreno 740v3 GPU (Meta Quest 3 at 690 MHz, Meta Quest 3S at 492 MHz). Whether the render stage trace appears as on Meta Quest or Meta Quest 2 has not been verified for these devices. [TODO: verify Quest 3/3S multiview render stage trace behavior with RenderDoc team]

## Vulkan Multiview

Similar to OpenGL, a render stage capture can appear different on a Vulkan app using multiview. The following timeline is from Meta Quest:

Note the single surface with two binning stages (the yellow time slice). The same Vulkan app running on Meta Quest 2 will have only one single surface and one single binning stage due to hardware multiview:

Meta Quest 3 and Meta Quest 3S both use the Adreno 740v3 GPU (Snapdragon XR2 Gen 2), which supports two Qualcomm-specific Vulkan extensions not available on Meta Quest 2 (Adreno 650): `VK_QCOM_multiview_per_view_viewports` and `VK_QCOM_multiview_per_view_render_areas`. These extensions may affect how the Vulkan multiview render pass appears in RenderDoc. The exact stage layout has not been verified for these devices. [TODO: verify Quest 3/3S Vulkan multiview render stage trace behavior with RenderDoc team]

## Tile Browser

The **Tile Browser** tab shows your rendered frame broken up by each rendered bin, with a heatmap overlay that indicates which bins took longer than others. You can click on a bin to select it in the **Tile Timeline**. Both the **Tile Browser** and the **Tile Timeline** UI can be accessed under **Window** on the menu.

## See Also

* [Use RenderDoc Meta Fork for GPU Profiling](/documentation/unity/ts-renderdoc-for-oculus/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)