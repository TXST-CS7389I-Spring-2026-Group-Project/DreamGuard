# Xrsim Intro

**Documentation Index:** Learn about xrsim intro in this documentation.

---

---
title: "Meta XR Simulator Overview"
description: "Meta XR Simulator is a lightweight XR runtime built for developers that enables the simulation of Meta Quest headsets and features on the API level."
last_updated: "2025-11-13"
---

<oc-devui-note type="important" heading="Standalone XR Simulator">This
documentation covers the newly released Standalone XR Simulator. Older versions
of the Meta Core SDK may not be fully compatible. If you encounter issues,
remove the 'com.meta.xr.simulator' package from your project and use the toggle
in Meta XR Simulator to set it as the OpenXR active runtime. For legacy usage,
refer to the <a href="/documentation/unity/xrsim-intro-1.0">Archived
XR Simulator documentation.</a></oc-devui-note>

The Meta XR Simulator is a lightweight
[OpenXR runtime](https://registry.khronos.org/OpenXR/specs/1.0-khr/html/xrspec.html#runtime)
built for developers that enables the simulation of Meta Quest headsets and
features at the API level. It serves as a comprehensive testing and development
tool, allowing you to simulate the Meta Quest experience directly on your
development machine - essentially acting as a virtual Meta Quest headset running
on your computer. It implements the OpenXR standard for XR applications, making
it compatible with any OpenXR application, including those built with Unity.

It allows you to test and debug apps without the need to put on or take off a
headset, and it helps scale automation by simplifying the setup of your testing
environment.

See [Get Started with Meta XR Simulator](/documentation/unity/xrsim-getting-started) for
requirements and installation instructions.

## Main components

### Device simulation

Simulates Meta Quest devices (like Quest 2, Quest 3, and Quest Pro) including:

- Stereoscopic rendering with device-specific characteristics
- Headset and controller tracking
- Hand tracking
- Body tracking
- Input systems (controllers, hands)

### Synthetic environments

The virtual spaces where your application runs.

When you use a physical Meta Quest headset, your application interacts with the
physical world around you. The headset uses cameras to capture your room, detect
walls and furniture, and enables passthrough allowing you to see your
surroundings. In the Meta XR Simulator, synthetic environments serve the same
purpose. They are pre-created 3D rooms that provide:

- **Passthrough data**: Visual representation of the "physical" space
- **Scene information**: Spatial understanding including walls, floors,
  ceilings, furniture, doors, and windows
- **Spatial anchors**: Places where you can attach virtual objects in the
  environment
- **Depth data**: Distance information for realistic interactions

Without a synthetic environment, your application would have no "physical world"
to interact with. This is especially critical for Mixed Reality applications
that blend virtual content with the physical environment.

## Key features

### Input simulation

Control the simulated headset and hands using your development machine:

- **Keyboard and mouse**: Map your keyboard and mouse to headset and controller
  movements
- **Xbox controller**: Use familiar gamepad controls
- **Hand tracking**: Test hand poses including pinch, poke, and grab gestures
- **Connect physical controllers**: Use your real Meta Quest controllers in the
  simulator

For more information see
[Input Simulation](/documentation/unity/xrsim-data-forwarding).

### Synthetic environments

Test your application in different room layouts without leaving your desk:

- **Built-in rooms**: Game room, living room, bedroom, and more

For more information see
[Synthetic Environments](/documentation/unity/xrsim-heroscenes).

### Session capture (record and replay)

Automate repetitive testing tasks:

- **Record user actions**: Capture your interactions once
- **Replay sessions**: Automatically replay actions for consistent testing
- **Zero-code automation**: No scripting required for basic test automation
- **CI/CD integration**: Run automated tests in your continuous integration
  pipeline

For more information see
[Session Capture (Record and Replay)](/documentation/unity/xrsim-session-capture).

### Multiplayer testing

Test multiplayer experiences on a single machine:

- **Multiple session**: Run several sessions at the same time and jump between
  them
- **Shared environment**: All sessions connect to the same synthetic environment

For more information see
[Multiplayer Testing](/documentation/unity/xrsim-multiplayer).

## User interface

You interact with the Meta XR Simulator through its window interface. The window
looks different depending on whether or not you have an application running in
the simulator.

**Note:** You can hover over any element in the simulator window to see a
tooltip with additional information.

### The XR Simulator window

#### When you first open the XR Simulator

When you open the XR Simulator without running an application, the window
displays a message and provides access to essential settings:

- Choose a synthetic environment from the available options
- Configure basic simulator settings
- Activate the simulator globally in your system

#### When an application is running

Once you launch an application in the simulator, the window becomes fully
interactive and displays:

- **Viewport**: The main view showing what would appear in the headset (left eye
  view by default)
- **Toolbar**: A control bar at the top with status indicators and buttons to
  open different panels
- **Side Panel**: Information and control panels that open on the right side
  when you click toolbar buttons
- **Console**: A log area that you can open to view Meta XR Simulator messages
  (hidden by default)

### Toolbar

The toolbar runs across the top of the simulator window and provides quick
access to status information and controls:

#### Left side: Connection and activation

- **Connection indicator**: A dot that shows whether the application being
  tested has successfully connected to the simulator
- **Global activation slider**: Activates the simulator globally in your system,
  meaning any OpenXR application will automatically attempt to connect to it

#### Middle: Side panel triggers

Buttons that open specific side panels. Click a button to open its corresponding
panel, which will appear on the right side of the window. Available panels
include:

- **Settings**
- **Record and Replay**
- **Inputs**
- **Input Bindings**
- **Console**

#### Right side: System functions

- **Information button**: Displays information about connected application, what
  extensions are enabled, and where configuration files are located
- **Help button**: Access to documentation and support resources
- **Feedback button**: Submit feedback about the simulator
- **Synthetic environment picker**: Opens the environment selection menu to
  choose a different synthetic room

### Side panels

Side panels provide detailed controls and information. They appear on the right
side of the window when activated from the toolbar.

#### Settings panel

Configure the simulated device properties:

- **Device model**: Choose between Quest 2, Quest 3, Quest Pro, and other
  supported devices
- **IPD (inter-pupillary distance)**: Adjust the distance between the eyes
- **Refresh rate**: Set the display refresh rate

**Note**: Some changes will only take effect after restarting the simulator.

#### Record and replay panel

Automate testing by recording and replaying user actions:

- **Record**: Capture a series of actions during your testing session
- **Save**: Store recordings locally for later use
- **Replay**: Play back recorded sessions for consistent, repeatable testing

For detailed information, see
[Capture a Meta XR Simulator Session](/documentation/unity/xrsim-session-capture).

#### Inputs panel

Control how inputs are simulated in the Meta XR Simulator:

- **Global Input Settings**: Select which parts of the tracking system are
  active:
  - Headset movement
  - Left hand/controller
  - Right hand/controller - This selection is especially important in situations
    where, for example, interaction needs to be performed using only one hand

- **Keyboard + Mouse**: This is where you access the Point + Click feature

- **Physical Controllers**: Connect Quest controllers to the simulator

- **Xbox Controller**: Controller simulator with Xbox controller

- **Movement Tracking Controls**: Allows you to play back recorded tracking data
  to test MovementSDK features such as body, face, and eye tracking.

#### Input Bindings panel

- View and customize keyboard and controller mappings

- View the keys or buttons assigned to each action (such as moving forward,
  rotating up, or grabbing)

- Customize the key or button assignments to suit your preferences.

#### Console panel

View Meta XR Simulator logs and diagnostic information.