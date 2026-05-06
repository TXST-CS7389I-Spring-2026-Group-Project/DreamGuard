# Unity Customize Passthrough Passthrough Occlusions

**Documentation Index:** Learn about unity customize passthrough passthrough occlusions in this documentation.

---

---
title: "Passthrough Occlusions in Unity"
description: "Resolve occlusions between real-world and virtual objects using depth-based rendering in your Unity passthrough app."
last_updated: "2024-09-11"
---

Sometimes you want your virtual content to blend seamlessly with the physical environment around your users. If you have a virtual character, you want it to block or occlude the real-world objects behind it, and you also want your character to be occluded by real-world objects in front of it. The Passthrough SDK doesn't provide this functionality out of the box, and its layers can be rendered either behind the app layer ([Passthrough AR](/documentation/unity/unity-customize-passthrough-passthrough-ar/) mode), or in front of the app layer (when the layer's **Placement** is set to **Overlay**).

The Meta Quest Platform provides several ways to achieve believable occlusions in your app.

## Generic occlusions with Depth API

Starting with Quest 3, the Depth API can provide real-time depth maps to your app. With this information, you can implement realistic dynamic occlusions so that real-world objects can obscure virtual objects in passthrough. Refer to the [Occlusions](/documentation/unity/unity-depthapi-occlusions/) feature of Depth API to learn more about it and how to incorporate it into your apps.

Depth API is a powerful tool, but comes with limitations. It might be too expensive for your app in terms of CPU or GPU usage, or you might not be satisfied with the latency of the real-time depth information. Incorporating it into a Unity project can be tricky when shader changes are needed. Finally, you might want to have occlusions working for older Meta Quest headsets, like Meta Quest 2.

## Occlusions for tracked real-world objects

If you have access to the geometry and the position of a real-world object, then you can occlude virtual content with it. For example, you can use tracked hands to occlude the virtual scene, such as a virtual light switch, and make hands appear as passthrough.

{:width="550px"}

Render the hands or an occluder with a material that only writes to the depth buffer, before other objects (render queue < 2000). Subsequent virtual objects or materials fail the depth test and will not render in the area of the occluder, giving the illusion of being behind the occluder.

{:width="450px"}

The layering between passthrough and VR can be made arbitrarily complex by alternating between rendering VR objects and passthrough occluders, and by utilizing the ZTest/ZWrite shader commands. For example, a virtual watch can be drawn on top of the passthrough hand/wrist by rendering the 3D model of a watch in an even lower render queue, which will occlude the rendering of the hand, or with ZTest disabled so that it ignores the hand as an occluder.

This approach for occlusion only works across the entire mesh, so it’s a fully opaque solution. For soft masking that allows translucency, the [SelectivePassthrough](/documentation/unity/unity-sf-passthrough/#selectivepassthrough) sample has a material and shader that can be used.

## More hand occlusions solutions

Specifically for hands, there are even more solutions aiming to improve the hands interaction in various MR environments. Those solutions use the Interaction SDK together with passthrough, allowing you to tune the hand representation to your specific type of interaction. Refer to [Hand Representation](/resources/hand-representation/#passthrough-hands) for more details.