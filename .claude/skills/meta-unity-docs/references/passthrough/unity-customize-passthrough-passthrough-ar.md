# Unity Customize Passthrough Passthrough Ar

**Documentation Index:** Learn about unity customize passthrough passthrough ar in this documentation.

---

---
title: "Passthrough AR"
description: "Render passthrough as a background layer so virtual objects appear on top of the real-world camera feed."
---

Passthrough AR is a common technique where you show passthrough in the background of your VR scene.

By default, a passthrough layer uses automatic environment reconstruction, which covers the entire screen. There are multiple means to combine passthrough and VR in interesting ways to create mixed reality experiences. You can:

* Use the layers' opacity values to create a full-screen blend between passthrough and VR.
* Use the application's alpha channel to let passthrough only show in selected regions.
* Use [surface-projected passthrough](/documentation/unity/unity-customize-passthrough-surface-projected-passthrough/) to specify the geometry onto which you project the passthrough images instead of an automatic environment depth reconstruction.

These techniques are not mutually exclusive. They can be combined to create more sophisticated visualizations. This section describes a number of typical usage scenarios, and how they are set up.

To show Passthrough AR, do the following:

1. Get your initial Unity project set up as detailed in [Get Started with Passthrough](/documentation/unity/unity-passthrough-gs/).
2. On the **Hierarchy** tab, select **OVRCameraRig**.
3. On the **Inspector** tab, find the **OVRPassthroughLayer** script.
4. Set **Placement** to **Underlay** in your **OVRPassthroughLayer** component.
5. Set the main camera's background to transparent:

   1. On the **Hierarchy** tab, select **OVRCameraRig** > **TrackingSpace** > **CenterEyeAnchor**.
   2. On the **Inspector** tab, expand the **Environment** section and set the **Background Type** to **Solid Color**.
   3. Change the **Background** color to black and the alpha value to 0.

Optional: Add multiple instances of [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/) to your scene, each with its own configuration. A maximum of three instances can be active at any given time.