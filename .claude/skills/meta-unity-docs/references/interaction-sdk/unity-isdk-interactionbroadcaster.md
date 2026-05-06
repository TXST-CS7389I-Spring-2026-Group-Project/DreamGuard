# Unity Isdk Interactionbroadcaster

**Documentation Index:** Learn about unity isdk interactionbroadcaster in this documentation.

---

---
title: "Interaction Broadcaster"
description: "Monitor interaction events from 3D and UI sources through a single event channel for analytics, tutorials, and global UI."
last_updated: "2026-04-13"
---

An Interaction Broadcaster is a scene-level singleton that observes all interaction state changes, including those from 3D and UI sources. It rebroadcasts them by using a ScriptableObject InteractionEventChannel and a static C# event. This helps centralize a data source for analytics, tutorials, global UI, and systems like the Feedback Manager.

## Overview

Most interactions adhere to a unified [Pointer Lifecycle and Pointer Events](/documentation/unity/unity-isdk-pointer-events/) model (Identifier, EventType, Pose, Data). The Broadcaster listens to those changes across interactors and interactables, normalizes them into an `InteractionEvent` struct, and raises:

- `InteractionEventChannel.OnEventRaised` (inspector-friendly)
- `InteractionBroadcaster.OnEventRaised` (C# static event)

This pattern avoids tight coupling and makes it easy to monitor behavior across the scene.

## How it works

The Interaction Broadcaster uses **auto-registration** to automatically set up connections between common types of Interactors (such as input sources like hands, controllers, and rays) and Interactables (such as buttons, panels, grabbable objects). This makes setup less error-prone.

The Interaction Broadcaster uses an **event flow** that lets you handle interaction events in a modular and decoupled way. This lifecycle consists of the following flow:

1. The interactable object, such as a button or grabbable item, detects an interaction event (such as hover, grab, or poke).
2. A handler processes the interaction event, determining the event type, such as hover, select, or move.
3. The broadcaster emits the event to the InteractionEventChannel, making it available to other systems.
4. If configured, the broadcaster can also send the event to a channel as a UnityEvent, C# delegate event, or using a custom event system.
5. All scripts, components, and systems that subscribed to this event receive it and can respond.

You can extend the Interaction SDK (ISDK) to define your own [Interactor](/documentation/unity/unity-isdk-interactor/) and [Interactable](/documentation/unity/unity-isdk-interactable/) pairs and add custom logic to handle the new types.

## Setup

You can add the Interaction Broadcaster to your scene in two ways:

### Option 1: Use the FeedbackManager prefab (Recommended)

The FeedbackManager prefab includes an InteractionBroadcaster component.

1. In the **Project** panel, navigate to **Packages** > **Meta XR Interaction SDK** > **Runtime** > **Prefabs** > **Feedback**.
1. Drag `FeedbackManager.prefab` into the root of your scene **Hierarchy**.

The prefab is already configured with both the InteractionBroadcaster and an InteractionEventChannel.

### Option 2: Standalone broadcaster

If you only need event broadcasting without the Feedback Manager:

1. In the **Hierarchy** panel, right-click an empty area and select **Create Empty**.
1. Name the GameObject "InteractionBroadcaster".
1. In the **Inspector**, click **Add Component** and search for **InteractionBroadcaster**.
1. (Optional) Assign an InteractionEventChannel ScriptableObject to the **Event Channel** field, or leave it null to use only the static C# event.

## Subscribe to events

Use one of the following options that fits your use case:

- **ScriptableObject channel** for designer wiring (connect to UnityEvents, VFX, and so on). See also Event Wrappers for per-object inspector flows.

- **Static C# event** for low-overhead.

To filter events in your subscription, use one of the following options:

- `InteractionType` (Hover/Unhover/Select/Unselect/Move/Cancel; plus UI types)
- Source `GameObject` (watch a single object)
- Interactor (use identifier or InteractorView)

## API reference

The Interaction Broadcaster exposes the following API for event monitoring:

### InteractionBroadcaster

- `static InteractionBroadcaster Instance { get; }` – Singleton instance of the InteractionBroadcaster
- `static event Action<InteractionEvent> OnEventRaised` – Static C# event fired when any interaction occurs
- `static void RegisterCustomInteractionType<TInteractor, TInteractable>()` – Registers a custom interactor-interactable pair for automatic event broadcasting
- `void RegisterInteractionType<TInteractor, TInteractable>()` – Instance method to register a custom interaction type

### InteractionEventChannel

- `event Action<InteractionEvent> OnEventRaised` – ScriptableObject-based event for inspector wiring
- `void Raise(in InteractionEvent evt)` – Manually raises an interaction event

### InteractionEvent

- `InteractionType _type` – The type of interaction (Hover, Select, Move, and so on)
- `IInteractorView InteractorView` – The interactor that triggered the event
- `GameObject _source` – The source GameObject that was interacted with
- `int _pointerId` – Unique identifier for pointer-based interactions

## Best practices

- Always unsubscribe in `OnDisable` to prevent memory leaks and handle domain reloads.
- Use ScriptableObject channels for Inspector wiring when you need designer-configurable event routing and don't mind minor overhead. Use static events for performance-critical code paths
- Always null-check `_source` or `InteractorView` before accessing.
- For systems that need to monitor interactions (e.g., tutorials, analytics, achievements), subscribe to the centralized broadcaster and filter events rather than attaching listeners to individual interactable objects

## Troubleshooting

### Problem: The broadcaster is not emitting any events after triggering interactions

**Solution:** Verify the following:

1. Ensure the InteractionBroadcaster component is active in your scene.
1. Check that the **Auto Register** option is enabled on the InteractionBroadcaster component in the Inspector.
1. If using custom interactor types, verify you called `RegisterCustomInteractionType<TInteractor, TInteractable>()` after the broadcaster starts.
1. Add a simple debug listener to confirm events are firing:

   ```csharp
   InteractionBroadcaster.OnEventRaised += (evt) => Debug.Log($"Event: {evt._type}");
   ```

### Problem: Custom interactor types are not broadcasting events

**Solution:** Register your custom types properly:

1. Ensure you call `RegisterCustomInteractionType<TInteractor, TInteractable>()` in your script's `Start()` method, after the broadcaster has initialized.
2. Verify that your custom types implement the required ISDK interfaces (IInteractor, IInteractable).
3. Check that your custom types are properly integrated with ISDK's registries.

### Problem: UI pointer events are not being received

**Solution:** Verify your UI setup:

1. Select your Canvas in the **Hierarchy**.
2. In the **Inspector**, check that **PointableCanvas** and **PointableCanvasUnityEventWrapper** components are attached and reference your Canvas.
3. If missing, click **Add Component** and search for **PointableCanvasModule** and **PointableCanvasUnityEventWrapper**.

## Example: Surface ripple cursor effect

A lightweight visual effect that demonstrates using Interaction Broadcaster to create input-agnostic poke feedback to trigger a ripple effect on a surface.

### Quick start (prefab)

Add `RippleCursorEffect.prefab`. It references the cursor prefab. Requires a PokeInteractor in your rig and an Interaction Broadcaster in the scene.

For a working example, see the UISetExamples scene in the [Creating UIs with UISet](/documentation/unity/unity-isdk-create-uiset-ui/) sample project.