# Unity Isdk Secondary Interactions

**Documentation Index:** Learn about unity isdk secondary interactions in this documentation.

---

---
title: "Secondary Interactions"
description: "Link interactions in a primary-secondary relationship so one depends on another, such as grab then use an object."
---

You can link some interactions to others in a primary-secondary relationship. This can be useful for making one interaction dependent on another ongoing interaction. For example, if you want to first grab and then use an object, a grab interaction could be a primary interaction and a use interaction could be secondary to the grab.

Two components are required to establish such a relationship between interactions.

## Secondary Interactor Connection

A `SecondaryInteractorConnection` indicates that one interactor depends on another interactor. The dependent interactor is considered “secondary” to the “primary” interactor.

For example: In the **HandGrabUseExamples** scene, the `SecondaryInteractorConnection` links the [`HandGrabUseInteractor`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_use_interactor) as a secondary interactor to the [`HandGrabInteractor`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactor).

## Secondary Interactor Filter

The `SecondaryInteractorFilter` indicates that one interactable is secondary to a primary interactable. In addition to linking two interactables, this component acts as a filter and should be added to the secondary interactable’s **Interactor Filter** list.

By default, the `SecondaryInteractorFilter` enables the secondary interaction when hovering the primary interactable. By checking **Select Required**, the secondary interaction is only enabled when the primary interactable is selected.

For example:  In the **HandGrabUseExamples** scene, you can grab the spray bottle at two different [`HandGrabInteractables`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable), and then use it by squeezing the trigger with the index or middle finger.

* Each one of the [`HandGrabInteractables`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable) is linked to a [`HandGrabUseInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_use_interactable) by a `SecondaryInteractorFilter`. In this example, the `HandGrabUseInteractable` acts as the secondary interactor to the primary `HandGrabInteractables`.
* The [`HandGrabUseInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_use_interactable)’s **Interactor Filter** list includes each `SecondaryInteractorFilter`.
* Because we only want to be able to use the spray bottle while actively selecting (grabbing) it, the **Select Required** toggle in the component is set to true.