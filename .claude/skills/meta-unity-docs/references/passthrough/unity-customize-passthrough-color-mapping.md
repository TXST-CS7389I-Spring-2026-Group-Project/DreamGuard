# Unity Customize Passthrough Color Mapping

**Documentation Index:** Learn about unity customize passthrough color mapping in this documentation.

---

---
title: "Color Mapping Techniques"
description: "Apply color tinting, gradient maps, posterization, and edge rendering effects to the passthrough camera feed."
---

The **Color Control** feature provides a number of different techniques to map each input color to a different output color. The following options are available:

- **None:** Display passthrough images unchanged.
- **Color Adjustment:** Adjust the image's brightness, contrast, and saturation. Saturation adjustment only has an effect on devices that support color passthrough.
- **Grayscale:** Adjust brightness and contrast and apply a posterization effect to grayscale passthrough images. Color passthrough images are converted to grayscale first if this option is chosen.
- **Grayscale To Color:** Colorize grayscale passthrough images, adjust brightness and contrast, and apply a posterization effect. Color passthrough images are converted to grayscale first if this option is chosen.
- **Color LUT:** Apply a color look-up table (LUT), which maps each RGB input color into an arbitrary RGB(A) in the passthrough image stream.
- **Blended Color LUTs:** Apply the blend between two color LUTs to the passthrough image stream. This option can be used to smoothly transition between two color LUTs.

The options **Grayscale** and **Grayscale To Color** are mainly provided for compatibility reasons. The **Color Adjustments**, **Color LUT**, and **Blended Color LUTs** options let you build apps that can leverage color passthrough. You can check if the current device supports color passthrough using the [`SupportsColorPassthrough`](/reference/unity/latest/class_o_v_r_manager_passthrough_capabilities/#a96dce99fa0ae6fb7d5d240df1cf9ab9c) flag, which is obtained from [`OVRManager.GetPassthroughCapabilities`](/reference/unity/latest/class_o_v_r_manager/#a81cf31ea18f41e25c25d8b5687817faa), and adjust the styling accordingly.

All of the color control features are available in scripting as methods on [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/): [`SetColorMap`](/reference/unity/latest/class_o_v_r_passthrough_layer/#aace95e10b9a6246738bfff12ba288b42), [`SetColorMapMonochromatic`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a955df5b308f13985e15e6bfc13279705), [`SetColorMapControls`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a4eded9a174d3963006f7eb66c3caaed9), [`SetBrightnessContrastSaturation`](/reference/unity/latest/class_o_v_r_passthrough_layer/#abe0d9f3a847aeac6124ed960f866a290), [`SetColorLut`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a58f56420f1a0dd9655dae90da504b759), [`DisableColorMap`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a9ea6834ca9ce7bf1a58e48bc639e6172). These features are mutually exclusive. Calling any of these methods will replace the color control defined in a previous call.

## Color Look-Up Tables (LUTs)

A color LUT is a 3D array which maps each RGB color to an arbitrary new RGB(A) color. Color LUTs enable a wide range of effects, ranging from subtle color grading to stylizations such as posterization, selective coloring, and chroma keying.

To apply a color LUT to an **OVRPassthroughLayer** component, follow these steps:

1. In the **OVRPassthroughLayer** component, under **Color Control**, select **Color LUT**.
2. Under **LUT**, select a 2D texture that represents a color LUT. See [Creating Color LUTs](/documentation/unity/unity-passthrough-creating-color-luts/) for more information on supported formats and how to generate them.
3. Check **Flip Vertically** if the LUT has black (RGB values 0, 0, 0) in the top-left corner of the texture, otherwise uncheck it.
4. The **Blend** slider controls the mix between the original and the replacement color. Leave it at 1 to fully apply the LUT.

To apply a color LUT via scripting interface, do the following:

1. Create an instance of [`OVRPassthroughColorLut`](/reference/unity/latest/class_o_v_r_passthrough_color_lut/#a58f56420f1a0dd9655dae90da504b759). The LUT data can be passed as a 2D texture or as 3D array of color values. Refer to the [API reference](/reference/unity/latest/class_o_v_r_passthrough_color_lut/) for all constructor signatures.
2. Call [`SetColorLut`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a58f56420f1a0dd9655dae90da504b759) on the instance of [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/) the color LUT should be applied to, passing the `OVRPassthroughColorLut` instance as the first parameter. The `weight` parameter corresponds to the **Blend** value specified in the inspector.
3. Optionally, you can apply an `OVRPassthroughColorLut` instance to multiple passthrough layers by passing it to `SetColorLut` calls on multiple layers.

This excerpt demonstrates the essential statements discussed in this section:
```csharp
public class PassthroughColorLUTController : MonoBehaviour
{
    Private Texture2D _2dColorLUT;
    OVRPassthroughColorLut ovrpcl;

    // other code...

    GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
    passthroughLayer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
    ovrpcl = new OVRPassthroughColorLut(_2dColorLUT, false);
    passthroughLayer.SetColorLut(ovrpcl, 1);
}
```
For a working example, see the [Passthrough Color LUT Tutorial](/documentation/unity/unity-passthrough-tutorial-passthrough-color-lut/).

### Animating Color LUT Transitions

[`OVRPassthroughLayer.SetColorLut`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a58f56420f1a0dd9655dae90da504b759) is a lightweight operation that doesn't add a lot of overhead and can be called in every frame. To fade a color LUT in or out, call
```cpp
SetColorLut(lut, weight)
```
with a varying value for `weight` during the transition. To transition between two different color LUTs, call
```cpp
SetColorLut(lutSource, lutTarget, weight)
```
with a varying value for `weight` during the transition.

## Animate Color LUT Contents

Some use cases require color LUTs to change continuously beyond linear interpolation. For example, an app could dynamically adjust the saturation of different color hues based on the state of the app or external input (such as audio).

Call [`UpdateFrom`](/reference/unity/latest/class_o_v_r_passthrough_color_lut/#a2cfe58f30c155907931a2af032629a2c) on an instance of [`OVRPassthroughColorLut`](/reference/unity/latest/class_o_v_r_passthrough_color_lut/) to update the LUT data of an existing color LUT. The updated data will automatically be used on all passthrough layers that are currently using that `OVRPassthroughColorLut` instance. There is no need to call [`OVRPassthroughLayer.SetColorLut`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a58f56420f1a0dd9655dae90da504b759) to propagate the changes.

For high-resolution color LUTs (especially 64), calling `UpdateFrom` in every frame can have a notable impact on app performance. In such cases, try to keep the LUTs as small as possible - a resolution of 32 or lower is recommended.

### Additional Notes

- There is a constraint on the maximum resolution allowed for color LUTs, which can be queried using [`OVRManager.GetPassthroughCapabilities`](/reference/unity/latest/class_o_v_r_manager/#a81cf31ea18f41e25c25d8b5687817faa). On current Meta Quest devices, the maximum resolution is 64.
- The color LUT resolution impacts memory usage and GPU performance. For that reason, it is advisable to keep the resolution as small as possible given use case and quality constraints. For example, start with a color LUT of resolution 16, then check if increasing the resolution to 32 significantly improves the visual quality.
- For high-resolution color LUTs (especially 64), constructing an `OVRPassthroughColorLut` object can take a few milliseconds and is best done in advance.
- `OVRPassthroughColorLut` instances occupy an amount of memory that is proportional to the LUT data size. [`Dispose`](/reference/unity/latest/class_o_v_r_passthrough_color_lut/#a50a644cfb7c9c11879b0db91886c18a1) can be called to free the memory immediately.
- An instance of `OVRPassthroughColorLut` can be applied to multiple passthrough layers by passing it to [`SetColorLut`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a58f56420f1a0dd9655dae90da504b759) on multiple layers. Doing so reduces the memory usage (compared to creating a separate `OVRPassthroughColorLut` instance per layer).