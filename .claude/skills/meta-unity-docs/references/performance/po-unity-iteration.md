# Po Unity Iteration

**Documentation Index:** Learn about po unity iteration in this documentation.

---

---
title: "Unity Iteration Speed Best Practices"
description: "Offers guidance on improving iteration time when developing Unity apps."
last_updated: "2025-07-16"
---

One of the core time sinks of iteration speed is the time it takes to build and deploy changes to the Quest headset for testing. To reduce the time spent doing this, we have highlighted some best practices to apply. These changes might require some modifications to your project.

First, if you are modifying the way your project uses assets, we would recommend looking into the tools [OVR Build APK and OVR Quick Scene Preview](/documentation/unity/unity-build-android-tools/).

All the information on the test project and the data displayed in this document can be found in Appendix A of this guide.

## Keeping up to date with Unity version

In the latest tests, Unity has made significant progress in reducing build times between their versions year after year. It is recommended to look into getting the latest LTS build to get the benefits of faster build times and stability. For a project in early development, there is the possibility to get more improvements by using the latest released version, but it could be less stable than the LTS since it’s still in development.

- Upgrading from Unity 2020 to 2021 = 8%-16% reduction in iteration time

    Unity provides information on how to upgrade to a newer version in their [Upgrading Unity](https://docs.unity3d.com/Manual/UpgradeGuides.html) topic.

    

    

- See data in Appendix A

## Meta Horizon Link

Meta Horizon Link is a useful and fast way to iterate on a project. With a headset connected to your computer, you can start the scene in Unity and get the results directly in the headset as well as take in the inputs from the device. This is useful in cases where the changes are device-independent, such as logical code changes, asset positioning, and asset updates. Situations where it might not be beneficial include iterating on elements that are device-dependent, such as shaders and rendering, or logic specific to the device.

Learn how to [Set up and connect Meta Horizon Link and Air Link](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/connect-with-air-link/).

## Meta XR Simulator

The Meta XR Simulator simulates the Meta Quest headset and features directly on your development computer. It lets you iterate quickly on your project without the need to build or even put on a headset. For mixed reality, it provides synthetic environments that make it very easy to test different sizes and layouts of rooms. This enables you to test changes that are device agnostic, as well as verify app logic, repetitive movement/functionality, and mixed reality rooms.

For more information, see [Meta XR Simulator](/documentation/unity/xrsim-intro/).

## Script only patch

Unity has introduced an option to patch the generated APK when developers make code-only changes. This significantly reduces the time required to build the changes. You can find the details on how to build the patch in the [Unity manual](https://docs.unity3d.com/Manual/android-AppPatching.html).

The following are some specific items to consider:

- You will need to make a full build to start. Subsequent builds will be able to use the patch.
- When using IL2CPP, make sure that the **Strip Engine Code** option in **Player Settings** is disabled. The **Strip Engine Code** option is found by navigating to **Edit** > **Project Settings** > **Player**. Under the **Android** section, in **Other Settings**, navigate to the **Optimization** heading and locate the **Strip Engine Code** checkbox.

    - This should be only for iteration purposes. You can decide how to handle this for production.

  For a project that includes the assets in the APK, you can improve build times by 45% since it builds only code that has changed.

## Addressables

The addressables system is directed towards separating the assets building process from the code building process. This helps in reducing the time spent building by only building what has changed.

The addressables system is built on top of the asset bundles that Unity uses, making asset management easier. This system also makes it easy to group assets together and ensure that dependencies are included in the bundles.

It is also possible to set up the addressables to load assets remotely so you can set up a local asset server on your development machine and serve the assets to the device. This means that when assets are modified, you only need to build the addressables and restart the app on the device. No deployment to the device is required.

Here is a comparison of a Unity LTS 2021.1.15f1 base project versus an addressables project:

**Note**: See Appendix A for details on projects

By dividing assets from code builds, the build of the APK is more than 50% faster. When modifying the assets only, without the need to rebuild the APK, it was calculated to be about 75% faster. Additionally, in this scenario, consider that assets-only changes are updating the addressable bundles that are loaded remotely using a local hosting solution. Therefore, no time is spent on pushing the bundles to the device.

In this case, the assets have been separated into four groups based on the shared materials, prefabs, and meshes they use. This way the groups to rebuild are smaller and take less time to rebuild. There are obviously tradeoffs between the number of groups to load and the size of the groups and that will depend on each project.

Another important aspect is to ensure that dependencies of the assets included in the groups are not duplicated in multiple groups. For example, consider scene 1 and scene 2, which depend on material X; scene 1 is in group 1 and scene 2 in group 2. If material X is not explicitly included in a group, then it will be included in both groups. In this case, when updating material X both group 1 and group 2 would need to be updated. This would also cause potential issues at runtime since we would have two instances of material X, which could increase memory usage and prevent batching. Material X would be better placed in a shared group. This can be verified using the [addressable analyze](https://docs.unity3d.com/Packages/com.unity.addressables@1.1/manual/AddressableAssetsAnalyze.html) tool.

### Edge cases

When modifying a shader, it can be costly to build time when that shader is used in multiple scenes. At this time, the addressables system will load and unload each scene that depends on the shader to check the variants that are required. In projects with over 1000 scenes this can give very high build times.

In the project, bundles were changed to contain the materials in these scenes explicitly, meaning material was added to the group based on the scene that used them. By doing so, the addressables system doesn’t need to open each scene and stops when it looks at the material.

The addressables settings must be changed to use non-recursive dependency calculations. This can be set in the AddressableAssetSettings file or it can be found at **Window** > **Asset Management** > **Addressables** > **Settings**. Addressables package needs to be installed first to be visible in **Window** of Unity Editor. Navigate to **Window** > **Package Management** > **Package Manager**. Select **Unity Registry**. Locate **Addressables** in the **Packages** section and proceed to **Install**. See [Addressables package](https://docs.unity3d.com/Packages/com.unity.addressables@2.3/manual/index.html) for more information.

Here is some data that reflects this discovery:

### How to set it up

The [addressables manual](https://docs.unity.cn/Packages/com.unity.addressables@1.18/manual/AddressableAssetsGettingStarted.html) from Unity is a great source of information to understand how to set up addressables. This section adds some additional information that can help with the setup and how to use it for faster iteration.

Once you have the addressables group set up, you might end up with some local groups and remote groups for the final product. For the sake of iteration, you would benefit from having them all remote while you iterate. To do so, you can use [addressables profiles](https://docs.unity.cn/Packages/com.unity.addressables@1.18/manual/AddressableAssetsProfiles.html). You can create a new profile where the local groups would also be built to the same location as the remote ones, and the load path would be the same as the remote load path.

Setup steps include the following:

1. Set up the addressables group.

    - Set local and remote groups.

2. Set the [catalog to be remote](https://docs.unity.cn/Packages/com.unity.addressables@1.18/manual/AddressableAssetSettings.html#content-update).

    - Set the catalog version so it’s not using the timestamp, because once the build is made, it will always load the same catalog file name. The file name is stored in the APK. See the [Catalog Settings](https://docs.unity.cn/Packages/com.unity.addressables@1.18/manual/AddressableAssetSettings.html#catalog) section of the Unity documentation for more information.

3. Create a profile for local hosting / developer iteration.

    - Set local groups to have the same build and load path as the remote group.

4. Build addressables.
5. Build client and deploy.
6. For asset / scene / shader changes that are in addressables, you only need to rebuild the addressables.

    - Restart the app on-device to get the latest changes.
    - You can also set up some reload functionality in the app. This requires a bit more logic to be implemented in the app. See the [Checking for content updates at runtime](https://docs.unity3d.com/Packages/com.unity.addressables@1.19/manual/ContentUpdateWorkflow.html#checking-for-content-updates-at-runtime) section of the Unity documentation for more information.

### Using Unity local hosting

There are multiple solutions to set up your own local hosting, but Unity provides a simple solution, [Hosting Services](https://docs.unity.cn/Packages/com.unity.addressables@1.18/manual/AddressableAssetsHostingServices.html). This is already included with the addressables package that makes local testing faster and easier.

This is very useful to iterate on assets without having to set up our own local hosting. Setup steps include the following:

1. Open the Local hosting window.
2. Set up the LocalDevIteration Profile to use the local hosting variables for the load path (`http://[PrivateIpAddress]:[HostingServicePort]`).

    - In most cases, use PrivateIpAddress_1 since it is using a 192.168.X.X network IP which will be reachable from your Quest and seems to be more reliable. (Check firewall for 100 and 172 IP.)
    - In most corporate environments your firewall will most likely block any port if it’s not set specifically to allow communication. You can either pick a port that is open and set it in the window or set your firewall to open the desired port.
    - Make sure you are on the same network; no VPN.

3. Build addressables.
4. Start the local hosting.

    - Warning: If you start the Unity scene while the local hosting is running it will stop it and the port will be still marked as used. You will need to either pick a new port or close that port usage (sign out from Windows user works).

6. Run the build on device. Make sure it is on the same network.

## Fix long addressables build time

If you are experiencing long addressables build times, increase the scriptable build pipeline’s maximum cache size.

- Go to **Edit** > **Preferences** > **Scriptable Build Pipeline** and slide the bar to increase the cache size. If you don't see **Scriptable Build Pipeline**, install it by following **Window** > **Package Management** > **Package Manager** > **Packages: Unity Registry** and locate it in the **Packages** section. This package is also automatically installed as part of Addressables installation. See [Unity Scriptable Build Pipeline](https://docs.unity3d.com/Packages/com.unity.scriptablebuildpipeline@2.0/manual/index.html) for more information.

For example, say you have a Unity project with the following stats:

- Unity Version: 2022.3.2f1
- Assets folder size: ~63 GB
- APK Size: 105,419 KB
- Obb Size: ~2 GB
- Distributed Assets: ~9.5 GB

    - 49 Bundles: 7.2 GB
    - 10 mp4 files: 524 MB
    - 2 audio .obb files: 1.72 GB

- 76 Addressables Groups

When using the 20GB default maximum cache size, the project’s addressables can take ~3.5 hours to build.  With the **Log Cache Miss** enabled, there are a significant number of cache misses and the cache becomes full.

For this example, 40GB of cache is actually needed, so by adjusting the cache size to 45GB the build time is now reduced to **~10 minutes** for iterations with a small number of content changes.

## OVR Quick Scene Preview (QSP)

If converting your current project to use addressable is a lot of work and you want a quicker solution while converting your project, the tool [OVR Quick Scene Preview](/documentation/unity/unity-build-android-tools/#ovr-quick-scene-preview) will divide the project into bundles that will be pushed to the device. Here are some data points on how it can be beneficial using Unity LTS 2020.3.14f1:

Observe that there is an improvement of over 53% in all areas except for the clean build. There is a higher time cost to make this initial build, but the subsequent builds are given a considerable time boost that is worth it.

In this case, as explained in Appendix A, there are a lot of scenes and the QSP system divides the bundles based on the scenes, therefore, it will create a lot of bundles that it must push to the device. In a scenario with less scenes, the time difference will be reduced. This initial build should also be a one-time setup; any updates of the code or assets would reflect the specific data related to the changes.

As an example, the same project was used but only took 187 scenes to build using QSP for the initial build.

It can be concluded that QSP can help with iteration time but will have a larger time cost on the first build. This is a good solution if we don’t want to modify our current project to use addressables. That being said, using addressables will give you a faster iteration speed for all the different changes we have tested.

## Domain reloads

In the editor, iterations can be slowed by domain reload. This occurs because of the process that needs to be done to reset the scripting states, reset all static fields and registered handlers. A more complex or larger code base makes the process take longer.

You can control settings based on your needs in the following way:

1. In the Unity editor, go to **Project Settings** >**Editor**.
2. Disable the play mode setting.

    

3. Reset the scripting state using the instructions at [Unity Domain Reloading](https://docs.unity3d.com/Manual/DomainReloading.html).

## Appendix A

The project used is the [Unity Asset Streaming Sample](https://github.com/oculus-samples/Unity-AssetStreaming). It consists of:

- Textures: 781 files - 9.35GB
- FBX/models: 1800 files - 403MB
- Scenes: 1052 scenes
- Materials: 293 files
- Shaders: 4 project specific

Scenes are loaded and unloaded. In addition to the main scene at runtime, each scene is a different part on the map and a different LOD level.

The project was also converted to use addressables to get the metrics shown below. The scene was split into 4 groups based on shared material/textures/prefabs and the materials used by these scenes in their respective groups were added. A group was also added for shared assets to avoid duplication of assets in multiple groups.

### Data

The data reflects tests for changing a specific aspect of the project and rebuilding the project to see the change on device.

Multiple tests were conducted as described here:

- Material: Change one material property
- Prefab: Modify a prefab used in a scene
- Scene: Move an object in a scene
- Shaders: Modify the properties in one shader code
- Script: Modify code
- Script Only: Make a first build, then modify code and make another build using the app patching mode. We calculate the time of the second build.
- NO-OP: A build after no changes have been made. This is not a standard use case but gives interesting data points.

#### Base project

Total time to build the APK of the base project without using addressables:

#### Addressables project

For the same project, the assets were separated in some addressables groups to serve them remotely. This helps the iteration time since you only need to rebuild either the assets or the scripts in most cases.

Addressables package version 1.19.11 was used to get this data: