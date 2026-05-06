# Unity Isdk Input Processing

**Documentation Index:** Learn about unity isdk input processing in this documentation.

---

---
title: "Input Data Overview"
description: "Follow the data flow from headset sensors through From OVR Source components and hand modifiers to interaction-ready input."
last_updated: "2024-08-07"
---

## Overview

This topic explains the process of how Interaction SDK gets, structures, and modifies tracking data to create interactions. In Interaction SDK, all interactions rely on accurate data about the position and rotation of your headset's hands and controllers, which the SDK gets directly from your headset's camera and controllers.

## Process Flow

Interaction SDK formats the input data using a set of interfaces ([`IController`](/reference/interaction/latest/interface_oculus_interaction_input_i_controller), [`IHand`](/reference/interaction/latest/interface_oculus_interaction_input_i_hand), [`IHmd`](/reference/interaction/latest/interface_oculus_interaction_input_i_hmd/), and [`IBody`](/reference/interaction/latest/interface_oculus_interaction_body_input_i_body/)). Each interface handles a certain type of data. For example, `IHand` interprets raw hand data, and `IController` interprets raw controller data. The interfaces define how the raw input data is organized so that it's understandable by Interaction SDK components and prefabs.

To actually get the raw input data from the OVR (Oculus Virtual Reality) Plugin (your headset), Interaction SDK uses the **From OVR ...Source** or **From Unity XR ...Source** components, where "..." is the source of data (for example, "From OVR Body Data Source" or "From OVR Hand Data Source"). Each of the **From ...Source** components gets a specific type of data and uses one of the three interfaces to organize it. The data from these components is what your interactions use to determine the location of your hands, controllers, or body. You don't need to manually add these components since they're already included in their relevant Interaction SDK prefab (for example, the **OVRHands** prefab contains the **From OVR Hand Data Source** component and the **UnityXRHands** prefab contains the **From Unity XR Hand Data Source** component).

The **From ...Source** components are:
- In Meta Interaction SDK package:
  - From OVR Body Data Source
  - From OVR Controller Data Source
  - From OVR Controller Hand Data Source
  - From OVR Hand Data Source
  - From OVR Hmd Data Source
- In Meta Interaction SDK Essentials package if Unity XR Hands is installed:
  - From Unity XR Hand Data Source
  - From Unity XR Controller Data Source
  - From Unity XR Hmd Data Source

<em>Raw input data from the headset going to each of the <b>From ...Source</b> components, which each use one of the three Interaction SDK interfaces.</em>

### Processing Hand Input Data

Once you have input data from a **From ...Source** component, it's usable by an [Interactor](/documentation/unity/unity-isdk-interactor/). However, hand data can be processed before it's [routed](#routing-input-data) (sent) to an Interactor. Other types of input data don't need to be processed. Processing hand data lets you:
- Minimize or remove jitter (shaking).
- Pose the virtual hand's fingers differently than your physical fingers, such as when poking a button or grabbing a virtual object.
- Offset the origin of a ray interactor based on the position of your virtual hand or controller instead of your physical hand or controller.

To process hand data, you pass it to certain components that use the **IHand** interface, like [HandFilter](#handfilter), and then you use that component as the data source for your Interactor instead of the **From ...Source** component. You do this regardless of whether you're using hands or controller driven hands.

Here's a recommended way to process hand data, taken from the [HandGrabExamples](/documentation/unity/unity-isdk-example-scenes/#handgrabexamples) scene. To use this process for interactions other than poke or grab, omit the **SyntheticHand** since only poke and grab need to modify the hand joint data in order for the hand visual to look right.

<em>Hand data being processed by multiple components before being rendered.</em>

Here's how those components process the hand data.
1. **FromOVRHandDataSource** converts data from **OVRHand** using **IHand** so Interaction SDK components can understand it.
   -  If **FromUnityXRHandDataSource** is used instead, it converts OpenXR data received from Unity XR Hands from OpenXR into Core SDK hand data.

1. [HandFilter](#handfilter) smoothes the input hand position data using the provided filter, reducing jitter.

1. [SyntheticHand](/documentation/unity/unity-isdk-input-processing/#synthetic-hand-modifier) overrides hand joint data to affect the hand's pose. It does that to prevent fingers from going through buttons during a poke and to make fingers conform to a pose when grabbing an object.

1. **HandVisual** renders the hand using the processed data.

   <box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
     <section>
       <embed-video width="100%">
          <video-source handle="GPKzUgJzOireehMDALRTCw6l35lGbosWAAAF" />
       </embed-video>
     </section>
     <text display="block" color="secondary">
         <b>Video</b>: View of a rendered hand poking a button and touchpad object.
     </text>
   </box>

<em>The difference between hand data not processed by <b>SyntheticHand</b> and data processed by <b>SyntheticHand</b>. During the poke, the hand that uses unprocessed data passes through the button, but the left hand that uses processed data visually limits the poke.</em>

### Routing Hand Input Data {#routing-input-data}

All hand and controller input data eventually goes to an Interactor. Because all hand data components, like the components in the section above, implement **IHand**, you use any of them to drive various aspects of your application. For example, in a ray interaction, you could use hand data from **HandFilter** to set the origin of the ray, and you could use **SyntheticHand** to set the hand visuals.

 {:width="550px"}

<em>Routing data to different parts of an application. In the top diagram, the <b>RayInteractor</b> receives hand data to decide where your physical hand is, but the <b>HandVisual</b> receives a filtered version of that data to smooth the movement of the virtual hand. In the bottom diagram, <b>SyntheticHand</b> receives two sets of data. The data directly from <b>Hand</b> tells it where your physical hand is, and the data from <b>HandGrabVisual</b> determines how the virtual hand should look when you grab something.</em>

## Key Components

### OVRInteraction

The **OVRInteraction** prefab is the base for attaching for hands, controllers, and controller driven hands.

By default, the **OVRInteraction** prefab contains just the following:

{:width="289px"}

For instructions on setting up hands, controllers, and controller driven hands, see [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

For instructions on setting up Unity XR data providers, see [Getting Started with Interaction SDK and Unity XR](/documentation/unity/unity-isdk-getting-started-unityxr/).

### Hand

The primary data component for hands in the Interaction SDK is called **Hand**.

**Hand** provides pose data, pinch states, pointer pose, and input availability as related to hands.

The **FromOVRHandDataSource** component can source this data from **OVRHand**. The **FromUnityXRHandDataSource** component converts OpenXR data received from Unity XR Hands from OpenXR into Core SDK hand data.

#### IHand

**IHand** is the primary interface through which Hand data is accessed. Components consuming hand data should prefer to do so through the **IHand** interface rather than the concrete **Hand**.

#### HandRef

**HandRef** is a passthrough component for **IHand** components. All interactor prefabs have **HandRef** components on their root GameObject that their child components can be wired to.

The primary advantage to doing this is to minimize the amount of scene wiring necessary to connect an interaction prefab. Instead of having to wire the **Hand** from the **OVRInput** prefab to each component that needs it in the interaction prefab, only one connection needs to be made to the top level **HandRef** of the prefab, and all child objects in the prefab will reference that **HandRef**.

### Controller

The primary data component for controllers in the Interaction SDK is called **Controller**.

**Controller** provides controller pose data, button states, and input availability as related to controllers.

The **FromOVRControllerDataSource** component can source this data from **OVRInput**. The **FromUnityXRControllerDataSource** component sources this data from the [Unity Input System's](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.8/manual/index.html) OpenXR support.

#### IController {#icontroller}

**IController** is the primary interface through which Controller data is accessed. Components consuming controller data should prefer to do so through the **IController** interface rather than the concrete **Controller**.

#### ControllerRef {#controllerref}

Similar to **HandRef**, **ControllerRef** is a passthrough component for **IController** components. All interactor prefabs that wish to reference controllers should have **ControllerRef** components on their root GameObject, which their child components can be wired to.

### Modifiers

Data travels from OVRPlugin up to input data types like **Controller** and **Hand** through a number of classes, all of which implement the **IDataSource** interface through a generic base class: **DataSource&lt;TDataType>**.

The most important field on the interface is the **IDataSource.InputDataAvailable** event, which is the way in which components pass updated tracking & pose data to dependencies.

**DataSource** derived classes are able to provide data of a given type (e.g. HandDataAsset).

**DataModifier** (which itself derives from DataSource) adds further functionality: it acts as a post-processor on a HandDataAsset. DataModifiers read data from a DataSource, apply changes, cache the results, then offer those results through the IDataSource interface.

#### LastKnownGoodHand {#last-known-good-data-modifier}

The **LastKnownGoodHand** only passes the last known good hand data down the modifier chain. If the data it’s fed becomes invalid for any reason (tracking lost, low tracking quality), it maintains the last valid hand data.

#### SyntheticHand {#synthetic-hand-modifier}

**SyntheticHand** can be used to manually modify the joint data of a hand, including the position and rotation of the fingers and wrist. It's used in the **HandGrab** scene to adjust how your fingers grab the mug. It's also used when pushing buttons to visually limit how far the finger can move--this is called poke limiting.

<oc-devui-note type="note"><b>SyntheticHand</b> should be updated after all interaction logic for the frame has completed, typically at the end of the frame.</oc-devui-note>

**SyntheticHand** will gracefully lock and unlock joints by either tweening them into the desired pose or constraining the maximum rotation and spread allowed. Tweaking the provided curves and speeds can allow more/less snappiness.

You can override joints by directly calling into a **SyntheticHand**. In other contexts, there are components that may look to drive joint locking and unlocking:

* For [Poke Touch Limiting with Hands](/documentation/unity/unity-isdk-poke-interaction/#poke-touch-limiting-with-hands), a **HandPokeLimiterVisual** will drive this modifier.
* For [Hand Grab Posing](/documentation/unity/unity-isdk-hand-grab-interaction/#hand-grab-posing), a **HandGrabInteractorVisual** will drive this modifier.

Because multiple InteractorVisuals may want to lock or unlock joints at the same time, an [InteractorGroup](/documentation/unity/unity-isdk-interactor-group/) can ensure that only one InteractorVisual affects a **SyntheticHand** at a time.

#### HandFilter {#handfilter}

**HandFilter** employs a One Euro Filter to smooth both the positional and rotational data of an input stream. The One Euro Filter is a speed-based filter that helps eliminate jitter without increasing lag. The field **Filter Parameters** is optionally set using a **HandFilterParameterBlock** asset containing the following values for wrist position, wrist rotation, and finger rotation:

* Beta (0 indicates maximum lag, while 10 is minimal lag).
* Min cutoff (0 indicates maximum filtered jitter at the expense of more lag, while 10 is minimally filtered jitter).
* D cutoff represents the derivative cutoff and can be used to further tune the effect.

Additionally, **HandFilterParameterBlock** contains a frequency value, measured in frames per second, which is passed to the One Euro Filter. If no value is set for **Filter Parameters**, the filter will not be employed.

### Body {#body}

The primary data component for body in the Interaction SDK is called **Body**.

**Body** provides pose data, input availability, and the joint mapping as relates to the body skeleton. The **FromOVRBodyDataSource** component can source this data from **OVRBody** in the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/), which is available individually or as part of the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/).

If using only Unity XR data sources, body data is unavailable.

#### IBody

**IBody** is the primary interface through which Body data is accessed. Components consuming body data should prefer to do so through the **IBody** interface rather than the concrete **Body**.

#### ISkeletonMapping

Different body skeletons can contain different joint sets and parent/child relationships, and the **ISkeletonMapping** represents these parameters.

When using local pose data for joints, the parent of the joint must be known in order for the data to be useful. The ISkeletonMapping.**TryGetParentJointId()** method should be used to find the parent of a provided joint.

Because **BodyJointId** contains a large set of joints that may not be available in the current skeleton data, the ISkeletonMapping.**Joints** HashSet should be checked for the presence of a given joint before use.

**BodySkeletonMapping** is a ScriptableObject containing an **ISkeletonMapping**, that itself exposes the **ISkeletonMapping** interface.

## Learn more

- For a complete overview of Interaction SDK architecture, see [Architecture Overview](/documentation/unity/unity-isdk-architectural-overview/).
- To learn about Interactors, which are attached to your hands or controllers to initiate interactions, see [Interactors](/documentation/unity/unity-isdk-interactor/).
- To learn about Interactables, which are attached to objects that should respond to Interactors, see [Interactables](/documentation/unity/unity-isdk-interactable/).
- To learn about how Interactors are prioritized when there's more than one hovering at a time, see [InteractorGroup](/documentation/unity/unity-isdk-interactor-group/)
- To learn about the components of body pose detection, see [Body Pose Detection](/documentation/unity/unity-isdk-body-pose-detection/).