# Po Memory Ram

**Documentation Index:** Learn about po memory ram in this documentation.

---

---
title: "Memory / RAM"
description: "Identify Horizon OS memory limits and diagnose when your app exceeds available RAM on Meta Quest."
last_updated: "2025-08-12"
---

Like all computing devices, the Meta Quest devices have a limited amount of memory (or RAM) available to run applications. However, the Horizon OS must reserve a portion of physical memory for its own use in performing tracking, and running background applications.

As such, the amount of memory reserved for an immersive application is less than the total amount of memory available on-device.

## Memory Limits

| Device | Memory Limit |
|--------|--------------|
| Quest 2 | 4.4 GiB |
| Quest Pro | 4.4 GiB |
| Quest 3, Quest 3S | 5.75 GiB |

Note that these limits are checked against your app's PSS, or Proportional Set Size. See the next section for details on how to measure PSS for your app.

## Identifying and Reducing Memory Usage

To learn more about tools to identify and reduce your memory usage, see [Getting a Handle on Meta Quest Memory Usage](/blog/getting-a-handle-on-meta-quest-memory-usage).

## Identifying Out-Of-Memory Crashes

Unfortunately, it is possible for your app to exceed the memory limit and _not_ be immediately killed by the [Android low memory killer daemon (lmkd)](https://source.android.com/docs/core/perf/lmkd). When your app is run on Horizon OS in a low-memory-pressure situation (i.e. no background updates, no background apps playing music or providing information), it is possible to exceed memory limits without crashing.

However, when examining your released app's [Crash analytics dashboard](/resources/publish-crash-analytics/), you may see crashes with the **Low Memory Kill** Crash Type. This indicates that your app is exceeding the memory limit for the platforms that have this crash type, and users in high-memory-pressure situations are crashing as a result.

When testing your app on a developer headset, you can also identify out-of-memory crashes by recording [logcat](/documentation/unity/ts-logcat) output and searching for the `lowmemorykiller` tag.