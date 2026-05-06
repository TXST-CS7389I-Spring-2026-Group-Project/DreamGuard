# Unity Cubemap

**Documentation Index:** Learn about unity cubemap in this documentation.

---

---
title: "Cubemap Screenshots"
description: "Take cubemap screenshots with the OVR Screenshot Wizard."
---

The OVR Screenshot Wizard allows you to easily export a 360 screenshot in cubemap format.

Cubemap previews may be submitted with applications to provide a static in-VR preview for the Meta Horizon Store. For more information, see the Meta Quest [Content Guidelines](/policy/content-guidelines/).

You may also use OVRCubemapCaptureProbe to take a 360 screenshot from a running Unity app.

<image style="width: 400px;" handle="GNPZGAFQQca4Le8DAAAAAADBufZnbj0JAAAD" src="/images/documentationunitylatestconceptsunity-cubemap-1.png" alt="Cubemap screenshot"/>

## Cubemap Usage

First, import [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) into your project individually or bundled as part of the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/). This will add a new **Meta** pull-down menu to your menu bar. Select **Meta** > **Tools** > **OVR Screenshot Wizard** to launch the tool.

By default, the screenshot will be taken from the perspective of your Main Camera. To set the perspective to a different position, assign any Game Object to the **Render From** field in the Wizard and click **Render Cubemap** to save.

The generated cubemap may be saved either as a Unity Game Object or as a horizontal 2D atlas texture in PNG or JPG format with the following face order (horizontal left to right): +x, -x, +y, -y, +z, -z.

## Cubemap Options

 <image style="width: 800px;" handle="GFN8FwGu05uzLe8DAAAAAAD1KP1gbj0JAAAD" src="/images/documentationunitylatestconceptsunity-cubemap-3.png" alt="OVR Screenshot Wizard"/>

**Render From**: You may use any Game Object as the "camera" that defines the position from which the cubemap will be captured.

To assign a Game Object to function as the origin perspective, select any instantiated Game Object in the Hierarchy View and drag it here to set it as the rendering position in the scene. You may then position the Game Object anywhere in the scene.

If you do not specify a Game Object in this field, the screenshot will be taken from the Main Camera.

**Size**: Sets the resolution for each "tile" of the cubemap face. For submission to the Meta Store, select 2048 (default, see [Content Guidelines](/policy/content-guidelines/) for more details).

### Save Mode

* Save Cube Map: Generate Unity format Cubemap
* Save Cube Map Screenshot: Generate a horizontal 2D atlas texture, resolution: ( 6 * Size ) * Size
* Both: Save both Unity Cubemap and 2D atlas texture

**Cube Map Folder**: The directory where OVR Screenshot Wizard creates the Unity format Cubemap. The path must be under the root asset folder "Assets"

**Texture Format**: Sets the image format of 2D atlas texture (PNG or JPEG).

**Render Cubemap**: Click the button to generate cubemap.