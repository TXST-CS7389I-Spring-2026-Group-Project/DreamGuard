# Immersivedebugger Overview

**Documentation Index:** Learn about immersivedebugger overview in this documentation.

---

---
title: "Immersive Debugger"
description: "How to install and use the in-headset Immersive Debugger tool for Unity."
last_updated: "2025-12-16"
---

## Overview

Immersive Debugger for Unity is a tool that lets you monitor, visualize, troubleshoot, and iterate your application's gameplay from your headset. It comes with:

- A UI panel for in-headset debugging

  The panel includes a [control bar](#control-bar), a [console panel](#console-panel), and an [Inspector panel](#inspector-panel) populated with your application-specific debug options.

- An optional Unity Editor framework that lets you customize the in-headset debugging experience

  The debugger can be customized with the scripting attribute `[DebugMember]` or Inspector component `DebugInspector`.

With Immersive Debugger, you can do the following without having to remove your headset:

- View Unity logs from the Console Log panel
- [Available from v74 SDK] View the scene hierarchy and the details of the selected game objects of the application in the Inspector panel
- Watch specified application variables in the Inspector panel
- Tweak specified float/int/boolean/Enum variables from the Inspector panel
- Invoke any Action for the specified function from the in-headset Inspector panel
- Visualize specified application data in 3D space via Gizmo drawings

These tools can speed up your development process by letting you:

- Iterate quickly within the headset for spatial and visual elements
- Debug a Mixed Reality experience that relies on features like Scene
- Monitor state changes (especially upon ephemeral events) and identify issues via console logs

## Quick start

### Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies.

### Enable the tool in Unity Editor

From the menu bar, select **Meta** > **Tools** > **Immersive Debugger**.

The Project Settings window opens to the Immersive Debugger tab. Select **Enable**.

### Add debug options from Unity Editor

**Note**: If you enable the Immersive Debugger while you add the [DebugMember] attributes, Immersive Debugger can incrementally cache the members to track upon compilation.

You can tailor your Immersive Debugger experience by adding your own debug options to the in-headset inspector panel.

You can add programming elements of your interest with either or both of the following:

- Scripting attributes

  Add a `[DebugMember]` attribute to any of your properties, fields, action functions and customize them through the parameters of the attribute.

  **Note:** If not done automatically by your IDE, this requires assembly dependency to [`Meta.XR.ImmersiveDebugger.Interface`](/reference/unity/latest/namespace_meta_x_r_immersive_debugger).

- Add a `DebugInspector` component to any game object and configure the debug options.

 

 

**Note**: Some attributes being added using `DebugInspector` might not be showing correctly in the in-headset panel, this is due to Unity runtime stripping off the properties/functions that it deemed not being used. To avoid this, use `[DebugMember]` scripting attributes to access them instead which will guarantee preserving the member.

### Use Immersive Debugger in the headset

This tool works with Link and can also be deployed within an apk build in headset. For the apk build, we used [OVROverlay compositor layer](/documentation/unity/unity-ovroverlay/) to make the text sharper.

<oc-devui-note type="note" heading="Development Build is not required when using v77 or later">
  Starting in v77, Immersive Debugger doesn't require Development Build to be used in headset anymore. If there's a requirement to enforce Immersive Debugger to only show in Development Build, go to Immersive Debugger Settings > Advanced > Integration > Enable Only In Debug Build.
</oc-devui-note>

(For versions before v77) Before deploying the build to the headset, go to **File** > **Build Profiles**. Select **Android** and then enable **Development Build**.

**Note:** To use the tool in a production build, see [Advanced use cases](#advanced-use-cases).

## Functionalities in detail

This section provides more detailed information on the elements of Immersive Debugger.

### In-headset panels

The in-headset UI includes a [control bar](#control-bar), a [console panel](#console-panel), and an [Inspector panel](#inspector-panel).

#### Control bar

When Immersive Debugger is enabled, you can see a control bar in the game world automatically (if Display at Startup is enabled in Project Settings) or when you use the selected Toggle Display Input Button.

The top left of the panel displays how long you've been running the app.

From left to right, the icons on the bottom let you:

- Make the panel transparent/opaque
- Anchor the panel to a fixed position or make it move with the headset
- Enable or disable rotating the panel with head yaw
- Change the panel distance

The buttons on the right toggle the Inspector panel and the Console panel.

#### Console panel

This panel shows all the console logs from Unity and allows you to toggle severity, clear logs, and collapse or expand identical logs.

Click a specific entry in the log to show a full stack trace panel and turn it off from the top right corner.

#### Inspector panel

This panel lets you:

- View scene hierarchy and inspect game objects
- Watch the variables' runtime value
- Click the button to call functions
- Select the eye icon to show/hide gizmos
- Tweak variables' runtime values

The left top icon lets you choose between **Custom Inspectors** mode or the **Hierarchy View** mode.

When **Hierarchy View** mode selected, you're able to load all your scenes from the application and clicking through the game objects within to inspect monobehaviour components and their members. By default, only the public members are shown, you can change this in the ImmersiveDebugger Settings by turning on **Advanced** > **Hierarchy View** > **Inspect Private Members**.

When **Custom Inspectors** mode selected, the left sidebar shows categories for your debug items. You can specify your item's category within the `DebugMember` attribute or Inspector panel. A selection of pre-configured Meta XR debugging items are also available out of the box, which you can turn off in Settings. See [Meta Feature Pre-build Debugging](#meta-feature-pre-build-debugging) for more information.

### Debug functionalities

Here are the debug functionalities and the support status for each of them:

 | Function | Support status |
 | -- | -- |
 | Watch | Supports everything that supports `ToString()`. Vectors have a special UI to show separate fields. Additionally `Texture` data type is supported with a preview rendered |
 | Action | Supports parameter-less functions only. |
 | Gizmos | Supports various DebugGizmoTypes that can be checked in `GizmoTypesRegistry` class. All gizmos except Axis can take additional `Color` parameters in `[DebugMember]` to draw with that color. |
 | Tweak | Supports tweaking float/int with a slider UI via `[DebugMember(Tweakable = true, Min = xxx, Max = xx)]`. supports tweaking boolean with a UI toggle, also supports tweaking Enum with a dropdown UI. |

### Scripting attributes example

When Immersive Debugger is enabled, our framework will automatically collect the symbols with annotations in the Editor upon compilation and pipe them to the in-headset panel at runtime. To not overwhelm runtime perf to find those symbols, we pre-bake the debug member classes in the project with a Scriptable Object located at `Assets/Resources/ImmersiveDebuggerSettings.asset`.

```csharp
using Meta.XR.ImmersiveDebugger;
public class ExampleClass : MonoBehaviour
{
 // gizmo drawing, specifying a category
 [DebugMember(GizmoType = DebugGizmoType.Axis, Category = "MyDebugCategory")]
 private Pose _gameObjectPos;

 [DebugMember(GizmoType = DebugGizmoType.Line, Color = DebugColor.Red)]
 private Tuple<Vector3,Vector3> _direction;

 // just watch, note Tweakable is by default true for all supported types
 [DebugMember(Tweakable = false)]
 private bool _gameState;

 // tweak the value within a range
 [DebugMember(Min = 0.0, Max = 1.0)]
 private float _param;

 // action / call function, static members are also supproted
 [DebugMember]
 private void SpawnObject() {/* some code here */}
 private void Update()
 {
  // Update and consume all the debug options, e.g.:
  _gameObjectPos = new Pose(transform.position, transform.rotation);
 }
}
```

## Meta Feature Pre-build Debugging

To make Immersive Debugger and Meta XR SDK easy to use, we have pre-built some debugging functionalities for some SDK features. These are:

- Camera Rig
- Passthrough
- Scene Debugger (Mixed Reality Utility Kit / MRUK)

These builtin debugging options are by default available to you under Meta provided categories, you can turn them off by going to Immersive Debugger Settings > **Inspected Data Sets**.

## Advanced use cases

This section provides instructions for more specific use cases.

### Use Immersive Debugger in a production build

(**NOTE**: this is no longer needed after v77) To use the tool in a production build rather than a development build, add `IMMERSIVE_DEBUGGER_ALLOW_USE_IN_PROD` as a Scripting Preprocessor Define in the project settings. The tool can then be used in non-development builds.

### Use your own camera instead of OVRCameraRig

We have provided a custom integration config to expose overrides for the camera used by Immersive Debugger. To provide a script, override the `CustomIntegrationConfigBase` (check out the `ExampleCustomIntegrationConfig`) and fill the mono script in the settings section with **Use Custom Integration Config** enabled. To find this setting, in the Project Settings page, click on **Meta XR** > **Immersive Debugger** > **Advanced** > **Integration** > **Use Custom Integration Config**.

### Add inspectors to components at runtime

(Available from v83 onward) The `RuntimeAPIs` class provides runtime methods for adding inspector panels for components to the Immersive Debugger. This allows you to debug components at runtime without using `DebugMember` attributes. This is useful for debugging components that are created dynamically or for adding inspectors to components that you don't have source access to.

### Add a custom panel to the Immersive Debugger

(Available from v83 onward) You can add a custom panel to the Immersive Debugger by implementing the `IPanelRegistrar` interface. You can use this interface to create custom panels that will register themselves with the Immersive Debugger at runtime.

## Known Issues

| Issue | Explanation |
| --- | --- |
| OVRManager dependency (OVROverlay) | Immersive Debugger makes use of `OVROverlay` compositor layer to make the text sharper, which relies on your project using `OVRManager`. |
| HDR (in camera and device graphics settings) not supported yet | There may be some visual glitches in the Immersive Debugger panels when using HDR. |