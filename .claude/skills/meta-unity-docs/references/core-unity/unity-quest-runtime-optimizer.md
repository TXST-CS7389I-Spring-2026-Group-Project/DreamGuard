# Unity Quest Runtime Optimizer

**Documentation Index:** Learn about unity quest runtime optimizer in this documentation.

---

---
title: "Meta Quest Runtime Optimizer"
description: "Monitor and optimize Meta Quest app performance using the Runtime Optimizer tool for bottleneck detection and resolution."
last_updated: "2026-02-17"
---

<oc-devui-note type="note">
  This tool is currently in its experimental phase. Meta is actively gathering feedback from users to shape its future.
</oc-devui-note>

The Meta Quest Runtime Optimizer helps developers identify and resolve performance bottlenecks in Meta Quest applications. This tool provides real-time analysis and actionable insights to optimize application performance, and runs without additional dependencies.

## Requirements

- **Meta Horizon OS**: V78+
- **Unity engine version**: Unity 2022.3+
- **Platform OS**: Windows 10+ (64-bit)
- Unity ID: [Create or log in to your Unity account](https://id.unity.com/)

## Installation

1. Navigate to [Meta Quest Runtime Optimizer package](https://assetstore.unity.com/packages/slug/325194) in the Unity Asset Store and click **Add to my assets**.
1. In Unity, navigate to **Window** > **Package Management** > **Package Manager**.
1. Select **My Assets** and add the **Meta Quest Runtime Optimizer** package.

## Setup and configuration

### Step 1: Open the Meta Quest Runtime Optimizer

Navigate to **Meta** > **Tools** > **Runtime Optimizer** in Unity.

### Step 2: Enable the Meta Quest Runtime Optimizer

Toggle **Runtime Optimizer Enabled** on. This updates your project to include all necessary configurations.

<oc-devui-note type="note">
  Disable this option when submitting your application for release candidate (RC) builds.
</oc-devui-note>

### Step 3: Build your APK

Build your APK in **development mode**.

### Step 4: Verify the APK path

Ensure that your APK's Executable path looks correct. For example: `com.unity.template.vr/com.unity3d.player.UnityPlayerGameActivity`

### Step 5: Connect the tool

Click **Launch** to start your application. The tool will automatically connect to your device.

**Note:** If you start the application a different way, connect to the tool by clicking **Connect**.

You should see the "Connected" status message if it successfully starts.

### Step 6: Freeze the frame to start the analysis

Click **Freeze Frame** to freeze the current frame. This allows the tool to pause the current state of the application and a snapshot of the current frame is taken.

Once successfully frozen, you should see the **Freeze Frame** button update to **Unpause** and the **Analyze** button will be enabled.

## Using the Meta Quest Runtime Optimizer

The Runtime Optimizer offers two analysis modes:

- Bottleneck analysis
- What if? analysis

### 1. Bottleneck analysis

This mode parses captured runtime data, highlights potential bottlenecks, and provides suggestions for improvement.

<oc-devui-note type="important">
  Ensure your headset remains active during the analysis. If the device enters sleep mode during capture, the analysis will be interrupted and you'll need to restart the capture.
</oc-devui-note>

Use the dropdown menu and select **Bottleneck analysis**. Then, click **Analyze** to start the analysis.
Once the analysis is complete, click the capture in the **Captured Frames** section to view the data in the **Analysis** section.

#### Analysis sections

- **Insights**: Provides a summary of the captured metrics with actionable items for developers to further experiment with and reduce performance bottlenecks.
- **Render Breakdown**: Organizes different runtime data (Mesh, Texture, Material) as Unity Entities and ranks their cost. This process integrates several different types of real-time metrics to create actionable suggestions.

The tool also allows developers to add custom rules catered to their specific needs, providing flexibility and customization.

### 2. What if? analysis

The "What if?" analysis helps you understand the cost of all game objects in your frustum at a high level of accuracy. This is achieved by performing a sweeping A/B experiment, which mimics a flow where developers manually disable an object and rebuild their APK to figure out its impact.

Use the dropdown menu and select **What if? analysis** then click **Analyze** to open the **Select Game Objects to Test** window.

The window displays all game objects in your frustum. You can select multiple game objects to test at once and use **Search** with keywords to filter the list.

Once you've selected the game objects you want to test, click **Scan Selected Game Objects** to begin the analysis.
The result will be shown in the **Analysis** section.

#### How it works

1. When starting the analysis by clicking **Freeze Frame**, the optimizer freezes the headset to guarantee experiment reproducibility.
2. Measures the baseline cost of what's in view.
3. Iteratively disables all game objects in the frustum while measuring the before and after of a frame's GPU time.
4. Ranks all GameObjects by their cost in descending order.

The tool takes measurements over several frames and later averages them. The test takes approximately 200ms per GameObject in your frustum.

## Material and shader analysis

For more advanced usage, download and enable the [Qualcomm Adreno Offline Compiler tool](https://qpm.qualcomm.com/#/main/tools/details/Adreno_GPU_Offline_Compile). This tool displays detailed material and shader analysis, enabling deeper discovery of graphics bottlenecks.

## Known issues

- **OS V78**:
  - Very rare kernel panics after What if? analysis
  - Dynamic objects are not supported and do not show up in What if? analysis or Bottleneck analysis