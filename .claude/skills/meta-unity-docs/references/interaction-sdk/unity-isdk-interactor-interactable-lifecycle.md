# Unity Isdk Interactor Interactable Lifecycle

**Documentation Index:** Learn about unity isdk interactor interactable lifecycle in this documentation.

---

---
title: "Interactor Interactable Lifecycle"
description: "Learn how Interactors and Interactables advance through the interaction lifecycle and respond to state changes."
---

The interaction lifecycle represents the possible statuses of an [Interactor](/documentation/unity/unity-isdk-interactor/) or [Interactable](/documentation/unity/unity-isdk-interactable/). There are four possible statuses: Disabled, Normal, Hover, and Select. Each status is an [Active State](/documentation/unity/unity-isdk-active-state/).

Interactors and Interactables can only move between these states as follows:

Disabled <-> Normal <-> Hover <-> Select

{:width="150px"}

**Interactors** can be in any of the following states:

- **Disabled**: The **Interactor** is in a disabled state where no hovering or selection can occur.

- **Normal**: The default state.

- **Hover**: The **Interactor** is in a state where it can select (may have optional **Interactable**).

- **Select**: The **Interactor** is selecting (may have optional **Interactable**).

<em>The cube is using the <b>InteractorDebugVisual</b> component to visually display the current state of a <b>PokeInteractor</b>. The interactor goes from normal (red) to hover (blue) and finally to select (green).</em>

**Interactables** can be in any of the following states:

- **Disabled**: The **Interactable** is in a disabled state where no hovering or selection can occur.

- **Normal**: The default state.

- **Hover**: The **Interactable** has one or more **Interactors** hovering (and none selecting it).

- **Select**: The **Interactable** has one or more **Interactors** selecting it.

## Related Topics

- To learn how to use Active State, see [Use Active State](/documentation/unity/unity-isdk-use-active-state/).
- To use Active State to build a custom hand pose, see [Build a Custom Hand Pose](/documentation/unity/unity-isdk-building-hand-pose-recognizer/).