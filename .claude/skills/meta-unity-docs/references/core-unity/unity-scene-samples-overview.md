# Unity Scene Samples Overview

**Documentation Index:** Learn about unity scene samples overview in this documentation.

---

---
title: "Unity Scene samples overview"
description: "Browse Scene samples and reference apps from the Meta XR Core SDK and Starter Samples for Unity."
last_updated: "2025-05-07"
---

In this page, you will learn about our Samples and Reference Apps for Scene.

To Learn more about Scene concepts, read [Scene Overview](/documentation/unity/unity-scene-overview/).

## Samples

Samples for Scene can be found within the Core SDK's [UPM package](/downloads/package/meta-xr-core-sdk/). Each sample focuses on a common use case, but tries to keep it to a minimum to demonstrate individual features.

- [Custom Scene Manager](/documentation/unity/unity-scene-sample-customscenemanager/):
  Use the OVRAnchor API to create your own Scene Manager. Part of the Core SDK samples.
- [Mixed Reality](/documentation/unity/unity-scene-sample-mr/):
  Use Scene, Passthrough and Boundary in a single sample. Part of the Core SDK samples.

For more Scene samples, please see [Mixed Reality Utility Kit](/documentation/unity/unity-mr-utility-kit-samples/).

### Prerequisites

Before exploring samples, complete the setup steps outlined in [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/) to create a project with the necessary dependencies, including the ability to run it on a Meta Quest headset. These samples build upon that project.

### Setup

The samples stored within the Meta XR Core SDK package are under the header "Mixed Reality Sample" and must be extracted from the Unity Package Manager window as shown in the [Import Samples for Meta XR SDKs page](/documentation/unity/unity-import-samples/#small-samples-within-the-sdk).

1. On the menu, go to **Window** > **Package Management** > **Package Manager**.
1. Select **My Assets** and click **Meta XR Core SDK**. Then, click the **Samples** tab and import or update the Mixed Reality Samples.
1. In the Unity Editor, find Mixed Reality Samples under the Project tab. One way is to use the search field with scope set as **In Asset**.
1. Drag and drop the sample onto the Hierarchy panel. And Ensure that you have set the permissions correctly for [accessing Spatial Data](/documentation/unity/unity-spatial-data-perm/):
   In **OVRCameraRig** > **OVRManager** > **Quest Features**, make sure to set **Scene Support** to **Required**. This will add the correct manifest tags.
1. Further down in the **OVRManager** > **Permission Requests On Startup**, make sure to check the **Scene** checkbox. This will perform a runtime permission request when the app begins.
1. If you have previously created an Android Manifest, update it by going to the menu **Meta** > **Tools** > **Update AndroidManifest.xml**.

All samples require a **Scene Model**, which means you need to have run **Space Setup** *on-device*. The more robust your **Space Setup** process was, the richer your experience will be when you run the samples. You can create and clear your **Scene Model** any time in the Meta Quest headset by going to **Settings** > **Environment Setup** > **Space Setup** > **Set Up**.  Note that if you build and run one of the samples *on-device* before you have captured a **Scene Model**, the sample will trigger the **Space Setup** process to set up your room.

## Reference Apps
Reference Apps are applications which combine multiple APIs together to create a compelling short experience. These Reference Apps should serve as more advanced tutorials that show how to extend the basic samples into more realistic use cases. They are distributed via individual GitHub repositories under the larger [Oculus Samples](https://github.com/orgs/oculus-samples/repositories?type=all) repository.

### Phanto
Phanto showcases the best practices in using **Scene Model** and **Scene Mesh** for an action packed game. Find out more on the [Phanto GitHub repository](https://github.com/oculus-samples/Unity-Phanto).

### Discover
Discover is a Mixed Reality (MR) project that demonstrates how to use key MR features and quickly integrate them in your own project. Here you can see how to use Scene API, Interaction SDK, Passthrough, Spatial Anchors and Shared Spatial Anchors. [Discover](https://www.meta.com/experiences/discover/7041851792509764/) is available on the Meta Horizon Store and [GitHub](https://github.com/oculus-samples/Unity-Discover).

### The World Beyond
The World Beyond is a room-scale Mixed Reality game using Scene capabilities together with passthrough, voice, and hand tracking capabilities. This app turns your room into a forest and lets you play with a delightful creature named Oppy, who interacts with you and your space in new ways. [The World Beyond](https://www.meta.com/experiences/the-world-beyond/4873390506111025/) is available on the Meta Horizon Store and [GitHub](https://github.com/oculus-samples/Unity-TheWorldBeyond).