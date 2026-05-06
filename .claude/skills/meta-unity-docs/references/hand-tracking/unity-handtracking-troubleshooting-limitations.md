# Unity Handtracking Troubleshooting Limitations

**Documentation Index:** Learn about unity handtracking troubleshooting limitations in this documentation.

---

---
title: "Troubleshooting and Limitations"
description: "Known limitations of hand tracking and troubleshooting steps for common issues."
---

## Troubleshooting {#troubleshooting}

The following questions help you troubleshoot issues you may encounter during rendering and integrating hands in the app:

* Why don’t I see hands in my app?

    There can be many reasons why hands are not rendering in your app. To begin with, verify that hand tracking is enabled on the device and that hands are working correctly in the system menus. Ensure that you have used OVRHandPrefab to add hands in the scene.

* Why do I see blurry/faded hands?

    Your hands may not be properly tracked since the cameras on the Meta Quest headset have a limited field of view. Make sure the hands are closer to the front of the Meta Quest headset for better tracking.

* Can I use another finger besides the index finger for the pinch gesture?

    Yes. Use the [`OVRHand.GetFingerIsPinching()`](/reference/unity/latest/class_o_v_r_hand#afd44f43efd7755b3f81302e269b8d795) method from OVRHand.cs with the finger that you want to track instead. For more information about tracking fingers, go to the [Add Interactions](/documentation/unity/unity-handtracking-interactions#add-interactions) section.

## Understanding Hand Tracking limitations
Hand tracking for Meta Quest has some known limitations. While these limitations may be reduced or even eliminated over time, they are currently part of the expected behavior. For more specific issues, go to the [Troubleshooting](#troubleshooting) section.

### Occlusion

  Tracking may be lost, or hand confidence may become low when one hand occludes another. In general, an app should respond to this by fading the hands away.

### Noise

  Hand tracking can exhibit some noise. It may be affected by lighting and environmental conditions. You should take these conditions into consideration when developing algorithms for gesture detection.

### Controllers + Hands

  Controllers and hands are not currently tracked at the same time. Apps should support either hands or controllers, but not at the same time.

### Lighting

  Hand tracking has different lighting requirements than inside-out (head) tracking. In some situations, this could result in functional differences between head tracking and hand tracking, where one may work while the other has stopped functioning.