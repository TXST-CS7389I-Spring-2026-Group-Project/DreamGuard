# Po Graphics Jobs

**Documentation Index:** Learn about po graphics jobs in this documentation.

---

---
title: "Graphics Jobs in Unity"
description: "Enable Graphics Jobs in Unity to offload rendering commands from the main thread and improve CPU performance on Quest."
last_updated: "2024-11-11"
---

Unity’s Graphics Jobs feature is an optimization that helps increase throughput on the main thread when used together with Multithreaded Rendering. Traditionally, the main thread generates rendering commands, which are then sent to the render thread and translated to the appropriate graphics command for the device (i.e., for Quest it would be Vulkan). When rendering work is heavy, the main thread spends much of its time generating these commands, causing slowdowns. Graphics Jobs shift this work off the main thread and onto worker threads so that the main thread can immediately start the next frame's work.

<oc-devui-note type="note" heading="Legacy mode can benefit performance">
  Using Graphics Jobs in Legacy mode can benefit performance up to 2 FPS, as tested in major projects.
  You can enable Legacy Graphics Jobs starting in <b>Unity 2022.3.35f1</b>.
  Try this mode for performance optimization if your application is main thread-bound.
</oc-devui-note>

## Enabling Legacy Graphics Jobs

Ensure that the Multithread Rendering option is enabled in the Unity Player Settings.
To check this setting, navigate to **Edit** > **Project Settings** > **Player** (Android tab) > **Other Settings** > **Multithreaded Rendering**.
Next, follow the instructions for your Unity version to enable Legacy Graphics Jobs.

### In Unity 6

To enable Legacy Graphics Jobs in Unity 6:

1. Select **Edit** > **Project Settings**.
2. In the window that appears, select **Player** from the left pane. Select the Android tab to modify Android settings.
3. Expand the **Other Settings** section.
4. Check the **Graphics Jobs (Experimental)** checkbox.
5. The **Graphics Jobs Mode** field will appear. Set the dropdown to “Legacy”.

### In Unity 2022 LTS

For Unity versions 2022.3.35f1 and newer, Graphics Jobs Mode cannot be set within the editor UI. Instead, you will have to enable the feature using *Editor Scripting*. Follow these instructions in the Unity Editor to get this working:

1. In the Project window, right-click the **Assets** folder and select **Create** > **Folder**. Name this new folder “Editor” – it’s important to name the folder this way because editor scripts rely on [special folder names](https://docs.unity3d.com/Manual/SpecialFolders.html) to execute.
2. Within the new **Editor** folder, right-click and select **Create** > **Scripting** > **Empty C# Script**. Name the script “EnableLegacyGraphicsJobs”.
3. Copy the following code sample into the script. This enables Graphics Jobs in Legacy mode when the project is built:

```cs
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnableLegacyGraphicsJobs : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;
    public void OnPreprocessBuild(BuildReport report)
    {
        PlayerSettings.graphicsJobs = true;
        PlayerSettings.graphicsJobMode = GraphicsJobMode.Legacy;
    }
}
```

### Debugging

To ensure that the Graphics Jobs feature has been enabled and is running in Legacy mode, add the following code to the `Start` method of any `MonoBehaviour` script:

```cs
void Start()
{
    RenderingThreadingMode mode = SystemInfo.renderingThreadingMode;
    Debug.Log("Graphics Jobs Mode " + mode);
}
```

When running in Play mode in the Unity Editor, `RenderingThreadMode` will always be set to `MultiThreaded`, so you will have to verify by connecting your Quest headset to your development machine and using [Android Debug Bridge (ADB)](/documentation/unity/ts-adb/). Once you are connected and running your build, open the command line and enter:

```console
adb shell logcat -s Unity
```

If you see your debug message show up in the printed output, you have successfully enabled Legacy Graphics Jobs. For help setting up ADB, see [Set Up Your Device](/documentation/unity/unity-env-device-setup). For more information on working with Logcat, see [Collect Logs with Logcat](/documentation/unity/ts-logcat).

## Learn More

For more on Graphics Jobs and Editor Scripting, refer to these Unity tutorials:

* [Multithreaded Rendering and Graphics Jobs](https://learn.unity.com/tutorial/optimizing-graphics-in-unity#64dc7690edbc2a26d993c1aa)
* [Introduction to Editor Scripting](https://learn.unity.com/tutorial/introduction-to-editor-scripting#)