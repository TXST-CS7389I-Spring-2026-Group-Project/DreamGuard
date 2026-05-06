# Unity Colocation Tips Tricks Faq

**Documentation Index:** Learn about unity colocation tips tricks faq in this documentation.

---

---
title: "Colocation Tips, Tricks, and FAQs"
description: "Troubleshoot common colocation issues and find answers to frequently asked questions about permissions and setup."
last_updated: "2025-11-05"
---

## Enable specific settings and permissions

Certain permissions are required to have a colocated experience.

 - Enable Wi-Fi
     - Found in **Settings** > **Wi-Fi** to enable Wi-Fi
 - Enable Enhanced Spatial Services
     <!-- vale off -->
     - Found in **Settings** > **Privacy & safety** > **Device permissions** > **Enhanced spatial services**
     <!-- vale on -->
 - Enable Bluetooth
     - Found in **Settings** > **Bluetooth**

## Decrease the chance of false positives and failures

 - Quest device must be aware of the area
     - Look around the intended playspace for more accurate colocation
     - The easiest way would be to run **Space Setup**
         - Found in **Settings** > **Environment Setup** > **Space Setup**
 - The playspace must have some unique visual cues to determine the position of your Quest
     - Colocation uses a [Visual Positioning System (VPS)](https://www.meta.com/blog/visual-positioning-system-vps/?srsltid=AfmBOoqlYSzPlp4hsPd93W7lYQ7hIc_r6he8-Pn4hbjX8CTSrx3H_rjZ) which requires unique visual cues

## Best practices for developing colocation

 - [Upload an app to the app store](/resources/publish-upload-overview)
 - Accounts
     - Use two unique Meta accounts which can be [test users](/resources/test-users)
     - The Meta accounts should be added to [a non-production release channel](/resources/publish-release-channels-add-users/) such as ALPHA or BETA
 - As an individual developer testing colocation
     - Temporarily [turning off proximity sensor](/documentation/unity/ts-mqdh-basic-usage/#turn-off-the-proximity-sensor) on your devices will help make sure the Quest does not go to sleep when testing
 - Handling Drift
     - Colocation uses anchors as the common reference point, however the anchors start to drift if distance between the Quest and the anchor is 3 meters away

## FAQ

### Why is an avatar or user not aligning to the shared anchor

To align a user to a shared anchor, the user need to manipulate their own tracking space. This is done by moving the camera to the shared anchor. However, if there is anything in the scene preventing a user from moving their camera, then the user will not be able to align to the shared anchor. One such thing could be the **MRUK component** with **World Locking enabled**. If the user now tries to move their camera, the MRUK component will prevent the user from doing so. To solve this issue, either 1. disable world locking by clearing the checkbox, or 2. leave world locking enable (if needed) and move **MRUK.Instance.TrackingSpaceOffset** instead.

### What is the difference between colocating using Shared Spatial Anchors and Space Sharing

Colocating using Shared Spatial Anchors is generally used in apps where the virtual objects do not interact with the physical environment. Colocating using Space Sharing is generally used in apps where virtual objects
interact with the physical environment. For example, if an app is written where a virtual cube that needs to sit on a table or bounce off walls then it is recommended to use Space Sharing so that you and your guest can see the interactions.
If however, the virtual objects don't need to be aware of the physical environment at all (that is, the virtual objects can go through walls, tables, and so on) then using Shared Spatial Anchors is recommended.

### What is the technical difference between Shared Spatial Anchors and Space Sharing for colocation

Shared Spatial Anchors and Space Sharing are both different ways of colocating.
Colocating with Shared Spatial Anchors involves a Host creating and sharing a spatial anchor. Then a guest would load and localize the spatial anchor from the Host.
Colocating using Space Sharing involves using Scene Anchors, this would involve having a **Space Setup** and share that Space with a guest.

### What are the differences between using Shared Spatial Anchors and Space Sharing

If the app intends a colocated or local multiplayer experience that uses the scene (that is, walls, ceilings, tables, and so on) it is recommended to use Space Sharing. Otherwise it is recommended to use Shared Spatial Anchors.

### How do I know if I am using Shared Spatial Anchors or Space Sharing

The functions for using [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/) or [Space Sharing](/documentation/unity/unity-space-sharing/) are unique and can be found in the links

### Other best practice links

[Shared Spatial Anchors Best Practices](/documentation/unity/unity-spatial-anchors-best-practices/)