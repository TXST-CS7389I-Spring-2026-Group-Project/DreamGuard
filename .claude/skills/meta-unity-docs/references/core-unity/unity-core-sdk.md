# Unity Core Sdk

**Documentation Index:** Learn about unity core sdk in this documentation.

---

---
title: "Meta XR Core SDK Overview"
description: "The Meta XR Core SDK provides foundational components for building Meta Quest apps in Unity."
last_updated: "2025-12-12"
---

This topic provides an overview of the Meta XR Core SDK for Unity.

## What is the Meta XR Core SDK?

The Meta XR Core SDK for Unity is a UPM package that includes all of the fundamental tools and assets needed to start building XR apps for Meta Quest headsets in the Unity Editor.

The Core SDK includes the following key features:

- [The Meta XR Rig](/documentation/unity/unity-ovrcamerarig)
- [The Project Setup Tool](/documentation/unity/unity-upst-overview)
- [Building Blocks](/documentation/unity/bb-overview)
- [Immersive Debugger](/documentation/unity/immersivedebugger-overview)
- [Asset Library](/documentation/unity/unity-asset-library-overview)
- [The OVRInput user input management API](/documentation/unity/unity-ovrinput)
- Support for Mixed Reality features like [passthrough](/documentation/unity/unity-passthrough/), [depth](/documentation/unity/unity-depthapi-overview/), and [spatial anchors](/documentation/unity/unity-spatial-anchors-overview/)
- Support for [trackable objects](/documentation/unity/unity-core-trackables/)
- Support for [compositor layers](/documentation/unity/os-compositor-layers/)

The Core SDK is the foundational SDK on which all other Meta XR SDKs and feature packages depend. Most Meta XR SDKs provide helpful abstractions built on top of lower-level Core SDK APIs. For example, the [Mixed Reality Utility Kit](/documentation/unity/unity-mr-utility-kit-overview) provides a number of prepackaged scene queries, graphical helpers, and development tools built on the lower-level Core SDK Scene API.

## How do I set up the Meta XR Core SDK?

To download and import the Meta XR Core SDK:

1. Go to the [Unity Asset Store](https://assetstore.unity.com/publishers/25353), and sign in using your Unity credentials.
2. Navigate to the [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169) page.
3. Select **Add to My Assets** to add the package to your Unity account's assets.
4. Select **Open in Unity** to open the **Package Manager** window in the Unity Editor.
5. Enter Unity credentials if prompted.
6. In the **Package Manager** window, in the upper-right side of the window, select **Install** to install the Core SDK.

## Learn more

The Meta XR Core SDK spans a number of feature sets and use cases. As a result, most of the documentation for Core SDK APIs is spread out across the Developer Center documentation site.

For reference documentation specific to the Core SDK UPM package, see the following:
- [Core SDK UPM samples](/documentation/unity/unity-core-sdk-samples)
- [Core SDK API reference](/reference/unity/latest)