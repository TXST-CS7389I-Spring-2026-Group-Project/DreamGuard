# Xrsim Session Capture

**Documentation Index:** Learn about xrsim session capture in this documentation.

---

---
title: "Capture a Meta XR Simulator Session"
description: "This doc explains how you can record and replay Meta XR Simulator sessions."
last_updated: "2024-10-04"
---

## Overview

Session Capture is useful for replaying a sequence of inputs in the exact same manner and checking if the results are the same as before. You can build fast manual tests, automated tests that run in Continuous Automation, or play against yourself in a multi-player test setup. It records inputs and takes screenshots using an exact time algorithm.

## Create a recording

To create a recording:

1. Start an app with the runtime set to the **Meta XR Simulator**.

2. Load a scene (optionally run with the Synthetic Environment Server).

3. In the Meta XR Simulator window, click **Record & Replay** > **Record**.

4. Select a VRS file to record to. Session Capture is now in the Recording state.

5. Use your keyboard and mouse or Xbox controller to enter inputs and interact with the app.

    - To save a snapshot, click **Take Snapshot(s)**.
    - Increase or decrease the number of frames to capture according to your testing scenario.

6. To exit the Recording state, click **Stop Recording**.

## Replay a recording

After you [create a recording](#create-a-recording):

1. Start the same app with Meta XR Simulator.

2. Select the VRS recording.

3. In the Meta XR Simulator window, click **Record & Replay** > **Replay**.

4. Select a VRS file to save to. Session Capture is now in the Replay state.

   **Note**: When you take a snapshot, the Capture number is displayed.

5. To exit the Replay state, click **Record & Replay** > **Stop Replay**.

## Automate replay

<oc-devui-note type="important"> This works with standalone applications but not with Play mode in Unity. </oc-devui-note>

Add the following to `%AppData%\Roaming\MetaXR\MetaXrSimulator\persistent_data.json` (after updating `replay_path` to your recording's location):

  ```
  "session_capture": {
      "delay_start_ms": 1000,
      "exec_state": "replay",
      "quit_buffer_ms": 1000,
      "quit_when_complete": true,
      "record_path": "C:\\open\\test_recordings\\recording.vrs",
      "replay_path": "C:\\open\\test_recordings\\replay.vrs"
  }
  ```

Launch the application with Meta XR Simulator to automatically replay.

**Note:** The recording automatically closes one second after the replay is complete. If you want the Meta XR Simulator to stay open after the replay is complete, change `quit_when_complete` to `false`.

**JSON field definitions:**

- `session_capture` (object, required) - top-level block that contains all of the automation session_capture parameters
- `delay_start_ms` (int, optional) - number of milliseconds to wait before starting the replay
- `exec_state` (string, required) - which state to start the automation in (only replay is supported for now)
- `quit_buffer_ms` (int, optional) - how long to delay after replay is complete before quitting the Meta XR Simulator
- `quit_when_complete` (bool, optional) - whether to quit the Meta XR Simulator after replay is complete
- `record_path` (string, required) - absolute path to the record file to use
- `replay_path` (string, required) - absolute path to the replay file to save