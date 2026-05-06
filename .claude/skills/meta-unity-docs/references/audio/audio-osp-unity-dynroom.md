# Audio Osp Unity Dynroom

**Documentation Index:** Learn about audio osp unity dynroom in this documentation.

---

---
title: "Dynamic Room Modeling in Unity"
description: "Simulate realistic room acoustics with the Oculus Spatializer dynamic room modeling feature in Unity."
---

<oc-devui-note type="warning" heading="End-of-Life Notice for Oculus Spatializer Plugin">
<p>The Oculus Spatializer Plugin has been replaced by the Meta XR Audio SDK and is now in end-of-life stage. It will not receive any further support beyond v47. We strongly discourage its use. Please navigate to the Meta XR Audio SDK documentation for your specific engine:

<br>- <a href="/documentation/unity/meta-xr-audio-sdk-unity-intro/">Meta XR Audio SDK for Unity Native</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unity</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unity</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-unreal-intro/">Meta XR Audio SDK for Unreal Native</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unreal</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unreal</a>
</p>

<p><strong>This documentation is no longer being updated and is subject for removal.</strong></p>
</oc-devui-note>

The Oculus Spatializer provides dynamic room modeling, which enables sound reflections and reverb to be generated based on a dynamically updated model of the current room within the VR experience and the user's position within that space.

## Overview

Prior to the 1.22 release, the Oculus Spatializer could only use a simple reflection model based on a rectangular prism "shoebox". This approach is quite effective at filling the space and making it sound natural. With this approach, sound reflections and reverb are binaurally spatialized, meaning the timing, volume, and other effects are different in each ear. This provides the right cues to reinforce the directionality of the sound. This simple shoebox reflection system is designed for ease of use and always places the listener at the center with the dimensions of the box set by the sound designer. This allows for a simple workflow where everything is controlled in the middleware tools. The sound designer can determine the dimensions and reflection coefficients for the walls of the box in order to obtain more natural sounding spatialization that fits as closely as possible to the VR experience.

However, because the room is a fixed size, this simple approach is somewhat limited. The listener was always placed in the center, even when they move near a wall or other fixed object within the experience. In addition, reverb offers little value because it isn’t dynamic. The Oculus Spatializer now provides more dynamic capabilities to further increase the realism of the sound without greatly increasing the computational complexity or impacting the workflow.

In the 1.22  and later releases, the Oculus Spatializer integrates with the game engine in order to fit the shoebox to the actual space within the VR experience, making it dynamically conform to the shape and size of the space. This enables the dimensions of the room to change dynamically and enables the sound characteristics to change as the listener moves within the room. In order to achieve this, the Spatializer uses raycasting within the game engine. To keep this simple, there is a default implementation that connects the Spatializer to the engine raycaster. It is designed to be as lightweight as possible, so there are only a small number of rays that are cast. In addition, it maintains a history of previous raycast results and refines the estimation over time.

The Spatializer also includes a visualization option in the Unity Editor, enabling you to see the dimensions of the room and where the rays are hitting. The ability to visualize the raycasting is particularly useful for dealing with unintentional collisions with geometry that isn't meant to affect the sound, such as UI objects.

## Using Dynamic Room Modeling

To use the Oculus Spatializer within the Unity Editor, attach the following script to a game object:

`Assets/OSPNative/scripts/OculusSpatializerUnity.cs`

* Click on the `Add Component` button and search for `Oculus Spatializer Unity`.
* From the results, select `Oculus Spatializer Unity`.

  <image style="width: 1200px;" handle="GHrTUgJ5PglngfwCAAAAAAA9zXISbosWAAAD" src="/images/addOculusSpatializerUnity.PNG"/>

The easiest approach is to add this script to a static, empty game object in the scene. It will then activate the geometry engine, overriding the current implementation.

The script exposes public variables that can be accessed after the game object with the script is added to the scene:

<image style="width: 1200px;" handle="GFjrewR9Yq70faUBAAAAAAAwTuUUbosWAAAD" src="/images/OculusSpatializerUnity.PNG"/>

You can modify the following variables:

* Layer Mask: Geometry can be tagged with a given layer enum. The Spatailizer will only use geometry that matches what has been selected in Layer Mask.
* Visualize Room: When turned on, you will see the rays hitting the geometry in the Unity Editor. Only geometry assigned to the layer(s) selected in the Layer Mask will be shown. You will also see the shoebox reverb model dynamically change to fit the current space as you move throughout the VR experience. You will also see the results from setting other variables, such as Rays Per Second, Max Wall Distance, etc.
* Rays Per Second: This specifies the number of rays that are randomly sent out from the listener's position per second to approximate room size and shape. A larger value produces a more accurate approximation of the room characteristics but requires more CPU resources.
* Room Interp Speed: This specifies the time it takes (in seconds) for the room to smoothly transition from one room approximation to the next. The larger the number, the slower the transition. If this value is too small, the reflections and reverb could sound erratic and jump around. If this value is too large, the reflections and reverb may seem to lag behind when moving from one space to another space, especially where there are substantial differences in room size or shape.
* Max Wall Distance: The maximum length (in feet) that each ray will travel. If a wall is not hit within that range, it will not be used in approximating the room size.
* Ray Cache Size: The number of rays that are cached to approximate the room characteristics. The larger the cache, the more rays will be used to approximate the current room characteristics. If this value is too large, the sound may not transition quickly enough when moving from one space to another, especially where there are substantial differences in room size or shape. If this value is too small, the sound may be perceived as being too erratic.
* Dynamic Reflections Enabled: If you turn off Dynamic Reflections Enabled, then reverb will persist, but reflections will be turned off. See "Reverb and Reflections", below, for more information.
* Legacy Reverb: Dynamic room modeling provides a reverb effect which is smoother and more accurate than the legacy reverb effect. However dynamic room modeling requires more CPU resources. You can use the legacy reverb model if you need to reduce CPU resource usage, and the legacy reverb effect is satisfactory for your application.

### Direct Sounds, Reflections, and Reverb

Following is a brief explanation about the difference between direct sounds, reflections and reverb.

When a sound is generated, the listener initially hears the *direct sound*. This is the strongest signal the listener hears, and for our spatializer, is key to localizing a sound in 3D. In an anechoic chamber, you would hear only the direct sound because the reflections are absorbed by the walls. In the real world, these sounds would be dry and unrealistic because sounds always find objects to bounce off and return to the listener.

Next is reflections. When you are in a room, the sound hits the walls, some of it is absorbed, and some bounces back into the room as *reflections*. The immediate reflection of the sound is called first order reflection. In a room with six sides, such as a box, the listener first hears the direct sound, followed by six reflections. When the six first-order reflections bounce off the walls again, each one creates another six reflections, creating a total of 36 (6 x 6 = 36). These reflections are much quieter than the first six and are called second-order reflections.

First and second order reflections are unique in that they are fairly distinct. They are like an echo in that the sound is very similar to the original. First, second and even third order (36 * 6) are considered part of the reflection portion of the sound. Reflections are the second most important piece of the spatializer; they give the listener a sense of the shape of the room.

Reverberation takes reflections one step further. After the third order reflection, the bounces start getting fuzzy and undefined. The result is a network of bounces that sound like continuous noise. These high-order bounces are the *reverb* portion of the sound and are modeled differently from the reflection portion because simulating them is CPU-intensive. We do tricks to cut down on the CPU and approximate the high order bounces.

In our terminology, reflections are the first few bounces that still sound discreet, and reverb is the higher order bounces that are indistinguishable from each other.