# Ts Mqdh Custom Commands

**Documentation Index:** Learn about ts mqdh custom commands in this documentation.

---

---
title: "Create a Custom Command"
description: "Build and run custom ADB commands in Meta Quest Developer Hub to automate device workflows."
last_updated: "2024-10-01"
---

As a developer, you are accustomed to using shortcut commands that enable you to perform unique functions and tasks during your VR project development. When developing for Android-based virtual reality apps, Android Debug Bridge (ADB) commands are an essential part of interacting with your device through your computer. Meta Quest Developer Hub (MQDH) lets you create your own commands, which means that you don't need to switch between your editor and command prompt, or type lengthy commands. With just a single click, you can run a custom command that's a combination of multiple ADB commands, use unique shortcuts for repetitive tasks, or leverage command arguments that offer more precise insight into development.

## Create a Command

1. Open MQDH and go to **Device Manager**.
2. Under **Custom Commands**, click **Create Command**.

   {:width="650px"}

3. In the Custom Command dialog box, do the following:
   * In **Name**, give your command a unique descriptive name.
   * In **Command**, enter the custom command. If your command requires device ID, replace the device ID with `_MQDH_CONNECTED_DEVICE_SERIAL_ID_` in your command syntax. When you run the command, MQDH replaces this string with the currently connected device ID. In this way, you don't have to modify or create new commands for each device.

   {:width="600px"}

4. To immediately open the output in a new window, select **Display command output in a new window**.
5. Click **Save**.
6. To modify or delete a command, click the ... icon for more options to modify or delete the command.

## Run a Command
To run a command, ensure that the device is connected to your development machine and that it is available in the Device Manager of Meta Quest Developer Hub.

1. On **Device Manager**, under **Custom Commands**, click **Run**.
2. To view the output, click **View Output**.

   {:width="600px"}

## Import and Export Commands

Share your custom commands externally, for example, with another developer or on another computer, through a JSON file. Similarly, if you have a list of custom commands saved externally, import them in MQDH using a formatted JSON file.

1. Next to **Create Command**, click the ... icon to see more options.
2. To import commands in MQDH, from the list of more options, click **Import Commands**, and locate the JSON file on your computer that contains the custom commands.
3. To export commands, from the list of more options, click **Export Commands**. This exports all the custom commands in a JSON file. To export a specific command, click the ... icon next to the command you want to export, and click **Export**.

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)