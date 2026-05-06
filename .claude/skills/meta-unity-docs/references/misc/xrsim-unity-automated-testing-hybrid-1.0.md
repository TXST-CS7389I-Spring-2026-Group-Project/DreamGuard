# Xrsim Unity Automated Testing Hybrid 1.0

**Documentation Index:** Learn about xrsim unity automated testing hybrid 1.0 in this documentation.

---

---
title: "Hybrid Automation Testing"
description: "Combine zero-code navigation with programmatic validation to test specific app areas in the Meta XR Simulator."
last_updated: "2024-09-13"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

The hybrid approach combines the strengths of both zero-code and programmatic methods. It is useful when testing specific areas of an application that are not accessible at the beginning of gameplay. Here, the zero-code automation can navigate the game to the desired location and be followed by programmatic testing to scrutinize the application's behavior in that context.

Since this approach combines zero-code and programmatic testing, please read [Zero-code Automation Testing In Action](/documentation/unity/xrsim-unity-automated-testing-in-action-pt-1.0/) and [Programmatic Automation Testing](/documentation/unity/xrsim-unity-automated-testing-programmatic-1.0/) first.

## Outline

By following this guide, you will be able to set up and run hybrid automation tests against the [Oculus Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples) project. The steps are outlined below:

1. Clone [Oculus Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples).
2. Download Meta XR Simulator.
3. Record a zero-code automation test to bring the game to a certain stage.
4. Set Meta XR Simulator to replay the recording.
5. Prepare programmatic testing and execute the test.

## Download and build the test app

Run the following script in Powershell to download and build the game to .exe:

```
# Clone project source code
cd c:/
mkdir tmp
cd tmp
git clone https://github.com/oculus-samples/Unity-StarterSamples.git

# Build project into .exe
C:\”Program Files”\Unity\Hub\Editor\2022.3.20f1\Editor\Unity.exe -buildTarget win64 -openfile "C:\tmp\Unity-StarterSamples/Assets/StarterSamples/Usage/SceneManager.unity" -buildWindows64Player "c:/tmp/output/game.exe" -logFile c:/tmp/log.txt -quit
```

## Download Meta XR Simulator

Run the following script in PowerShell to download and install the Meta XR Simulator and then launch the game. These commands can be run alongside the previous script but are separated here for clearer tutorial structure.

```
# 1. Download and Unzip Meta XR Simulator [Start]
# If you already have Meta XR Simulator install, skip to the next step of this script. (Note: the download progress can take up to 5 minutes. Alternatively you can manually download it from https://developers.meta.com/horizon/downloads/package/meta-xr-simulator/)

cd c:/tmp

Invoke-WebRequest -Uri https://npm.developer.oculus.com/com.meta.xr.simulator/-/com.meta.xr.simulator-65.0.0.tgz -OutFile MetaXRSimulator.tgz

tar -xvzf "MetaXRSimulator.tgz"

Start-Sleep 1

Rename-Item -Path "package" -NewName "MetaXRSimulator"

# Download and Unzip Meta XR Simulator [End]

```

## Record test on Meta XR Simulator

1. Run the following script below to start the app for recording.

    ```
    # 1. Launch a Synthetic Environment
    Start-Process -FilePath "C:/tmp/MetaXRSimulator/MetaXRSimulator\.synth_env_server\synth_env_server.exe" -ArgumentList "Bedroom" -PassThru

    # 2. Activate Meta XR Simulator
    c:/tmp/MetaXRSimulator/MetaXRSimulator/activate_simulator.ps1

    mkdir C:\tmp\test_recordings\

    # 3. Launch the game
    Start-Process -FilePath "C:\tmp\output\game.exe" -Wait

    # 4. Deactivate Meta XR Simulator
    C:\tmp\MetaXRSimulator\MetaXRSimulator\deactivate_simulator.ps1

    # 5. Stop Synthetic Environment
    Get-Process -Name "synth_env_server" | Stop-Process
    ```

2. Click **Record & Replay** > **Record** to start a recording. Select a VRS file to record to (for example, `C:\tmp\test_recordings\recording.vrs`).
3. Interact with the game on Meta XR Simulator, for demo purpose we will only walk around with **AWSD** key on the keyboard.
4. Click **Stop Recording** button to stop the recording.
5. Close Meta XR Simulator.

## Set Meta XR Simulator to replay the recording

Run the following script to configure the Simulator for replaying the recorded session.
Note that `delay_start_ms` is required to ensure the target scene is fully loaded.

```
# 1. Update the config file.
$jsonFilePath = "C:\tmp\MetaXRSimulator\MetaXRSimulator\config\sim_core_configuration.json"
$jsonContent = Get-Content $jsonFilePath | Out-String
$jsonObject = $jsonContent | ConvertFrom-Json

if (-not ($jsonObject.PSObject.Properties.Name -contains "session_capture")) {
    $jsonObject | Add-Member -MemberType NoteProperty -Name "session_capture" -Value "session_capture"
}
$jsonObject.session_capture = @{
    "delay_start_ms" = 10000
    "exec_state" = "replay"
    "quit_buffer_ms" = 1000
    "quit_when_complete" = $false
    "record_path" = "C:\tmp\test_recordings\recording.vrs"
    "replay_path" = "C:\tmp\test_recordings\replay_new.vrs"
}
if (-not ($jsonObject.PSObject.Properties.Name -contains "use_batch_mode")) {
    $jsonObject | Add-Member -MemberType NoteProperty -Name "use_batch_mode" -Value $true
}
$jsonObject.use_batch_mode = $true

if (-not ($jsonObject.PSObject.Properties.Name -contains "log_location")) {
    $jsonObject | Add-Member -MemberType NoteProperty -Name "log_location" -Value "log_location"
}
$jsonObject.log_location = "C:\tmp\test_recordings\MetaXRSimulator.log"

$jsonObject | ConvertTo-Json -Depth 10 | Set-Content $jsonFilePath
```

## Prepare programmatic testing and execute the test

For the programmatic testing segment, reuse all scripts from Programmatic Automation Testing. However, adjust `yield return new WaitForSeconds(10);` to a longer duration (for example, `20`) to ensure the recording is replayed before assertions.

When it runs, you will see the test scene load, followed by the automatic replay of the recording. After that, the test runner will execute and the test results will be displayed.