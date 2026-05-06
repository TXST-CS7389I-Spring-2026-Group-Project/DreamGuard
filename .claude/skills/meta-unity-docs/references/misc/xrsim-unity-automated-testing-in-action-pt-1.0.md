# Xrsim Unity Automated Testing In Action Pt 1.0

**Documentation Index:** Learn about xrsim unity automated testing in action pt 1.0 in this documentation.

---

---
title: "Zero-code Automation Testing In Action"
description: "Following the instructions in this document, you can utilize Meta XR Simulator to establish Zero-Code automation testing for the Unity Starter Samples Project."
last_updated: "2024-11-26"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

This guide demonstrates utilizing Meta XR Simulator for automation testing in Unity projects, using the Oculus Unity Starter Sample app for replicability. By following this guide, you can create tests for the Passthrough Sample app and run them automatically on your local computer. The same procedure can be used to set up tests for CI as well.

The Meta XR Simulator automation testing solution is a zero-code approach that can automate numerous manual tests, particularly smoke tests and regression tests.

The primary advantage of Meta XR Simulator automation testing is its zero-code solution. It relies on prerecorded test steps and screenshot comparisons to confirm test success. Therefore, in this guide you will:

1. Start by recording the test.
2. Replay the recording to produce the expected test results.
3. Lastly, execute the automation test on CI and compare the results with the expected outcomes.

In this guide, you will be testing the torch feature on the passthrough sample. When the user grabs the torch, the room will darken, and only the torch will illuminate. If you were manually testing this, you would grab the torch and move around to check other spots in the room. Therefore, in the automation test, you will replicate this same process.

## Record the Test

To set up a test, you will start by recording it. This process is typically manual and done on a developer or QA local computer. Ensure that the computer has the game engine, game source code, and Meta XR Simulator installed.

1. Run the following script in Powershell to download and build the game to Exe. Ensure the correct path and version number for your Unity installation are used.

   ```
   # Clone project source code
   cd c:/
   mkdir tmp
   cd tmp
   git clone https://github.com/oculus-samples/Unity-StarterSamples.git

   # Build project into Exe
   C:\”Program Files”\Unity\Hub\Editor\2022.3.20f1\Editor\Unity.exe -buildTarget win64 -openfile "C:\tmp\Unity-StarterSamples/Assets/StarterSamples/Usage/Passthrough.unity" -buildWindows64Player "c:/tmp/output/game.exe" -logFile c:/tmp/log.txt -quit
   ```

    

2. Execute the following commands in PowerShell to download and install the Meta XR Simulator, and then launch the game. These commands can be run alongside the previous script but are separated here for clearer tutorial structure.

    ```
    # 1. Download and Unzip Meta XR Simulator [Start]
    # If you already have Meta XR Simulator install, skip to the next step of this script. (Note: the download progress can take up to 5 minutes. Alternatively you can manually download it from https://developers.meta.com/horizon/downloads/package/meta-xr-simulator/)

    cd c:/tmp

    Invoke-WebRequest -Uri https://npm.developer.oculus.com/com.meta.xr.simulator/-/com.meta.xr.simulator-65.0.0.tgz -OutFile MetaXRSimulator.tgz

    tar -xvzf "MetaXRSimulator.tgz"

    Start-Sleep 1

    Rename-Item -Path "package" -NewName "MetaXRSimulator"

    # Download and Unzip Meta XR Simulator [End]

    # 2. Launch a Synthetic Environment
    Start-Process -FilePath "C:/tmp/MetaXRSimulator/MetaXRSimulator\.synth_env_server\synth_env_server.exe" -ArgumentList "Bedroom" -PassThru

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

    

3. Record test on Meta XR Simulator:

    a. Click **Record & Replay** > **Record** to start a recording. Select a VRS file to record to (for example, `C:\tmp\test_recordings\recording.vrs`).

    b. Interact with the game on Meta XR Simulator as below: get the controller to point to the torch and hit and hold the **U** key on your keyboard, that will grab and light up the torch.

    c. Click **Take Snapshot(s)** button on the Record/replay window. Move around and take another screenshot for another spot of the room.

    d. Click **Stop Recording** button to stop the recording

    e. Close Meta XR Simulator.

    

## Replay to generate the expected test result

The test steps have been recorded in the preceding steps. You will now replay them to ensure the result can be utilized later as the expected outcome. Typically, this task is carried out on developer or QA local computers. These commands can be run alongside the previous script but are separated here for clearer tutorial structure.

Run the following command in Powershell to replay to generate the expected test result: `C:\\tmp\\test_recordings\\replay_target.vrs`

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
    "quit_when_complete" = $true
    "record_path" = "C:\tmp\test_recordings\recording.vrs"
    "replay_path" = "C:\tmp\test_recordings\replay_target.vrs"
}
$jsonObject | ConvertTo-Json -Depth 100 | Set-Content $jsonFilePath

# 2. Launch a bedroom for Synthetic Environment
Start-Process -FilePath "C:/tmp/MetaXRSimulator/MetaXRSimulator\.synth_env_server\synth_env_server.exe" -ArgumentList "Bedroom" -PassThru

# 3. Activate Meta XR Simulator
c:/tmp/MetaXRSimulator/MetaXRSimulator/activate_simulator.ps1

# 4. Launch the game to replay
Start-Process -FilePath "C:\tmp\output\game.exe" -Wait

# 5. Deactivate Meta XR Simulator
C:\tmp\MetaXRSimulator\MetaXRSimulator\deactivate_simulator.ps1

# 6. Stop Synthetic Environment
Get-Process -Name "synth_env_server" | Stop-Process
```

## Run the automation test

In this step, you will execute the test on the Continuous Integration (CI) system and verify the outcome. Typically, this is performed on CI machines, but it can also be executed on QA or developers' local machines to automate smoke testing or bug retesting.

Before commencing, certain items must be copied/installed to the CI machine:

1. The game.exe
2. Recording.vrs and replay_target.vrs
3. Meta XR Simulator
3. Git
4. Visual Studio 2019

To simplify the process, I will copy the entire C:/tmp folder to my CI machine – an AWS instance. Once completed, execute the following command to replay and assess the test results.

**Known issues:** `vrs_pixmatch.py` may fail for some vrs files. Use the provided script at the end of this document to replace `C:/tmp/MetaXRSimulator/MetaXRSimulator/scripts/vrs_pixmatch.py`

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
    "quit_when_complete" = $true
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

# 2. Activate Meta XR Simulator
C:/tmp/MetaXRSimulator/MetaXRSimulator/activate_simulator.ps1

# 3. Launch a Synthetic Environment (skip this step if it is already running)

Start-Process -FilePath "C:/tmp/MetaXRSimulator/MetaXRSimulator\.synth_env_server\synth_env_server.exe" -ArgumentList "Bedroom" -PassThru

Start-Sleep -Seconds 5

# 4. Launch the game
Start-Process -FilePath "C:\tmp\output\game.exe" -Wait

# 5. Deactivate Meta XR Simulator
C:\tmp\MetaXRSimulator\MetaXRSimulator\deactivate_simulator.ps1
# Stop Synthetic Environment
Get-Process -Name "synth_env_server" | Stop-Process

# 6. Compare result

# Prepare requirements.txt for windows:
findstr /v "vrs==1.0.4" C:\tmp\MetaXRSimulator\MetaXRSimulator\scripts\requirements.txt > C:\tmp\tmp.txt
move C:\tmp\tmp.txt C:\tmp\requirements_windows.txt

# Download pyvrs
cd c:/tmp
git clone https://github.com/facebookresearch/pyvrs.git
cd pyvrs
git submodule sync --recursive
git submodule update --init --recursive

# Build and Run Pyvrs via Pixi
# Install pixi (details can be found: https://pixi.sh/latest/#__tabbed_1_2)

iwr -useb https://pixi.sh/install.ps1 | iex

pixi run install_pyvrs

pixi task add run_test "python C:/tmp/MetaXRSimulator/MetaXRSimulator/scripts/vrs_pixmatch.py C:/tmp/test_recordings/replay_new.vrs C:/tmp/test_recordings/replay_target.vrs --threshold 0.4 --best_match_pixels_diff_threshold 40000 --diffs_output_path C:/tmp/test_recordings"

pixi task add pip_install "pip install -r C:/tmp/requirements_windows.txt"
pixi run pip_install
pixi run run_test
```

## Update tests

If your app updates after the test setup, the tests might fail, and you need to update the tests.

1. If the update involves only cosmetic changes, such as a cube changing from red to green, you simply need to verify the test result on the CI. If the screenshot shows the green cube, you can straightforwardly use it as the expected result for subsequent tests.

1. If the game undergoes significant changes, such as game map or interaction mechanism changes, then you'll need to redo the recording and replaying process to update both the recording and the expected results.

## Recommendations

- Some CI machines may not match the performance of our local computers, leading to FPS drops and possibly resulting in inconsistent test results. However, after manually verifying the screenshot comparisons and confirming acceptable matches, consider using the test results as the expected outcome for subsequent runs.

## Known issues

Some known issues exist with `vrs_pixmatch.py`. Replace it with the following script:

```
# (c) Meta Platforms, Inc. and affiliates. Confidential and proprietary.
# -*- coding: utf-8 -*-

import argparse
from pathlib import Path
import numpy as np
import math

import pyvrs

import sys

from PIL import Image
from pixelmatch.contrib.PIL import pixelmatch
from matplotlib import pyplot as plt

class Stream:
    def __init__(self, streamId:str, startTime:int, filtered_reader:pyvrs.filter.FilteredVRSReader) -> None:
        self.streamId = streamId
        self.startTime = startTime
        self.filtered_reader = filtered_reader

# Display the image of the frames passed in for the specified camera angles
def displayFrames(record:  Image, replay: Image) -> None:
    plt.figure(figsize=(20, 10))

    # Create a plot to present each image and
    # adjust the image according to the frames value
    plt.subplot(1, 2, 1)
    image = np.asarray(record)
    plt.imshow(image, vmin=0, vmax=255, cmap="gray")
    plt.title("Record")
    plt.axis("off")

    plt.subplot(1, 2, 2)
    image = np.asarray(replay)
    plt.imshow(image, vmin=0, vmax=255, cmap="gray")
    plt.title("Replay")
    plt.axis("off")

    plt.show()

def getStreams(reader:pyvrs.SyncVRSReader, sId: str) -> [Stream]:
    ret = []

    streamIds = reader.stream_ids
    # print(streamIds)

    for streamId in streamIds:
        if not streamId.startswith(sId):
            continue

        streamIdx = streamId[len(sId)+1]

        # NOTE: This doesn't separate the streams by index, only by type.
        # TODO: Handle multiple image streams in the same VRS file by iterating over reader.stream_ids() to find the matches
        filtered_reader = reader.filtered_by_fields(
                            stream_ids = {streamId},
                            record_types = {'configuration'},
                        )
        startTime = filtered_reader[0].timestamp
        filtered_reader = reader.filtered_by_fields(
                            stream_ids = {streamId},
                            record_types = {'data'},
                        )
        ret.append( Stream(streamId, startTime, filtered_reader) )
        # print(streamId, streamIdx, startTime)

    return ret

def getSnapshots(stream:Stream):
    ret = []
    for record in stream.filtered_reader:
        # print(record)
        meta = record.metadata_blocks[0]
        # print(record.timestamp, meta['iValue'])
        numFrames = int(meta['iValue'])
        ret.append({'numFrames': numFrames, 'timestamp': record.timestamp-stream.startTime})

    return ret

def getTimestamp(stream:Stream, idx: int) -> float:
    # print(idx, len(stream.filtered_reader))
    assert idx < len(stream.filtered_reader)
    record = stream.filtered_reader[idx]
    return record.timestamp-stream.startTime

def getFrameIdx(stream:Stream, timestamp:float):
    nextIdx = 0
    record = None
    while nextIdx < len(stream.filtered_reader):
        record = stream.filtered_reader[nextIdx]
        if timestamp < record.timestamp-stream.startTime:
            return nextIdx
        nextIdx += 1
    # use the last record
    return len(stream.filtered_reader)-1

def getFrame(stream:Stream, frameIndex:int):
    if frameIndex < 0 or frameIndex >= len(stream.filtered_reader):
        return None

    record = stream.filtered_reader[frameIndex]
    # print("getFrame:", frameIndex, record)
    pixels = np.array(record.image_blocks[0])
    # print(record, f'shape={pixels.shape}')
    image = Image.fromarray(np.uint8(pixels)).convert('RGBA')
    # print(image.format, image.size, image.mode)
    return {'image':image, 'timestamp': record.timestamp}

def formatTime(time:float):
    return "{:.3f}sec".format(time)

def compareImages(args, sampleFrame, sampleIdx, matchFrame, matchIdx, prefix):
    img_diff = None
    if(args.diffs_output_path):
        img_diff = Image.new("RGBA", matchFrame['image'].size)

    numPixelsDiff = pixelmatch(sampleFrame['image'], matchFrame['image'], output=img_diff, threshold=args.threshold, includeAA=True)

    if(args.diffs_output_path):
        sampleFrame['image'].save(f"{args.diffs_output_path}/{prefix}-sampleIdx-{sampleIdx}.png")
        matchFrame['image'].save(f"{args.diffs_output_path}/{prefix}-matchIdx-{matchIdx}.png")
        img_diff.save(f"{args.diffs_output_path}/{prefix}-{numPixelsDiff}-diffs-sampleIdx-{sampleIdx}-matchIdx-{matchIdx}.png")

    return numPixelsDiff

def findTheMatchingFrame(frame_stream, frame_name, target_frame_stream, target_frame_name, sampleIdx, max_test_frames, args, minPixelsDiff, bestMatch):
    sampleTS = getTimestamp(frame_stream, sampleIdx)
    sampleFrame = getFrame(frame_stream, sampleIdx)
    matchStartIdx = getFrameIdx(target_frame_stream, sampleTS)

    # search around the frame of interest until the closest match is found

    # find the matching frame in the target_frame_stream
    for matchIdx in range(matchStartIdx, matchStartIdx+max_test_frames):
        matchFrame = getFrame(target_frame_stream, matchIdx)
        if not matchFrame:
            break

        numPixelsDiff = compareImages(args, sampleFrame, sampleIdx, matchFrame, matchIdx, f"{target_frame_name}-{frame_name}")

        print("  compared ", frame_name, sampleIdx, " and ", target_frame_name, matchIdx, ": #px diff=", numPixelsDiff)
        if numPixelsDiff >= minPixelsDiff:
            continue

        minPixelsDiff = numPixelsDiff
        bestMatch = {'sampleStream':frame_name, 'minPixelsDiff':numPixelsDiff, 'sampleFrame':sampleFrame, 'sampleIdx':sampleIdx, 'matchStream':target_frame_name, 'matchFrame':matchFrame, 'matchIdx':matchIdx}

        if(minPixelsDiff == 0):
            break
    return (minPixelsDiff, bestMatch)

def main():

    parser = argparse.ArgumentParser(description='use pixelmatch to diff vrs files')
    parser.add_argument('record', type=str, help="VRS reference recording file to compare against")
    parser.add_argument('replay', type=str, help="VRS replay to check")
    # parser.add_argument('record frames')
    parser.add_argument('--threshold', type=float, required=False, default=0.1, help="threshold over which the pixels are considered to be different")
    parser.add_argument('--sample_location', type=float, required=False, default=0.5, help="where in the group to test: 0=beginning, 0.5=middle, 1=end")
    parser.add_argument('--max_test_frames', type=int, required=False, default=3, help="max number of matches to perform in each direction")
    parser.add_argument('--show_matches', type=bool, required=False, default=False, help="display the corresponding screenshots side-by-side once a match is identified")
    parser.add_argument('--diffs_output_path', type=str, required=False, default=None, help="output folder for image diffs")
    parser.add_argument('--best_match_pixels_diff_threshold', type=int, required=False, default=100, help="threshold on the pixels diff of the best match")

    args = parser.parse_args()

    record_path = Path(args.record)
    record_reader = pyvrs.SyncVRSReader(record_path)

    replay_path = Path(args.replay)
    replay_reader = pyvrs.SyncVRSReader(replay_path)

    # NOTE: only use the first snapshot stream
    record_snapshot_stream = getStreams(record_reader, "400")[0]
    replay_snapshot_stream = getStreams(replay_reader, "400")[0]

    # print(f'snapshots for {record_path}')
    record_groups = getSnapshots(record_snapshot_stream)
    # print(f'snapshots for {replay_path}')
    replay_groups = getSnapshots(replay_snapshot_stream)

    assert len(record_groups) == len(replay_groups)

    print("Comparing", record_path, "with", replay_path, ": num groups=", len(record_groups))

    # NOTE: only use the first frame stream for now
    record_frame_stream = getStreams(record_reader, "8003")[0]
    replay_frame_stream = getStreams(replay_reader, "8003")[0]

    groupOffset = 0
    for groupIdx in range(0, len(record_groups)):
        assert record_groups[groupIdx]['numFrames'] == replay_groups[groupIdx]['numFrames']
        numFrames = record_groups[groupIdx]['numFrames']

        sample_location = args.sample_location
        sampleIdx = groupOffset+int(numFrames*sample_location)
        max_test_frames = args.max_test_frames

        print("Group", groupIdx, ":numFrames=", numFrames,"sampleIndex=", sampleIdx)

        minPixelsDiff = 100000000000
        bestMatch = None

        # first check the replay against the record
        (minPixelsDiff, bestMatch) = findTheMatchingFrame(record_frame_stream, "record", replay_frame_stream, "replay", sampleIdx, max_test_frames, args, minPixelsDiff, bestMatch)

        if minPixelsDiff > args.best_match_pixels_diff_threshold:
            print("best match diff is > ", args.best_match_pixels_diff_threshold, " so matching record against replay")
            # then check the record against the replay
            (minPixelsDiff, bestMatch) = findTheMatchingFrame(replay_frame_stream, "replay", record_frame_stream, "record", sampleIdx, max_test_frames, args, minPixelsDiff, bestMatch)

        # report the best match
        if bestMatch:
            print(f" Best sample is {bestMatch['sampleStream']}#{bestMatch['sampleIdx']} ts={formatTime(bestMatch['sampleFrame']['timestamp'])}, best match is {bestMatch['matchStream']}#{bestMatch['matchIdx']} ts={formatTime(bestMatch['matchFrame']['timestamp'])}, #px diff={bestMatch['minPixelsDiff']}")
            if(args.show_matches):
                displayFrames(bestMatch['sampleFrame']['image'], bestMatch['matchFrame']['image'])
            if(bestMatch['minPixelsDiff']> args.best_match_pixels_diff_threshold):
                print(" The best match's pixels difference exceeds the threshold of ", args.best_match_pixels_diff_threshold, ". So this is a mismatch")
                sys.exit(-1)
            else:
                print(" The best match's pixels difference is less than the threshold of ", args.best_match_pixels_diff_threshold, ". So this is a match")
        else:
            print(" no match found!")
            sys.exit(-2)

        groupOffset += numFrames

    return 0

if __name__ == "__main__":
    main()

```