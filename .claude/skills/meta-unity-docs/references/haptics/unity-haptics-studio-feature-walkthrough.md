# Unity Haptics Studio Feature Walkthrough

**Documentation Index:** Learn about unity haptics studio feature walkthrough in this documentation.

---

---
title: "Haptics Studio Feature Walkthrough"
description: "Explore the authoring tools and design capabilities available in Haptics Studio for Meta Quest controllers."
---

## Overview

By the end of this guide, you will be able to:

- Analyze audio files and fine-tune parameters for haptic conversion
- Create haptic files from scratch using freeform design tools
- Edit haptic designs using the various amplitude, emphasis, and frequency features
- Organize and share your haptic files

## Design Tools
Outside of the main editor window, you have two places where you can access and edit your design tools.

- **Right panel** - houses the analysis/parameter settings, design information such as emphasis points and clip duration, and the marker library.
- **Tool Bar** - your quick-access option for the various editing tools you need to refine your designs.

## Audio Analyzer

If you choose to start designing your haptic effect from a sound file, you must first convert an existing audio file to haptic envelopes, after which you can make manual edits and fine-tune the design.
The algorithm is run automatically every time you start a new project or add a new audio file to an existing project. It will automatically generate both amplitude and frequency envelopes, as well as emphasis breakpoints.
To adjust the output of the algorithm, you can change its parameters by opening Analysis in the right panel. Each change of an analysis parameter will automatically trigger the algorithm to run and generate a new output.

This step helps make the haptic experience as close as possible to your design intent. In some cases, no further manual editing is necessary.

### Amplitude

The Gain and Resolution parameters control the amplitude envelope, allowing you to fine-tune the intensity and detail of the haptic output.

#### Gain

The Gain parameter adjusts the overall amplitude of the haptic output. By increasing or decreasing the gain, you can make the haptic feedback more intense or softer. This parameter is useful for adjusting the overall strength of the haptic effect to match your design intent.

#### Resolution

The Resolution parameter controls the level of detail in the amplitude envelope. A lower resolution value will result in a smoother envelope with fewer breakpoints, while a higher resolution value will generate a more detailed envelope with more breakpoints. This parameter is useful for adjusting the level of detail in the haptic feedback to match the audio file's dynamics.

### Emphasis

The Sensitivity and Reduction parameters control the automatic creation of emphasis points in the haptic clip's amplitude envelope.

#### Sensitivity

When converting audio to haptics, the algorithm detects quick changes in energy in the audio signal. These are well suited to becoming emphasis breakpoints. The sensitivity slider allows you to adjust the number of emphasis points that are added to the amplitude envelope. Emphasis points can either replace short rises in the amplitude envelope, such as a click, or they can add texture on top of a longer modulation, such as an engine sputtering while running.

#### Envelope Reduction

The Envelope Reduction parameter controls the intensity of the underlying amplitude breakpoint that accompanies the emphasis breakpoint. By adjusting this parameter, you can change the perceived intensity of the emphasis point relative to the continuous vibration from the amplitude envelope. A low reduction value will make the emphasis point stand out less, while a high reduction value will make it more prominent.

### Frequency

The final set of analysis parameters control how the audio signal's frequency content is mapped to the haptic frequency envelope.

#### Audio Filter

The Audio Filter range slider allows you to specify a frequency window from which the analysis algorithm should extract the frequency envelope from the audio. The algorithm will then search for the strongest frequency within that window. This feature is useful when working with audio assets used in a game. It allows you to eliminate high-frequency content from the source audio, that should be heard but not felt, and focus on tracking lower frequencies.

#### Haptic Output

The Haptic Output range slider controls how the extracted frequency modulation from the audio signal is mapped to the frequency envelope. By adjusting this parameter, you can fine-tune the range of vibrations produced by the haptic output. For example, setting the range from 0-100% will produce wide, sweeping frequency modulation, while clamping the value from 0-20% will produce only lower, smoother vibrations. Similarly, clamping the value from 80-100% will produce only higher, sharper vibrations.

## Envelope Selection

You can switch between Amplitude and Frequency Envelopes using the toggle at the bottom of the editor window (keyboard shortcut F).

## Stereo Split

If you have a stereo audio file, you can split the output haptic file into left and right channels. Right-click on the clip and select "Split channels".

## Freeform Haptics / Pen Tool

The Pen tool allows for quick prototyping and drawing simple haptic effects without needing an audio track. It is designed much like a vector drawing tool, allowing you to plot and shape your envelope by clicking within the editor to add or draw points. The Pen tool is automatically selected when creating a new freeform clip. The initial point will be set at (0, 0). If you want to start the haptic envelope at a different value, place the starting point by clicking on the right side of the Y axis. If you want to switch to the Pen tool at any time, use the keyboard shortcut P.

Click on the canvas to place your first point. This begins filling the amplitude envelope. Press Enter when you're done to complete the envelope.

When designing freeform haptics, a default frequency envelope will be created. To adjust it, navigate to the frequency envelope using the toggle switch and edit the curve as you did in the first envelope.

## Infinite Canvas

The editor provides an infinite canvas, allowing you to draw freely. The length of your clip is defined by the position of the last set breakpoint.

For more precise control over the clip length, you can specify the exact duration of your haptic clip by entering a value for the last breakpoint into the time input field in the right panel.

## Batch Analysis

Quickly adjust analysis parameters for multiple selected clips at once, streamlining your workflow. Select all clips that you want to edit and adjust the parameters in the Analysis panel on the right.

## Editing Breakpoints

The second step of the editing process is manual breakpoint editing. Haptics Studio allows you to fine-tune and edit the amplitude envelope, emphasis points, and frequency envelope. Manual editing is typically done to make minor adjustments before finalizing the haptic design. This feature gives you precise control over the haptic output, allowing you to make subtle changes to the amplitude, emphasis, and frequency to achieve the desired feel.

### Adding Breakpoints

To add a breakpoint in Haptics Studio, select the Pen tool from the Tool Bar (keyboard shortcut P) and move your cursor between two existing breakpoints or in the empty space at the end of the haptic envelope. The Pen tool will show a preview of the updated envelope that you can confirm by clicking.

### Removing Breakpoints

To remove one or more breakpoints, first select the breakpoint(s) you wish to remove. Once selected, press the Delete button on your keyboard.

### Changing Breakpoints

To modify a breakpoint, click and drag it around. If you want to edit multiple points you can click on them by holding the Shift key, or create a multiselection by clicking and dragging a selection rectangle. Once selected, the breakpoint can be dragged to a new time position or value. If you would like to keep the current time position of the breakpoint but modify its value, you can press and hold the Shift key while dragging. Note that if the Pen tool was previously enabled, selecting multiple points will revert to the Selection tool.

### Setting Breakpoints by Value

To set a breakpoint at a specific time or value, you can manually enter numeric values in the Design tab in the right panel.

### Multi-Selecting Breakpoints

To select multiple breakpoints, left-click and drag over the breakpoints you want to select. After releasing the mouse button, you can shift the breakpoints' position in time, or scale their values by dragging the handles on the top or bottom of the dotted bounding box.

### Cut, Copy and Paste Breakpoints

You can cut or copy one or more breakpoints using keyboard shortcuts or the Edit menu. After pasting, the breakpoint(s) will appear in the center of the edit view. You can then drag them left or right to reposition them in time. You can also paste your selection in place, this will automatically paste the copied portion of the envelope in its original position in time.

## Editing Emphasis Points

### Creating an Emphasis Point

To create an emphasis breakpoint, select the Emphasis tool from the Tool Bar (keyboard shortcut E) and move your mouse over the amplitude envelope. The Emphasis tool will show a preview of the updated envelope that you can confirm by clicking. Note that an emphasis point is composed of an emphasis intensity and frequency, and it's attached to an amplitude point. The Emphasis tool will either create an emphasis point on an existing amplitude breakpoint if the mouse is hovering over one, or create a new amplitude breakpoint instead.

### Removing an Emphasis Point

To remove an emphasis breakpoint, select it and click the bin icon in the pop-up that will appear, or in the right panel. You can also remove multiple emphasis points at once by selecting multiple points and clicking the bin icon in the right panel.

### Changing the Intensity of an Emphasis Point

An emphasis point will automatically create an underlying point for the continuous vibration. You can adjust the intensity of both the emphasis breakpoint and underlying amplitude breakpoint by selecting and dragging.

### Changing the Sharpness of an Emphasis Point

To change the sharpness of an emphasis point, select the top point and a pop-up will appear at the top of the editor. Selecting the rounded icon on the left of the popup will create a soft emphasis point, while the one on the right will create a sharper emphasis point.

## Trim

To export a haptic clip with a shorter duration than the original clip, select the Trim tool from the Tool Bar (keyboard shortcut T). The editor view will move to the end of the clip, and a trim bar will appear. You can adjust the new clip duration by dragging the trim bar to the left, or by entering the desired clip duration in the "New Clip Duration" field on the right panel. You can confirm the new duration with Enter, the **Confirm** button, or the checkmark icon in the trim widget above the trim line. Once a trim point is set the exported file will have that exact duration, all points after that time mark will be ignored.

### Editing the Trim Position
You can edit an existing trim point by clicking on the "New Clip Duration" field in the Design tab.

### Reverting the Trim Operation
You can revert the trim operation by clicking the Revert button in the Design tab of the right panel, or by clicking the bin in the trim widget while editing the trim line.

## Best Practices

Read the [Haptic Design Guidelines](/design/haptics-overview) or check out the Advanced Editing Tips tutorial to learn more about best practices when designing haptics for your apps.

## Learn More

Return to the [Get Started](/documentation/unity/unity-haptics-studio-get-started/) section to learn how to install and set up Haptics Studio, or visit the [Troubleshooting](/documentation/unity/unity-haptics-studio-troubleshooting) page if you are experiencing issues.