# Unity Isdk Choosing Grab Type

**Documentation Index:** Learn about unity isdk choosing grab type in this documentation.

---

---
title: "Choosing a Grab Interaction Type"
description: "Compare ISDK grab interactor types and select the right one for your input modality and range requirements."
last_updated: "2026-03-11"
---

Interaction SDK provides five grab interactor types organized across two dimensions: input modality (hands or controllers) and range (near-field or far-field). Each type is purpose-built for a specific combination of these dimensions.

This page explains the differences between grab types and helps you select the right combination for your use case. For setup instructions, see the dedicated pages linked in [Learn more](#learn-more).

## Grab interaction types

ISDK organizes grab interactors into two parallel tracks: a hand and a controller. Each track offers a near-field and a far-field variant.

### Hand grab types

- **`HandGrabInteractor`**: Near-field grab for hand tracking. Selects objects through rigidbody overlap with per-finger detection and supports authored `HandGrabPose` assets for precise finger placement. Grab types include Pinch, Palm, or All (default: All).

- **`DistanceHandGrabInteractor`**: Far-field grab for hand tracking. Selects objects through conical frustums and supports authored `HandGrabPose` assets with per-finger rules. Grab types include Pinch, Palm, or All (default: Pinch).

- **`TouchHandGrabInteractor`**: An experimental near-field alternative for hand tracking. Uses sphere overlap per joint and generates procedural (collider-based) hand poses instead of authored ones.

### Controller grab types

- **`GrabInteractor`**: Near-field grab for controllers. Selects objects through rigidbody overlap combined with an `ISelector` interface (for example, a trigger press). Does not use hand poses or finger rules.

- **`DistanceGrabInteractor`**: Far-field grab for controllers. Selects objects through conical frustums combined with an `ISelector` interface. Does not use hand poses or finger rules.

## Comparison table

CDH = controller-driven hands

| Dimension | Hand Grab | Distance Hand Grab | Controller Grab | Distance Grab | Touch Hand Grab |
|---|---|---|---|---|---|
| **Input** | Hands + CDH | Hands + CDH | Controllers (+ hands) | Controllers | Hands + CDH |
| **Range** | Near (arm's reach) | Far | Near (arm's reach) | Far | Near |
| **Selection** | Rigidbody overlap + per-finger | Conical frustums | Rigidbody overlap + `ISelector` | Conical frustums + `ISelector` | Sphere overlap per joint |
| **Hand poses** | Authored (`HandGrabPose`) | Authored (`HandGrabPose`) | None | None | Procedural (collider-based) |
| **Finger rules** | Yes (per-finger) | Yes (per-finger) | No | No | N/A (collider-driven) |
| **Grab types** | Pinch / Palm / All (default: All) | Pinch / Palm / All (default: Pinch) | N/A (`ISelector`) | N/A (`ISelector`) | N/A |
| **Status** | Stable | Stable | Stable | Stable | Experimental |

Key relationships between types:

- `HandGrabInteractor` and `DistanceHandGrabInteractor` share the `IHandGrabInteractable` interface, so they use the same hand poses, finger rules, and grab type settings.
- `GrabInteractor` and `DistanceGrabInteractor` both rely on `ISelector` for external selection logic.
- `HandGrabInteractable` and `DistanceHandGrabInteractable` integrate with the `Grabbable` component and its transformer system for object movement. The other types use their own movement mechanisms.

## Decision guide

Select a grab type based on the input modality and range your interaction requires.

| Input | Range | Grab type |
|-------|-------|-----------|
| Hands | Near-field, authored poses | `HandGrabInteractor` / `HandGrabInteractable` |
| Hands | Near-field, procedural poses | `TouchHandGrabInteractor` / `TouchHandGrabInteractable` (experimental) |
| Hands | Far-field | `DistanceHandGrabInteractor` / `DistanceHandGrabInteractable` |
| Controllers | Near-field | `GrabInteractor` / `GrabInteractable` |
| Controllers | Far-field | `DistanceGrabInteractor` / `DistanceGrabInteractable` |
| Both | Any | Combine multiple interactable types on the same object |

For most applications, start with `HandGrabInteractor` for near-field interactions and `DistanceHandGrabInteractor` for far-field interactions. Add the corresponding controller grab types if your application also supports controllers.

## Combining types on one object {#combining-types}

To support multiple input modalities, add multiple interactable components to the same GameObject. Grab interactable types are designed to coexist.

Key behaviors when combining types:

- **Shared hand poses**: `HandGrabPose` assets are shared between `HandGrabInteractable` and `DistanceHandGrabInteractable`. You do not need to duplicate poses when both are present on the same object.
- **Automatic setting propagation**: When you add a `DistanceHandGrabInteractable` to a GameObject that already has a `HandGrabInteractable`, the distance variant auto-copies the grab type settings, grab rules, and hand poses.
- **Quick Actions wizard**: The [Quick Actions](/documentation/unity/unity-isdk-quick-actions/) wizard supports adding all interaction types to an object.

Common combinations:

| Combination | Purpose |
|-------------|---------|
| HandGrab + DistanceHandGrab | Near and far grab with hands |
| HandGrab + Grab | Hand and controller near grab |
| HandGrab + DistanceHandGrab + Grab + DistanceGrab | Full support for all modalities and ranges |

## Learn more {#learn-more}

### Grab type reference pages

- [Hand Grab Interactions](/documentation/unity/unity-isdk-hand-grab-interaction/)
- [Distance Hand Grab Interactions](/documentation/unity/unity-isdk-distance-hand-grab-interaction/)
- [Controller Grab Interactions](/documentation/unity/unity-isdk-grab-interaction/)
- [Distance Grab Interactions](/documentation/unity/unity-isdk-distance-grab-interaction/)
- [Touch Hand Grab Interactions](/documentation/unity/unity-isdk-touch-hand-grab-interaction/)

### Related topics

- To configure how grabbed objects move and respond to physics, see [Hand Grab Interactions — How grabbed objects move](/documentation/unity/unity-isdk-hand-grab-interaction/#how-grabbed-objects-move).
- To create sliders, dials, or drawers, see [Constrained Grab Interactions](/documentation/unity/unity-isdk-constrained-grab-interactions/).
- To configure two-handed grab, see [Two-Handed Grab Interactions](/documentation/unity/unity-isdk-two-handed-grab-interaction/).
- To add grab interactions with Quick Actions, see [Grab Quick Action](/documentation/unity/unity-isdk-grab-quick-action/).

### Design guidelines

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.

#### Core interactions

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.