# Virtual Camera Publisher Sdk Unity

**Documentation Index:** Learn about virtual camera publisher sdk unity in this documentation.

---

---
title: "Virtual Camera Publisher"
description: "Publish up to three custom virtual cameras from your Unity app for use in system recording, casting, and livestreaming on Meta Quest."
last_updated: "2026-01-21"
---

The Virtual Camera Publisher is a feature that allows Unity apps running on HzOS v81 or higher to create and share custom virtual cameras. These cameras can be used for system recording, casting, and livestreaming. This functionality is designed to help developers quickly add new and creative camera angles, making it easier for content creators to capture engaging perspectives:

## User in-headset POV
   {:width="320px"}

   *App seen through headset.*

## Stands virtual camera POV
   {:width="320px"}

   *Recording from virtual camera placed in the stands.*

## Courtside virtual camera POV
   {:width="320px"}

   *Recording from virtual camera placed courtside.*

## Drone virtual camera POV
   {:width="320px"}

   *Recording from virtual camera programmed to circle the court.*

## How the Virtual Camera Publisher works

With the Virtual Camera Publisher, you can register up to three virtual cameras at a time within your app. Once registered, these cameras appear in the system camera picker UI, making them available for use by other apps and system features. All camera-consuming apps that also declare `horizonos.permission.APP_DEFINED_CAMERA` in their manifest can see your virtual cameras. You have the flexibility to customize camera names, assign unique IDs, set the resolution and framerate, and even provide custom thumbnails for the camera picker interface.

## How to set up the Virtual Camera Publisher

For detailed integration steps, advanced usage, and the latest updates, please refer to the official [GitHub repository](https://github.com/oculus-samples/Unity-VirtualCameraPublisher). Additional resources, such as a sample project and troubleshooting tips, are also available here.