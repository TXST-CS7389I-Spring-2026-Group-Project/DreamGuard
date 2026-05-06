# Po Perf Opt Mobile

**Documentation Index:** Learn about po perf opt mobile in this documentation.

---

---
title: "Basic Optimization Workflow for Meta Quest Apps"
description: "Step-by-step workflow for profiling, diagnosing, and resolving performance issues in Meta Quest apps."
last_updated: "2024-12-09"
---

This topic guides you through a basic optimization workflow that uses profiling to help identify bottlenecks and issues with development builds of apps.

## FPS vs. Milliseconds per Frame

Optimization discussions often focus on frames per second (FPS), but a better metric to consider when optimizing apps may be milliseconds per frame. Measuring each frame in milliseconds provides a more actionable goal. Every frame must complete its CPU and GPU work in a set amount of time, usually measured in milliseconds, to hit a certain frame rate.

When using milliseconds per frame as a metric, each frame has a budget (i.e., a specific amount of time it must fit into). Performance targets for relevant frame rates are as follows:

* 60 FPS = 16.6 milliseconds per frame
* 72 FPS = 13.9 milliseconds per frame
* 90 FPS = 11.1 milliseconds per frame
* 120 FPS = 8.3 milliseconds per frame

All Meta Quest apps require a minimum of 72 FPS to satisfy Virtual Reality Check (VRC) requirements. Meta Quest 3 and Meta Quest 3S also support 90 Hz and 120 Hz display refresh rates, corresponding to the 90 FPS and 120 FPS frame budgets listed above. The next section of this guide shows you how profiling can help you achieve your target frame rate by identifying areas for optimization.

## Profiling Workflow

When profiling in this workflow, the first goal is to discover if the app is GPU bound or CPU bound. One way to determine if an app is CPU or GPU bound is to simply render nothing at all. This can be done by turning off the render camera and letting the app continue to run. This eliminates the cost of the render pipeline, including culling, submitting draw calls, running shaders, and so on. Watch the frame rate of the app and the milliseconds each frame takes to complete.

When you are done testing, look at your results and consider the following:
* If the app's performance is not affected or affected very little when not rendering anything, the app is likely CPU bound.
* If performance improves significantly, the app is likely GPU bound.

### Addressing CPU Bound Apps

Common causes for an app to be CPU bound include the complexity of app logic, physics simulation, garbage collection stalls, and so on.An instrumented profiler, like Unity Profiler, can track down these performance bottlenecks.Focus on optimizing only the most expensive code paths. Any app logic that takes longer than two milliseconds can probably be optimized.

### Addressing GPU Bound Apps

GPU bound apps usually fall into one of two categories:

* A *vertex-bound* app has issues with scene complexity.
* A *fragment-bound* app has issues with shader complexity.

The way to test for this is to render fewer pixels. This can be done by setting the app's render scale to something small, like 0.01. This will cause fewer fragments to be rendered, but retain the scene complexity.

When you are done testing, look at your results and consider the following:
* If performance is not affected, the app is likely vertex-bound.
* If performance improves, the app is likely fragment-bound (also commonly called fill-bound).

> **Note**:  To change the render scale in Unity, set the [eyeTextureResolutionScale](https://docs.unity3d.com/ScriptReference/XR.XRSettings-eyeTextureResolutionScale.html). 

#### Vertex Bound

The method used to determine if an app is CPU or GPU bound (disabling all render cameras) means that some CPU-side calculations, like frustum culling, are considered vertex-bound operations. While this is not completely accurate, these operations are often optimized by the same actions that optimize vertex-bound apps. The most common issues for a vertex-bound app are as follows:

* Culling objects is taking too long
* Too many draw calls are being issued
* Too many vertices are being rendered

Use a frame debugger like [RenderDoc Meta Fork](/documentation/unity/ts-renderdoc-for-oculus/) on a part of your app that has issues in order to identify these bottlenecks. Simplifying complex geometry and reducing draw calls will often fix these issues. You may want to look into an LOD system or batching draw calls, depending on the needs of your app.

#### Fill Bound

If an app is fill-bound, one or more of its shaders need to be optimized. Pixel complexity tends to be the main issue for fill-bound shaders.

### Optimize and Iterate

Use your findings to address the primary issue surfaced during the workflow. Profiling and optimizing is an iterative process. In most cases, addressing a single issue will not achieve the desired performance level. It usually takes a few rounds of optimization to get to desired performance. Go through the full workflow again after each optimization pass. If the app was CPU bound before, and it's been optimized, it could end up being fragment-bound next.