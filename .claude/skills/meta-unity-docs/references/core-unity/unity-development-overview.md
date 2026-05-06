# Unity Development Overview

**Documentation Index:** Learn about unity development overview in this documentation.

---

---
title: "Develop Unity apps for Meta Quest VR headsets"
description: "Create a VR scene in Unity with Meta XR packages and deploy it to a Meta Quest headset."
last_updated: "2025-09-11"
---

This page introduces components you might use when developing Unity apps for Meta Quest VR headsets.
Meta Quest VR headsets run an Android-based operating system called Horizon OS, which lets you access their features for immersive VR experiences.

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

To enhance your app's user experience and broaden its reach, check out the [Design guidelines](#design-guidelines) section.

## Developing for Horizon OS in Unity

Meta provides several useful Unity packages to help you develop VR apps for Meta Quest headsets. The [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/), for example, includes a custom extended reality (XR) rig and support for fundamental XR features. Other specialized Meta XR SDKs enable you to integrate different types of user input into your Unity project.

### The Meta XR rig

In a standard 3D scene, a single virtual camera captures the ["Game View"](https://docs.unity3d.com/Manual/GameView.html). This contrasts with XR scenes, which use a rig that consists of multiple individual virtual cameras and scripts that map headset movements to the view that is represented to the end user.

In Unity XR development, the XR rig (also called the ["XR Origin"](https://docs.unity3d.com/Manual/xr-origin.html) in Unity documentation) serves as the center of tracking space in an XR scene. The XR rig contains the [GameObjects](https://docs.unity3d.com/Manual/GameObjects.html) that map controller and headset tracking data from your device to the scene in your app. This tracking data is used to move the scene camera, and it also factors into controlling interactions and animating controller and hand poses.

The Meta XR Core SDK includes the **OVRCameraRig** prefab, which contains a custom XR rig that replaces Unity's conventional **Main Camera**. Other Meta XR SDKs for Unity include similar XR rig prefabs, with some slight differences. For example, the Interaction SDK includes the **OVRCameraRigInteraction**, which extends the Meta XR Core SDK **OVRCameraRig** with controller and hand tracking.

For more information about the Meta XR rig and prefabs, see [Configure Meta XR Camera Settings](/documentation/unity/unity-ovrcamerarig).

### Handling user input

Apps developed with Meta XR SDKs can access and handle input from a user's head, hands, face, and voice using Meta Quest headset and touch controller technology.

#### Interactions

Users can interact with XR applications in a number of immersive ways. In the environment, they can move their head to discover different areas of the scene,
reach out with their hands to grab objects, and use the buttons on their controllers to perform complex operations such as locomotion movement.

*Interactions* dictate how a user's hand movements and controller actions affect the objects and environment around them.

Use OVRInput to access controller buttons, triggers, and tracking through low-level APIs. See [Controller Input and Tracking Overview](/documentation/unity/unity-ovrinput) for more information about using the OVRInput API.
For handling more complex interactions and customization, check out the [Meta XR Interaction SDK](/documentation/unity/unity-isdk-interaction-sdk-overview).
If you need compatibility with non-Meta platforms, use the [Unity Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.14/manual/QuickStartGuide.html).

#### Body and face tracking

Meta Quest headsets track the complex movements of a user's body and face. Using the [Meta XR Movement SDK](/documentation/unity/move-overview/), you can leverage the tracking capabilities of the Meta Quest hardware to create immersive and responsive experiences for users.

#### Keyboard input

Your app might require users to enter text. Horizon OS apps for Meta Quest can process text from a virtual keyboard using APIs included with the Meta XR Core SDK. Alternatively, you can use a physical keyboard with tracking provided via the Mixed Reality Utility Kit (MRUK). See the following links to learn more about virtual keyboards and tracking physical ones:

- [Virtual Keyboard Overview](/documentation/unity/VK-unity-overview): add virtual keyboard support to your app
- [Unity Trackables](/documentation/unity/unity-core-trackables): track physical objects including keyboards
- [Unity-TrackedKeyboard](https://github.com/oculus-samples/Unity-TrackedKeyboard): tracked keyboard text input example project on GitHub

#### Voice input

In addition to using physical movements and controller input, you can develop apps that enable users to interact with their environment with their voice.

Use the [Meta XR Voice SDK](/documentation/unity/voice-sdk-overview/) to enhance the XR experience with more natural and flexible ways for people to interact with the app. For example, voice commands can shortcut controller actions with a single phrase, and interactive conversation can make the app more engaging.

### Integrating mixed reality features

The [Mixed Reality Utility Kit (MRUK)](/documentation/unity/unity-mr-utility-kit-overview) includes a set of utilities and tools to perform common operations when building spatially-aware apps for Meta Quest. MRUK makes it easier to program against the physical world and use other mixed reality features provided by Meta.

## Exploring new features

To explore new features, use the in-editor [Building Blocks](/documentation/unity/bb-overview) tool that ships with Meta XR Core SDK.

You can add Building Blocks to your scene by dragging and dropping prefabs shipped with Meta XR SDKs into your scene.
Check out the [Core SDK samples](/documentation/unity/unity-core-sdk-samples) for sample projects or learn more about individual SDKs from [Meta XR SDKs for Unity](/documentation/unity/unity-sdks-overview).

## Testing and debugging using Meta's tools

Testing and debugging your app is a critical step in the XR development workflow. Meta provides several useful tools and workflows as extensions of the Unity Editor and as separate applications.

### Previewing your scene

To preview your project scene during development, use the following tools:

- [Meta Horizon Link](/documentation/unity/unity-link/): enables you to stream your app to a headset that is connected to your development machine using a USB-C cable or over Wi-Fi.

  **Note:** Link is currently only supported on Windows. If you are developing on macOS, use the Meta XR Simulator to test your projects during development.

- [Meta XR Simulator](/documentation/unity/xrsim-getting-started): simulates the extended reality environment of a headset on your development machine.
  XR Simulator is available for both Windows and macOS development. It allows you to preview your project scene on your computer without a VR headset.

### Debugging builds

To debug your app from a headset, you can use Meta's [Immersive Debugger](/documentation/unity/immersivedebugger-overview/).
When previewing your project using Link or Meta XR Simulator, use the Unity-supported [Android Debug Bridge for Meta Quest](/documentation/unity/ts-adb/).

## Managing devices

To manage your test devices during development, use Meta Quest Developer Hub (MQDH).

MQDH enables you to do the following:

- View device logs and generate [Perfetto](/documentation/unity/ts-perfettoguide/) traces to help with debugging
- Capture screenshots and record videos of what you see in the headset
- Deploy apps directly to your headset from your computer
- Share your VR experience by casting the headset display to the computer
- Download the latest Meta Quest tools and SDKs
- Upload apps to the [Meta Horizon Developer Dashboard](/manage/) for store distribution
- Disable the proximity sensor and the boundary system for an uninterrupted testing workflow

To get started with MQDH, see [Get Started with Meta Quest Developer Hub](/documentation/unity/ts-mqdh-getting-started).

## Learn more

To learn more about creating XR scenes for Meta Quest in Unity, see the following resources:

- [Configure Meta XR Camera Settings](/documentation/unity/unity-ovrcamerarig/)
- [Build Configuration Overview](/documentation/unity/unity-build/)
- [Official Unity Documentation](https://docs.unity3d.com/Manual)

### Design guidelines

Design guidelines are Meta's human interface standards that assist developers in creating user experiences. Refer to the following resources to begin, and explore additional design guidelines in subsequent Unity documents.

* [What is an immersive experience?](/design/mr-overview/)
* [Design tips and tricks](/design/mr-design-guideline/)
* [Accessibility](/design/accessibility/)
* [User comfort](/design/comfort/)

## Next steps

To learn how to quickly set up a Unity project for Meta Quest headset development, see [Set up Unity](/documentation/unity/unity-project-setup/).