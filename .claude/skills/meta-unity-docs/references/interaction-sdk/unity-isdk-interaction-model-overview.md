# Unity Isdk Interaction Model Overview

**Documentation Index:** Learn about unity isdk interaction model overview in this documentation.

---

---
title: "Interaction Model Overview"
description: "Understand how interaction models pair Interactor and Interactable components to define actions in Interaction SDK."
last_updated: "2024-08-07"
---

## What is an Interaction Model?

Interaction SDK is built around interactions, which are actions you can take in your virtual environment, like teleporting, grabbing an object, or poking a button. An interaction model is the structure that defines most interactions.

## How do Interaction Models work?

Interaction Models work by pairing an **interactor** component performing the interaction with an **interactable** component being interacted with. The interactor component is attached to the hand or controller and determines whether an interaction of that type has taken place, while the interactable component specifies the logic for how that type of interaction should behave.

## Learn more

For more detailed information about the components of an interaction model, please see [Interactor](/documentation/unity/unity-isdk-interactor/), [Interactable](/documentation/unity/unity-isdk-interactable/), and [Pointer Event](/documentation/unity/unity-isdk-pointer-events/).