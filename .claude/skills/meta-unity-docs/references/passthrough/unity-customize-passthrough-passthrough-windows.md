# Unity Customize Passthrough Passthrough Windows

**Documentation Index:** Learn about unity customize passthrough passthrough windows in this documentation.

---

---
title: "Passthrough Windows"
description: "Display the passthrough camera feed through a bounded window cut into your virtual scene."
last_updated: "2025-12-09"
---

If your virtual scene fills the entire screen, for example, because you are in a room or you want to use a skybox, you can create windows through which passthrough is visible by modifying the framebuffer’s alpha channel after everything else has been rendered. Writing an alpha value of zero to a certain region of the screen will allow passthrough to shine through the virtual scene completely in that location. Writing an alpha value between zero and one will create a blend between passthrough and VR. Originally, it was advised to configure the application’s framebuffer to use non-premultiplied alpha by setting `OVRManager.eyeFovPremultipliedAlphaModeEnabled = false;` to ensure that the framebuffer's color values were correctly multiplied with the alpha value during compositing. Nowadays, this is no longer necessary as the shader now uses multiplicative blending, which eliminates the need for runtime settings adjustments.

{:width="450px"}

1. Create a new unlit shader:
   1. Return float4(0, 0, 0, alpha) as color from the fragment shader, where alpha corresponds to the desired passthrough opacity at the fragment location.
   2. Adjust the blend mode to leave the RGB channels unaffected while using the alpha to achieve a multiplicative blending effect with the existing content in the framebuffer:

   ```cpp
   BlendOp Add // set blending operation to additive
   Blend Zero SrcAlpha // multiply both RGB and alpha channels of the target by the source alpha, to enable multiplicative blending
   ```

2. Create a new material using this shader and configure its render queue such that it renders after everything else, for example, 5000.
3. Assign the new material to an object to make it appear as if it was textured with passthrough.

**Note**: If depth testing is enabled when rendering the passthrough window objects, they will be properly occluded by virtual objects in front of them. The occlusion happens based on the depth of the passthrough window geometry, not based on the real-world depth of the content seen through it.