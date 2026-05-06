# Ts Mqdh Media

**Documentation Index:** Learn about ts mqdh media in this documentation.

---

---
title: "Debugging Tools"
description: "Capture screenshots, record videos, and cast your Meta Quest headset display through MQDH."
last_updated: "2025-02-26"
---

During a debugging session, interacting with the headset without wearing it can come in very handy. Meta Quest Developer Hub (MQDH) offers tools to accelerate debugging and iteration by letting you capture screenshots, record videos, cast your headset to computer, and launch browser in headset, all from MQDH.

## Take a screenshot

Capture monoscopic or stereoscopic screenshots of the live display of your headset with a single click, which avoids typing long commands or pressing combination of keys.

1. [Connect your headset with MQDH](/documentation/unity/unity-quickstart-mqdh/#connect-headset-to-meta-quest-developer-hub).
2. Run your app on the connected headset.
3. On **Device Manager**, under **Device Actions**, click **Capture** or press CTRL + Shift + S keyboard shortcut to capture the screenshot.
4. To change the setting between stereoscopic and monoscopic, click the gear icon next to **Capture**, and in the **Screenshot Type** list, select **Single Eye** for monoscopic screenshot or **Both Eyes** for stereoscopic screenshot. By default, MQDH captures monoscopic screenshot.

MQDH opens the screenshot in a new window, from where you can copy the screenshot to the clipboard as well as take another screenshot. All screenshot images are stored locally on your computer in the following folder, depending on your operating system:

* **Windows:** `C:\Users\janedoe\AppData\Roaming\odh\captures`
* **macOS:** `/Users/janedoe/Library/Application Support/odh/captures`

{:width="600px"}

## Record a video

Record a monoscopic or stereoscopic MP4 video from your headset for a maximum of three minutes and use it for debugging or in marketing collaterals. Audio is not recorded with the video.

1. [Connect your headset with MQDH](/documentation/unity/unity-quickstart-mqdh/#connect-headset-to-meta-quest-developer-hub).
2. Run your app on the connected headset.
3. On **Device Manager**, under **Device Actions**, click **Record** or press CTRL + Shift + R keyboard shortcut to start and stop the recording.
4. To change the settings for the recording, such as switching between stereoscopic and monoscopic, click the gear icon next to **Record**.
5. In the **Video Record Settings** window that opens:
    - Under **Eye View**, select **Single Eye, Stereo Audio** for monoscopic recording or **Both Eyes, No Audio** for stereoscopic recording. By default, MQDH records monoscopic video.
    - Under **Resolution**, select the resolution and frames per second (fps), for example, **1024p @ 36fps (1:1)** (default), **1080p @ 36fps (16:9)**, and so on.
    - Under **Bitrate**, choose the bitrate, which ranges from **5 Mbps (very low quality)** (default) to **40 Mbps (very high quality)**.
    - Click **Save**.

MQDH opens the recording in a video player, from where you can play the video or share it externally. All recordings are stored locally on your computer in the following folder, depending on your operating system:

* **Windows:** `C:\Users\janedoe\AppData\Roaming\odh\captures`
* **macOS:** `/Users/janedoe/Library/Application Support/odh/captures`

You can also disable auto-opening the video player in **Video Record Settings** by unchecking the checkbox.

{:width="600px"}

## Cast headset footage

Cast provides a reliable and high-quality Quest-to-Desktop casting experience, enabling you to easily stream and record VR gameplay footage with others.

### Start casting

1. [Connect your headset with MQDH](/documentation/unity/unity-quickstart-mqdh/#connect-headset-to-meta-quest-developer-hub).
2. Navigate to the **Device Manager**. Under the **Device Actions** section, select **Cast** to start casting.
3. Once the casting window appears, use the menu bar at the top. This tool offers several options to customize your casting experience.
4. To stop casting, either close the casting window or click the stop icon next to **Cast Device**.

**Note:** While casting, MQDH turns off the proximity sensor automatically. Once you stop casting, MQDH reactivates the proximity sensor to prevent unexpected battery drainage.

### Change aspect ratio

You have the option to switch between different aspect ratios while casting and recording. To do so, select the dropdown menu that shows **Original (1:1)**.

* **Original (1:1)** - This is the uncropped image from the compositor which is roughly square (per eye).
* **Cropped (16:9)** - This is a widescreen view created by vertically center-cropping the top and bottom of the source image.
* **Cinematic (16:9)** - This is a widescreen view created by increasing the horizontally rendered FOV by around 30 degrees.
  * Selecting this aspect ratio requires restarting any active immersive applications.
  * The perceived horizontal resolution inside the headset will decrease slightly.
  * This may impact in-game performance due to rendering additional geometry.

### Cast wirelessly

Enjoy the convenience of wireless casting and stream the headset display directly to your computer.

1. Ensure your headset and desktop are connected to the same Wi-Fi network.
2. On **Device Manager**, under **Device Actions**, toggle on **ADB over Wi-Fi** to enable wireless casting.
3. When **ADB over Wi-Fi** is turned on, casting sessions will automatically use a wireless connection when possible.
4. To stop casting wirelessly, simply toggle off **ADB over Wi-Fi** and start a new casting session.

### Take a screenshot

While casting, capture uncompressed, full-resolution screenshots of your headset with a single click.

1. Start a casting session.
2. In the casting window, click the camera button to capture a screenshot.
3. Screenshots are stored on your headset and you can access them by navigating to **File Manager** > **On Device** > **Images**.

### Record a video

Cast allows you to cast and record high quality videos simultaneously for debugging or marketing purposes. Footage captured using the method below is encoded at the source resolution. This results in a noticeably higher bitrate and smoother frame pacing, as opposed to capturing the screen of the casting window, for instance, using OBS.

1. Start a casting session.
2. If you want both system and gameplay audio included in your recording, make sure the speaker icon is not muted. To enable audio, click the speaker icon so there is no line through it. To disable audio, click the speaker icon so a line appears through it.
    * **Note:** Microphone audio is not yet supported.
3. In the casting window, click the camcorder button to start recording and click again to end recording.
4. Videos are stored on your headset and you can access them by navigating to **File Manager** > **On Device** > **Videos**.

### Enable input forwarding

To control your headset in a casting session with the precision of your mouse and keyboard, you can forward input from your computer keyboard.

1. Start a casting session in MQDH.
2. Click the mouse icon to enable input forwarding.
3. Click the keyboard icon to start forwarding text input. When text input forwarding is active, keystrokes from your computer keyboard are sent directly to the headset as text input. This is useful for typing into text fields, search bars, or any on-screen keyboard within the headset without needing to use the VR keyboard or controllers.

   **Note**: Enabling text input forwarding captures keyboard input for text entry, which disables the movement key bindings listed below (WASD, arrow keys, etc.). To regain device control, click the keyboard icon again to disable text input forwarding.

   You can now control your headset with the following key bindings:

    <table>
      <thead>
        <tr>
          <th>Action</th>
          <th>Key Binding</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>Look Around</td>
          <td>Mouse2 Drag</td>
        </tr>
        <tr>
          <td>Move Forward</td>
          <td>W, ↑</td>
        </tr>
        <tr>
          <td>Move Right</td>
          <td>D</td>
        </tr>
        <tr>
          <td>Move Backward</td>
          <td>S, ↓</td>
        </tr>
        <tr>
          <td>Move Left</td>
          <td>A</td>
        </tr>
        <tr>
          <td>Turn Right</td>
          <td>→</td>
        </tr>
        <tr>
          <td>Turn Left</td>
          <td>←</td>
        </tr>
        <tr>
          <td>Fly Upward</td>
          <td>Q, Space</td>
        </tr>
        <tr>
          <td>Fly Downward</td>
          <td>E, Ctrl</td>
        </tr>
        <tr>
          <td>Sprint</td>
          <td>Shift</td>
        </tr>
        <tr>
          <td>Gaze Click</td>
          <td>Mouse1
            <br>
            <em>Note: to use Gaze Click, you must disable hand tracking and disconnect the controllers.</em>
          </td>
        </tr>
        <tr>
          <td>Menu</td>
          <td>Esc</td>
        </tr>
      </tbody>
    </table>

    To disable input forwarding, click the mouse icon again.

### Configure casting

To configure your casting and recording experience, click the gear icon located in the menu bar at the top. The streaming settings you choose will impact the quality of the casted image:

1. **Target Bitrate** controls the overall quality of the streams. A higher bitrate will reduce visual artifacts, but it might also increase latency. Cast by default, adaptively uses the highest bitrate that doesn't impact latency.
2. **Maximum Resolution** caps the maximum allowed stream resolution. Lowering the resolution could potentially improve performance. If the casting stream is lagging or significantly delayed, reduce the maximum resolution.
3. **Maximum Framerate** sets the maximum frame rate of the stream. Higher frame rates will result in a smoother stream but could impact system performance.
4. **Adaptively Skip Frames** decides whether the encoder should skip frames based on system performance. If you disable this setting, it might reduce stuttering but it could also increase the worst-case stream latency.
5. **Pause When Recording** decides whether the stream should automatically pause while recording. Enabling this setting can improve the smoothness of captured videos.

The recording settings play a crucial role in determining the quality of captured videos. Here's a closer look at these settings:

1. **Target Bitrate** controls the overall quality of recorded videos. Higher bitrates will result in larger file sizes.
2. When you set **Capture Format** to **MAX**, you can produce near-lossless video files optimized for post-production. A minute of VR footage is around 600 MB while a minute of MR footage is around 1.2 GB.
3. **Target Framerate** sets a target variable frame rate for recorded videos. Higher frame rates may impact in-headset performance.

### Tips and best practices

1. **Use the record video button in the menu bar** - Capturing footage through the record video button ensures encoding at the source resolution. This method offers higher bitrate and smoother frame pacing compared to capturing the casting window, such as with OBS.
2. **Enable 60 FPS for recordings** - Beyond merely setting 60 FPS as your target recording frame rate in Cast settings, you might need to enable the **Pause When Recording** setting. This is to maintain a consistent frame rate, depending on the game.
3. **Select the same eye as your dominant hand** - If you're right-handed, it's best to use your right eye for casting and recording. This alignment ensures actions like peering down a scope are framed correctly.
4. **Delete video files after transfer** - Video files recorded by Cast can be quite substantial, particularly when selecting **MAX** as the capture format. Verify that your device has ample storage space before initiating a recording. Remember to delete video files manually after moving them to your computer.

## Link

[Use Link for your app development](/documentation/unity/unity-link).   [Link](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/requirements-quest-link/) is currently only supported on Windows.

## Access Library

Instead of manually searching for screenshot images, video recordings, or recorded metrics files in the local folders on your computer or headset, you can use the Library.

1. [Connect your headset to MQDH](/documentation/unity/unity-quickstart-mqdh/#connect-headset-to-meta-quest-developer-hub).
2. In the menu on the left, select **File Manager**.
   * To see the list of screenshots, video recordings, and metric recordings saved on the headset, select **On Device**.
   * To see the list of screenshots and video recordings captured directly from MQDH, click **MQDH Local**.

## Open web page in headset

Launch a web page in the headset from MQDH without using the VR keyboard in headset. You can open any URL; this is a great way to launch a WebXR application that you want to test or debug.

1. [Connect your headset to MQDH](/documentation/unity/unity-quickstart-mqdh/#connect-headset-to-meta-quest-developer-hub).
2. On **Device Manager**, locate **Browser** under **Device Actions**. Type the URL and click **Open**.
3. Put on your headset to view the web page.

## Launch and kill apps

You can launch and kill apps from the **Device Manager**.

Do the following to launch an app:

1. [Connect your headset with MQDH](/documentation/unity/unity-quickstart-mqdh/#connect-headset-to-meta-quest-developer-hub).
2. On **Device Manager**, under **Apps**, find the desired app, and then click **Launch App**.

To kill an app, on **Device Manager**, under **Apps**, find the running app, and then click **Stop App**.

## Test in multiplayer

Multiplayer testing allows developers to seamlessly test multiplayer destination sessions from MQDH. It allows one or more devices to launch an activity with a group launch intent attached in order to debug and test launching into private and public lobbies within the app. The feature works for any app that has:

* At least one destination registered as group launch in the developer portal. When creating the destination, you must set the **Group Launch Capacity Max** to a value greater than 1.
* At least one build uploaded to a release channel.

To use multiplayer testing, on **Device Manager**, under **Device Actions**, click **Join** next to **Multiplayer Testing**. Fill in the following fields in the modal:

* **Active Devices** - This is populated with devices that MQDH knows about, and allows selecting any connected devices. At least one device must be selected.
* **Org Name** - Lists all of the Orgs associated with the signed-in user, and defaults to the selected Org from the **App Distribution** tab. Updating this value will reset **App Name** and **Display Name**, and populate **App Name** with all apps associated with the new Org. An Org must be selected.
* **App Name** - This is populated with all apps in the selected Org that contain a package name. Changing this value resets **Display Name**. An app must be selected.
* **Destination Name** - This is populated with the names provided when creating the destinations in the developer dashboard. This only includes destinations marked as group launch. A destination must be selected.
* **Lobby Session ID** - This is the session ID that will be passed along with the deep link intent. This field cannot be left blank.

You can optionally use the **Close modal after launch** check box.

Once everything has been filled in, click **Launch** to start the activity on the selected devices.

## Vrruntime debug settings

[ADB command-line tool](/documentation/unity/ts-adb/) enables direct communication with your Meta Quest headset and provides a convenient way of setting system properties (`adb shell setprop`). However, trying to remember the names and values of different system properties can be difficult. MQDH offers the **VrRuntime Debugging Tool** that allows you to experiment with multiple vrruntime settings and rendering features, in a more user-friendly and less error-prone manner.

To use this tool, On **Device Manager**, under **Device Actions**, click the gear icon next to **VrRuntime Debugging Tool**.

{:width="600px"}

This panel accepts three types of input: toggle switch, dropdown menu and text box. To adjust individual settings, you can toggle a switch, or select an item from the dropdown menu, or type in a numerical value in the text box and press **Enter**, based on the type of the interface provided.

More explanation for system properties can be found in the table below.

<table>
  <thead>
    <tr>
      <th>Property Name</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>debug.oculus.textureWidth</td>
      <td>Set eye buffer width (pixels)</td>
    </tr>
    <tr>
      <td>debug.oculus.textureHeight</td>
      <td>Set eye buffer height (pixels)</td>
    </tr>
    <tr>
      <td>debug.oculus.cpuLevel</td>
      <td>Set CPU to a recommended level</td>
    </tr>
    <tr>
      <td>debug.oculus.gpuLevel</td>
      <td>Set GPU to a recommended level</td>
    </tr>
    <tr>
      <td>debug.oculus.eyeFovUp
        <br>
        debug.oculus.eyeFovDown
        <br>
        debug.oculus.eyeFovInward
        <br>
        debug.oculus.eyeFovOutward
      </td>
      <td>Set device FOV angle for a specific direction</td>
    </tr>
    <tr>
      <td>debug.oculus.colorspace.overrideColorspace</td>
      <td>Set display colorspace override</td>
    </tr>
    <tr>
      <td>debug.oculus.forceSpaceWarp</td>
      <td>Set motion vector source for Application SpaceWarp</td>
    </tr>
    <tr>
      <td>debug.oculus.swapInterval</td>
      <td>Set a specific swap interval value: "0", "1", "2", "3"</td>
    </tr>
    <tr>
      <td>debug.oculus.dynResScaler</td>
      <td>Set dynamic resolution scale factor
        <br>
        <em>Note: this only works for apps that have dynamic resolution enabled.</em>
      </td>
    </tr>
    <tr>
      <td>debug.oculus.localDimming</td>
      <td>Set local dimming mode on/off</td>
    </tr>
    <tr>
      <td>debug.oculus.gfr.mode</td>
      <td>Set Gaze Foveated Rendering (GFR) mode on/off</td>
    </tr>
    <tr>
      <td>debug.oculus.foveation.dynamic</td>
      <td>Set dynamic foveated rendering on/off</td>
    </tr>
    <tr>
      <td>debug.oculus.foveation.level</td>
      <td>Set foveation level
        <br>
        <em>Note: this property is only enabled when dynamic foveated rendering is off.</em>
      </td>
    </tr>
    <tr>
      <td>debug.oculus.foveation.subsampled</td>
      <td>Set subsampled layout on/off</td>
    </tr>
    <tr>
      <td>debug.oculus.layerFilter</td>
      <td>Set layer filtering mode</td>
    </tr>
    <tr>
      <td>debug.oculus.layerFilter.foveate</td>
      <td>Set sharpening radius for foveated rendering</td>
    </tr>
    <tr>
      <td>debug.oculus.pfr.autofilter</td>
      <td>Set auto filter mode</td>
    </tr>
  </tbody>
</table>

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)