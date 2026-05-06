# Unity Core Trackables

**Documentation Index:** Learn about unity core trackables in this documentation.

---

---
title: "Unity Trackables"
description: "Track dynamic objects like keyboards, controllers, and hands using the Unity Trackables API."
last_updated: "2025-05-29"
---

## Learning Objectives

- **Explain** what a **trackable** is and how it differs from static scene anchors.
- **Grant** the Spatial Data permission required for all tracking APIs.
- **Use** **TrackerConfiguration** static support checks (e.g. `OVRAnchor.TrackerConfiguration.KeyboardTrackingSupported`) before configuring.
- **Configure** an **OVRAnchor.Tracker** via **ConfigureAsync** and handle unsupported or partially satisfied configurations.
- **Fetch** trackables with **FetchTrackablesAsync** and handle the returned list.
- **Implement** continuous polling (e.g. looping with a delay) to automatically refresh your trackables list.

<box height="10px"></box>
---
<box height="10px"></box>

## Overview

A **trackable** is a physical object (e.g. a keyboard) that can be detected and tracked at runtime, unlike [scene anchors](/documentation/unity/unity-scene-overview#scene-anchors), which are captured during Space Setup. You can use the low-level Core SDK (**OVRAnchor.Tracker**) for full control or the high-level MRUK APIs ([MRUKTrackable](/documentation/unity/unity-mr-utility-kit-trackables)) for event-driven simplicity.

In order for your app to be able to detect trackables, you must configure an anchor tracker. You can then periodically query for trackables. This topic describes the low-level API available in the Meta XR Core SDK. Use this if you want full control.

<box height="10px"></box>

<oc-devui-note type="note" heading ="Trackables in MRUK">
  For most use cases, consider using the <a href="/documentation/unity/unity-mr-utility-kit-trackables">MRUK</a> package. It provides a simple event-driven API for using trackables.
</oc-devui-note>

<box height="20px"></box>
---
<box height="10px"></box>

## OVRAnchor.Tracker in Meta XR Core SDK

Use the low-level Meta XR Core SDK when you need full control over detection. Create an **OVRAnchor.Tracker**, configure it for specific types, and poll with **FetchTrackablesAsync**.

### Spatial Data Permission

Like Scene Anchors, trackables require the [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/).

### Configuring the Tracker

To track a particular type of trackable, create and configure an **OVRAnchor.Tracker**:

```csharp
OVRAnchor.Tracker _tracker;

async void Start()
{
    // Create a tracker
    _tracker = new OVRAnchor.Tracker();

    // Configure it to detect keyboards
    var result = await _tracker.ConfigureAsync(new OVRAnchor.TrackerConfiguration
    {
        KeyboardTrackingEnabled = true,
    });

    if (result.Success)
    {
        // Keyboard tracking enabled!
    }
}
```

If the requested configuration is not supported, or if the configuration cannot be fully satisfied at the current time, then **ConfigureAsync** will return a result indicating an error. Additionally, the [Configuration](/reference/unity/latest/class_o_v_r_anchor_tracker/) property represents the current state of the tracker. If `requestedConfiguration != tracker.Configuration`, some aspect of the requested configuration could not be satisfied.

The **TrackerConfiguration** class has static methods to check for feature support prior to configuring the tracker. For example, to check whether keyboard tracking is supported, use `OVRAnchor.TrackerConfiguration.KeyboardTrackingSupported`.

### Fetching Trackables

Similar to [OVRAnchor.FetchAnchorsAsync](/documentation/unity/unity-core-trackables#:~:text=OVRAnchor.FetchAnchorsAsync), use **FetchTrackablesAsync** to get a list of all currently tracked anchors.

```csharp
List<OVRAnchor> _anchors = new List<OVRAnchor>;();

async void UpdateTrackables()
{
    var result = await _tracker.FetchTrackablesAsync(_anchors);
    if (result.Success)
    {
        foreach (var anchor in _anchors)
        {
            Debug.Log($"Anchor {anchor} is a trackable of type {anchor.GetTrackableType()}");
        }
    }
}
```

The list updates as objects enter or leave view. Call this method periodically to refresh your list of trackables.

### Continuous Polling

To automatically refresh without manual calls:

```csharp
async void PollLoop()
{
    while (enabled)
    {
        await UpdateTrackables();
        await Task.Delay(TimeSpan.FromSeconds(1));
    }
}
```

<box height="20px"></box>
---
<box height="10px"></box>

## Related Samples

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img src="/images/unity-mruk-samples-keyboardtracking.gif" alt="Space Sharing Motif Sample" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
  <heading type="title-small-emphasized">MRUK Keyboard Tracker Sample</heading>
    <p>Demonstrates how to build a keyboard tracker using MRUKTrackables and trackable events. Includes the <b>Bounded3DVisualizer</b> that fits prefabs, such as a Passthrough Overlay, to a detected keyboard’s 3D bounds.</p>
    <a href="/documentation/unity/unity-mrmotifs-colocated-experiences">View sample</a>
  </box>
</box>

<box height="30px"></box>
---
<box height="20px"></box>

## Related Content

- [Track Objects in MRUK](/documentation/unity/unity-mr-utility-kit-trackables)
  Track keyboards using MRUK-trackables.