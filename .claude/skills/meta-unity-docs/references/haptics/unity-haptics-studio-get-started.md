# Unity Haptics Studio Get Started

**Documentation Index:** Learn about unity haptics studio get started in this documentation.

---

---
title: "Get Started With Haptics Studio"
description: "Install and set up Meta Haptics Studio to design custom haptic effects for your app."
last_updated: "2026-01-26"
---

## Overview

By the end of this guide, you will be able to:

- Understand how to access learning material such as in-app tutorials, video tutorials, and example projects to guide you through the basics of haptic design.
- Know how to create your first project and access example projects.

## Haptic Design

Designing haptics is a relatively new field to most creators, application and game developers. To help you get started, we've curated diverse learning resources introducing you to the editor and the world of haptic design. On the Haptics Studio Desktop home screen, click the Learning tab on the left to navigate to the Learning section.

### In-App Tutorials

Our in-app tutorials are designed to guide you through the fundamental concepts of haptic design in an interactive way. To get the most out of our tutorials, make sure your headset is connected and ready to go. By doing this, the tutorial can seamlessly sync with your headset, allowing you to test and experience the haptics in real-time. Simply put on your headset, trigger one of the clips, and verify that everything is working as expected. If you encounter any issues, refer to the [troubleshooting guide](/documentation/unity/unity-haptics-studio-troubleshooting).
As you proceed through the tutorials, your progress will be tracked, allowing you to pick up where you left off at any time. The panel on the left displays the content and guides you step-by-step through the different concepts. Each topic has Next and Back buttons to continue forward or go backward.

The tutorials cover the following topics:

- **Amplitude Modulation:** how changing the amplitude of a haptic clip can vary its intensity on the skin.
- **Frequency Modulation:** how changing the frequency of a vibration can alter the vibration feedback from a soft rumble to a sharp buzz.
- **Emphasis Points:** how using sharp emphasis parameters can create short succinct clicks.
- **Creative Analysis:** how you can use the built-in parameters of our audio analysis to quickly make significant changes to the haptic experience.
- **Freeform Haptics:** how to create custom haptic effects from scratch using the Pen tool.

### Learning Resources

For a more visual approach, our [tutorials on YouTube](https://www.youtube.com/watch?v=wsJRVtlWFOc) cover topics such as the haptic design workflow and advanced editing techniques. Explore our [GitHub repository](https://github.com/oculus-samples/haptics-studio-examples/) for example projects that demonstrate best practices and creative applications of haptic design. Additionally, explore our haptic design guidelines and best practices to further enhance your skills and knowledge.

## Basics of Haptic Design

### Haptic Envelopes

When designing haptics with Haptics Studio, you can control the motor that produces vibration feedback using three fundamental methods: Amplitude Envelope, Frequency Envelope, and Emphasis Points.
Each envelope consists of a series of points over time, called breakpoints. You can edit these breakpoints by moving them in time and change their values, or add new ones using the Pen tool.

#### Amplitude Envelope

The first and most important method for designing haptics is the amplitude envelope. The amplitude envelope controls the strength or intensity of the vibration over time, modulating the amount of force that the motor is creating as it moves. See this simple triangle amplitude envelope with three breakpoints. In this example, we cause the force of the vibration to slowly rise and then descend:
It's like adjusting the volume of a speaker - you can make the vibration louder or softer by changing the amplitude. For example, you might use an amplitude envelope to create a gentle, soothing vibration for a relaxing experience, or a strong, intense vibration for a more energetic effect.

#### Frequency Envelope

The frequency envelope controls the speed of the vibration over time. It's like adjusting the pitch of a sound - you can make the vibration faster or slower by changing the frequency. For example, you might use a frequency envelope to create a slow, rumbling vibration for a dramatic effect, or a fast, buzzing vibration for a more playful feel.
Similar to the above example, here is how a simple triangle frequency envelope will affect the vibration output:

#### Emphasis Points

So far, we've been controlling continuous vibration by modulating amplitude and frequency. But what if we want a short and crisp haptic feedback that only lasts tenths of a second, like a sharp button click? That's where emphasis points come in.

An emphasis point is a short, momentary haptic sensation that can be used to create a sharp, distinct feeling. Like breakpoints on the amplitude envelope, the intensity of an emphasis point can be adjusted to create a softer or sharper click.
For example, the buttons on the side of our phones have a short, crisp click when we press them, while older computer keyboards might have softer clicks.

## Set Up Meta Haptics Studio

### Windows

1. Download [Meta Haptics Studio for Windows](/downloads/package/meta-haptics-studio-win) from the Meta Quest Downloads page.
2. Unzip the file.
3. Run Meta Haptics Studio.exe. Studio will run after the installation is complete.
4. When Meta Haptics Studio runs for the first time, Windows Defender will ask for permissions to allow the application to access the network. Check all of the boxes to allow communication with your VR headset over the local area network.

### Mac

1. Download [Meta Haptics Studio for Mac](/downloads/package/meta-haptics-studio-macos) from the Meta Quest downloads page.
2. Unzip the file.
3. Open Meta Haptics Studio.dmg.
4. Drag and drop Meta Haptics Studio.app to the Applications folder.
5. Open the Meta Haptics Studio application from the Applications folder.

Next, either create your first haptic clip, start the tutorial, or browse our haptic library.

## Set Up VR Companion App

In order to audition haptics on your Quest device, you need to install the [VR Companion app](https://www.meta.com/experiences/meta-haptics-studio/6759764157450104/) onto your headset.

**Note:** Your data is only sent directly between the Haptics Studio Desktop and VR application via Wi-Fi or USB cable. It is never shared or uploaded to the cloud.

1. Finish the installation of Haptics Studio Desktop on your Mac or PC and open the app.
2. Click the headset icon on the top right, this will open the connection panel.
3. Click Open Meta Store
4. Install the [VR Companion app](https://www.meta.com/experiences/meta-haptics-studio/6759764157450104/).

    

5. Launch the app on your headset.
6. To connect the VR App and Desktop Application, make sure both your computer and headset are connected to the same network. For best results, you should disable your VPN connections and firewalls temporarily as they can interfere with the auto discovery. Alternatively, you can connect your headset to your desktop with a USB cable and select "USB Connection" in the VR app.

7. On your headset, in the connection assistant on the left hand side you should find your computer by name. Select it and click Connect. You will be prompted for a 4 digit code.

    

8. On your Desktop, the Connection Panel  should now display the 4 digit code.
9. Enter this code in the VR Companion app. If you want the pairing state to persist, please make sure the Remember Device switch is turned on before you select the Connect button.

**Tip:** If your headset is connected via USB and in Developer Mode, you can switch HMD Sleep Mode on or off using the toggle button. This will prevent your HMD from falling asleep and disrupting your workflow.

The connection should now be established. You can check the connection status at the headset icon on the top right of Haptics Studio Desktop.

## Creating Your First Project

### Starting with an Example Project

To get you started quickly you can find example projects containing curated haptic clips based on [Oculus Audio Pack](/downloads/package/oculus-audio-pack-1/) content.
You can access these example projects in the Learning Section. The haptic clips are free to use and licensed under Creative Commons, providing a great resource for you to learn how to achieve different types of effects.

### Starting a New Project

In Meta Haptics Studio, you can create haptic effects either based on a pre-existing audio file or from scratch.
Projects in Haptics Studio can contain either a single haptic clip or multiple haptic clips.

1. **Start a New Project:** On the home screen of the Haptics Studio Desktop, click 'New project'. You will then be able to choose how you would like to start designing haptic effects, either from audio or from scratch using the freeform flow.

2. **Design From Audio:** Locate and open existing audio files (.wav, .ogg, .mp3, .aiff) that you want to use as the basis for your haptic designs. If you don't have any audio files, you can download the [Oculus Audio Pack](/downloads/package/oculus-audio-pack-1/) to use as example content.

    a. **Audio Analysis and Haptic Generation:** Once an audio file is imported, the Haptics Studio Desktop App automatically analyzes the audio and generates haptic effects from it.

    **Note**: If the audio file is multichannel, it will be automatically down-mixed to mono to create a single haptic clip. If you prefer to generate haptics for each channel separately, you can right click on the stereo clip and select "Split channels".

    b. **Haptic Envelopes:** After the haptic effects are generated, you will see two main curves representing the "Amplitude" and "Frequency" envelopes of the haptic experience. These envelopes can be viewed and switched between using a toggle in the bottom bar of the editing window. To deepen your understanding of amplitude and frequency envelopes and their impact on the haptic experience, you can access the Haptics Tutorial from the home screen of the app.

3. **Design Freeform Haptics:** Some scenarios might not require an audio file, such as a short UX/UI haptic effect. The Pen tool allows you to create precise and simple haptic effects by drawing your own custom envelope. Simply select the Pen from the Tool Bar (keyboard shortcut P) and start drawing envelopes.

**Haptic Envelopes:** When you start with the Freeform Flow, you will automatically be drawing an amplitude curve. A default frequency envelope will be created alongside your amplitude, which you can further edit and refine using the Pen tool to create/adjust breakpoints.

4. **Connect Your Headset**: Ensure your headset is connected to be able to audition your creations. Check your connection by clicking on the headset icon top right.

5. **Audition the Haptic Feedback**: Launch the Haptics Studio VR App on your headset. You can trigger the haptic feedback by pressing the B/Y buttons on your controller or by using the play button within the app.

    **Note**: To streamline your design process, you can audition your haptic designs directly from the desktop app without needing to constantly put on and take off your headset. Simply select a clip in the app and press the trigger on your controller to test your design in real-time.

6. **Edit/Refine Haptics**: In the Haptics Studio Desktop App, you can either adjust the analysis parameters or proceed with manual breakpoint editing. You can feel the changes directly on your controllers.

7. **Export Your Haptic Clip**: When you are finished designing your haptics, you can export your clips. The exported .haptic files can be integrated into your projects using the Haptics SDK ([Unity](/documentation/unity/unity-haptics-sdk), [Unreal](/documentation/unreal/unreal-haptics-sdk)) or [FMOD](https://fmod.com/docs/2.03/studio/instrument-reference.html#haptics-instrument).

## VR Companion App

### Connect Your HMD

To connect your Quest Device to Meta Haptics Studio Desktop, follow the steps described in the above section [Set Up](/documentation/unity/unity-haptics-studio-get-started/#set-up-vr-companion-app).

### Homescreen

From the Home screen, you can:

- Open the currently open project in Studio Desktop App
- Open one of your Pinned Projects
- Explore the Haptic sample library

### Navigating a Project

Use the thumb stick to navigate the clip list. Select a clip using the trigger or the A/X buttons, then use B/Y to play the selected clip.

### Haptic Sample Library

The Haptic Sample Library can be accessed during onboarding or at any time by clicking the Library icon in the top-right corner of the interface. Browse curated haptic effects for inspiration and quick use in your projects.

### Pinning and Unpinning Projects

To add a project to your Pinned Projects list, open a project and click the pin icon next to the project name. This will store your project on the HMD locally without the need for an active connection to Meta Haptics Studio desktop.
To unpin a project, open the project from the pinned projects list and click the pin icon.

## Export Haptic Designs

1. **Select the clip(s):** Choose the clip or clips you want to export in the clip list.
2. **Export:** Go to File > Export Selected or click the 'Export' Button on the bottom right of the screen.
3. **Choose file format:**
    - **For Unity, Unreal and/or FMOD Engine integration:** Select `.haptic`.
    - **For iOS integration:** Select ``.ahap.``  To ensure that your haptic files are optimized for playback on Apple devices, your clip will be exported as two files: `TRANSIENTS.AHAP` and `CONTINUOUS.AHAP`. For more information on how to [play](https://developer.apple.com/documentation/corehaptics/chhapticpattern/init(contentsof:)) these files, please refer to the [Apple Developer Documentation](https://developer.apple.com/documentation/corehaptics).
    - For other platforms that accept audio waveforms: Select waveform (.wav).
4. **Save location:** Choose a location to save the exported file.
5. **To view** exported clip(s) on your disk click 'View files'.

Once you've completed these steps, you can integrate the exported files into your game or application using the Haptics SDK ([Unity](/documentation/unity/unity-haptics-sdk), [Unreal](/documentation/unreal/unreal-haptics-sdk)).

## Manage and Share Projects

### Grouping Clips

To better navigate your clips in a project, you can create groups. Select the clips you want to group and right-click or use the shortcut (Command-G/Ctrl-G).

### Organizing Clip with Markers

Markers can be used to label and navigate specific points in your project, making it easier to organize and reference important moments.
You can create a marker by enabling the Marker tool (keyboard shortcut M) and clicking on the point that you want to annotate.
In the Marker tab in the right panel you can add a description or name or delete it by clicking the bin icon. You can also move a marker position by dragging the icon horizontally in the editor area.

### Sharing a Project

To share your project, click on the share icon on the top right of the editor. This will package your project with all haptics and audio resources as a shareable .zip file. This ensures that all necessary files are included, making it easy for others to work on the project without issues.

## Learn More

Explore all the Haptics Studio features in the [Feature Walkthrough](/documentation/unity/unity-haptics-studio-feature-walkthrough).