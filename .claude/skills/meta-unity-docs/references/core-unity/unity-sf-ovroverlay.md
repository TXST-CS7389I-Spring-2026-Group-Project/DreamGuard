# Unity Sf Ovroverlay

**Documentation Index:** Learn about unity sf ovroverlay in this documentation.

---

---
title: "OVROverlay Sample Scene"
description: "Sample app depicting how to add an overlay VR Compositor Layer in Unity using OVROverlay."
---

The OVROverlay sample scene demonstrates how to use the OVROverlay script to render high-quality text and textures as VR compositor layers.  For a conceptual overview of VR compositor layers and details of the OVROverlay script and settings, please see the OVROverlay &amp; Compositor Layers page.

When loading the sample scene you’re presented with a dialog directly in front of the camera. This dialog demonstrates two concepts, the Level Loading Example and the OVROverlay vs. Application Render Comparison.

The Level Loading Example demonstrates how to render cubemap and text layers that can be used for a more comfortable loading experience by allowing free head movement while the app’s main thread is blocked.

The OVROverlay vs. Application Render Comparison demonstrates rendering the same content (a identical set of Quads) to OVROverlay layers and directly to the eye buffer. On mobile devices you’ll also see the option to load an OVROverlay cylinder layer.

The goal of this topic is to help you understand the basic implementation and expression of various OVROverlay layers, as well as the difference between rendering to a compositor layer vs directly to an eye buffer. You can also use this sample scene as a starting point for your own application.

<image alt="OVROverlay sample menu with Level Loading and Overlay vs. Application Render comparison options." handle="GBV82AKSExM0YjUBAAAAAABZ5E06bj0JAAAD" src="/images/ovroverlay-sample-menu.png"/>

## Scripts
**OVROverlaySample.cs** - The `OVROverlaySample.cs` script controls the sample app. Annotations are provided in the C# file that describe the components of the script.

## Major Components
There are a number of elements in this scene, but for the purposes of this sample we’ll focus on components that demonstrate functionality in the sample.

<image alt="Unity Hierarchy view showing CompositorLayerLoadingScreen and WorldVsOverlayComparison objects." handle="GPp33QJj9-Ot5J8EAAAAAACmNEBybj0JAAAD" src="/images/ovroverlay-sample-components.png"/>

### CompositorLayerLoadingScreen
This component contains two basic OVROverlay layers. `CubemapLayer` is a cubemap texture that is expressed in the scene as the world-locked “environment”. `LoadingTextLayer` is a Quad layer with a simple text texture that indicates to a user that the user is in a loading interstitial.

### WorldVsOverlayComparison
Controls the layers that are toggled in OVROverlay vs. Application Render Comparison. `OverlayUIGeometry` is comprised of 10 Quad “Tile” layers rendered to a second camera, and then fit to a quad to be sent to the compositor layer.. `WorldspaceUIGeometry` is comprised of the same 10 Quad “Tile” layers, but rendered directly to the eye buffer instead of a compositor layer.

### OVROverlayText
The components in `OVROverlayText` are simple OVROverlay Quad layers that display information about the scene.