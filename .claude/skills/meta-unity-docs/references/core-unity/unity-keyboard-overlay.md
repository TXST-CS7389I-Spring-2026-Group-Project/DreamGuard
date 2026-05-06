# Unity Keyboard Overlay

**Documentation Index:** Learn about unity keyboard overlay in this documentation.

---

---
title: "Enable Keyboard Overlay"
description: "Utilize the keyboard overlay feature to use the system keyboard as an overlay."
last_updated: "2026-01-20"
---

While you can use physical Bluetooth keyboards for text entry on a Meta Quest headset, interacting with a system keyboard inside a VR app can create a more immersive experience. The keyboard overlay feature lets you display and enter text using the system keyboard, avoiding the need to develop your own custom keyboard experience. The keyboard overlay lets you access features such as voice dictation and multiple language support.

<image alt="Meta Quest system keyboard overlay displayed inside a VR application for text entry." style="width: 100%;" src="/images/unity-keyboard-overlay-qds2.jpg"/>

## Prerequisites

### Hardware requirements
- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  

### Headset requirements
<!-- vale on -->

- Supported Meta Quest headsets:
  
  - Quest 2
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest Pro
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3S and 3S Xbox Edition
  <!-- vale off -->
  

<!-- vale on -->

<!-- vale on -->

### Software requirements
<!-- vale off -->
- Unity Editor 2022.3.15f1 or later (6.1 or later recommended)
  
  
  

<!-- vale on -->

## Project setup

The keyboard overlay feature requires installing the Meta XR Core SDK in your project. See [How do I set up the Meta XR Core SDK?](/documentation/unity/unity-core-sdk/#how-do-i-set-up-the-meta-xr-core-sdk/) for installation instructions.

## Enable the system keyboard

When you set up the system keyboard to appear as an overlay, it automatically appears when an editable UI text element receives the input focus. When the app displays the keyboard, it loses the input focus, triggering the `OVRManager.InputFocusLost` event. When the app closes the keyboard, it gains the input focus, triggering the `OVRManager.InputFocusAcquired` event.

You can enable the system keyboard in two ways: using the project setting from Unity or invoking the system keyboard programmatically.

### Enable system keyboard from Unity

To enable the keyboard overlay project setting:

1. [Enable focus awareness](/documentation/unity/unity-focus-awareness/).
2. From the **Hierarchy** view, select **OVRCameraRig** to open settings in the **Inspector** view.
3. Under **OVR Manager**, in the **Quest Features** section, select **Require System Keyboard**.

### Enable system keyboard programmatically

To show the system keyboard programmatically, use Unity's [TouchScreenKeyboard](https://docs.unity3d.com/ScriptReference/TouchScreenKeyboard.html) interface:

1. [Enable focus awareness](/documentation/unity/unity-focus-awareness/).
2. Declare a variable to store the TouchScreenKeyboard instance and a variable to hold the string the keyboard returns.

    ```
private TouchScreenKeyboard overlayKeyboard;
public static string inputText = "";
```

3. When a text UI element receives focus, call the `TouchScreenKeyboard.Open()` method to activate the keyboard. Doing so takes input focus away from the app, triggering the `OVRManager.InputFocusLost` event. Pass `TouchScreenKeyboardType.Default` to the method since it is the only supported type for keyboard overlay.

      ```
overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
```

4. To retrieve the typed contents, call the `TouchScreenKeyboard.text` property to return the text displayed in the input field of the keyboard and store it in the variable to use it elsewhere.

    ```
if (overlayKeyboard != null)
    inputText = overlayKeyboard.text;
```

## Preview Keyboard Overlay

To test or preview the keyboard overlay feature, do the following:

1. Open the app in which you've implemented the keyboard overlay feature.
2. Point the cursor to the editable UI text element.

   You can see the system keyboard overlay on the app.