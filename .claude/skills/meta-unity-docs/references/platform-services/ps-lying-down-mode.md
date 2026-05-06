# Ps Lying Down Mode

**Documentation Index:** Learn about ps lying down mode in this documentation.

---

---
title: "Lying Down Mode"
description: "Control whether Lying Down Mode stays active while your app runs on Meta Quest."
last_updated: "2025-1-27"
---

## What is Lying Down Mode?

Lying Down Mode is an Accessibility setting designed to support people who are using Meta Quest while in bed.

When Lying Down Mode is **disabled**, users resetting their view by holding  will have an orientation guaranteed to be aligned with gravity ("up" points towards the ceiling, and "down" points towards the floor).

When Lying Down Mode is **enabled**, this restriction is removed. Users who are, for example, lying on their backs in bed, staring at the ceiling, can reset their view by holding , and have an orientation where "forward" points towards the ceiling, and "up" points to the head of the bed.

Lying Down Mode can be enabled and disabled in "Settings" > "Accessibility" > "Mobility" > "Lying Down Mode".

## Disabling Lying Down Mode for your App

As an app developer, you may want to force-disable Lying Down Mode while your app is running. Two common reasons for doing so are:

- Your app is already designed to support lying-down users, and expects users to be facing "up"
- Your app automatically resets the user's view in certain cases and involves intense physical motion. During intense physical motion, a moving user could inadvertently reset their view to an orientation where "down" does not point towards the floor, causing vestibular disorientation during future physical motion.

To force-disable Lying Down Mode for the lifetime of your app, add the following line to your app's AndroidManifest.xml:

```
<meta-data android:name="com.oculus.app_opt_out_lying_down_mode" android:value="true" />
```

Users with Lying Down Mode enabled, upon launching an app with Lying Down Mode force-disabled, will receive the notification: **This app is unavailable while lying down. Use this app in an upright position.** When resetting their view while running an app with Lying Down Mode force-disabled, users' new orientation is guaranteed to be aligned with gravity.