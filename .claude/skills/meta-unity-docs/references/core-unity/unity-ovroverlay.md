# Unity Ovroverlay

**Documentation Index:** Learn about unity ovroverlay in this documentation.

---

---
title: "Use VR Compositor Layers"
description: "Use OVROverlay to add VR Compositor overlay layers in Unity apps."
last_updated: "2024-12-17"
---

This guide covers how to implement Compositor layers in your Unity application. To learn how Compositor layers work and how to debug them, see [Compositor Layers](/documentation/unity/os-compositor-layers/).

OVROverlay is a script available in the Oculus/VR/Scripts folder that enables the compositor layers.

OVROverlay supports up to 15 layers in a single scene. However, each scene cannot contain more than one cylinder and one cubemap layer per scene. If a compositor layer fails to render, only quads fall back and are rendered as scene geometry. Cubemaps and cylinders are not displayed at all. A common example is when you attempt to render more than the maximum number of compositor layers. If you need to support more than 15 objects, you can combine planar elements into a single RenderTexture and use a single OVROverlay layer.

## Adding an overlay or underlay layer

1. In the Hierarchy view of Unity Editor, create an empty GameObject.
2. Drag the OVROverlay script from `Packages/com.meta.xr.sdk.core/Scripts/OVROverlay.cs` to the GameObject.<br>

   **Note**: OVROverlay uses OVRManager.

   

## Understanding OVROverlay script configurations

The OVROverlay script contains several configuration settings.

### Overlay type

Select the type of overlay. Options available are Overlay, Underlay, and None.

* **Overlay** is the default type. It is rendered in front of the eye buffer.
* **Underlay** is rendered behind the eye buffer. The Underlay compositor layers are more bandwidth-intensive because the compositor must punch a hole in the eye buffer with an alpha mask to make underlays visible. Texture bandwidth is often a VR bottleneck, so use them with caution and be sure to assess their impact on your application.
* **None** hides the layer.

### Composition depth

Composition depth is an optional feature, disabled by default, which allows you to set the depth of the layer. It is used to determine the ordering of layers in the scene. When comparing two layers, the overlay/underlay with the smaller compositionDepth is composited in the front of the overlay/underlay with the larger compositionDepth. For example, a scene with numerous overlays and underlays would be -

[Camera] (Overlay) 2 / 1 / 0 [Eyebuffer] -1 / 0 / 1 (Underlay)

### Enable depth buffer testing

To enable Composition Depth testing for a particular layer, select the **Enable Depth Buffer Testing** checkbox.

This feature can be useful in situations like loading or pausing a screen overlay. If this feature is not enabled on a given layer, your OVROverlay layer will always render on top of the scene.

**Note**: It’s possible for an object that should be behind another object, in terms of distance from the camera, to be drawn in front because it is implemented as an overlay and you've chosen to ignore the composition depth. This sends conflicting cues about the depth of these objects and can be uncomfortable for the viewer.

### Overlay shape

Select the shape of the overlay layer.

* **Quad** layers are a flat texture with four vertices. Most commonly used as a panel to display text or information in a scene.

* **Cubemap** layers are textures that contain six squares to form a cube to surround an object. Most commonly used for reflections and background or surroundings of a scene. You can also use it for low overhead loading or startup scenes. See the [Cubemap Screenshots](/documentation/unity/unity-cubemap/) page for more information about cubemaps.

* **Cylinder** layers are a single texture that is wrapped around the camera in a cylinder. Most commonly used for curved UI interfaces.

* **Equirect** layers are a single texture that is wrapped into a sphere and projected to surround the user's view. Most commonly used for 360/180 video playback.

* **Offcenter Cubemap** layers are cubemaps where the centroid is moved forward on the z axis 30 degrees, enabling higher fidelity, in terms of more pixels, in front of the user.

* **Fisheye** sets up OVROverlay to display a fisheye image. You can adjust transform settings to change the FovX, FovY, Horizon, and Meridian of the Fisheye layer.

   * Position X: Adjusts the meridian of the fisheye. This is the horizontal right handed offset of the center of the image in degrees.
   * Position Y: Adjusts the horizon of the fisheye. This is the vertical offset of the center of the image in degrees.
   * Scale X: Adjusts the horizontal FOV of the fisheye in degrees.
   * Scale Y: Adjust the vertical FOV of the fisheye in degrees.
   * Rotation X, Y, Z: Adjusts the Yaw, Pitch, and Roll of the layer.

### Bicubic filtering

You can enable GPU hardware bicubic filtering tuned for Meta Quest display to enjoy additional fidelity when presenting VR imagery. If you are developing for Meta Quest, the system will fall back to default bilinear interpolation, allowing you to use the bicubic filtering features without having to special-case older devices.
* To enable bicubic filtering, select the **Bicubic Filtering** checkbox.

**Note**: Bicubic filtering requires more GPU resources as the kernel footprint increases. This is especially true for trilinear minification as it requires two bicubic calculations from separate mip-levels. If used directly for compositor layers, the increased GPU costs will manifest itself in composition timing, which may lead to frame drops and negatively impact the VR experience. You should weigh the increased visual fidelity against the additional GPU resources required to offer the best VR user-experience.

### Composition layer filtering

To improve the image quality of composition layers, enable composition layer filtering. Layer filtering methods support supersampling tuned to the Quest display-optics system.

Enabling supersampling (left) reduces flicker for high contrast edges (right) for layers that are undersampled by the compositor to match the display resolution.

Sharpening improves clarity of high contrast edges and can counteract blur when upsampling to meet display resolution.

By enabling auto filtering, the layer is rendered at peak visual quality while preventing adverse artifacts, like screen tears. In this mode, the runtime automatically applies the chosen supersampling and sharpening filters only when beneficial. Examples include:

* A sharpening filter is applied to mitigate layer blur.
* A supersampling filter is applied to mitigate flicker.

If the layer does not require any filtering then opting into auto filtering will result in no operation with no performace overhead.

Auto filtering accounts for dynamic and complex interaction with the layers, like layer resizing and player movement in 3D space. Additionally, it considers the following parameters before filtering the layers:
* Rendered pixels per degree of the layer.
* Hardware pixels per degree of the display.
* GPU utilization.
* Layer visibility.

#### Enable layer filtering

To enable supersampling or sharpening, select a normal or expensive variants from the **Super Sample** or **Sharpen** dropdown. Expensive variants are computationally expensive and should be applied only if the application has the additional GPU capacity.

**Important:** Composition layer filtering requires more GPU resources. This is especially true for the sharpening and supersampling algorithms as these utilize larger kernel footprints. If used directly for composition layers, using these algorithms increases the GPU resources that are essential affecting composition timing. This may lead to frame drops and negatively impact the VR experience. The bottom line is that when applying composition layer filtering, you should weigh the increased visual fidelity against the additional GPU resources required to offer the best VR user experience.
Alternatively, you can use auto filtering mode to mitigate the risk of unnecessary filtering. Auto filtering will gracefully degrade the panel quality during instances of constrained GPU resources.

#### Enable auto filtering

To enable Auto filtering, select **Auto Filtering** checkbox and then choose a either normal or expensive filter variant from the filter dropdowns. For example, if the app chooses expensive variant of shaperning and a normal variant of supersampling, then
autofiltering will filter the layer with chosen filter only when needed.

### Textures

Associate the texture that you’d like to render on the overlay layer. If you leave it as **None**, which is the default option, it uses the renderer.material main texture, if available.

### Dynamic texture

Select if the content rendered to the overlay is dynamic, which means if the texture should be updated each frame while the overlay persists. This checkbox is automatically checked when a rendertexture is associated with the layer.

### Is protected content

Select if you want to protect the layer with HDCP on Rift and L1 Widevine DRM on Meta Quest.

**For Meta Quest**: To enable support on the headset, you must also render the OVROverlay layer as an External Surface and select **Is Protected Content** in Player Settings > Virtual Reality > Oculus.

### Is external surface

Select the **Is External Surface** checkbox to identify that the layer will be used to pass through textures or video from an external [Android Surface](https://developer.android.com/reference/android/view/Surface).

This feature allows the creation of Android Surface and lets Timewarp layer manage it. In your Unity project, you can create a quad overlay and render the Surface texture directly to the TimeWarp layer. For more information about rendering surface texture, go to the [Animated Loading Screen](/blog/tech-note-animated-loading-screen/) tech note.

Use the **External Surface Width** and **External Surface Height** to define the size of the output.

<image alt="OVROverlay Inspector section with Is External Surface enabled and width and height fields." style="width: 350px;" handle="GCOmEgO2DusAhs0CAAAAAABUi6lkbj0JAAAB" src="/images/external_surface.jpg"/>

### Use default rects

When you select Cylinder or Offcenter Cubemap layer, you can define a single input texture that consists of both, the left and the right image. Set the texture as **Left Texture**. Do not set the same texture for both eyes.

Unchecking this box opens the **Source Rects** dialog where you can define how the left and right texture are positioned in the single input texture. ‘Monoscopic’, ‘Stereo Left/Right’, and ‘Stereo Top/Bottom’ presets are available for quick selection.

<image alt="OVROverlay Source Rects settings showing left and right eye texture layout options." style="width: 350px;" handle="GMcUEwOLys9VGYAHAAAAAAANo6x_bj0JAAAB" src="/images/ovroverlay_rect.jpg"/>

### Override color scale

Override any global color settings for the layer. Select the **Override Color Scale** checkbox to set a **Color Scale** and **Color Offset** input.

<image alt="OVROverlay Override Color Scale section with Color Scale and Color Offset fields." style="width: 350px;" handle="GJm6EwPrdOlKvzkIAAAAAADRzDYIbj0JAAAB" src="/images/ovrovrlay_colorscale.jpg"/>

## Using underlays

Underlays depend on the alpha channel of the render target. Do the following to add underlays:

1. Set alpha to 1 in the [Color](https://docs.unity3d.com/6000.1/Documentation/Manual/sprite/shape-renderer/color.html) window, if a scene object that should occlude an underlay is completely opaque. If the occluder is transparent, i.e., alpha 0&#60;1, use `Underlay Transparent Occluder.shader` from `Packages/com.meta.xr.sdk.core/Resources/`.

    **Note**: Overlays do not require any special handling for transparency.
2. Poke a hole in the texture after all the objects are drawn to the eye buffer.
3. Use `Underlay Impostor.shader` from `Packages/com.meta.xr.sdk.core/Resources/` to draw imposters in the delta space. Ensure you perform this operation after all opaque textures are drawn, but before the alpha. This allows the underlay to show through the empty space.

### Example

In the following example, most of the scene geometry is rendered to the eye buffer. The application adds a gaze cursor as a quad overlay and a skybox as a cubemap underlay behind the scene.

<image alt="Example: unity ovroverlay 3." style="width: 350px;" handle="GE98FwFeBm6wLe8DAAAAAADc9Hwdbj0JAAAD" src="/images/documentationunitylatestconceptsunity-ovroverlay-3.png"/>

Take a note of the dotted sections of the eye buffer, indicating where OVROverlay has punched a hole to make the cubemap underlay visible behind scene geometry.

Check if the cubemap in the scene is transparent. If yes, use `OVRUnderlayTransparentOccluder`, which is required for any underlay with alpha less than 1. If it is stereoscopic, specify two textures and set the size to 2.

## Using cylinder overlays

The center of a cylinder overlay game objects is used as the cylinder’s center. The dimensions of the cylinder are encoded in `transform.scale` as follows:

* [scale.z] cylinder radius
* [scale.y] cylinder height
* [scale.x] length of the cylinder arc

To use a cylinder overlay, your camera must be placed inside the inscribed sphere of the cylinder. The overlay fades out when the camera approaches to the inscribed sphere's surface. Only half of the cylinder may be displayed, so the arc angle must be smaller than 180 degrees.

## Using offcenter cubemap overlays

Offcenter cubemap compositor layers are useful for increasing resolution for areas of interest/visible areas by offsetting the cubemap sampling coordinate. They are similar to standard cubemap compositor layers.

Attach the OVROverlay script to an empty game object and specify the texture coordinate offset in the Position Transform field. For more information, see OVROverlay in the Unity Scripting Reference.

## World-locked vs head-locked Layers

Overlay layers should almost always be world-locked, which means that they maintain their position with respect to the world behind it.

* World-locked overlays use TimeWarp, similar to non-overlay content, and are much less prone to judder.
* Head-locked overlays bypass TimeWarp and exactly follow head motion. The exception being small UI elements like a gaze cursor or targeting reticle.

Overlays are world-locked by default. To make a head-locked overlay, make the layer (usually a Quad) a child of `OVRCameraRig` center eye anchor.

## Common OVROverlay use cases

We have listed a few common use cases for using compositor layers and different OVROverlay features.

### Playing high-quality video playback

It is extremely important for video playback apps to use compositor layers instead of rendering to the eye buffers that Unity ordinarily renders into.

There are two ways to use OVROverlay to display high quality video:

* To play video on an overlay layer that is rendered or created in game, you can override the **Use Default Rects** feature. This allows you to render a single texture, saving the cost of rendering two textures, at a higher resolution.
* To play high-quality external video, use the **Is External Surface** feature to use an Android plugin to feed external video directly into the compositor.

### Loading screen

A lightweight loading screen can be added using two OVROverlay layers.

1. Add a cubemap layer that is the background of the loading screen. If you leave this blank, it displays a black void.
2. Add another overlay layer, usually a simple quad, with some texture or text that indicates that the user is in a loading interstitial.

For more information, open the OVROverlay sample in the [Starter Samples](/documentation/unity/unity-starter-samples).

### Gazing cursor or targeting reticle

To add a gaze cursor or targeting reticle (or similar), add an OVROverlay quad to your scene as a child of the `OVRCameraRig` center eye anchor.

## OVROverlay samples

We have demonstrated OVROverlay features in the [OVROverlay Sample](/documentation/unity/unity-sf-ovroverlay/) documentation. Learn more about exploring samples scenes in the [Starter Samples](/documentation/unity/unity-starter-samples) documentation.