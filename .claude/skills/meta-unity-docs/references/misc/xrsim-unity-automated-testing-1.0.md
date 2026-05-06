# Xrsim Unity Automated Testing 1.0

**Documentation Index:** Learn about xrsim unity automated testing 1.0 in this documentation.

---

---
title: "Meta XR Simulator Automation Testing"
description: "You can run automation tests for Unity projects with Meta XR Simulator. This introduces a new way for testing many manual tests."
last_updated: "2024-07-02"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

## Overview

Meta XR Simulator can accelerate app and game development and is also a useful tool for testing purposes.

### Testing approaches

The tool offers three distinct approaches: zero-code automation testing, programmatic automation testing, and a hybrid combination of both.

#### Zero-code automation testing

Zero-code automation testing employs a record-and-replay methodology with snapshot testing. This approach requires no modifications to your app or game and doesn't necessitate additional libraries or custom code. It facilitates effortless test creation without the need for coding, making it particularly advantageous for smoke testing. It's recommended to integrate such tests into Continuous Integration (CI) pipelines, executing them regularly (for example, per pull request, hourly, or nightly). These tests can seamlessly run on cloud infrastructure without the requirement for physical Meta XR devices. See [Zero-code automation testing](/documentation/unity/xrsim-unity-automated-testing-in-action-pt-1.0/) for more information.

#### Programmatic automation testing

Programmatic automation testing offers a more precise examination of your application. For instance, scenarios where an object is obscured behind another object cannot be captured through screenshots alone, but can be validated using this method. Additionally, this approach can handle dynamic situations that the zero-code approach may struggle with, such as randomly spawning game objects. See [Programmatic Automation Testing](/documentation/unity/xrsim-unity-automated-testing-programmatic-1.0/) for more information.

#### The hybrid approach

The hybrid approach combines the strengths of both zero-code and programmatic methods. It is useful when testing specific areas of an application that are not accessible at the beginning of gameplay. In that scenario, the zero-code automation can navigate the game to the desired location, followed by programmatic testing to scrutinize the application's behavior in the desired context. See [Hybrid Automation Testing](/documentation/unity/xrsim-unity-automated-testing-hybrid-1.0/) for more information.

### Use cases for automation testing with XR Simulator

XR Simulator also offers testing functionality on the following use cases:

#### Testing apps with different room layouts(Synthetic Environment)

The simulator includes built-in rooms and tools for creating custom room layouts, ensuring your app can handle a variety of environments. This is ideal for automation testing. Developers can focus on developing with a primary selection of rooms, while automation testing handles other layouts and generates test reports. The tests can be run on either CI or a QA computer. See [Test App with Different Room Layouts (Synthetic Environments)](/documentation/unity/xrsim-unity-automated-testing-in-action-rooms-1.0/) for more information.

#### Mutliplayer testing

XR Simulator enables testing multiple game clients on a single computer, but switching between windows to interact with different clients can be cumbersome. Using the recording and replaying feature allows you to automatically run other clients, letting you to focus on testing and debugging the main client. Additionally, all client interactions can be recorded and replayed in CI for repeated testing. See [Automate Multiplayer Testing](/documentation/unity/xrsim-unity-automated-testing-multiplayer-1.0/) for more information.