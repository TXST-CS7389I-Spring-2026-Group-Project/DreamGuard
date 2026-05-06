# Ts Scriptable Testing

**Documentation Index:** Learn about ts scriptable testing in this documentation.

---

---
title: "Use Meta Quest Scriptable Testing Services to Enable E2E Testing"
description: "Automate end-to-end testing on Meta Quest by suppressing system dialogs and controlling device state."
---

On-device automated End-to-End (E2E) testing for Meta Quest developers presents a challenge because user-focused device features can get in the way of tests. For example, modal system dialogs can prevent tests from launching, or a test device might enter auto-sleep in the middle of a long performance test. With the Meta Quest Scriptable Testing services, it is possible to disable these features to reliably run automated tests. Additionally, the number of manual steps required to configure a device for testing and to reset it between repeated runs has been greatly reduced, and in many cases eliminated. The goal of the services is to streamline the hardware configuration and testing process so developers can test more efficiently and at scale.

## Use Cases

Meta Quest OS v44 and later include the testing services that enable verified Meta developers to reset their devices and disable certain system features for the purpose of facilitating developer tasks such as running automated tests. Specifically, the new services allow developers to do the following on their headsets with simple ADB commands, without the need to put on the headset:

* Set/Get properties:
  * Enable/disable certain modal (blocking) system dialogs, including the **Allow USB Debugging** and **Allow connected device to access files** dialogs.
  * Enable/disable the boundary.
  * Enable/disable auto-sleep.
* Reset device:
  * Factory reset the device.
  * Configure WiFi.
  * Log in a test user.

<oc-devui-note type="important" heading="DEVELOPER USE ONLY">Use of these features by non-developers, or for purposes other than development of Meta Quest software, is not supported, and attempting to do so could degrade the lifetime of the headset.
</oc-devui-note>

## Prerequisites

* Install ADB and fastboot on your development workstation if they were not installed during your environment setup. This can be done by installing the relevant [Android SDK Platform Tools](https://developer.android.com/studio/releases/platform-tools) package.
* Update your headset to Meta Quest OS v44 or later.
* Create a [test user](/resources/test-users/) if you don't have one already. Take note of the test user's email, password, and PIN.
* Ready the device for first use by logging in with either your developer account or one of your test user accounts. You can do this by either putting on the headset and following the standard user setup flow or by using the "Setup New Device" feature in [Meta Quest Developer Hub](/documentation/unity/ts-mqdh-device-setup/).

## Usage

The features can be accessed in scripts using the ADB commands described below, with no requirement to put on the headset. This makes them ideal for running automated tests locally or managing a device lab for scaled testing.

There are two main features of the testing services:

* **GET_PROPERTY** and **SET_PROPERTY** - These allow for controlling services that can interfere with automated testing.
* **WIPE_DEVICE** and **SETUP_FOR_TEST** - These reset the device to a known state to ensure sequential tests do not interfere with each other.

More detailed information on using these features is described in the following sections.

### Controlling Services that Interfere with Automation (SET/GET_PROPERTY)

The SET_PROPERTY and GET_PROPERTY commands execute synchronously and provide an appropriate return value. SET_PROPERTY returns whether or not the command is successful and, if unsuccessful, a corresponding error message. GET_PROPERTY simply returns a bundle containing the currently set values of all supported properties.

```
// Disable three system features that would otherwise interfere with an E2E test.
// Note that a developer (or test user) must be logged on and you must specify
// the Store PIN associated with that logged in account.
> adb shell content call --uri content://com.oculus.rc --method SET_PROPERTY \
   --extra 'disable_guardian:b:true'  \
   --extra 'disable_dialogs:b:true'   \
   --extra 'disable_autosleep:b:true' \
   --extra 'PIN:s:1234'
Result: Bundle[{Success=true}]

// Query the currently set property values.
> adb shell content call --uri content://com.oculus.rc --method GET_PROPERTY
Result: Bundle[{disable_guardian=true, disable_dialogs=true, disable_autosleep=true}]

// Simulate putting on the headset by triggering the proximity sensor.
> adb shell am broadcast -a com.oculus.vrpowermanager.prox_close

// Install your APK and execute your test here. E.g.,
> adb install /path/to/my_app.apk
> adb shell am start -S com.my.app.packagename/.MainTestActivity

// Re-enable the system features.
> adb shell content call --uri content://com.oculus.rc --method SET_PROPERTY \
   --extra 'disable_guardian:b:false'  \
   --extra 'disable_dialogs:b:false'   \
   --extra 'disable_autosleep:b:false' \
   --extra 'PIN:s:1234'

// Simulate taking the headset off by triggering the proximity sensor.
> adb shell am broadcast -a com.oculus.vrpowermanager.prox_far
```

To avoid unintended side effects, such as screen burn-in and rapid battery drain, re-enable any of the disabled properties after executing your test code. Factory resetting the device also restores all of these properties to their default values. However, rebooting will not reset them.

### Wiping the Device to Prepare for Testing (WIPE_DEVICE/SETUP_FOR_TEST)

To isolate and improve the repeatability of E2E test runs, best practice is to wipe the device to start the test from a known state. This reduces the chances that one test run is somehow affected by a previous run. This section describes how to do that by chaining together calls to WIPE_DEVICE and SETUP_FOR_TEST. An example flow looks like this:

```
// Factory reset the device. Note that if a user is logged on, it must be a
// developer (or test user) and you must specify the Store PIN associated with
// that account.
> adb shell content call --uri content://com.oculus.rc --method WIPE_DEVICE \
    --extra 'PIN:s:1234'
Result: Bundle[{Message=WIPE_DATA Pending, Success=true}]

// ...wait for the device to re-start…

// Login your test user (it must be a test user) and connect to local wifi.
> adb shell content call --uri content://com.oculus.rc --method SETUP_FOR_TEST \
    --extra 'WIFI_SSID:s:my_wifi_ssid' \
    --extra 'WIFI_PWD:s:my_wifi_password' \
    --extra 'WIFI_AUTH:s:WPA' \
    --extra 'EMAIL:s:my_test_user@tfbnw.net' \
    --extra 'PWD:s:my_test_user_password'

// ...wait for the device to re-start...

// [Optional] Disable auto-sleep. Note that disable_guardian and disable_dialogs
// was already completed as part of the SETUP_FOR_TEST call above.
> adb shell content call --uri content://com.oculus.rc --method SET_PROPERTY \
   --extra 'disable_autosleep:b:true' \
   --extra 'PIN:s:1234'
Result: Bundle[{Success=true}]

// Install your APK and execute your test here. E.g.,
> adb install /path/to/my_app.apk
> adb shell am start -S com.my.app.packagename/.MainTestActivity

// Repeat the process again for your next test, starting with WIPE_DEVICE.
```

### Python Helper Script for Device Wipe and Setup

To simplify the previous flow, the following Python helper script abstracts all the ADB commands into a single method call. Call `setupDeviceForTest()` with the appropriate parameters and it will:

1. Call WIPE_DEVICE.
2. Wait for the device to reboot.
3. Call SETUP_FOR_TEST.
4. Wait for the device to reboot.

When the method returns, your device will be ready for you to install your APK and run your test.

```
import subprocess
import time

# Ready the connected device to run a test by factory resetting the device,
# connecting to wifi and logging in a test user.
#
# testUserEmail/testUserPassword: specify the credentials of an existing Meta test
#   user (https://developers.meta.com/horizon/resources/test-users). This is the
#   user that will be logged into the device after it's been reset. Generally, these
#   email addresses are in the @tfbnw.net domain.
#
# testUserPin: if the device is in a logged in state, then it must be a developer
#   or test user that's logged in AND (for security purposes) this parameter must
#   specify that user's Store PIN. If calling this method when the device
#   is in a non-logged in state, this parameter is ignored.
#   Note that the logged in user may be different from the test user specified above.
#
# wifiSSID/wifiPassword: specify valid credentials for a wifi network that is in range.
#
# deviceId: specify the device's serial number--only required if multiple headsets
#   are connected to the host machine.
def setupDeviceForTest(
    testUserEmail,
    testUserPassword,
    testUserPin,
    wifiSSID,
    wifiPassword,
    deviceId=None,
):
    # Factory reset the device to get a clean test environment. This is a requirement
    # of the subsequent SETUP_FOR_TEST call. WIPE_DEVICE will reset the device but also
    # preserve ADB access after the subsequent reboot.
    result = __runAdbShell(
        f"content call --uri content://com.oculus.rc --method WIPE_DEVICE "
        f"--extra 'PIN:s:{testUserPin}'",
        deviceId,
    )
    if result.returncode == 0 and "Success=true" in result.stdout:
        __runAdbCommand("wait-for-disconnect", deviceId)
        __waitForDeviceBootCompleted(40, deviceId)
    else:
        print(
            f"WIPE_DEVICE call failed: returncode={result.returncode}; {result.stdout}; {result.stderr};"
        )
        return -1

    # Connect to wifi and log in the test user.
    result = __runAdbShell(
        f"content call --uri content://com.oculus.rc --method SETUP_FOR_TEST "
        f"--extra 'WIFI_SSID:s:{wifiSSID}' --extra 'WIFI_PWD:s:{wifiPassword}' --extra 'WIFI_AUTH:s:WPA' "
        f"--extra 'EMAIL:s:{testUserEmail}' --extra 'PWD:s:{testUserPassword}'",
        deviceId,
    )
    if result.returncode == 0 and "Success=true" in result.stdout:
        __runAdbCommand("wait-for-disconnect", deviceId)
        __waitForDeviceBootCompleted(40, deviceId)
        __waitForDumpSys("Horizon logged in: true", 25, deviceId)
    else:
        print(
            f"SETUP_FOR_TEST call failed: returncode={result.returncode}; {result.stdout}; {result.stderr};"
        )
        return -1

# Install the specified APK and launch the app.
def installAndStartApp(apkPath, packageName, activityName, deviceId=None):
    __runAdbCommand(f"install {apkPath}", deviceId)
    __runAdbShell(f"am start -S {packageName}/{activityName}", deviceId)

def sleepHeadset(deviceId=None):
    __runAdbShell("input keyevent POWER", deviceId)

def __runShellCommand(command):
    print(f"SHELL: {command}")
    split = command.split()
    result = subprocess.run(split, capture_output=True, text=True)
    return result

def __getDeviceArg(deviceId):
    if deviceId is None:
        return " "
    else:
        return f" -s {deviceId} "

def __runAdbShell(command, deviceId):
    return __runShellCommand("adb" + __getDeviceArg(deviceId) + "shell " + command)

def __runAdbCommand(command, deviceId):
    return __runShellCommand("adb" + __getDeviceArg(deviceId) + command)

def __waitForProperty(property, maxSeconds, deviceId):
    print(f"Waiting for {property} to turn true")
    start = time.time()
    while "1" not in __runAdbShell(f"getprop {property}", deviceId).stdout:
        if time.time() - start > maxSeconds:
            raise RuntimeError(f"timed out while waiting for {property} to turn true")
        __sleep(2)
    print(f"{property} is true")

def __waitForDeviceBootCompleted(maxSeconds, deviceId):
    __runAdbCommand("wait-for-device", deviceId)
    __waitForProperty("sys.boot_completed", maxSeconds, deviceId)
    __sleep(2)

def __waitForCommand(command, targetString, maxSeconds):
    print(f"Waiting for command '{command}' to return '{targetString}'")
    start = time.time()
    while True:
        result = __runShellCommand(command)
        # Break the loop if we find the target string in stdout or stderr.
        if (
            result.stderr.find(targetString) >= 0
            or result.stdout.find(targetString) >= 0
        ):
            break
        # Raise an exception if we don't find the target in time.
        if time.time() - start > maxSeconds:
            raise RuntimeError(
                f"timed out while waiting for command '{command}' to return '{targetString}'. \n"
                + f"STDOUT: {result.stdout} \n"
                + f"STDERR: {result.stderr} \n"
            )
        __sleep(2)
    print("Found return string: " + targetString)

def __waitForDumpSys(targetString, maxSeconds, deviceId):
    __waitForCommand(
        "adb" + __getDeviceArg(deviceId) + "shell dumpsys CompanionService",
        targetString,
        maxSeconds,
    )

def __sleep(seconds):
    print(f"SLEEP {seconds}s")
    time.sleep(seconds)
```

## Frequently Asked Questions

**Is it a problem if I see some OS dialogs when my device boots up after the WIPE_DEVICE / SETUP_FOR_TEST flow?**

Only if those dialogs are blocking you from programmatically launching your test (when running `adb install` and `adb shell am start`). These features endeavor to eliminate blocking dialogs only. Other non-blocking dialogs might still appear.

**SETUP_FOR_TEST returns successfully but my device isn't restarting. What went wrong?**

If your SETUP_FOR_TEST call returns `{Message=Login/Wifi Pending, Success=true}` but the device does not restart, that usually means there is a problem with either the supplied WiFi credentials or the test user login credentials. You can get more information by searching for `Skip NUX` in the device's logcat.  For instance, you might see an error message like these indicating what the problem is:

* `[Skip NUX] Failed to connect to WiFi.`
* `[Skip NUX] Failed to login.`

Note that spaces (such as in your WiFi SSID) must be escaped with a backslash.

**I call SETUP_FOR_TEST hundreds of times per day and my test user logins occasionally fail with cryptic failure messages. What can I do about this?**

If you use the same test user to log in dozens or hundreds of times per day, the authentication service may throttle logins by your test user. The best way to work around this limitation is to create more test users and rotate through them.

**Do these features work on an unprovisioned device?**

No. We do not provide support for unprovisioned devices.

**Does this work if I have multiple devices connected to my host computer?**

Yes. ADB commands can be routed to the correct device using the `-s` parameter to specify the target device's serial number. `adb devices` shows you the serial numbers of all connected devices.

**Why doesn't ADB recognize that my device is connected?**

As described in the Prerequisites section, you must first enable developer mode by logging into a device through traditional flows. This allows ADB to recognize when your device is connected. This access is lost if you use a traditional means for factory resetting your device (such as `fastboot erase userdata`) and you must re-login. You can avoid this situation by only using the WIPE_DEVICE command to reset your device when necessary and immediately following that with a valid SETUP_FOR_TEST call.

## Additional Testing Resources

Quest Scriptable Testing services get your device ready to run an E2E test, but they do not actually run and validate your test for you. For that, you need a test driver or testing framework. Several options are available, including:

* [AutoDriver](/documentation/unity/ts-autodriver/): a Meta developed, engine-agnostic, framework for recording user inputs and playing them back on a headset (for example, to drive an E2E test).
* [Unity Testing Framework](https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html): a Unity specific framework for creating, running and validating tests.
* [Unreal Engine Gauntlet Automation Framework](https://dev.epicgames.com/documentation/en-us/unreal-engine/gauntlet-automation-framework-in-unreal-engine): a UE specific framework for creating, running and validating tests.

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)