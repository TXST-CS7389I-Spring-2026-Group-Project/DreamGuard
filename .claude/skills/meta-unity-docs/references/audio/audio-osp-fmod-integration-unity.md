# Audio Osp Fmod Integration Unity

**Documentation Index:** Learn about audio osp fmod integration unity in this documentation.

---

---
title: "Install the Oculus Plugin for FMOD Studio Unity Integration"
description: "The FMOD Studio Unity Integration is a Unity plugin which allows the use of FMOD in Unity games."
---

<oc-devui-note type="warning" heading="End-of-Life Notice for Oculus Spatializer Plugin">
<p>The Oculus Spatializer Plugin has been replaced by the Meta XR Audio SDK and is now in end-of-life stage. It will not receive any further support beyond v47. We strongly discourage its use. Please navigate to the Meta XR Audio SDK documentation for your specific engine:

<br>- <a href="/documentation/unity/meta-xr-audio-sdk-unity-intro/">Meta XR Audio SDK for Unity Native</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unity</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unity</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-unreal-intro/">Meta XR Audio SDK for Unreal Native</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unreal</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unreal</a>
</p>

<p><strong>This documentation is no longer being updated and is subject for removal.</strong></p>
</oc-devui-note>

The FMOD Studio Unity Integration is a Unity plugin that allows the use of FMOD in Unity games.

## Compatibility

The Oculus Spatializer Plugin (OSP) for FMOD is compatible with the FMOD Studio Unity Integration for projects targeting Windows (32/64 bit), OS X, and Android. Two versions of the FMOD Studio Unity Integration are currently available: 2.0 and Legacy. The OSP for FMOD is compatible with:

- 2.0 (1.07.04 and higher)
- Legacy Version 1.07.03

## 2.0 Integration Installation

If you are migrating from the Legacy Integration, please follow FMOD’s migration guide here: [https://www.fmod.com/resources/documentation-api?page=content/generated/engine_new_unity/migration.html#/](https://www.fmod.com/resources/documentation-api?page=content/generated/engine_new_unity/migration.html#/)

Otherwise, take the following steps:

1. Follow the guide for setting up the 2.0 Integration here: [https://www.fmod.com/resources/documentation-api?page=content/generated/engine_new_unity/overview.html#/](https://www.fmod.com/resources/documentation-api?page=content/generated/engine_new_unity/overview.html#/)
1. [Follow instructions for using the OSP in FMOD Studio](/documentation/unity/audio-osp-fmod-usage/).
1. Open your project in the Unity Editor. Select *Assets* > *Import* > *Custom Package*, and select **OculusSpatializerFMODUnity.unitypackage** in `AudioSDK\Plugins\FMOD\Unity` to import into your project.
1. In the *Project* tab of *FMOD Settings*, click the *Add Plugin* button, and enter OculusSpatializerFMOD in the new text field.

You should now be able to load and play FMOD events that use the OSP in your Unity application runtime.

## Legacy Integration Installation

1. Follow instructions for setting up the Legacy Integration here: [https://www.fmod.com/resources/documentation-api?page=content/generated/engine_unity/getting_started.html](https://www.fmod.com/resources/documentation-api?page=content/generated/engine_unity/getting_started.html)
1. [Follow instructions for using the OSP in FMOD Studio](/documentation/unity/audio-osp-fmod-usage/)
1. Open your project in the Unity Editor. Then select **Assets** > **Import** > **Custom Package**, and select **OculusSpatializerFMODUnity.unitypackage** in `AudioSDK\Plugins\FMOD\Unity` to import it into your project.
1. In the Project view, select the `FMOD_Listener` script, which should be attached to an object at the root of the scene. In the Unity Inspector view, increment the size of Plugin Paths by one, and add ovrfmod in the new element.
1. **OS X platform only**: In `FMOD_Listener.cs`, in `LoadPlugins()`, modify the body of the foreach loop with the following code inside the OCULUS start/end tags:

    ```
    foreach (var name in pluginPaths)
     {
       // OCULUS start
       var path = pluginPath + "/";
       if(name.Equals("ovrfmod") && (Application.platform == RuntimePlatform.OSXEditor ||
           Application.platform == RuntimePlatform.OSXPlayer ||
           Application.platform == RuntimePlatform.OSXDashboardPlayer) )
       {
         path += (name + ".bundle");
         FMOD.Studio.UnityUtil.Log("Loading plugin: " + path);
       }
       else
       {
         path += GetPluginFileName(name);
         FMOD.Studio.UnityUtil.Log("Loading plugin: " + path);
         #if UNITY_5 && (UNITY_64 || UNITY_EDITOR_64)
         if (!System.IO.File.Exists(path))
         {
           path = pluginPath + "/" + GetPluginFileName(name + "64");
         }
         #endif
         #if !UNITY_METRO
         if (!System.IO.File.Exists(path))
         {
           FMOD.Studio.UnityUtil.LogWarning("plugin not found: " + path);
         }
         #endif
       }
       // OCULUS end

       uint handle;
       FMOD.RESULT res = sys.loadPlugin(path, out handle);
       ERRCHECK(res);
     }
    ```

Now you should be able to load and play FMOD events that use the OSP in your Unity application runtime.