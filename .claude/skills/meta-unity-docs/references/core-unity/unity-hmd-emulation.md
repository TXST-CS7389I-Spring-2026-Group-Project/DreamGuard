# Unity Hmd Emulation

**Documentation Index:** Learn about unity hmd emulation in this documentation.

---

---
title: "HMD Motion Emulation"
description: "Emulate the movement of a Meta Quest device in the Unity Editor during development."
---

Use HMD Motion Emulation to simulate the movement of a user directly in the Unity Editor.

Any app that uses `OVRCameraRig` or `OVRPlayerController` prefabs will enable the emulator. Otherwise, you can attach the [`OVRHeadsetEmulator`](/reference/unity/latest/class_o_v_r_headset_emulator) class to a game object.

To use the emulator, play/preview the scene in the editor and use your mouse and keyboard to:

| Input | Action |
|-|-|
| Ctrl (hold) + mouse movement | Update headset pitch and yaw |
| Ctrl (hold) + alt + mouse movement | Update headset roll |
| Ctrl (hold) + mouse wheel | Update headset height/elevation |
| Ctrl (hold) + middle mouse button | Reset the pose to scene default |

All in-editor manipulation is done while holding the Ctrl button on Windows. HMD Motion Emulation is not supported on Mac at this time.

By default, `OVRHeadsetEmulator.opMode` is set to `EditorOnly`, which make it effective only in the Unity Editor preview window. Set to `AlwaysOn` to activate the function in standalone builds.

By changing `OVRHeadsetEmulator.activateKeys` and `OVRHeadsetEmulator.pitchKeys`, you can change the default key-bindings.

Any updates/translation made in the Editor will also be reflected in the HMD.

If the HMD Motion Emulation is not working, check if `OVRHeadsetEmulator` is properly attached, and try stopping the scene and restarting.

## How it Works

[`OVRHeadsetEmulator`](/reference/unity/latest/class_o_v_r_headset_emulator) modifies `OVRManager.headPoseRelativeOffsetRotation` and `OVRManager.headPoseRelativeOffsetTranslation`. You can also write your own script or use Unity animation clips to modify those properties to automatically change the HMD pose.