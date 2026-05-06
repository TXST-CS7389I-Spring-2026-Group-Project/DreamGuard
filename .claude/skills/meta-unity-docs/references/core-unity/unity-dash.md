# Unity Dash

**Documentation Index:** Learn about unity dash in this documentation.

---

---
title: "Oculus Dash in Unity"
description: "Add Oculus Dash support to Rift Unity applications."
---

This guide describes how to add Oculus Dash support to Unity applications.

Rift Core 2.0 introduces substantial changes to Oculus Home and replaces the Universal Menu with Oculus Dash. This page describes how you can support Dash in your Unity app.

<image alt="Oculus Dash UI overlay rendered on top of a paused VR application in the Rift headset." style="width: 600px;" handle="GNloZQG3mTlbJbYAAAAAAABe6JIEbj0JAAAD" src="/images/documentationunitylatestconceptsunity-dash-1.png"/>

Dash re-implements Universal Menu as a VR compositor layer. Have a look at the “Introducing Oculus Dash” video in our [Welcome to Rift Core 2.0](https://www.meta.com/blog/quest/welcome-to-rift-core-beta-now-available/) blog post to get a sense of how it works.

Beginning with runtime 1.22, when users pause an application, instead of rendering the Universal Menu in an empty room, one of two things will happen:

* If your application supports Dash, the application will pause and the Dash menu UI will be drawn over your paused application.
* If your application does not support Dash, your application will be paused by the runtime and the user will be presented with the Dash menu UI in an empty room, similar to the way the Universal Menu was previously displayed.

When the Dash UI is active, the runtime will render tracked controllers in the scene to interact with the menu.

## Integrating Dash Support

With Dash we introduced the concept of input focus, or whether the user is focused on your app, or elsewhere. Adding Dash support to your app means correctly handling the times when your app is running, but the user's focus is elsewhere.

To check if your app has focus input, query OVRManager.hasInputFocus every frame. If your app has focus hasInputFocus will return true. If the user's focus is elsewhere, like when the user opens the Dash menu or removes their HMD, hasInputFocus will return false.

In single-player apps or experiences, you can pause the app, mute audio playback, and stop rendering any tracked controllers/hands present in the scene (Dash will use a separate set of hands).

Multiplayer experiences may wish to handle the loss of input focus differently. You're required to hide the hands and ignore any input while the app does not have focus input, but you may wish to continue audio playback and the match in the background.

For more information on OVRManager.hasInputFocus, see [Application Lifecycle Handling](/documentation/unity/unity-lifecycle/).

## Rendering Dash in Unity

To properly support Dash in your app, you must use Oculus OVRPlugin version 1.19, or later (see the Unity versions supported in the Getting Started Guide's [Install Unity Editor](/documentation/unity/unity-before-you-begin/#unity) for more information). We recommend using a Unity Editor version that includes built-in Dash support. These versions make it easy to configure the application for Dash support, and they automatically provide depth information that allows the Dash UI to be drawn over a scene without depth conflicts with scene content.

Dash support is provided in the following Unity versions:

* Unity 2017.3b11 or later
* Unity 2018.x

Add Dash support in the Unity Editor by selecting the following:

<image alt="Unity XR Settings panel with Share Depth Buffer and Dash Support checkboxes enabled." style="width: 600px;" handle="GHzsbgGMdmxbJbYAAAAAAAAlp-subj0JAAAD" src="/images/documentationunitylatestconceptsunity-dash-2.png"/>

Share Depth Buffer: Depth information helps avoid depth conflicts between the Dash UI rendered in the scene and objects in the scene, and enables compositor layer depth testing.

Dash Support: Check this box to configure your application to signal the Oculus runtime that the application is Dash-compatible. Do not check this box in builds intended for store submission until you have tested your application with Dash and verified correct functionality.

Dash support is enabled by default in all custom Unity builds with Dash support as well as Unity 2017.3b11 and later, and versions 2017.3f1-2. It is off by default in all other versions, and we plan to disable it by default in 2017.3f3 and later.

In addition to these checkbox settings, you may also enable Dash support by launching your application with the launch parameter -oculus-focus-aware true.

## Oculus Dash in the Unity Starter Samples

See the Input Focus sample in the [Unity Starter Samples](https://github.com/oculus-samples/Unity-StarterSamples) for an example of typical handling for the loss of input focus in an application.