# Unity Isdk Interactor Group

**Documentation Index:** Learn about unity isdk interactor group in this documentation.

---

---
title: "Interaction SDK InteractorGroups"
description: "Choose among BestHover, BestSelect, and FirstHover InteractorGroups to resolve conflicts when multiple Interactors compete."
---

**InteractorGroups** can be used to coordinate multiple **Interactors** based on state. These components become the root driving component in a group of [`IInteractors`](/reference/interaction/latest/interface_oculus_interaction_i_interactor), and will advance the lifecycle of the “best” `IInteractor` in the group in priority order.

**Note:**  **InteractorGroups** also implements (and acts as) `IInteractors`. This means they can be nested within other **InteractorGroups** to handle more complex scenarios. In all cases, the root-most **InteractorGroup** serves as the update-loop entrypoint for all nested `IInteractors`.

There are several types of **InteractorGroups** to choose from depending on your use case.

## BestHoverInteractorGroup

[`BestHoverInteractorGroup`](/reference/interaction/latest/class_oculus_interaction_best_hover_interactor_group) is the default **InteractorGroup**. Until there is a selection, the highest priority [`IInteractor`](/reference/interaction/latest/interface_oculus_interaction_i_interactor) that is hovering will remain the active `IInteractor` unless an `IInteractor` with a higher priority can hover.

Upon selection, the selecting `IInteractor` will not change until selection ends. By default, a group is prioritized in list-order (first = highest priority). An [`ICandidateComparer`](/reference/interaction/latest/interface_oculus_interaction_i_candidate_comparer) can be provided to prioritize `IInteractors` in a different order.

## BestSelectInteractorGroup

[`BestSelectInteractorGroup`](/reference/interaction/latest/class_oculus_interaction_best_select_interactor_group) can be used to coordinate multiple **Interactors** based on select state. Unlike **InteractorGroup**, up until there is a selection, multiple `IInteractors` may be in a hover state.

Upon selection, the selecting `IInteractor` will not change until selection ends.

## FirstHoverInteractorGroup

[`FirstHoverInteractorGroup`](/reference/interaction/latest/class_oculus_interaction_first_hover_interactor_group) keeps active the first interactor to Hover, preventing any other interactor to enter Hover state until it unhovers. When two interactors hover at exactly the same time, it will give priority to the best one based on list order or the [`ICandidateComparer`](/reference/interaction/latest/interface_oculus_interaction_i_candidate_comparer).

## ICandidateComparer

By default, **InteractorGroups** are prioritized in list-order (first = highest priority). [`ICandidateComparer`](/reference/interaction/latest/interface_oculus_interaction_i_candidate_comparer) can be provided to prioritize [`IInteractors`](/reference/interaction/latest/interface_oculus_interaction_i_interactor) in a different order based on their **CandidateProperties**.

For instance, for those `IInteractors` whose **CandidateProperties** can be cast to an [`ICandidatePosition`](/reference/interaction/latest/interface_oculus_interaction_i_candidate_position), a **CandidatePositionComparer** can prioritize `IInteractors` by their candidates’ position as measured by a distance to a common location.