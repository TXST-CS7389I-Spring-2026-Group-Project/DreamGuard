# Xrsim Body Tracking 1.0

**Documentation Index:** Learn about xrsim body tracking 1.0 in this documentation.

---

---
title: "Enable and Test Body Tracking Using the Meta XR Simulator"
description: "Validate body tracking features in the Meta XR Simulator without deploying to a physical headset."
last_updated: "2026-04-02"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

## Overview

Learn how to enable and test body tracking in the Meta XR Simulator to simplify app development, testing, and debugging.

<oc-devui-note type="important">
This feature works with all engines. The documentation below will guide you through enabling and testing body tracking for Unity.
</oc-devui-note>

## Prerequisites

1. [Install the Meta XR All-in-One SDK package](/downloads/package/meta-xr-sdk-all-in-one-upm/).

2. Set up the Meta XR Simulator with your development environment. For more information, see [Getting Started with Meta XR Simulator](/documentation/unity/xrsim-getting-started).

3. After installing the package, go to **Meta** > **Meta XR Simulator** and select **Activate**. If it is already selected, select **Deactivate** and then **Activate** again.

## Install the Meta Movement package from GitHub

1. In the Unity Editor, go to **Window** > **Package Management** > **Package Manager**.

   

2. In the Package Manager window, click the **+** button.

   

3. Select **Add package from git URL…**.

   

4. Enter *https://github.com/oculus-samples/Unity-Movement.git* and click **Add**.

   It may take a few minutes to install the package.

5. Ensure that Meta Movement is listed in **Packages** under **Other**.

## Set up a scene to test body tracking

1. Copy the Samples folder from the `\\Library\PackageCache\com.meta.movement@xxxxxx\Samples` directory into your `Assets` folder.

2. Open the Project Setup Tool by navigating to **Meta** > **Tools** > **Project Setup Tool** in the Unity menu bar.

   

3. Select **Fix All** so that all tracking is enabled.

   

4. Go to the copied Samples folder in `Assets` and access the subfolder `Scenes`.

5. Open the `MovementHighFidelty` scene.

6. Press Play to see body tracking run automatically for several seconds. The avatar's upper body should move.