# Unity Isdk Feedbackmanager

**Documentation Index:** Learn about unity isdk feedbackmanager in this documentation.

---

---
title: "Feedback Manager"
description: "Configure event-to-feedback rules for haptics, audio, and VFX using a single Feedback Manager component in Interaction SDK."
last_updated: "2025-10-30"
---

Feedback Manager is a centralized, rule-based system that maps interaction events to feedback actions such as haptics, audio, VFX, so you don't need to include feedback logic across each Interactable object. It consumes Interaction SDK's unified Pointer Events flow and plays feedback consistently across interactor types.

## Overview

Interaction SDK surfaces all core interactions (Hover, Select, and so on) through a unified Pointer Lifecycle and Pointer Events contract. Feedback Manager subscribes to those events (via the Interaction Broadcaster or a provided InteractionEventChannel) and executes reusable FeedbackAction assets. This keeps feedback decoupled from interactables and consistent across hands/controllers and UI.

## How it works

The Feedback Manager uses an event-driven architecture that processes interaction events and executes feedback based on matching rules:

- **Event-driven:** Listens for interaction events (Hover/Unhover/Select/Unselect/Move/Cancel; also UIHover/UISelect) emitted by interactable objects and UI pointers.
- **Rule-based:** A FeedbackConfig asset holds an ordered list of rules (first-match wins). Rules can filter by interaction type, Unity Tag, and InteractorKind (Poke, Ray, Grab, HandGrab, and so on).
- **Hierarchical overrides:** A per-object FeedbackSettings component can suppress feedback or override global rules.
- **Modular actions:** Each rule runs one or more FeedbackAction ScriptableObjects (for example, haptic clip, sound, VFX).
- **Haptics integration:** Works with the Haptics SDK for Unity if you want authored clips or controller impulses.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

- Interaction SDK v81 or later installed.

## Set up and configure the Feedback Manager

### Step 1: Add the prefab

1. In the **Project** panel, navigate to **Packages** > **Meta XR Interaction SDK** > **Runtime** > **Prefabs** > **Feedback**.
1. Drag `FeedbackManager.prefab` into the root of your scene **Hierarchy**.

The prefab includes both the FeedbackManager component and the InteractionBroadcaster component. It also references a default FeedbackConfig asset and InteractionEventChannel.

### Step 2: Verify interactor decorators

Interaction SDK's interactor prefabs (Poke, Ray, Grab, HandGrab, DistanceHandGrab, HandGrabUse) include an InteractorControllerDecorator component that routes haptic feedback to the correct controller or hand.

1. In the **Hierarchy** panel, expand your interaction rig and select an interactor.
1. In the **Inspector**, verify that the InteractorControllerDecorator component is present. If the component is missing, add it by selecting **Add Component** > **InteractorControllerDecorator**.

### Step 3: Test in Play mode

Press **Play** in the Unity Editor to test the default feedback configuration. The default rules provide light haptic feedback on hover and stronger haptic pulses on select for both 3D objects and UI interactions.

## Configure feedback

You can configure global rules or per-object overrides to define how the FeedbackManager behaves.

### Define global rules in FeedbackConfig

Use a FeedbackConfig asset to define feedback rules that apply across your entire scene. Rules are evaluated in order from top to bottom, and the first matching rule determines which feedback actions execute.

1. In the **Project** panel, right-click in your Assets folder and select **Create** > **Interaction SDK** > **Feedback Config**.
1. Name your asset (for example, "MyFeedbackConfig").
1. Select the asset in the Project panel to view it in the **Inspector**.
1. In the **Rules** list, click the **+** button to add a new rule.
1. Configure each rule with the following properties:
      - **Interaction Type**: Event type that triggers this rule (for example, HoverStart, SelectEnd, UISelectStart)
      - **Tag** (optional): Tag name that matches all GameObjects that contain that name
      - **Interactor Kind**: Interaction type that matches all interaction methods of the same type such as Any, Poke, Ray, Grab, and HandGrab
      - **Actions**: List of FeedbackAction assets to execute when this rule matches
1. Arrange rules from most specific (tagged, specific interactor kinds) to most general (any tag, any interactor kind). For guidance on rule ordering, see the [Best practices](#best-practices) section.
1. In the **Hierarchy**, select the **FeedbackManager** GameObject.
1. In the **Inspector**, assign your new FeedbackConfig asset to the **Config** field.

### Per-object overrides (FeedbackSettings)

Use the FeedbackSettings component to customize feedback for individual interactable objects, overriding the global rules.

1. In the **Hierarchy**, select an interactable GameObject such as a button or grabbable object.
1. In the **Inspector**, click **Add Component** and search for **FeedbackSettings**.
1. Select a **Mode**:
      - **Default**: Uses the global rules from the FeedbackConfig
      - **Suppress**: Blocks all feedback for this object
      - **Override**: Uses custom feedback actions per interaction type and blocks any global rules for that object.
1. If you selected **Override**, expand the **Overrides** section and configure feedback actions for specific interaction types.

## Create custom actions

Actions are ScriptableObjects derived from `FeedbackActionSO` with a single entry point:

```csharp
public override void Execute(int identifier, GameObject source, FeedbackManager manager) { ... }
```

### Example: Custom audio feedback action

This example shows how to create a custom feedback action that plays an audio clip when an interaction occurs:

```csharp
using Oculus.Interaction.Feedback;
using UnityEngine;

namespace YourNamespace
{
    [CreateAssetMenu(menuName = "Your Project/Feedback/Audio Feedback")]
    public class AudioFeedbackAction : FeedbackActionSO
    {
        [SerializeField]
        private AudioClip _audioClip;

        [SerializeField, Range(0f, 1f)]
        private float _volume = 0.8f;

        public override void Execute(int identifier, GameObject source, FeedbackManager manager)
        {
            if (_audioClip != null && source != null)
            {
                AudioSource.PlayClipAtPoint(_audioClip, source.transform.position, _volume);
            }
        }
    }
}
```

After creating this script:

1. Right-click in the **Project** panel and select **Create** > **Your Project** > **Feedback** > **Audio Feedback**.
1. Assign an AudioClip and adjust the volume.
1. Add the created asset to a rule's **Actions** list in your FeedbackConfig.

</oc-devui-note>

## API reference

The Feedback Manager exposes the following API:

### FeedbackManager

- `static FeedbackManager Instance` – Singleton instance of the FeedbackManager
- `static bool Exists` – Returns true if a FeedbackManager instance exists in the scene
- `FeedbackConfig Config` – The currently active FeedbackConfig asset
- `void PlayHaptics(int sourceId, in HapticPattern pattern)` – Plays a haptic pattern on the controller associated with the source ID
- `void StopHaptics(int sourceId)` – Stops haptic playback on the controller associated with the source ID

### FeedbackConfig

- `Rule FindRule(InteractionType type, GameObject src, IInteractorView view, int pointerId)` – Finds the first matching rule for the given parameters
- `static InteractorKind KindOf(IInteractorView view, int pointerId)` – Determines the InteractorKind from an interactor view

### FeedbackSettings

- `FeedbackMode Mode` – The feedback mode (Default, Suppress, or Override)
- `bool TryGetOverrideActions(InteractionType type, out IReadOnlyList<FeedbackActionSO> actions)` – Gets the override actions for a specific interaction type

## Best practices

Follow these recommendations when configuring the Feedback Manager:

- **Order rules from specific to general.** Place rules with specific tags and interactor kinds first, then add broader fallback rules. This ensures precise matching for special cases while providing default behavior for everything else. For example:
  - Rule 1: Tag="Button", InteractorKind=Poke, SelectStart → Strong haptic pulse
  - Rule 2: Tag="Button", InteractorKind=Any, SelectStart → Medium haptic pulse
  - Rule 3: Tag=Any, InteractorKind=Any, SelectStart → Light haptic pulse
- **Reuse FeedbackAction assets across multiple rules.** Instead of creating duplicate assets, reference the same FeedbackAction from multiple rules to maintain consistency and reduce asset management overhead.
- **Use Unity Tags to categorize interactables.** Assign tags like "Button", "Lever", or "Door" to express semantic categories, then write rules that target those tags. This makes it easy to apply consistent feedback to entire classes of objects.
- **Minimize allocations in Execute() methods.** Keep your custom FeedbackAction implementations efficient by avoiding frequent allocations. Consider using object pooling for VFX and audio sources.
- **Combine with Event Wrappers for flexibility.** If designers prefer inspector-based wiring for specific objects, use the Event Wrapper pattern on individual interactables while maintaining the Feedback Manager for global consistency.

## Troubleshooting

### Problem: Interactor doesn't trigger a haptic response

**Solution:** Verify the following:

1. Ensure the FeedbackManager GameObject is active in your scene.
1. Check that your FeedbackConfig has a rule matching the interaction type, interactor kind, and object tag.
1. Select the interactor in the Hierarchy and verify that an InteractorControllerDecorator component is attached.
1. If using a custom FeedbackAction, add debug logging to the `Execute()` method to confirm it's being called.

### Problem: Wrong feedback plays for an interaction

**Solution:** Check your rule order and object tags:

1. Review the rule order in your FeedbackConfig. Remember that the first matching rule executes.
1. Select the interactable object in the **Hierarchy** and verify it contains a matching Unity tag in the **Inspector**.
1. Temporarily add a DebugLogAction to your rules to see which rule is matching.

### Problem: Performance issues or frame drops during interactions

**Solution:** Optimize your feedback actions:

1. Use object pooling for particle effects and audio sources to avoid instantiation overhead.
1. Limit the number of simultaneous audio sources playing.
1. Profile your custom `Execute()` methods using Unity's Profiler to identify performance bottlenecks.

## Samples

For a comprehensive example of the Feedback Manager with default controller haptics configured, see the [ComprehensiveRigExample scene](/documentation/unity/unity-isdk-comprehensive-rig-example-scene/).