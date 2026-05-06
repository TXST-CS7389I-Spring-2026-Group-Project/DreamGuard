# Ts Mqdh Compositor Layer

**Documentation Index:** Learn about ts mqdh compositor layer in this documentation.

---

---
title: "Layer Visibility Control in VR Compositor"
description: "Use Meta Quest Developer Hub to control compositor layer visibility for performance analysis."
---

VR apps traditionally render everything into a single eye buffer, the projection layer, and submit it to [the Compositor](/documentation/unity/os-compositor/). However, the VR Compositor actually supports several different types of [Compositor Layers](/documentation/unity/os-compositor-layers/), which can usually provide better image quality than a single projection layer.

To help you understand the behavior of Compositor layers better and optimize your app performance accordingly, MQDH integrates a tool for layer visibility control. You can select a subset of Compositor layers to render during runtime and observe the impact of individual layers.

The following sections describe various features of this tool.

## Layer information display

Once a Meta Quest headset is connected, a button appears at the top-right corner of the metrics graphs area on the **Performance Analyzer** tab. The text on the button shows the number of visible layers compared to the total number of Compositor layers from the attached Meta Quest headset.

Clicking the button opens **Flag Settings**, where you can find the layer visibility control panel at the top.

All the Compositor layers are shown under **Layer Visibility**, with a checkbox next to their layer types indicating whether they are enabled. This layer information is pulled from the Compositor every second to reflect changes of the VR scene in the headset as close as possible to real-time.

## Layer visibility control

To control the visibility of each layer, select **Enable VR Runtime Debug Mode**.

With the debug mode enabled, the Layer Visibility checkboxes are activated and you are able to select and clear individual layers to see the effect of toggling their visibility in the headset.

If any part of the scene disappears after you disable a layer, you know it corresponds to that layer. You can also compare the change of the performance curves before and after a layer is disabled to estimate the performance impact from that layer.

## Layer properties

Hovering over individual layer items shows layer properties as a tooltip. Currently, the properties include rendered PPD (pixels per degree), texture resolution, recommended texture resolution, and recommended filtering. This information helps you understand the expected parameters from the submitted layers, so that the Compositor can generate a frame with improved quality without suffering expense penalties.

## See also

* [MQDH Performance and Metrics](/documentation/unity/ts-mqdh-logs-metrics)
* [Developer Tools for Meta Quest](/resources/developer-tools/)