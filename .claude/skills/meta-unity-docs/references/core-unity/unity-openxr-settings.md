# Unity Openxr Settings

**Documentation Index:** Learn about unity openxr settings in this documentation.

---

---
title: "OpenXR Project Settings for Meta Quest"
description: "A detailed guide on the OpenXR Settings Menu in Unity, explaining what each setting does and how to configure your application for optimal performance and quality on Meta Quest devices."
last_updated: "2025-12-08"
---

The OpenXR Settings Menu in Unity contains a lot of settings, but not a lot of
detail on what these settings do, or why you would want to change them. This
page is here to help, with a detailed guide on the pros and cons of each
setting, so that you can configure your application for the optimal performance
and quality.

## Render Mode

The Render Mode setting determines how your application renders stereoscopic
content for VR. You have two options:

**Multi-View (Recommended)**: Both eyes are rendered simultaneously using a
single set of draw calls. The GPU processes both eye buffers in parallel,
dramatically reducing CPU overhead and improving performance. This is the
optimal choice for the vast majority of VR applications.

**Multi-Pass**: Each eye is rendered sequentially with separate draw calls.
While this offers maximum compatibility with all shaders and rendering
techniques, it requires roughly double the CPU time to process draw calls
compared to multi-view.

### Performance Impact

Multi-view rendering can significantly boost your application's frame rate by
reducing CPU-bound bottlenecks. For a typical scene, you'll submit all geometry
and draw calls to the GPU just once, rather than twice—saving substantial CPU
time and allowing more headroom for game logic and physics.

### When to Use Multi-Pass

Consider multi-pass rendering only if:

- You're using legacy shaders that don't support multi-view instancing
- You need eye-specific rendering differences that can't be achieved with
  multi-view
- You're debugging rendering issues and need to isolate per-eye behavior

Learn more about implementing and optimizing multiview rendering in the
[Multi-view rendering guide](/documentation/unity/enable-multiview/).

## Color Submission Mode

The Color Submission Mode setting controls which color format your application
uses when sending rendered frames to the OpenXR runtime. Understanding this
setting helps you optimize bandwidth and ensure correct color reproduction on
Meta Quest devices.

### Recommended Setting: Auto (Default)

By default, "Auto Color Submission Mode" is enabled, which hides the manual
"Color Submission Modes" dropdown. **We strongly recommend leaving this setting
unchanged.** Here's why:

All Meta Quest devices feature 8-bit per channel displays. Submitting HDR (High
Dynamic Range) buffers to these displays offers no visual benefit, while
creating two significant problems:

- **Wasted Bandwidth**: Transferring larger HDR buffers consumes unnecessary
  memory bandwidth
- **Color Accuracy Issues**: HDR-to-SDR tone mapping can introduce color shifts
  and incorrect appearance

### How Auto Mode Works

When Auto mode is enabled, Unity automatically selects the optimal color format
based on your target device's display capabilities, ensuring efficient
performance and accurate color reproduction without manual configuration.

### Advanced: Manual Color Format Selection

If you disable Auto mode, you can manually specify a prioritized list of color
formats. At runtime, the OpenXR runtime compares your list against supported
formats from top to bottom, selecting the first matching format for frame
submission.

This manual approach is rarely needed—use it only for specialized rendering
pipelines or when targeting devices with specific color format requirements.

## Latency Optimization

The Latency Optimization setting controls when and where the critical
`xrWaitFrame` function is called, directly impacting your application's
perceived responsiveness and input latency. This setting determines the balance
between rendering stability and input freshness.

### Understanding the Options

**Prioritize Input Polling (Recommended)**: The `xrWaitFrame` call occurs on the
main thread before simulation begins. This ensures your application uses the
most recent predicted display time when querying head and controller poses,
resulting in more accurate positioning and lower perceived latency.

**Prioritize Rendering**: The `xrWaitFrame` call occurs on the render thread
after main thread simulation, just before rendering begins. While this can
improve rendering thread stability, it forces pose queries to use estimated
timing, potentially causing poses to lag one frame behind by display time.

### What is xrWaitFrame?

The `xrWaitFrame` function serves two critical purposes in OpenXR applications:

1. **Frame Pacing**: Synchronizes your application's frame submission rate with
   the device's display refresh rate, ensuring smooth, judder-free rendering
2. **Latency Management**: Intelligently delays frame starts to give your app
   just enough time to complete rendering, minimizing motion-to-photon latency

Learn more in the
[xrWaitFrame OpenXR specification](https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrWaitFrame.html).

### Performance Implications

Changing this setting affects your application in two significant ways:

#### 1. Pose Prediction Accuracy

When "Prioritize Rendering" is selected, the actual predicted display time isn't
available until after `xrWaitFrame` executes on the render thread. This forces
the main thread to query poses using estimated timing, which can cause a
mismatch between where objects appear and where they should be by the time the
frame displays—resulting in visible judder or positioning errors.

#### 2. Frame Release Timing

The OpenXR runtime paces frames based on render thread timing, not main thread
timing. With "Prioritize Rendering," the variable offset between main and render
threads can create unpredictable latency spikes between simulation and
rendering, especially when the threads drift out of sync.

### Recommendation

**Use "Prioritize Input Polling" for optimal results.** This setting delivers:

- More accurate head and controller tracking
- Reduced perceived latency
- Smoother overall experience
- Better correlation between simulation and rendered output

This is the default behavior in Meta's Oculus XR Plugin and represents best
practices for VR development on Meta Quest devices.

## Depth Submission Mode

Depth Submission Mode enables your application to share its depth buffer with
the OpenXR runtime alongside the color buffer. This powerful feature enables
sophisticated layer composition and occlusion effects that would otherwise
require complex workarounds.

### What Depth Submission Enables

When enabled, the OpenXR compositor can perform depth-based composition,
unlocking two key capabilities:

**1. Natural Hand Occlusion**: System-rendered hand tracking displays correctly
behind and in front of your application's 3D objects without requiring "hole
punching" techniques that can be visually jarring or performance-intensive.

**2. Proper System Layer Integration**: System UI elements and overlays can be
properly occluded by your scene geometry, creating a more cohesive mixed reality
experience where virtual and system elements coexist naturally.

### Performance Considerations

Enabling depth submission introduces GPU overhead in two areas:

**Depth Buffer Resolution**: Storing depth data to a texture requires a GPU
resolve operation. With MSAA enabled, this resolve step can take measurable
time, particularly at higher resolutions or MSAA sample counts.

**Runtime Composition Cost**: The OpenXR runtime must perform additional depth
testing and composition work for every frame, consuming GPU cycles that could
otherwise be used for your application's rendering.

### Transparency and MSAA Limitations

Depth-based composition has important limitations to consider:

- **Transparent Objects**: Materials with alpha blending typically don't write
  to the depth buffer, causing transparent surfaces to fail depth tests
  incorrectly. This can result in system elements appearing in wrong depth order
  relative to glass, water, or other transparent surfaces.

- **MSAA Artifacts**: When using MSAA (Multi-Sample Anti-Aliasing), the depth
  resolve process loses sub-pixel depth information. This can create visible
  artifacts along edges where layers meet, particularly at steep viewing angles
  or high-contrast boundaries.

### Recommendation

For most Meta Quest applications, **set Depth Submission Mode to "None"**. The
performance cost typically outweighs the benefits unless your application
specifically requires:

- Natural hand tracking occlusion with complex 3D environments
- System UI integration where depth relationships are critical to the user
  experience
- Minimal use of transparent materials that would cause depth ordering issues

If you're unsure, start with depth submission disabled and enable it only if
testing reveals a clear need for its capabilities.

## Foveated Rendering API

Foveated rendering reduces GPU workload by rendering the peripheral areas of
your view at lower resolution, matching human visual perception where only the
central field of view is sharp. Unity 6 introduces "SRP Foveation," a
significant evolution of this technology that offers substantially better
performance than the legacy implementation.

### Understanding the Options

**SRP Foveation (Recommended for Unity 6+)**: Intelligently applies foveated
rendering to specific render passes that benefit most from reduced pixel count,
while skipping passes where it would provide no benefit or even hurt
performance. This selective approach maximizes the performance gain while
maintaining visual quality.

**Legacy Foveation**: Applies foveated rendering only to the final pass of your
render pipeline. This all-or-nothing approach works reasonably well for simple
forward rendering but leaves significant performance gains on the table for
complex pipelines with multiple passes.

### Performance Impact

For applications using a single forward rendering pass, both options deliver
equivalent performance. However, **applications using post-processing, deferred
rendering, or multiple intermediate passes can see tremendous performance
improvements** with SRP Foveation—often 20-30% faster frame times compared to
Legacy mode.

The key difference: SRP Foveation applies fixed foveation to expensive geometry
passes while disabling it for lightweight fullscreen effects, giving you the
best of both worlds.

### Custom Render Pipeline Integration

When implementing a custom Scriptable Render Pipeline (SRP), you gain
fine-grained control over foveation. Enable or disable foveation for individual
passes using:

```csharp
builder.EnableFoveatedRasterization(true);  // Enable for this pass
builder.EnableFoveatedRasterization(false); // Disable for this pass
```

**Enable foveation when:**

- The pass draws multiple 3D objects with complex geometry
- MSAA is active (fixed foveation provides maximum benefit with MSAA)
- The pass writes to screen-sized render targets multiple times
- Pixel shader complexity is high

**Disable foveation when:**

- Performing single-draw fullscreen passes (blits, tone mapping, color grading)
- MSAA is disabled—the GPU can use fast paths that are more efficient than
  foveated rendering
- The pass already operates on lower-resolution render targets
- Precision is critical (final composite passes, text rendering)

Disabling foveation on lightweight fullscreen passes enables GPU fast paths that
can actually improve performance compared to always-on foveation.

### Recommendation

**Use SRP Foveation for all Unity 6+ projects** unless you're targeting older
Unity versions. The intelligent per-pass control delivers measurably better
performance than Legacy mode, especially for modern rendering pipelines with
post-processing effects.

## Use OpenXR Predicted Time

Unity's game engine and the OpenXR runtime each maintain independent time
clocks. While this separation normally works fine, small discrepancies between
these clocks can accumulate and cause visible judder, especially when frame
pacing isn't perfect.

### How It Works

The OpenXR clock provides a **predicted display time**—the exact moment when the
current frame will appear on the headset displays. This prediction is crucial
for accurate head and controller positioning. While Unity's OpenXR plugin
correctly uses this predicted time for pose queries, it doesn't synchronize
Unity's master clock to match.

This mismatch means game logic and physics run on Unity time, while rendering
poses use OpenXR time. When these clocks drift slightly out of sync, you may
notice subtle hitching or judder in head tracking, particularly during rapid
movements or when frame times vary.

### The Solution

Enabling "Use OpenXR Predicted Time" synchronizes Unity's master clock to the
OpenXR predicted display time. This ensures perfect alignment between:

- Game simulation timing
- Physics updates
- Head and controller pose queries
- Actual frame display timing

### When to Enable

**Enable this setting if you notice:**

- Subtle judder during head rotation, despite maintaining target frame rate
- Inconsistent feeling between head movement and scene response
- Occasional "micro-stutters" that performance profiling doesn't explain

**The benefit is most noticeable when:**

- Your application's frame time varies slightly (common with complex scenes)
- Physics simulation timing is critical to gameplay feel
- You're implementing precise hand-scene interactions

### Recommendation

Try enabling this setting and test with head tracking during rapid movements.
Most applications will benefit from the improved temporal consistency, though
the difference may be subtle. If you notice no improvement, the overhead is
minimal—you can safely leave it enabled as a "just in case" optimization.

## Additional Graphics Queue (Vulkan)

Vulkan's architecture allows applications to create multiple graphics queues,
enabling graphics commands to execute on separate threads simultaneously. In
theory, this parallelism could accelerate rendering by distributing work across
multiple GPU command processors.

### Why It's Disabled for Meta Quest

However, the Snapdragon mobile GPUs in Meta Quest devices don't benefit from
additional graphics queues. The architecture of these mobile GPUs is optimized
differently than desktop GPUs:

- **Single Command Processor Design**: Mobile GPUs typically use a unified
  command processor that serializes work from multiple queues anyway
- **Thermal Constraints**: Additional overhead from queue management can
  actually reduce performance on thermally-constrained mobile hardware
- **Memory Bandwidth**: Mobile devices are bandwidth-constrained, making
  parallel submissions less effective than on desktop

### Performance Impact

Enabling additional graphics queues on Meta Quest typically results in:

- Increased CPU overhead from queue management
- No measurable GPU performance improvement
- Potential for increased frame time variance
- Higher power consumption without performance benefit

### Recommendation

**Keep this setting disabled (off) for all Meta Quest applications.** The
feature exists for cross-platform compatibility with desktop VR platforms, but
provides no benefit on Meta's mobile hardware architecture.

## Offscreen Rendering Only (Vulkan)

In traditional non-VR applications, Unity renders to an onscreen framebuffer
that the operating system displays directly. However, VR works differently—the
final compositing and display happens in the OpenXR system compositor, not in
your application. This makes Unity's normal onscreen buffer completely
unnecessary.

### What This Setting Does

Despite the confusing name, this setting is actually straightforward: Unity
considers all XR rendering to be "offscreen" because your application never
directly displays to the physical screen. The system compositor handles that
final step.

Enabling "Offscreen Rendering Only" tells Unity to skip allocating an onscreen
presentation buffer that would never be used. Your application still renders
normally to eye buffers, which are then submitted to the OpenXR runtime for
compositing.

### Memory Savings

The onscreen buffer would typically match your eye buffer resolution (or
larger), and consume memory for:

- Color buffer storage
- Potential depth buffer (if depth testing was enabled)
- Swapchain overhead

For a typical Quest 3 application rendering at 2064x2208 per eye, this could
save 10-20MB of GPU memory that would otherwise be completely wasted.

### Recommendation

**Enable this setting for all Meta Quest applications.** There's no
downside—you're simply avoiding allocation of a buffer that would never contain
visible pixels. This is pure memory savings with zero performance cost.