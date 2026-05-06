# Os Compositor

**Documentation Index:** Learn about os compositor in this documentation.

---

---
title: "The compositor"
description: "The Meta Quest compositor handles reprojection, barrel distortion correction, and multi-layer compositing each frame."
last_updated: "2024-12-04"
---

The compositor is the part of Meta's VR operating system that performs every operation between your application submitting a frame, and that frame being drawn on-screen.

Although this system is not explicitly used by user applications, understanding the compositor is foundational to using [App Spacewarp](/documentation/unity/os-app-spacewarp/) and [Compositor layers](/documentation/unity/os-compositor-layers/).

The three main functions of the compositor are:

1. [Reducing perceived latency (reprojection / time warp)](#reducing-perceived-latency-reprojection--time-warp)
2. [Distorting frames](#distorting-frames)
3. [Adding compositor layers](#adding-compositor-layers)

## Reducing perceived latency (reprojection / time warp)

Some applications suffer from excessive fixed latency, which is a long period of time between pressing a button and seeing its result on-screen. Other applications fail to submit frames at the required framerate, causing stuttering and freezing during loading screens or heavy simulation loads.

In non-VR applications, these behaviors are annoying but tolerable in small amounts. In VR applications, images that lag behind the user's actual head or body movement or that are choppy and contain pauses can be uncomfortable due to a [visual-vestibular mismatch](/design/locomotion-comfort-usability#visual-vestibular-mismatches-and-comfort).

The system measures your application's render time and unblocks it just in time for composition, keeping the most recently completed frame ready for the compositor. This technique is called *Time Warp*. The compositor rotates the most recently completed frame to account for head rotation since the frame was generated. Your application had to commit to a head pose prediction tens of milliseconds before it will be displayed, so the compositor corrects for this just a few milliseconds before it will be displayed. The rotational reprojection makes the frames feel stable in the world, even if the frames are old.

If the player rotates far enough that the edge of the reprojected frame becomes visible, the rotational reprojection inserts black bars. If an application misses frames, this allows the player to feel like the game is responding to head motion, reducing the risk of discomfort.

Pure camera rotation introduces no parallax, making this an effective and computationally inexpensive way to reduce rotation mismatch discomfort. Translating a camera introduces parallax. Note that head rotation also results in translation of the eyes. The compositor usually lacks information to handle parallax changes. As such, the compositor does not account for translation and cannot entirely eliminate discomfort risks on its own. As long as eye translation is small, the rotational reprojection is a good solution.

### Additional reprojection algorithms

Time Warp is one of a class of algorithms that adjust old images based on newer data, called *reprojection*. Another reprojection algorithm of note is *positional timewarp*, which also attempts to adjust for changes in head position.

Positional timewarp, or positional reprojection, uses application-submitted depth buffer information to estimate a rendered image after applying parallax caused by head motion. Although the compositor has no way of knowing the content of previously-hidden pixels that are exposed by parallax changes, it fills in best guesses based on neighboring pixel data.

Because positional timewarp is a more expensive algorithm, it is only enabled for your application if [App Spacewarp](/documentation/unity/os-app-spacewarp/) (AppSW) is also enabled. With AppSW enabled, the compositor will expect to receive both a depth buffer and a motion vector buffer from your application. With that information, the compositor will create reprojected frames that correctly account for head translation and rotation (via positional timewarp), as well as movement of the objects in the scene, such as a car moving in the foreground (via Application Spacewarp).

## Distorting frames

A game engine running a VR application generally attempts to produce 2 images (called *eye buffer* or *projection layer* textures), representing the left- and right-eye view, at the display refresh rate. These are represented as 2 separate textures, at the device's recommended resolution.

_The rendered view for the left eye, in a scene in Horizon Workrooms_

However, unlike 3D applications on conventional displays, a VR system isn't done when it receives these images. The lenses in VR headsets create a *pincushion distortion* in the user's view of the physical display. The compositor creates an inverse *barrel distortion* when rendering to the display so that the net result is undistorted for the user.

_The final texture rendered to screen, for a scene in Horizon Workrooms, on Quest 2. Note the *barrel distortion* on the edges of each image._

## Adding compositor layers

Applications are allowed to submit multiple concurrent frames to the compositor, and the compositor is able to receive frames from multiple applications and services running concurrently. This is most often used for:
- [Multitasking](https://www.meta.com/blog/mountain-majesty-latest-meta-quest-software-update-adds-a-new-home-environment-share-to-headset-on-ios-and-more/). Each individual application or service is potentially unaware it is running in a multitasking setting; the compositor overlays each application's or service's output into its own window.
- System overlays to display achievements, notifications, etc. on top of the running application.
- Additional [compositor layers](/documentation/unity/os-compositor-layers/) for your application. Applications can ask the compositor to render a given texture as a compositor layer. These layers are rendered through a special path, as textures applied to a quad or cylinder primitive, that allows the texture to have a higher *pixels per degree* which appears crisper and easier-to-read on the display. Layers displayed this way also do not suffer the expense and quality penalties of resampling their contents into the eye buffers. To learn how to implement compositor layers in your application, see:
   - [Use VR compositor layers (Unity)](/documentation/unity/unity-ovroverlay/)
   - [Layers and UI quality (Spatial SDK)](/documentation/spatial-sdk/spatial-sdk-2dpanel-layers/)
   - [VR compositor layers (Unreal)](/documentation/unreal/unreal-overlay/)

Note that, although compositor layers are often called *overlay layers*, because they're usually thought of as rendering on top of your application, this name is inaccurate.
- Compositor layers can be rendered as underlays, in which case they are only visible if you set your eye buffer's `Alpha` to `0` in the location of the underlay.
- Many VR UI tools that offer *overlay layers* do not use the compositor layer system, but instead just render directly to your application's eye buffers in a manner that ignores depth tests.