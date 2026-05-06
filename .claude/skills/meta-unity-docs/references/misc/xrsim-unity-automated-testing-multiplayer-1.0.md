# Xrsim Unity Automated Testing Multiplayer 1.0

**Documentation Index:** Learn about xrsim unity automated testing multiplayer 1.0 in this documentation.

---

---
title: "Multiplayer Automation Testing"
description: "Record and replay multiple game client interactions on a single computer for automated multiplayer testing."
last_updated: "2024-12-20"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

Multiplayer testing has never been easier. The Meta XR Simulator enables testing multiple game clients on a single computer, though switching between windows to interact with different clients can be cumbersome. Using the recording and replaying feature allows developers to automatically run other clients, letting them focus on testing and debugging the main client. Additionally, all client interactions can be recorded and replayed in CI for repeated testing.

To conduct multiplayer testing, it's recommended to duplicate the project's source code so you can run them separately. Alternatively, you can compile the game into an executable. Each time you double-click the executable, it will launch a new client.

It's important to ensure each client uses a different simulator configuration for automated interactions, as the simulator uses these configurations to locate the recording files.

This tutorial covers recording for different clients and running them together. Use  [Oculus Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples) as the test app.

**Note:** This app doesn't have multiplayer logic. We're using it to demonstrate the multiclient automation feature.

## Download and build the test app

Run the below script on Powershell to download and build the game to .exe:

```
# Clone project source code
cd c:/
mkdir tmp
cd tmp
git clone https://github.com/oculus-samples/Unity-StarterSamples.git

# Build project to .exe
C:\”Program Files”\Unity\Hub\Editor\2022.3.20f1\Editor\Unity.exe -buildTarget win64 -openfile "C:\tmp\Unity-StarterSamples/Assets/StarterSamples/Usage/SceneManager.unity" -buildWindows64Player "c:/tmp/output/game.exe" -logFile c:/tmp/log.txt -quit
```

## Download Meta XR Simulator

Run the following commands in PowerShell to download and install the Meta XR Simulator, and then launch the game. These commands can be run alongside the previous script but are separated here for clearer tutorial structure.

```
# Download and Unzip Meta XR Simulator [Start]
# If you already have Meta XR Simulator install, skip to the next step of this script. (Note: the download progress can take up to 5 minutes. Alternatively you can manually download it from https://developers.meta.com/horizon/downloads/package/meta-xr-simulator/)

cd c:/tmp

Invoke-WebRequest -Uri https://npm.developer.oculus.com/com.meta.xr.simulator/-/com.meta.xr.simulator-65.0.0.tgz -OutFile MetaXRSimulator.tgz

tar -xvzf "MetaXRSimulator.tgz"

Start-Sleep 1

Rename-Item -Path "package" -NewName "MetaXRSimulator"

# Download and Unzip Meta XR Simulator [End]

```

## Record test on Meta XR Simulator

1. Run the following script to start the app for recording:

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

1. Click **Record & Replay** > **Record** and select a VRS file to record to (for example, `C:\tmp\test_recordings\recording.vrs`).

1. Interact with the game on Meta XR Simulator. For the demo, move around with the **WASD** keys.

1. When you're done recording, click **Stop Recording** and close Meta XR Simulator.

## Record Additional Client Interactions

Repeat the above steps to generate different recording files.

## Set up replaying for each client

Duplicate the core configuration file for each client. For example, create `C:\tmp\MetaXRSimulator\MetaXRSimulator\config\sim_core_configuration_client_1.json` and add the **session_capture** JSON blob to each of them. Each should use a different recording file (.vrs).

Example for client_1:: `C:\tmp\MetaXRSimulator\MetaXRSimulator\config\sim_core_configuration_client_1.json`

```
{
    "version": "...",
    ....
    "session_capture": {
        "delay_start_ms": 1000,
        "exec_state": "replay",
        "quit_buffer_ms": 1000,
        "quit_when_complete": true,
        "record_path": "C:\\tmp\\client_1_recording.vrs",
        "replay_path": "C:\\tmp\\client_1_replaying.vrs"
    }
}
```

## Launch a synthetic environment

```
Start-Process -FilePath "C:/tmp/MetaXRSimulator/MetaXRSimulator\.synth_env_server\synth_env_server.exe" -ArgumentList "Bedroom" -PassThru

Start-Sleep -Seconds 1
```

## Run the main client

If you want to test and debug the main client from the Unity Editor, open and run the app in Unity Editor as usual. Don't forget to activate Meta XR Simulator from the Unity Editor menu.

To fully automate all players, skip this step and move on to the next section.

## Run other clients

Open a PowerShell window and run the following command, assuming you will run two additional clients:

```
# Client 1 Script
$Script1 = {
    Write-Host "Client 1 is running."
    set XR_RUNTIME_JSON=C:/tmp/MetaXRSimulator/MetaXRSimulator/meta_openxr_simulator.json
    set META_XRSIM_CONFIG_JSON=C:/tmp/MetaXRSimulator/MetaXRSimulator/config/sim_core_configuration_client_1.json
    Start-Process -FilePath "C:\tmp\output\game.exe" -Wait
}

# Client 2 Script
$Script2 = {
    # Your script 2 code here
    Write-Host "Client 2 is running."
    set XR_RUNTIME_JSON=C:/tmp/MetaXRSimulator/MetaXRSimulator/meta_openxr_simulator.json
    set META_XRSIM_CONFIG_JSON=C:/tmp/MetaXRSimulator/MetaXRSimulator/config/sim_core_configuration_client_2.json
    Start-Process -FilePath "C:\tmp\output\game.exe" -Wait
}

# Start new PowerShell windows with different scripts and environments
Start-Process powershell -ArgumentList "-NoExit", "-Command", $Script1
Start-Process powershell -ArgumentList "-NoExit", "-Command", $Script2
```

The above commands will start two clients with different recordings replaying automatically.

These recording files and scripts can be uploaded to CI for automated testing. To verify the test results, you can use either zero-code testing or programmatic testing.