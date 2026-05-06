# Unity Haptics Studio Troubleshooting

**Documentation Index:** Learn about unity haptics studio troubleshooting in this documentation.

---

---
title: "Haptics Studio Troubleshooting"
description: "Resolve common problems when designing haptic effects in Meta Haptics Studio."
---

## Desktop App

### Windows Defender blocking features

The application needs to use the local network to find and transfer audio and haptics to your Meta Quest device. Allow Haptics Studio to access your private network and click the **Allow access** button. Ensure you check all the boxes to enable network connectivity of Haptics Studio.

### Missing audio file for a haptic clip

If the original audio file for a haptic clip cannot be found (deleted, moved, or renamed), press the **Relocate Missing File** button in the alert window to locate and reassign the correct file.

## VR Companion App

### VR Companion App cannot connect to the desktop app

- Ensure both the desktop and VR devices are on the same network.
- Check and adjust firewall settings on your computer or network router to ensure they are not blocking the connection. To reset Windows Defender firewall rules for Haptics Studio:
  - Close Haptics Studio.
  - Navigate to **Windows Defender Firewall** > **Advanced Settings** > **Inbound Rules**.
  - Delete all rules related to Haptics Studio.
  - Restart Haptics Studio.
- Manually connect by entering the desktop app's IP address into the VR app.
  - On Windows: Navigate to **Start** > **Settings** > **Network & Internet** > **Wi-Fi**, then select your network and look under **Properties**.
  - On Mac: Option-click the Wi-Fi icon in the menu bar.

### Application not synchronizing changes

If the Haptics Studio VR app is not synchronizing changes made in the desktop app, disconnect from the desktop application in the VR app and then reconnect. If the issue persists, restart the Haptics Studio VR application.