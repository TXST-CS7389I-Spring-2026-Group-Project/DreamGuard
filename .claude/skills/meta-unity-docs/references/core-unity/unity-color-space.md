# Unity Color Space

**Documentation Index:** Learn about unity color space in this documentation.

---

---
title: "Set specific color space in Unity"
description: "Set the specific color space to fine tune colors of your app on the Meta Quest headset."
last_updated: "2024-12-06"
---

Color is one of the core components that brings your app to life on the Meta Quest headset. When dealing with colors, one of the challenges is that despite having the same color values, colors can vary with the change in displays and technologies, such as an LCD monitor and an OLED Meta Quest headset. Differences can include low-level banding, hue shift, and under- or over-saturation of colors.  Colors you see on the monitor during app development may not necessarily match with the ones you see on the target headset. This is because different displays may use different color spaces and may not interpret the color values in the same way.

The following image illustrates the difference in color rendering between Rec 2020 and Rec 709.

<image alt="Side-by-side comparison of Rec 2020 and Rec 709 color space rendering on a color gradient." style="width: 600px;" src="/images/unity-rec2020-rec709.png"/>

## What is a color space?

Color space is a defined range of colors that facilitates color management. It defines the capabilities of a display device to reproduce colors based on the specific color information. For understanding purposes, they are typically graphed in reference to the industry-standard chromaticity diagram, which maps out all the color values that a human eye can perceive to x and y coordinates. In the diagram, the triangle represents the specific color space and its corners are called primaries, which represent the primary colors: Red, Green, and Blue. It charts the white point as D65. For detailed information about color management, go to [Color and Brightness Mastering Guide](/resources/color-brightness-mastering/).

<image alt="CIE chromaticity diagram with triangles showing Rec 2020, Rec 709, and P3 color space gamuts." style="width: 600px;" src="/images/unity-chromaticity-diagram.png"/>

### Color space options

To overcome the color variation that may occur due to different color spaces in use, you can remaster your app by setting the specific color space at runtime for your headset. The default color spaces are:

*   **Oculus Rift**: Between Adobe RGB and DCI-P3
*   **Oculus Rift S, Meta Quest 2**: Rec 709
*   **Meta Quest**: Rec 2020

In addition to the standard color spaces, you have a choice to select custom Meta-specific color spaces to master your apps for assets with different display technologies such as LCD vs. OLED.

The following tables list the available color spaces and their primaries:

**Standard color spaces**:

<table>
  <tr>
   <td><strong>Color Space</strong>
   </td>
   <td><strong>Red</strong>
   </td>
   <td><strong>Green</strong>
   </td>
   <td><strong>Blue</strong>
   </td>
   <td><strong>White </strong>
   </td>
  </tr>
  <tr>
   <td>Rec_2020
   </td>
   <td>(0.708, 0.292)
   </td>
   <td>(0.17, 0.797)
   </td>
   <td>(0.131, 0.046)
   </td>
   <td>D65 (0.313, 0.329)
   </td>
  </tr>
  <tr>
   <td>Rec_709
   </td>
   <td>(0.640, 0.330)
   </td>
   <td>(0.292, 0.586)
   </td>
   <td>(0.156, 0.058)
   </td>
   <td>D65 (0.313, 0.329)
   </td>
  </tr>
  <tr>
   <td>P3
   </td>
   <td>(0.680, 0.320)
   </td>
   <td>(0.265, 0.690)
   </td>
   <td>(0.150, 0.060)
   </td>
   <td>D65 (0.313, 0.329)
   </td>
  </tr>
  <tr>
   <td>Adobe_RGB
   </td>
   <td>(0.640, 0.330)
   </td>
   <td>(0.210, 0.710)
   </td>
   <td>(0.150, 0.060)
   </td>
   <td>D65 (0.313, 0.329)
   </td>
  </tr>
</table>

**Custom color spaces**:

<table>
  <tr>
   <td><strong>Color Space</strong>
   </td>
   <td><strong>Red</strong>
   </td>
   <td><strong>Green</strong>
   </td>
   <td><strong>Blue</strong>
   </td>
   <td><strong>White </strong>
   </td>
  </tr>
  <tr>
   <td>Rift_CV1
   </td>
   <td>(0.666, 0.334)
   </td>
   <td>(0.238, 0.714)
   </td>
   <td>(0.139, 0.053)
   </td>
   <td>D75 (0.298, 0.318)
   </td>
  </tr>
  <tr>
   <td>Rift_S (and Quest 2)
   </td>
   <td>(0.640, 0.330)
   </td>
   <td>(0.300, 0.600)
   </td>
   <td>(0.150, 0.060)
   </td>
   <td>D75 (0.298, 0.318)
   </td>
  </tr>
  <tr>
   <td>Quest (1)
   </td>
   <td>(0.6610, 0.3382)
   </td>
   <td>(0.2281, 0.7178)
   </td>
   <td>(0.1416, 0.0419)
   </td>
   <td>D75 (0.2956, 0.3168)
   </td>
  </tr>
</table>

### Set color space

The [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) lets you override the color space of the headset at runtime. When you enable the color gamut feature, it lets you fine tune and re-master colors on the target headset for the entire app. If you prefer to use a color space for a specific scene only, [you should set the color space programmatically](#set-color-space-programmatically).

**Note**: This is an optional setting. If you do not see any issues with the output from the default color space, do not use this setting. Leave it disabled to use the default color space of the headset.

1. In the **Hierarchy** view, select **OVRCameraRig**.
2. In the **Inspector** view, under **OVRManager** > **Display** > **Color Gamut**, select the color space you want your headset to use at runtime. Options include:
    *   **Unknown**: Returns the default value from `GetHmdolorSpace()` until `SetClientColorDesc()` is called.
    *   **Unmanaged**: No color correction takes into effect as this option forces the runtime to skip color correction. It is mostly useful for research and experimentation, and should be avoided for software distribution.
    *   **Rec_2020**: It’s a wider color gamut when compared to the Rec. 709 color space.
    *   **Rec_709**: Default color space for Oculus Rift S. It’s a narrow color gamut and commonly used by LCD panels. It shares the same primaries as the sRGB color space.
    *   **Rift_CV1**: Custom color space. Primaries lie between P3 and Adobe RGB color spaces.
    *   **Rift_S**: Custom color space. Same primaries as the Rec. 709 color space except it uses D75 white point.
    *   **Quest 1**: Custom color space. Primaries lie between P3 and Adobe RGB color spaces.
    *   **P3**: [DCI-P3 standard color space](https://en.wikipedia.org/wiki/DCI-P3).
    *   **Adobe_RGB**: Wider color gamut and uses D65 white point.

**Note**: This feature only handles color-space remapping. Unless specified, all color spaces use D65 white point. It does not affect brightness, contrast, or gamma curves. Some of these aspects such as gamma is handled by the texture format being used. From the GPU samplers' perspective, each texture continues to be treated as linear luminance including sRGB, which is converted to linear by the texture sampler.

### Override color space programmatically {#set-color-space-programmatically}

To set the color space for a specific scene, call the color space APIs in your script instead of using the setting from the OVRManager. This is because when you set the color space from OVRManager, Meta sets it for the entire app.

#### Get the default or current color space:

The OVRManager contains the `nativeColorGamut` and the `colorGamut` variables that store the headset's default and current color space, respectively. Use either of these variables to retrieve the headset's color space.

```
// Retrieves the current color space
OVRManager.ColorSpace CurrentColorSpace = OVRManager.colorGamut;
```

```
// Retrieves the default color space
OVRManager.ColorSpace HmdColorSpace = OVRManager.nativeColorGamut;
```

#### Set the color space:

To set the color space of your choice, assign a new value to the `OVRPlugin.colorGamut` variable.

```
OVRPlugin.colorGamut = OVRManager.ColorSpace.Rec_2020;
```
Available color space values are:

* `OVRManager.ColorSpace.Unknown`
* `OVRManager.ColorSpace.Unmanaged`
* `OVRManager.ColorSpace.Rec_2020`
* `OVRManager.ColorSpace.Rec_709`
* `OVRManager.ColorSpace.Rift_CV1`
* `OVRManager.ColorSpace.Rift_S`
* `OVRManager.ColorSpace.Quest`
* `OVRManager.ColorSpace.P3`
* `OVRManager.ColorSpace.Adobe_RGB`