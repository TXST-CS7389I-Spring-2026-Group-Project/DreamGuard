# Unity Openxr Settings Quest

**Documentation Index:** Learn about unity openxr settings quest in this documentation.

---

---
title: "OpenXR Meta Quest Support Settings"
description: "A detailed guide to the Meta Quest Support settings in the OpenXR project settings."
last_updated: "2025-12-08"
---

The Meta Quest Support feature in Unity's OpenXR plugin provides Quest-specific
optimizations and configurations for developing VR applications on Meta Quest
devices. These settings enable advanced rendering techniques, performance
enhancements, and platform-specific features that help you get the most out of
Quest hardware. For additional information about Unity's OpenXR implementation
for Meta Quest, refer to
[Unity's Meta Quest feature documentation](https://docs.unity3d.com/Packages/com.unity.xr.openxr@1.16/manual/features/metaquest.html).

## Symmetric Projection (Vulkan)

Symmetric Projection optimizes multiview rendering performance by adjusting how
eye buffers are allocated and rendered. Instead of using asymmetric fields of
view that precisely match each eye's natural viewing frustum, this feature
creates symmetric, aligned eye buffers that work more efficiently with the GPU's
multiview implementation.

### How It Works

Without Symmetric Projection, the left and right eyes have slightly different
viewing frustums—they converge on objects at different angles. While
anatomically correct, this asymmetry prevents certain GPU optimizations in
multiview mode.

Symmetric Projection adjusts:

- **Eye Buffer Dimensions**: Allocates eye buffer swapchains with dimensions
  optimized for symmetric rendering
- **Field of View Alignment**: Renders both eyes with matching, symmetric FOVs
  that align perfectly in multiview space

The GPU can then process both eyes more efficiently, taking better advantage of
multiview instancing and shared shader workload.

### Performance Benefits

Most applications see measurable performance improvements:

- **Reduced GPU Time**: Multiview instancing becomes more efficient when the GPU
  doesn't need to handle asymmetric view frustums
- **Better Memory Coherence**: Symmetric buffers improve cache utilization when
  the GPU processes both eyes in parallel
- **Lower Overhead**: Driver-level optimizations work better with aligned,
  symmetric geometry

Typical performance gains range from 5-15% in GPU-bound scenarios, with the most
benefit seen in complex scenes with many draw calls.

### Visual Quality

The visual difference is imperceptible in practice. The symmetric FOV adjustment
is minimal—most users won't notice any change in how the scene appears. The
benefit is purely performance, with no meaningful visual trade-off.

### Recommendation

**Enable Symmetric Projection for most applications.** The performance gain
typically outweighs any concerns, especially for geometry-heavy scenes. However,
if your application has unusual rendering requirements or custom shader behavior
that depends on precise per-eye asymmetry, profile both settings to confirm the
benefit.

Learn more about Symmetric Projection in this technical presentation:
[Symmetric Projection Deep Dive](https://youtu.be/RYkXBOEVrSY?si=7in-JxrCQ1d9AWe-&t=545)

## Optimize Buffer Discards (Vulkan)

When rendering with MSAA (Multi-Sample Anti-Aliasing), your application needs
significantly larger render targets to store the extra sample data—2x, 4x, or
even 8x the normal buffer size. However, on tile-based mobile GPUs like those in
Meta Quest devices, these massive MSAA buffers don't need to actually exist in
main memory.

### How Tile-Based Rendering Works

Quest devices use tile-based deferred rendering (TBDR), a mobile GPU
architecture that works very differently from desktop GPUs:

1. **Tile Phase**: The GPU divides the screen into small tiles and renders each
   completely in fast on-chip memory
2. **Resolve Phase**: Only the final resolved (non-MSAA) pixels get written to
   main memory
3. **MSAA Stays On-Chip**: The MSAA samples never need to leave the GPU's
   internal tile memory

This means the multi-sampled buffers are cleared at the start of each tile, used
during rendering, and discarded at the end—they never need backing storage in
main memory.

### Vulkan's Lazy Allocation

Vulkan provides "lazy memory allocation" specifically for this use case. Lazy
allocations don't consume actual VRAM until something tries to read from or
persistently store to the buffer. Since MSAA buffers on TBDR GPUs are purely
transient (used only during tile rendering), they can be lazily allocated,
consuming zero actual memory.

### Memory Savings

The memory savings are substantial:

- **4x MSAA at 1680x1760** (Quest 3 default resolution): Saves approximately
  90MB per eye buffer (180MB total for stereo)
- **4x MSAA at 1440x1584** (Quest 2 default resolution): Saves approximately
  66MB per eye buffer (132MB total)

This freed memory can be used for textures, geometry, or game state—real assets
that enhance your application.

### Performance Impact

Beyond memory savings, lazy MSAA buffers can slightly improve performance:

- No memory bandwidth wasted clearing/loading buffers that don't need
  persistence
- Better memory controller efficiency from reduced traffic
- No chance of accidentally spilling MSAA data to main memory due to memory
  pressure

### Recommendation

**Enable this setting for virtually every Meta Quest application.** There's no
downside—you're simply telling Vulkan the truth about how you use MSAA buffers
on tile-based GPUs. Even if you're not currently memory constrained, the freed
VRAM provides headroom for future features.

The only exception would be if you're doing something extremely unusual with
MSAA resolve targets (like custom sampling patterns that require persistent MSAA
data), but this is exceptionally rare.

## Multiview Render Regions Optimizations (Vulkan)

When Symmetric Projection is enabled, the symmetric eye buffers contain regions
around the edges that fall outside the actual viewing frustum—pixels that will
never be visible in the headset. Multiview Render Regions Optimization takes
advantage of this by telling the GPU to skip rendering these invisible pixels
entirely.

### How It Works

The optimization uses Vulkan's render region hints to inform the driver which
portions of the render target contain visible pixels. The GPU can then:

- Skip pixel shader execution for invisible regions
- Avoid writing data that won't be sampled
- Reduce memory bandwidth by not storing discarded pixels

Think of it as an automatic, GPU-level optimization that culls entire screen
regions before any rendering work happens.

### Performance Modes

**All Passes (Recommended)**: Applies render region optimization to every
rendering pass in your pipeline. This maximizes the performance benefit, as
every draw call avoids work in the invisible regions.

**Final Pass Only**: Applies optimization only to the final rendering pass
before submission. Use this mode if certain effects require sampling from the
full buffer, even in technically invisible regions.

### When to Use Final Pass

Some post-processing effects sample neighboring pixels to create their result:

- **Bloom**: Samples a wide radius around each pixel, potentially reaching into
  invisible regions
- **Depth of Field**: Bokeh shapes may sample outside the visible frustum
- **Motion Blur**: Velocity vectors might reference off-screen pixels
- **Custom Blur Effects**: Any convolution filter with large kernels

If these effects produce artifacts or incorrect results at screen edges with
"All Passes" enabled, switch to "Final Pass" to ensure intermediate buffers
remain fully populated.

### Performance Impact

The optimization's effectiveness depends on your scene:

- **Complex Shaders**: Greater benefit when pixel shaders are expensive (complex
  lighting, multiple texture samples, procedural effects)
- **High Fill Rate**: More impact when many overlapping objects write to the
  same pixels
- **Draw Call Count**: Modest per-draw savings multiply across hundreds of draw
  calls

Typical performance improvement ranges from 3-8% in GPU-bound scenarios, with
the highest gains in forward-rendered scenes with expensive fragment shaders.

### Potential Overhead

While rare, some rendering scenarios might actually see reduced performance:

- Very simple shaders where the region hint overhead exceeds saved work
- Scenes with minimal overdraw where few pixels would be skipped anyway
- Drivers that handle render regions suboptimally (usually fixed in driver
  updates)

### Recommendation

Start with **"All Passes"** for maximum performance. If you notice:

- Visual artifacts at screen edges in post-processed scenes
- Unexpected behavior with full-screen effects
- Bloom or blur effects that look "clipped"

Then switch to "Final Pass" mode. Profile both settings with your specific
application to confirm the benefit.

## Space Warp Motion Vector Texture Format

Application SpaceWarp is Meta's frame extrapolation technology that generates
intermediate frames from depth and motion vector data, effectively doubling your
perceived frame rate. Understanding how motion vectors work helps you optimize
this feature for maximum performance.

### How Motion Vectors Work

Motion vectors describe how each pixel moved from the previous frame to the
current frame, expressed in normalized device coordinate (NDC) space.
Traditionally, these vectors contain three components:

- **R Channel (X delta)**: Horizontal movement in screen space
- **G Channel (Y delta)**: Vertical movement in screen space
- **B Channel (Z delta)**: Depth change (forward/backward movement)
- **A Channel**: Unused

The OpenXR runtime uses all three channels to accurately extrapolate the next
frame based on observed motion patterns.

### The Optimization: RG16f Format

While the Z-delta channel provides technically correct depth motion, SpaceWarp
can perform surprisingly good extrapolation without it. Here's why:

**Mathematical Insight**: Objects moving toward or away from the camera appear
to scale exponentially based on perspective projection. However, over the tiny
time delta between frames (8-16ms), this exponential scaling is nearly
indistinguishable from linear scaling. The error is typically sub-pixel and
imperceptible.

By treating all motion as perpendicular to the viewing direction (ignoring
Z-delta), SpaceWarp can use:

- **RG16f format** (2 channels, 16-bit float each) = 4 bytes per pixel
- Instead of **RGBA16f format** (4 channels, 16-bit float each) = 8 bytes per
  pixel

This cuts motion vector texture size in half—a significant bandwidth saving when
rendering at VR resolutions.

### Performance vs. Quality

**Performance Benefit**: Halving the motion vector texture size means:

- 50% less memory bandwidth when writing motion vectors
- 50% less texture bandwidth when SpaceWarp reads them
- Smaller memory footprint for motion vector buffers
- Faster clear operations

**Quality Impact**: Extensive testing shows the visual difference is nearly
impossible to detect. The approximation error from ignoring Z-delta is typically
less than 0.5 pixels even in worst-case scenarios (rapid forward motion toward
large objects).

### When to Use RGBA16f

Consider using the full RGBA16f format only if your application features:

- Extremely rapid forward/backward camera movement (unusual in VR due to comfort
  constraints)
- Close-proximity objects with high detail where sub-pixel accuracy matters

For typical VR experiences, the performance benefit of RG16f far outweighs the
imperceptible quality difference.

### Recommendation

**Use RG16f format for almost all applications.** The 2x bandwidth reduction
significantly improves SpaceWarp performance with no noticeable visual
degradation. Only switch to RGBA16f if profiling reveals specific artifacts that
Z-delta information would resolve.

## Force Remove Internet Permission

Android applications can request various system permissions in their manifest
file, granting access to device features like the camera, microphone, or network
connectivity. The `INTERNET` permission is particularly common—and often added
automatically even when your application doesn't actually use network features.

### Why This Matters

The Android Internet permission is often added by default through:

- Unity package dependencies that assume network capability
- Third-party SDKs that have optional networking features
- Analytics or debugging tools included in development builds
- Transitive dependencies that bring in networking libraries

Even if your application never opens a network connection, the permission may
still appear in your final APK's manifest.

### User Trust and Privacy

Users reviewing app permissions before installation see the Internet permission
as a potential privacy concern. Applications that request network access when
they don't actually need it may:

- Raise unnecessary privacy concerns during app review
- Reduce user trust during installation
- Trigger additional security scrutiny in enterprise environments
- Fail compliance checks for applications that should be air-gapped

### What This Setting Does

Enabling "Force Remove Internet Permission" strips the `INTERNET` permission
from your Android manifest during the build process, regardless of how it got
there. This ensures your final APK only requests permissions it actually uses.

### Important Considerations

Only enable this setting if your application truly doesn't require network
connectivity:

**Safe to Enable When:**

- Your app is entirely offline (single-player game, local tools, demos)
- All content is bundled with the application
- No analytics, crash reporting, or telemetry services are active
- No third-party ads or monetization SDKs are included

**Do Not Enable If:**

- You use multiplayer or any online features
- Analytics or crash reporting services are active
- In-app purchases require network validation
- Content is downloaded or streamed
- Social features require internet connectivity

Removing the permission when your app actually needs network access will cause
runtime exceptions and crashes when attempting network operations.

### Recommendation

**Enable this setting only for guaranteed offline applications.** If you're
unsure whether a dependency requires network access, test thoroughly after
enabling this setting. Network-related crashes will appear immediately when the
app tries to connect.

For hybrid apps with optional online features, keep the Internet permission
enabled—there's no safe way to request it dynamically at runtime.

## System Splash Screen

The first seconds of application startup are critical for user perception.
Instead of showing Unity's default splash screen, you can display a custom
branded image that better represents your application's identity and sets
expectations for the experience ahead.

### What It Does

This setting lets you provide a custom splash screen image that appears during
application initialization, before your first scene loads. The OpenXR system
compositor displays this image, ensuring users see your branding immediately
upon launch instead of generic Unity branding.

### Benefits of Custom Splash Screens

**Brand Identity**: Establish your application's visual identity from the first
moment users launch it. Professional, custom splash screens signal quality and
attention to detail.

**User Expectations**: Use the splash screen to communicate what type of
experience users should expect—whether it's a game, productivity tool, social
space, or immersive experience.

**Loading Perception**: A well-designed splash screen makes loading feel more
intentional and less like waiting. Users are more patient when they see
purposeful branding rather than generic placeholders.

### Technical Requirements

Your splash screen image should:

- Match the headset's native resolution for optimal clarity
- Use appropriate formats (typically PNG for transparency support)
- Be optimized for file size (it will be bundled with your APK)
- Follow Meta's design guidelines for VR splash screens

### Design Considerations

**Keep it Simple**: Splash screens appear at fixed distances in VR space. Avoid
fine text or intricate details that may be hard to read. Focus on bold logos,
simple graphics, and clear branding.

**Respect Comfort**: Consider the visual impact of your splash screen.
High-contrast patterns or bright colors on dark backgrounds can be uncomfortable
in VR when they suddenly appear. Aim for balanced, comfortable visuals.

**Cultural Sensitivity**: Your splash screen is seen by users worldwide. Ensure
imagery and text are culturally appropriate and internationally understandable.

### Implementation

To set up a custom splash screen:

1. Prepare your splash screen image according to Meta's specifications
2. In Unity, navigate to Project Settings > XR Plugin Management > OpenXR > Meta
   Quest Support
3. Assign your image to the System Splash Screen field
4. Build and test to ensure it appears correctly during launch

Learn more about creating effective splash screens in the
[Unity Splash Screen documentation](/documentation/unity/unity-splash-screen/).

### Recommendation

**Always use a custom splash screen for published applications.** The default
Unity splash screen doesn't reflect your brand and may confuse users about what
application they've launched. Even a simple, clean branded screen is better than
the default.

## Target Devices

The Target Devices setting defines the complete list of Meta Quest headsets your
application officially supports. This seemingly simple checkbox list has
significant implications for how the OpenXR runtime handles your application on
different devices.

### How Runtime Compatibility Works

When your application launches on a Meta Quest device, the OpenXR runtime checks
whether that device is in your supported devices list. Based on this check, the
runtime may adjust its behavior to ensure your application runs correctly, even
on hardware you haven't explicitly tested.

### Compatibility Mode Behavior

**If a device IS in your list:** The runtime assumes you've tested on that
hardware and optimized accordingly. It uses standard behavior and provides full
access to device-specific features.

**If a device is NOT in your list:** The runtime enters compatibility mode,
which may:

- Limit access to newer device-specific features
- Apply conservative performance settings
- Enable additional compatibility layers that add overhead
- Restrict resolution or refresh rate options

### Strategic Device Selection

Consider your target market and testing capabilities:

**Include devices you've tested on:** Only add devices where you've verified
proper functionality, acceptable performance, and good user experience.

**Future-proof considerations:** Newer devices often provide backward
compatibility. If your app runs well on Quest 2, it will likely run even better
on Quest 3 due to more powerful hardware. However, device-specific features
(like color passthrough on Quest 3) require explicit support.

**Update as you test:** Start with a conservative device list and expand it as
you test on additional hardware. It's better to officially support fewer devices
correctly than claim support for untested hardware.

### Device-Specific Features

Some features are only available on specific headsets:

- **Color Passthrough**: Quest Pro, Quest 3, Quest 3S
- **Eye Tracking**: Quest Pro
- **Face Tracking**: Quest Pro
- **Higher Eye Buffer Resolution**: Quest 3 (1680x1760 per eye vs. 1440x1584 on
  Quest 2)
- **Higher Refresh Rates**: Varies by device (90Hz, 120Hz support)

If your application requires these features, restrict the compatibility list to only devices that support them.
If your application uses these features optionally, include all devices in the compatibility list. Ensure your app checks for those features at runtime and responds appropriately if any feature is missing, such as by disabling related functionality.

### Testing Requirements

Before adding a device to your supported list:

- Test your application on the physical hardware
- Verify performance meets your targets (frame rate, temperature, battery life)
- Confirm all features work correctly
- Test edge cases specific to that device's capabilities
- Validate the user experience matches your quality bar

### Store Implications

The Meta Horizon Store uses your Target Devices list to determine which users can
see and install your application. Users with unsupported devices won't see your
app in the store, preventing negative reviews from incompatible hardware.

### Recommendation

**Start with the devices you have access to test on, and expand the list as you
verify compatibility.** Don't guess—if you haven't tested on a device, don't
claim support for it. The runtime's compatibility mode is better than shipping
untested configurations that may fail.

For maximum reach, prioritize testing on:

1. Quest 2 (largest install base)
2. Quest 3 (newest mainstream device)
3. Quest Pro (if using advanced features like eye tracking)
4. Quest 3S (newest entry-level device)

## Late Latching (Vulkan)

Late Latching is an advanced technique that updates head and controller tracking
poses at the last possible moment before rendering begins. By refreshing these
poses on the render thread immediately before drawing, you can minimize
motion-to-photon latency—the delay between a real-world movement and seeing its
effect on the displays.

### How It Works

In standard rendering pipelines, head and controller poses are queried once on
the main thread during simulation, then used unchanged for rendering
milliseconds later. While the poses were accurate when queried, they become
slightly outdated by the time pixels actually appear on the displays.

Late Latching breaks this pattern by querying poses twice:

1. **Main Thread (Simulation Time)**: Queries poses for game logic, physics, and
   simulation
2. **Render Thread (Just Before Draw)**: Re-queries poses exclusively for
   rendering, updating view matrices and controller positions to reflect the
   most recent tracking data

### The Visual vs. Simulation Gap

This creates an intentional mismatch:

- **What You See**: Rendered with the latest possible tracking data, minimizing
  perceived latency
- **What the Game Knows**: Simulated using slightly older poses from the main
  thread

For most users, this mismatch is imperceptible and worth the trade-off for
reduced visual latency. However, certain interaction patterns may reveal the
disconnect.

### When It Helps Most

Late Latching provides the greatest benefit for:

**High-Speed Movements**: Rapid head turns or fast controller motions where even
small latency reductions are perceptible

**Precision Interactions**: Fine motor tasks (drawing, aiming, virtual surgery)
where the tightest possible hand-eye coordination matters

**Comfort-Sensitive Users**: Individuals particularly sensitive to
latency-induced discomfort

### Potential Concerns

While Late Latching reduces visual latency, it introduces some complexity:

**Physics Interactions**: Your physics simulation runs on main thread poses
while rendering shows late-latched poses. If a user is interacting with
physically-simulated objects, there may be subtle misalignment between where
their hand appears and where physics thinks it is.

**Grab/Release Timing**: The moment a user presses a button to grab an object,
the visual hand position (late-latched) might not exactly match the simulation
hand position that triggers the grab. This discrepancy is usually sub-millimeter
but can matter for precision interactions.

**Networking**: In multiplayer scenarios, your local view uses late-latched
poses while networked poses are still based on simulation timing. This can
create subtle differences in how your actions appear locally vs. remotely.

### Performance Impact

Late Latching adds negligible overhead. The pose query itself is fast, and the
benefits to comfort and perceived responsiveness generally far outweigh the tiny
performance cost.

### Recommendation

**Enable Late Latching for most applications**, particularly those involving:

- Fast-paced action or movement
- Precise hand tracking interactions
- Any scenario where minimizing perceived latency improves user experience

**Consider disabling if your application:**

- Relies heavily on perfect alignment between physics simulation and visual hand
  positions
- Has extremely tight tolerance requirements for object manipulation
- Shows noticeable artifacts from the simulation/rendering pose mismatch

Test with your specific interaction patterns to determine if Late Latching
benefits your application. Most developers find the latency reduction valuable.

Learn more about implementing and optimizing late latching in the
[Late Latching documentation](/documentation/unity/enable-late-latching).

## Late Latching Debug Mode

Late Latching Debug Mode is a specialized diagnostic tool that modifies the
behavior of late latching to help you understand and troubleshoot how poses are
being updated on the render thread. This setting is exclusively for development
and debugging—it should never be enabled in production builds.

### What It Does

Enabling debug mode changes late latching behavior in several ways:

**Enhanced Logging**: Adds verbose logging output that tracks:

- When poses are queried on the main thread vs. render thread
- The time delta between main thread and render thread pose queries
- Any discrepancies or unexpected timing issues in the late latching pipeline
- Performance metrics showing late latching overhead

**Altered Rendering Behavior**: May introduce visual markers or modified
rendering patterns that help you identify which objects are being transformed by
late-latched poses versus simulation-time poses. This can reveal mismatches
between simulated and rendered positions.

**Performance Instrumentation**: Adds timing markers and instrumentation that
help profile the cost of late latching operations, making it easier to identify
if late latching is introducing performance bottlenecks in your specific
application.

### When to Use Debug Mode

Enable this setting only when you're actively investigating:

**Pose Timing Issues**: If you suspect late-latched poses aren't being applied
correctly or at the expected times

**Visual-Simulation Mismatches**: When debugging why rendered hand positions
don't align with physics interactions or input responses

**Performance Profiling**: To measure exactly how much overhead late latching
adds to your specific rendering pipeline

**Integration Problems**: When first implementing late latching and verifying
it's working correctly with your custom rendering setup

### Performance Impact

Debug mode adds significant overhead:

- Additional logging operations every frame
- Extra instrumentation and timing queries
- Potential visual artifacts or modified rendering that helps debugging but
  hurts performance
- Increased memory usage from logging buffers

Your application may run substantially slower with debug mode enabled—this is
expected and why it should never ship in production.

### Recommendation

**Never enable this setting in production or release builds.** Late Latching
Debug Mode is purely a development tool. If late latching is working correctly,
you'll never need to enable debug mode.

Enable it temporarily only if:

- You're experiencing problems with late latching behavior
- You need to verify late latching is functioning correctly in a custom
  implementation
- You're profiling performance to decide whether late latching benefits your
  application

Once debugging is complete, **immediately disable this setting** before any
performance testing or user-facing builds.