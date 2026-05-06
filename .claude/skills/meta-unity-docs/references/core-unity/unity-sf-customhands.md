# Unity Sf Customhands

**Documentation Index:** Learn about unity sf customhands in this documentation.

---

---
title: "CustomHands Sample Scene"
description: "Sample app depicting how to add custom hand support in Unity."
---

The Unity CustomHands sample scene demonstrates how to use custom hand models in an app that uses controller-based interactions.

In this scene, the user has a pair of custom hands that can be moved around and posed.

The goal of this topic is to help you use the prefabs and components that make custom hands work. You can also use this sample scene as a starting point for your own application.

Take note that this sample focuses on controller-based hand interactions and does not work with hand tracking.

<image alt="CustomHands sample scene showing a pair of custom hand models posed in a VR environment." handle="GAu3AQPQoShZnwIBAAAAAADgMON0bj0JAAAD" src="/images/customhandsbanner.png"/>

## Scene Walkthrough

This section briefly describes the prefabs that make the core functionality of this scene work.

* **CustomHandLeft and CustomHandRight prefabs** – Prefabs that contain the necessary components for custom hands. The prefabs also include `OVRGrabber` grabbing functionality.

### CustomHandLeft and CustomHandRight Prefabs

These prefabs are used to implement custom hands. Their components are as follows:

#### RigidBody

A standard Unity component that allows physics to be applied to an object. It is required to allow physical interaction with other objects in the scene. It’s included here to support `OVRGrabber`.

#### OVRGrabber

Enables the hands to grab and interact with objects that have been configured with an `OVRGrabbable` component.

#### Hands

This component implements the custom hands, specifies their controller assignment, and handles posing and animation. The following are its properties:

* **Controller** - Controller that the custom hand is attached to.
* **Animator** - The model and animation information for the custom hand.
* **Default Grab Pose** - Contains the default grab poses for the custom hands.

## Using in Your Own Apps

As shown in the sample, adding custom hands just requires you to add the `CustomHandLeft` and `CustomHandRight` prefabs. They come equipped with models and animators you can use and modify in your own applications.