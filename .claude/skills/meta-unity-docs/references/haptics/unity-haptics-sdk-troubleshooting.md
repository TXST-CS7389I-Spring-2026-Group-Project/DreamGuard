# Unity Haptics Sdk Troubleshooting

**Documentation Index:** Learn about unity haptics sdk troubleshooting in this documentation.

---

---
title: "Haptics Troubleshooting"
description: "Resolve audio-haptics sync issues, report bugs, and get support for the Haptics SDK."
---

## Sync issues between haptics and audio

A time offset can occur between audio and haptics due to different execution paths with different time durations.
While a permanent synchronization mechanism is not yet available, a temporary solution is to delay the faster of the two (audio or haptics) to match the other. The offset between audio and haptics can vary under different running conditions so we cannot recommend a fixed value for the delay. The code below simply uses an example of one second.
The code below simply uses an example of one second:

```
public class MyMonoBehaviour : MonoBehaviour
{
    SomeFunction()
    {
        // Call a function after 1 second
        StartCoroutine(CallFunctionAfterDelay(delay / 1000.0f));
    }

    private IEnumerator CallFunctionAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        // Call function here
    }
}
```

## Getting support

For any issues, bugs, or feature suggestions, contact [haptics-feedback@meta.com](mailto:haptics-feedback@meta.com). When reporting issues,
attach relevant screenshots or logs but avoid including personal information. If you are looking for an older version of Meta XR Haptics SDK,
see [Meta XR UPM Packages](/documentation/unity/unity-package-manager/) for instructions on downloading and importing it
from Meta’s NPM registry.

## Gathering logs

Logs are crucial for diagnosing issues. The Haptics SDK for Unity logs some messages to the Unity log and others to the Android log. Reference these logs to get more information when you experience issues. For the Android log, the SDK uses the Unity and HapticsSDK log tags.

### Get logs from the headset

1. Increase the log buffer size. Without doing this, the logs will often only contain the last few seconds, and important log messages like the one about the Haptics SDK initialization will be missing:

```
adb logcat -G 16M
```

2. Reproduce the problem. It is important to reproduce the problem right before gathering the logs, so that the messages related to the problem are still contained in the limited log buffer.

3. Pull the logs and save them to a file:

```
adb logcat -d > logs.txt
```

4. Verify that the logs contain information related to the Haptics SDK. The following should show some Haptics SDK log messages:

```
cat logs.txt | grep HapticsSDK
```

5. Ideally this goes back to the initialization of the Haptics SDK, which logs a message similar to this:

```
05-26 14:08:03.444  9451  9662 I HapticsSDK: haptics_sdk_c_api_core::c_api::helpers: Haptics SDK version <version> initialized
```

### Get logs from Windows/Link applications

When using Play Mode in the Unity Editor on Windows/Link, the Haptics SDK logs end up in e.g. C:\Users\Username\AppData\Local\Unity\Editor\Editor.log. Note that unlike on Android, Haptics SDK log messages are not marked with a tag like HapticsSDK.