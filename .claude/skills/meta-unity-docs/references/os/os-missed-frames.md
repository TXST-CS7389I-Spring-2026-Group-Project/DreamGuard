# Os Missed Frames

**Documentation Index:** Learn about os missed frames in this documentation.

---

---
title: "Missed frames and frame recovery"
description: "What happens when an application doesn't render a frame in time, and how Horizon OS helps recover."
last_updated: "2026-03-09"
---

When a VR application fails to render a frame before the display needs it, the Horizon OS employs a series of mechanisms to maintain visual comfort and minimize the impact on the user. Understanding this process helps you design applications that degrade gracefully under load.

## The frame deadline

Meta Quest headsets refresh their displays at a fixed rate (72, 90, or 120 Hz, depending on the [display refresh rate](/documentation/unity/mobile-display-refresh-rate/) your app requests). Your application must submit a new frame before each display refresh. The time budget per frame depends on the refresh rate:

| Refresh Rate | Time Per Frame |
|---|---|
| 72 Hz | 13.9 ms |
| 90 Hz | 11.1 ms |
| 120 Hz | 8.3 ms |

If your application's combined CPU and GPU work exceeds this budget, the frame will not be ready in time. This is called a missed frame or stale frame.

## What happens when a frame is missed

When your application misses a frame deadline, the following chain of events occurs:

### 1. The compositor re-displays the previous frame

The [compositor](/documentation/unity/os-compositor/) always has the most recently submitted frame available. If no new frame arrives in time, it re-displays the previous frame. This is reported as a `Stale` frame in the [VrApi logcat stats](/documentation/unity/ts-logcat-stats/).

### 2. Rotational reprojection (TimeWarp) is applied

Before re-displaying the old frame, the compositor applies rotational reprojection (also called TimeWarp). This rotates the old frame to match the user's current head orientation, so the world still feels stable even though the frame content is outdated.

This is effective for head rotation because pure rotation introduces no parallax. However, TimeWarp does not correct for head translation, so users may notice slight positional drift during missed frames.

### 3. The display refresh rate stays the same

The display continues to refresh at its configured rate regardless of application frame rate. This means the compositor always outputs a frame to the display on time — it just may be a reprojected version of a previous frame rather than new content.

## What it looks like to the user

Occasional missed frames are difficult to notice thanks to reprojection. However, sustained frame drops produce visible symptoms:

- Judder: Objects appear to stutter or vibrate because their positions jump between old and new frames rather than moving smoothly.
- Black bars at the periphery: When the user rotates their head significantly between the old frame and the current pose, reprojection reveals areas that were not rendered. These appear as flickering black regions at the edges of the view.
- Increased latency: The displayed content lags further behind the user's actual head position, which can contribute to discomfort.

## Understanding GPU utilization during missed frames

A common source of confusion is that GPU utilization can appear well below 100% even when your application is GPU-bound and missing frames. This happens because `GPU%` measures the proportion of time the GPU hardware is actively working versus sitting idle. When your application drops to half rate, the GPU finishes its work and then sits idle until the next frame begins — causing utilization to drop even though each frame is too expensive.

### How frame skipping affects GPU utilization

At 72 Hz, the display refreshes every 13.9 ms. If your GPU work takes 18 ms, the frame cannot be delivered on time. The compositor will re-display the previous frame, and your application effectively drops to half rate — delivering a new frame every other display refresh (every 27.8 ms).

At half rate, the GPU renders for 18 ms and then sits idle for roughly 10 ms before the next frame begins. The GPU hardware reports utilization as the ratio of active time to total elapsed time, so the idle gap between frames pulls the reported percentage down:

| Scenario | GPU Work | Effective Frame Interval | Approximate GPU% |
|---|---|---|---|
| Hitting frame rate (72 FPS) | 12 ms | 13.9 ms | 86% |
| Missing frames (36 FPS) | 18 ms | 27.8 ms | 65% |
| Missing frames (36 FPS) | 22 ms | 27.8 ms | 79% |

In the second row, the application is clearly GPU-bound — it cannot complete its work within a single 13.9 ms frame. Yet GPU utilization reads only 65%, because the GPU is idle for a large portion of the doubled frame interval. A developer seeing 65% utilization might assume they have 35% headroom, when in reality their GPU work exceeds the frame budget by 30%.

### The 50% threshold

To recover from half-rate back to full frame rate, the GPU work must fit within a single display refresh interval (13.9 ms at 72 Hz). Since the GPU is being measured across the full half-rate interval of 27.8 ms, this means:

- 13.9 ms of GPU work out of 27.8 ms total = 50% utilization

Therefore, GPU utilization must drop below approximately 50% while at half rate before the application can return to delivering every frame. Any utilization above 50% at half rate means the GPU work still exceeds the single-frame budget.

### Example: Diagnosing a "65% GPU" bottleneck

Consider an application running at 72 Hz that reports:
```
FPS=36/72, Stale=36, GPU%=0.65, App=18.05ms
```

This tells you:
1. `FPS=36/72` — The app is submitting 36 frames per second to a 72 Hz display (half rate).
2. `Stale=36` — Every frame is being displayed twice (36 stale frames per second).
3. `GPU%=0.65` — The GPU is 65% utilized. Because the app is running at half rate, the GPU is idle between frames, pulling this number down from what you might expect.
4. `App=18.05ms` — The actual GPU render time per frame is 18.05 ms, which exceeds the 13.9 ms budget.

To hit full frame rate, the developer needs to reduce `App` GPU time from 18.05 ms to below 13.9 ms — a 23% reduction in GPU work, not the 35% headroom that 65% utilization might suggest.

<oc-devui-note color="highlight">
<b>Key takeaway:</b> When your application is missing frames, always check the <code>App</code> GPU time in logcat rather than relying on <code>GPU%</code>. The <code>App</code> value shows the actual time spent rendering each frame in milliseconds, making it straightforward to compare against your frame budget. If <code>App</code> exceeds 13.9 ms (at 72 Hz), 11.1 ms (at 90 Hz), or 8.3 ms (at 120 Hz), your application is GPU-bound regardless of what <code>GPU%</code> reports.
</oc-devui-note>

## How the OS helps recover

Horizon OS includes several mechanisms that automatically reduce the impact of missed frames or help your application avoid them:

### Dynamic Resolution

If [Dynamic Resolution](/documentation/unity/dynamic-resolution/) is enabled, the OS automatically reduces your application's [render scale](/documentation/unity/os-render-scale/) when it detects dropped frames. This reduces the number of pixels the GPU must shade, helping your application get back within its frame budget. When GPU headroom returns, the render scale is increased again.

### Fixed Foveated Rendering (FFR)

[Fixed Foveated Rendering](/documentation/unity/os-fixed-foveated-rendering/) reduces the resolution of pixels at the edges of the eye buffer, where the user is less likely to be looking. If your application has enabled dynamic foveation (`debug.oculus.foveation.dynamic`), the OS can increase the FFR level when the GPU is under pressure, reclaiming GPU time at the cost of peripheral image quality.

### Display refresh rate throttling

If your application requested a display refresh rate higher than 72 Hz and the device encounters thermal pressure, the OS may [dynamically throttle the refresh rate](/documentation/unity/mobile-display-refresh-rate/#considerations-for-changing-refresh-rates) back down to 72 Hz. This gives your application a larger per-frame time budget (13.9 ms instead of 11.1 ms or 8.3 ms). If conditions worsen further, the OS can halve the effective frame rate while maintaining the display refresh rate.

### Application SpaceWarp

[Application SpaceWarp (AppSW)](/documentation/unity/os-app-spacewarp/) is an opt-in feature that allows your application to intentionally render at half the display refresh rate (e.g., 36 FPS on a 72 Hz display). The application submits motion vectors and a depth buffer alongside each frame, and the compositor synthesizes the in-between frames using both motion extrapolation and positional reprojection.

When AppSW is active, the compositor generates frames that account for both head movement and in-scene object movement, producing significantly smoother results than simple rotational reprojection alone. This effectively doubles your application's time budget per rendered frame.

<oc-devui-note color="highlight">
AppSW is not an automatic recovery mechanism — it must be integrated into your application. See the <a href="/documentation/unreal/unreal-asw/">Unreal</a>, <a href="/documentation/unity/unity-asw/">Unity</a>, or <a href="/documentation/native/android/mobile-asw/">Native</a> guides for implementation details.
</oc-devui-note>

## Monitoring frame performance

Use the following tools to detect and diagnose missed frames:

- [VrApi logcat stats](/documentation/unity/ts-logcat-stats/): The `Stale` counter shows how many frames were re-displayed in the past second. A `Stale` count equal to the refresh rate with steady FPS indicates pipeline latency (which extra latency mode can smooth out). A `Stale` count between 0 and the refresh rate means inconsistent frame delivery, which the user will perceive as judder.
- [OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool/): Provides an in-headset overlay showing FPS, stale frames, GPU utilization, and other performance counters in real time.
- `App` GPU time in logcat: If the `App` value exceeds your per-frame budget (e.g., >13.9 ms at 72 Hz), your application is GPU-bound. If `App` is within budget but `Stale` is non-zero, the bottleneck is likely on the CPU side.
- `GPU%` utilization: Values consistently at or near 1.0 indicate the GPU is fully saturated. However, be aware that GPU% can be [misleadingly low during frame drops](#the-50-threshold) — always cross-reference with `App` GPU time to confirm whether the GPU is the bottleneck.

## Best practices

- Enable [Dynamic Resolution](/documentation/unity/dynamic-resolution/) as early as possible. This is the single most effective way to let the OS help you maintain frame rate, and it is a prerequisite for accessing the highest GPU levels on Quest 2 and later.
- Only request higher refresh rates (90, 120 Hz) if your application can sustain them. A smooth 72 Hz experience is better than a 90 Hz experience with frequent dropped frames.
- If your application has high visual complexity and can tolerate the tradeoffs (transparency limitations, motion vector requirements), [Application SpaceWarp](/documentation/unity/os-app-spacewarp/) can provide up to 70% more GPU headroom.
- Enable [Fixed Foveated Rendering](/documentation/unity/os-fixed-foveated-rendering/) for meaningful GPU savings, especially for pixel-shader-heavy applications, with minimal perceptible quality loss in typical use.
- Use the monitoring tools above throughout development rather than only at the end. Frame rate issues are easier to address when caught early.