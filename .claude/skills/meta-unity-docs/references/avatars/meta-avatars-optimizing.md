# Meta Avatars Optimizing

**Documentation Index:** Learn about meta avatars optimizing in this documentation.

---

---
title: "Optimize Avatars using Meta Avatars SDK"
description: "A guide on optimizing Avatars for the Meta Avatars SDK."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

This page provides information on ways to optimize the use of Meta Avatars in your app.

## Meta Avatar SDK GPU Skinning

Use the Meta Avatar SDK GPU skinning solution to save CPU resources instead of Unity's. Enable this by setting the Default Skinning Type to OVR_UNITY_GPU_FULL.

## Run Body Solver Off Main Thread

Run the Body Solver system asynchronously, not on the main thread. This reduces CPU usage, with an average cost of one additional frame of solver latency. This serialized property will appear on any class that inherits from `OvrAvatarInputManager`.

For more information, go to [OvrAvatarEntity](/documentation/unity/meta-avatars-ovravatarentity/).

## OvrAvatarManager Settings

There are several settings on the `OvrAvatarManager` component that can be adjusted based on the performance needs of your App, such as network throttling and asset preloading. For more information, go to [OvrAvatarEntity](/documentation/unity/meta-avatars-manager-prefab/).

## Lip Sync Mode

When enabling lip sync for Avatars, choosing between Original and Enhanced modes provides a tradeoff between quality and CPU savings.