# Unity Isdk Interactor

**Documentation Index:** Learn about unity isdk interactor in this documentation.

---

---
title: "Interaction SDK Interactors"
description: "Configure the IInteractor interface, its Drive loop, and ComputeCandidate logic to control how interactions begin and end."
---

An **Interactor** is the component attached to your hand or controller that initiates any action, like a grab, teleport, or poke. To initiate an action, the **Interactor** uses input data to determine where it is in world space, and then it uses its unique `ComputeCandidate()` method to select a nearby **Interactable** component of the same type (ex. a **Ray Interactor** selects a unique **Ray Interactable**). **IInteractor**'s `Drive()` method determines when `ComputeCandidate()` should run, when a selection should occur, what should happen during a selection, and so on. All **Interactors** share the **Interactor** base class, which implements **IInteractor** and, by extension, **IInteractorView**.

## IInteractor

The **IInteractor** interface exposes methods with which you can modify the state of an interaction. Some of these include:

```
        void Enable();
        void Disable();

        void Preprocess();
        void Process();
        void Postprocess();

        void ComputeCandidate();
        bool HasCandidate { get; }
        object Candidate { get; }

        bool ShouldHover { get; }
        void Hover();

        bool ShouldUnhover { get; }
        void Unhover();

        bool ShouldSelect { get; }
        void Select();

        bool ShouldUnselect { get; }
        void Unselect();

        bool ComputeShouldSelect()
        bool ComputeShouldUnselect()

        bool HasInteractable { get; }
        bool HasSelectedInteractable { get; }
```

Additionally, **Interactor** exposes several methods to get a reference to a hovered or selected **Interactable**, as well as methods to get and subscribe to changes of the **InteractorState**.

All **Interactors** in the SDK implement [**IUpdateDriver**](/reference/interaction/latest/interface_oculus_interaction_i_update_driver) and by default advance themselves on Unity Update.

Should you choose to control the advancing of an **Interactor** through its lifecycle yourself (as may be the case if you are adopting **Interactors** into an existing project or external framework), you can set the **IsRootDriver** property to false, and then invoke the **IInteractor** methods of an **Interactor** directly.

Another instance when an **Interactor** may not want to advance itself by default is if its lifecycle needs to be coordinated and prioritized amongst other **Interactors**. For this, an [InteractorGroup](/documentation/unity/unity-isdk-interactor-group/) is recommended to drive a group of **Interactors**.

## Selector

An **ISelector** defines a selection mechanism for an interaction (eg. button press, index pinch). For instances where **Interactors** don’t themselves define a selection mechanism (eg. Ray, Grab), an **ISelector** can be provided to those **Interactors**.

## Reference

For reference information about interactors, see the following links.

- [IInteractor](/reference/interaction/latest/interface_oculus_interaction_i_interactor)
- [ISelector](/reference/interaction/latest/interface_oculus_interaction_i_selector)

## Related Topics

- To learn about Interactables, which are attached to objects that should respond to Interactors, see [Interactables](/documentation/unity/unity-isdk-interactable/).
- For a complete overview of Interaction SDK architecture, see [Architecture Overview](/documentation/unity/unity-isdk-architectural-overview/).
- To learn how Interactors are prioritized when there's more than one hovering at a time, see [InteractorGroup](/documentation/unity/unity-isdk-interactor-group/).
- To learn how the location of your hand or controller is determined, see [Input Data Overview](/documentation/unity/unity-isdk-input-processing/).