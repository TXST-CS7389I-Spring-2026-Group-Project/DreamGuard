# Gpu Improved Algorithms

**Documentation Index:** Learn about gpu improved algorithms in this documentation.

---

---
title: "Mobile GPUs and improved algorithms"
description: "GPU-friendly rendering algorithms and techniques optimized for Meta Quest mobile hardware."
last_updated: "2024-08-16"
---

This article discusses algorithms that run faster on mobile GPUs (like those on Meta Quest devices) than on desktop/console GPUs. Meta recommends these algorithms for Quest development.

## Background

Mobile GPUs have very small amounts of on-chip memory due to power constraints, but reading and writing this memory is much faster than on desktop GPUs, similar to how CPU's L1 cache is much faster to access than its L3 cache.

This detail makes some algorithms, considered too expensive for desktop GPUs, very inexpensive on mobile GPUs.

## Multi-sampled anti-aliasing (MSAA)

MSAA is a technique which reduces aliasing on the edges of objects, where aliasing is usually most noticeable. It creates multisampled color and depth buffers that sample and record multiple screen-space positions corresponding to each individual output pixel. If a triangle covers all multisampled locations in an output pixel, its fragment shader only runs once, and there are no additional costs compared to rendering without any anti-aliasing. If different triangles cover different sampled locations in an output pixel, the fragment shader runs once per-triangle in the output pixel, and the results are blended.

Because the depth buffer is tiled and fits in the ~1mb of on-chip GPU memory, it is _extremely_ fast to access. A multisampled depth buffer requires more memory, resulting in your output frame being cut into more, smaller tiles, but internal benchmarks show that 4x MSAA is extremely cheap on Quest devices. As such, Meta recommends this anti-aliasing algorithm. For more information about MSAA costs, see [Multisample Anti-Aliasing Analysis for Meta Quest](/documentation/native/android/mobile-msaa-analysis/).