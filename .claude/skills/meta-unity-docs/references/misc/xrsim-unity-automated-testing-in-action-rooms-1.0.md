# Xrsim Unity Automated Testing In Action Rooms 1.0

**Documentation Index:** Learn about xrsim unity automated testing in action rooms 1.0 in this documentation.

---

---
title: "XR Simulator Automated Testing with Room Layouts"
description: "Use Meta XR Simulator for zero-code automation testing across various room layouts and synthetic environments."
last_updated: "2024-06-10"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

This guide illustrates how to employ Meta XR Simulator for automated testing of your app with various room layouts. During app development, you likely tested in a single room. However, it's recommended to regularly test with different layouts to ensure compatibility across various environments.

## Obtaining room layouts

There are two primary methods for obtaining room layouts, each offering built-in rooms and tools for creating customized layouts:

1. **Meta XR Simulator Built-in Synthetic Environment Server**
2. **Scene Json**

Before proceeding, ensure you have reviewed the previous tutorial on [Testing Unity Starter Samples - Passthrough](/documentation/unity/xrsim-unity-automated-testing-in-action-pt-1.0) as we will reuse most of its steps.

## Approach 1: Utilizing the synthetic environment server

The synthetic environment server offers three pre-existing rooms.

To test with a different room:

- If you previously tested with the bedroom synthetic environment, switch to a different one using the following commands:

  ```powershell
  Start-Process -FilePath "C:/tmp/MetaXRSimulator/MetaXRSimulator\.synth_env_server\synth_env_server.exe" -ArgumentList "LivingRoom" -PassThru
  ```
  or
  ```powershell
  Start-Process -FilePath "C:/tmp/MetaXRSimulator/MetaXRSimulator\.synth_env_server\synth_env_server.exe" -ArgumentList "GameRoom" -PassThru
  ```

A sample script for recording:

```
# 1. Download and Unzip Meta XR Simulator
# skipped

# 2. Launch a Synthetic Environment
Start-Process -FilePath "C:/tmp/MetaXRSimulator/MetaXRSimulator\.synth_env_server\synth_env_server.exe" -ArgumentList "LivingRoom" -PassThru

# 3. Activate Meta XR Simulator
c:/tmp/MetaXRSimulator/MetaXRSimulator/activate_simulator.ps1

mkdir C:\tmp\test_recordings\

# 4. Launch the game
Start-Process -FilePath "C:\tmp\output\game.exe" -Wait

# 5. Deactivate Meta XR Simulator
C:\tmp\MetaXRSimulator\MetaXRSimulator\deactivate_simulator.ps1

# 6. Stop Synthetic Environment
Get-Process -Name "synth_env_server" | Stop-Process
```

We recommend that you use the same Synthetic Environment for the remaining steps of the tutorial on [Test Unity Starter Samples - Passthrough](/documentation/unity/xrsim-unity-automated-testing-in-action-pt-1.0).

### Create custom synthetic environments

You can create customized synthetic environments using the [Synthetic Environment Builder](/documentation/unity/xrsim-synthetic-environment-builder/), allowing you to convert any Unity scene into a synthetic environment.

## Approach 2: Utilizing scene Json

Scene Json provides a lightweight solution for testing apps with room layouts. There are 2 built-in Scene Json files under `C:\tmp\MetaXRSimulator\MetaXRSimulator\config\anchors\`.

Instead of launching a Synthetic Environment, add the following Json blob into the root of `C:\tmp\MetaXRSimulator\MetaXRSimulator\config\sim_core_configuration.json`:

```
"scene_anchor_data" : "C:\tmp\MetaXRSimulator\MetaXRSimulator\config\anchors\scene_anchors_room_with_furnitures.json"
```

A full sample script for recording is:

```
# 1. Download and Unzip Meta XR Simulator
# skipped

# 2. Launch a Synthetic Environment
# Do NOT use Synthetic Environment

# Update the config file.
$jsonFilePath = "C:\tmp\MetaXRSimulator\MetaXRSimulator\config\sim_core_configuration.json"
$jsonContent = Get-Content $jsonFilePath | Out-String
$jsonObject = $jsonContent | ConvertFrom-Json
if (-not ($jsonObject.PSObject.Properties.Name -contains "scene_anchor_data")) {
    $jsonObject | Add-Member -MemberType NoteProperty -Name "scene_anchor_data" -Value "scene_anchor_data"
}
$jsonObject.scene_anchor_data = "C:\tmp\MetaXRSimulator\MetaXRSimulator\config\anchors\scene_anchors_room_with_furnitures.json"

$jsonObject | ConvertTo-Json -Depth 100 | Set-Content $jsonFilePath

# 3. Activate Meta XR Simulator
c:/tmp/MetaXRSimulator/MetaXRSimulator/activate_simulator.ps1

mkdir C:\tmp\test_recordings\

# 4. Launch the game
Start-Process -FilePath "C:\tmp\output\game.exe" -Wait

# 5. Deactivate Meta XR Simulator
C:\tmp\MetaXRSimulator\MetaXRSimulator\deactivate_simulator.ps1

# 6. Stop Synthetic Environment
# Do NOT use Synthetic Environment

```

We recommend that you use the same scene Json for the remaining steps of the tutorial on [Test Unity Starter Samples -- Passthrough](/documentation/unity/xrsim-unity-automated-testing-in-action-pt-1.0).

### Create your own scene Json

You can record scene data from your Oculus using the [Scene recorder](/documentation/unity/xrsim-scene-recorder-1.0/) and load the file into the root of `C:\tmp\MetaXRSimulator\MetaXRSimulator\config\sim_core_configuration.json`

```
"scene_anchor_data" : "path to your scene json"
```