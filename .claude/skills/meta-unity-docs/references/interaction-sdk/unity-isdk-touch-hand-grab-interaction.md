# Unity Isdk Touch Hand Grab Interaction

**Documentation Index:** Learn about unity isdk touch hand grab interaction in this documentation.

---

---
title: "Touch Hand Grab Interactions"
description: "Add physics-based hand interactions that shape finger poses around object colliders using Touch Hand Grab in Interaction SDK."
last_updated: "2025-11-03"
---

<oc-devui-note type="important" heading="Experimental">
This feature is considered experimental. Use caution when implementing it in your projects as it could have performance implications resulting in artifacts or other issues that may affect your project.
</oc-devui-note>

***

**Design Guidelines:** Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Design Guidelines](#design-guidelines) at the bottom of the page to learn about best practices and to minimize risks of user discomfort.

***

Touch Hand Grab lets you use hands or controller driven hands to grab objects based on their collider configuration and dynamically conform fingers to their surface. This feature isn't available for controllers. To learn about best practices when designing for hands, see [Designing for Hands](/resources/hands-design-intro/). To try Touch Hand Grab in a pre-built scene, see the [Touch Grab](/documentation/unity/unity-isdk-example-scenes/#touchgrabexamples) scene.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## TouchHandGrabInteractor

The TouchHandGrabInteractor defines the touch interaction properties for this interactor including the set of spheres that are used for testing overlap with associated interactables.

- **Open Hand** configures the maximum open-hand joint rotations that a procedural grab will match.

- **Hand Sphere Map** configures the set of spheres per hand joint that are checked for overlap with a corresponding interactable.

- **Hover Location** and **Min Hover Distance** defines the broadphase location and radius that is used for choosing a corresponding interactable as a candidate.

- **Grab Location** defines the root of the point transform for the IPointable events that TouchHandGrabInteractor broadcasts.

- **Curl Delta Threshold** defines the angular threshold difference between a locked finger curl and a tracked finger curl under which a finger release will not trigger.

- **Curl Delta Threshold** defines the consecutive time threshold a locked finger curl must be greater than the curl delta threshold of a tracked finger curl under which a finger release will not trigger.

- **Iterations** gives you control over the number of iterations that should be used to compute procedural finger poses. A higher iterations will be more accurate but less performant.

## TouchHandGrabInteractable

The TouchHandGrabInteractable defines the colliders that are used by the interactor to test overlap against and be conformed to during selection.

- **Bounds Collider** configures the broadphase collider for this interactable for more performant overlap testing. Overlaps outside of this collider will be ignored.

- **Colliders** configures the list of colliders that the TouchHandGrabInteractor will conform fingers to. These can be primitive colliders or convex mesh colliders. Concave objects should be split into a set of convex hull mesh colliders.

## TouchHandGrabInteractorVisual

The TouchHandGrabInteractorVisual forwards the grab state of a TouchHandGrabInteractor to a [SyntheticHand](/documentation/unity/unity-isdk-input-processing/#synthetic-hand-modifier).

## Reference

For reference information about the components used by grabbables, see the following links.

- [TouchHandGrabInteractor](/reference/interaction/latest/class_oculus_interaction_touch_hand_grab_interactor)
- [TouchHandGrabInteractable](/reference/interaction/latest/class_oculus_interaction_touch_hand_grab_interactable)

## Known Issues

Touch grab interactions have the following known issues:

- When interacting with larger objects, it is possible for the object to become locked to the user's hand if the fingers cannot be spread far enough to disengage the sphere overlap detection.
- Finger joints can look incorrect in certain situations.

## Learn more

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