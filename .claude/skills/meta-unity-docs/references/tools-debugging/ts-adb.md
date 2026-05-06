# Ts Adb

**Documentation Index:** Learn about ts adb in this documentation.

---

---
title: "Android Debug Bridge for Meta Quest"
description: "Install, configure, and use Android Debug Bridge (ADB) to manage and debug Meta Quest headsets."
last_updated: "2024-07-11"
---

Android Debug Bridge (ADB) is a command-line tool included with the [Android SDK](https://developer.android.com/studio) that is the main tool used to communicate with Meta Quest headsets during all stages of development. ADB is a highly versatile tool required to install apps and issue other important commands from the computer to the headset.

While this guide describes common actions, it's recommended that you also read the [official Android documentation](https://developer.android.com/studio/command-line/adb).

For a list of available commands and options, make sure ADB is installed and enter:

```
adb help
```

## Connect to a device with ADB

From the OS shell, it is possible to connect to and communicate with an Android device either directly through USB, or via TCP/IP over a Wi-Fi connection.

To connect a device via USB, plug the device into the PC with a compatible USB cable. After connecting, open up an OS shell and type:

```
adb devices
```

If the device is connected properly, ADB will show the device id list such as:

```
List of devices attached
    ce0551e7                device
```

You can't use ADB if no device is detected. If your device is not listed, the most likely problem is that you did not install the correct USB driver (see [Oculus ADB Drivers](/downloads/package/oculus-adb-drivers/))<!-- TODO: verify live page title at /downloads/package/oculus-adb-drivers/ -->.  Also, check that Developer Mode is enabled for your device in the Meta Horizon mobile app. Trying another USB cable or port can sometimes resolve connection issues.

## Connect ADB via Wi-Fi

Connecting to a device via USB is generally faster than using a TCP/IP connection, but a TCP/IP connection is sometimes indispensable.

To connect via TCP/IP, first make sure the device is already connected via USB, and then use this command to determine its IP address:

```
adb shell ip route
```

The output should look something like this:

```
10.0.30.0/19 dev wlan0  proto kernel  scope link  src 10.0.32.101
```

The IP address of the device follows `src`. Using the IP address and port (generally 5555), issue the following commands:

```
adb tcpip <port>
adb connect <ipaddress>:<port>
```

For example:

```
> adb tcpip 5555
    restarting in TCP mode port: 5555
> adb connect 10.0.32.101:5555
    connected to 10.0.32.101:5555
```

The device can now be disconnected from the USB port. As long as `adb devices` shows only a single device, all ADB commands will be issued for the device via Wi-Fi.

To stop using the Wi-Fi connection, issue the following ADB command from the OS shell:

```
adb disconnect
```

## Use ADB to install applications

Run the following command to install an APK on your headset:

```
adb install <APK_PATH>
```

For example, on Windows:

```
adb install C:\Dev\Android\MyProject\VrApp.apk
```

On macOS:

```
adb install ~/Dev/Android/MyProject/VrApp.apk
```

Use the `-r` option to overwrite an existing APK of the same name already installed on the target device:

```
adb install -r <APK_PATH>
```

**Note**: Installing apps with `adb install` bypasses the normal Quest install route. Cloud Backup will not register apps installed this way and will not perform backups for them.

For more information, see the [Installing an Application](https://developer.android.com/studio/command-line/adb.html#move) section of Android's **Android Debug Bridge** guide.

## Connection troubleshooting

The sections below provide possible solutions to common connection issues with ADB.

### Computer is unable to detect the device

There can be many reasons why your computer is unable to detect the headset. Here are a few suggestions:

- Ensure that you have turned on Developer Mode in the Meta Horizon app on your mobile device.
- Check if the issue is caused by a faulty USB cable. Connect the device by using a secondary USB cable. If you do not have the secondary USB cable, connect any other Android device to verify if the issue is with the USB cable.
- Ensure that your computer has all the necessary permissions to access your headset. Typically, when you connect your device with the computer over a USB cable, you see a prompt to permit your computer to access the device. If you accidentally denied permission, disconnect the USB cable, restart the device, and then connect the cable again. When prompted for permission, select **Allow**.
- On some devices, connecting while a VR app is running or when ADB is waiting for a device may prevent the device from being reliably detected. In those cases, try ending the app and stopping ADB using **Ctrl-C** before reconnecting the device. Alternatively, the ADB service can be stopped using the following command, after which it will automatically restart the next time any ADB command is run:

```
adb kill-server
```

### Terminal returns an error: `adb` command not found

- Begin by verifying whether `adb` is correctly installed. Go to `/Android/SDK/platform-tools/` folder and check for the `adb` tool. If the tool is missing, download the standalone [Android SDK Platform-Tools package](https://developer.android.com/studio/releases/platform-tools).
- Check whether you've set the environment variables correctly.
- You can run adb from the `/Android/SDK/platform-tools/` folder by prefixing `./` to the `adb` command. For example, instead of `adb devices`, use `./adb devices`.

### Multiple devices are connected at the same time

Multiple devices may be attached at once, and this is often valuable for debugging client/server applications. Be aware that when the same device is simultaneously connected by Wi-Fi and USB, ADB will show the device as two devices. When there are multiple listed devices, ADB must be told which device to target using the `-s` switch. For example, consider if `adb devices` showed the following:

```
List of devices attached
    ce0551e7                device
    10.0.32.101:5555        device
```

The listed devices could be two separate devices, or one device that is connected both via Wi-Fi and plugged into USB (perhaps to charge the battery). In this case, **all** ADB commands must take the following form, where &lt;device id&gt; is the identifier reported by `adb devices`:

```
adb -s <device id> <command>
```

For example, to issue a logcat command to the device connected via TCP/IP:

```
adb -s 10.0.32.101:5555 logcat -c
```

To issue the same command to the device connected via USB:

```
adb -s ce0551e7 logcat -c
```

## Learn more

To learn more about developer tools, see the following resources:

- [Developer Tools for Meta Quest](/resources/developer-tools/)