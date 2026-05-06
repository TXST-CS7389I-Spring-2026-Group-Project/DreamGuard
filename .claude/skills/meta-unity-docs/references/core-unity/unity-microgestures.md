# Unity Microgestures

**Documentation Index:** Learn about unity microgestures in this documentation.

---

---
title: "Hand tracking microgestures OpenXR extension"
description: "Detect subtle hand gestures using the microgestures OpenXR extension with sample scenes and prefab components."
last_updated: "2025-03-14"
---

## Overview

Microgestures expand the capabilities of hand tracking by recognizing low-calorie thumb tap and thumb swipe motions performed on the side of the index finger. These gestures trigger discrete D-pad-like directional commands.
The hand pose and motion of the thumb is as follows: initially, the thumb must be raised above the index finger (not touching the index finger). The other fingers should be slightly curled as in the picture below for best performance: i.e. not too extended, nor completely curled into a fist.
A tap is performed by touching the middle segment of the index finger with the thumb, and then lifting the thumb.
The four directional thumb swipes performed on the surface of the index finger are:
* **Left swipe**: a swipe towards the index fingertip on the right hand, and away from the index fingertip on the left hand. On the right hand for example, the motion is as follows: the thumb starts raised above the index finger, touches the middle segment of the index finger, slides towards the index fingertip, and lifts.
* **Right swipe**: the same motion as the left swipe, but in the opposite direction. On the right hand for example, the thumb starts raised above the index finger, touches the middle segment of the index finger, slides away from the index fingertip, and lifts.
* **Forward swipe**: the thumb starts raised above the index finger, touches the middle segment of the index finger, slides forward, and lifts.
* **Backward swipe**: the thumb starts raised above the index finger, touches the middle segment of the index finger, slides backward/downward, and lifts.
Note that the motions can be performed at moderate to quick speeds, and should be performed in one smooth motion. The detection of the gesture happens at the end of the motion.

{:width="600px"}

## Compatibility

### Hardware compatibility

* Quest 2, Quest Pro, and the Quest 3 family of devices.

### Software compatibility

* Unity version 2021 LTS and above
* Integration SDK version 74 and above

## Setup

1. [Install the Meta XR Core SDK Package V74 or higher.](/downloads/package/meta-xr-core-sdk/).
2. Under **Window** > **Package Management** > **Package Manager**, find **Meta XR Core SDK**, then under the **Samples** tab, click **Import** next to **Sample Scenes**.
3. Open the HandMicrogestures sample scene: **Assets** > **Samples** > **Meta XR Core SDK** > **[Version]** > **Sample Scenes** > **HandMicrogestures.unity**.

### Using the sample scene

Your hands should appear in front of you when the sample scene loads.

In the sample scene, there is a panel in front of you which displays the gestures that were recognized for each hand. Try performing some thumb swipes, taps, and index pinches using each hand. The text under “Left Hand Gesture” and “Right Hand Gesture” should be updated to indicate the gesture that was performed.

Four directional arrows and two circle icons will also be displayed in front of you. These icons are used to illustrate the mapping of the microgestures to cardinal directions (up, down, left, right) and thumb-tap selection and pinch. The corresponding icon for the gesture will turn blue when recognized. Those icons map to the raw upstream API signal.

### Prefabs and public functions

To integrate Microgesture input in a Unity scene, create two GameObjects: "LeftHandGestures" and "RightHandGestures." Attach the **OVRMicrogestureEventSource** script to each GameObject.

Assign the Hand reference on each GameObject to the corresponding left or right [**OVRHand**](/reference/unity/latest/class_o_v_r_hand) in the **OVRCameraRig**.
The **OVRMicrogestureEventSource** triggers a **GestureRecognizedEvent** when a gesture is detected on either hand. Subscribe to this event to implement custom application logic.

### OVRMicrogestureEventSource Class
```csharp
/// <summary>
/// This class emits events based on recognized microgestures for a specific OVRHand.
/// It tracks the state of gestures to emit the corresponding events.
/// </summary>
public class OVRMicrogestureEventSource : MonoBehaviour
{
    [SerializeField]
    private OVRHand _hand;

    /// <summary>
    /// Event triggered when a microgesture is recognized by the system. This event provides the specific type of microgesture detected.
    /// </summary>
    public UnityEvent<OVRHand.MicrogestureType> GestureRecognizedEvent;

    /// <summary>
    /// Gets or sets the OVRHand associated with this event source, which is used to detect gestures.
    /// </summary>
    public OVRHand Hand
    {
        get { return _hand; }
        set { _hand = value; }
    }

    // ...
}
```
## Troubleshooting

### Testing microgestures on device

Load the sample scene and ensure your hands appear in front of you. Perform various gestures with each hand, such as thumb swipes, taps, and index pinches, and check that the text under **Left Hand Gesture** and **Right Hand Gesture** updates correctly to indicate the gesture performed. Additionally, look for the directional arrows and circle icons in front of you - when a gesture is recognized, the corresponding icon should turn blue, confirming that the microgestures feature is functioning properly.

### Evaluating microgestures on device without code changes

Currently, evaluating microgestures on a device requires code changes for your app.