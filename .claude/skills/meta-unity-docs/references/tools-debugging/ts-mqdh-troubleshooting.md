# Ts Mqdh Troubleshooting

**Documentation Index:** Learn about ts mqdh troubleshooting in this documentation.

---

---
title: "Troubleshooting MQDH"
description: "Solutions for common Meta Quest Developer Hub errors including connection, login, and setup issues."
last_updated: "2024-10-01"
---

This guide covers the most common errors and issues that you may encounter while setting up or using Meta Quest Developer Hub (MQDH).

* **I have multiple Meta accounts. Does it matter which one I use to log in to MQDH?**

  You must use the same Meta account to log in to MQDH as the one you used to log in to the Meta Quest headset.

* **Do I have to use ADB over WiFi to connect the headset to the computer?**

  No, it's optional. ADB over WiFi lets you connect your Meta Quest device to the computer wirelessly, which means it's not tethered to your computer through a USB-C 3.0 cable. It's a matter of preference whether you like it connected wirelessly or via a USB-C 3.0 cable.

* **MQDH doesn't detect my headset even when it is connected to the computer over a USB-C cable.**

  There are two things to check:

    * After you connect your headset to the computer over a USB-C 3.0 cable, wear your Meta Quest device and look for the prompt to let the computer access the headset over the USB-C 3.0 cable.

    * You may not have turned on the developer mode from the Meta Horizon mobile app. Follow steps 1 to 3 from the [Connect headset to Meta Quest Developer Hub](/documentation/unity/ts-mqdh-getting-started/#connect-headset-to-meta-quest-developer-hub) section.

* **I am unable to connect the headset to the computer using ADB over WiFi.**

  MQDH uses ADB v1.0.41 and requires you to turn on the developer mode from the Meta Horizon mobile app. You may have connected the headset to the computer over a USB-C 3.0 cable, but may not have turned on the developer mode from the Meta Horizon mobile app. Follow steps 1 and 2 from the [Connect headset to Meta Quest Developer Hub](/documentation/unity/ts-mqdh-getting-started/#connect-headset-to-meta-quest-developer-hub) section.

  Check if you're connected to any virtual private network (VPN). At times, VPN connectivity may interfere with ADB connectivity. Disconnect from the VPN, restart MQDH, and enable ADB over WiFi.

* **I can't view certain app content while casting my device.**

  Casting can be restricted for licensed app content.

* **Casting is stuck in the waiting for connection state.**

  Make sure there is no notification in your Meta Quest device that requires your action. At times, if there's an unresponsive app that requires further action, casting may stay suspended.

* **OVR Metrics Tool is installed, but it's stuck in the refreshing state.**

  After OVR Metrics Tool is installed, you must restart the device before the tool is available for use.

* **I already have OVR Metrics installed on my Meta Quest device but MQDH is unable to detect it.**

  MQDH works with the latest version of tools and SDKs. MQDH may be unable to detect OVR Metrics Tool because you may not have the latest version of the tool available. Install the latest version from MQDH.

* **I have one of the Meta Quest tools already installed. However, MQDH doesn't list the tool under the Installed tab.**

  MQDH lists packages under the Installed tab only if you've installed it from MQDH. It doesn't recognize packages downloaded directly from the Downloads page.

* **I'm a Meta Quest for Business developer and MQDH doesn't seem to work with the headset.**

  Currently, MQDH doesn't support Meta Quest for Business firmware.

* **I keep receiving build failures when uploading my APK to a release channel.**

  When uploading a new build to a release channel, it must have a different version than the build it is replacing. Not incrementing the version code is a common cause of failures when uploading your APK. Unity includes functionality that automatically increments the version code by 1 every time a build is created. It is highly recommended that you use this feature. See [Auto Increment Version Code](/documentation/unity/unity-build-android-tools#auto-increment-version-code) in Optimize Build Iterations for Android Apps for more information.