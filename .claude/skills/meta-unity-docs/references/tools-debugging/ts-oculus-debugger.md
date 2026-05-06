# Ts Oculus Debugger

**Documentation Index:** Learn about ts oculus debugger in this documentation.

---

---
title: "Using Oculus Debugger for VS Code"
description: "Debug Meta Quest applications in real time using the VS Code extension."
---

Oculus Debugger is a Visual Studio (VS) Code extension that is primarily intended for real-time debugging of Unreal Engine and Native apps for Meta Quest headsets. It fully supports debugging Meta Quest applications (APK files) and Meta Quest Native C/C++ binaries and is integrated with Unreal Engine's project generator.

## Features of the Oculus Debugger Extension

* Attaches to and/or launches targets defined in `launch.json`.
* Supports debugging native C/C++ (ELF) binaries.
* Supports debugging Meta Quest apps shipped via APK files.
* Integrates with Unreal Engine, which produces a VS Code workspace file with build and launch configurations.
* Uses LLDB debugger, which is downloaded from Meta servers so no additional setup is required.
* Supports setting breakpoints or watch variables, stepping over lines of code, stepping into executed functions, views of running threads, and their stacktraces.

## Installation

1. Download and install VS Code from the [VS Code](https://code.visualstudio.com/) website.
2. Open VS Code and click the **Extensions** icon.
3. Search "Oculus Debugger" from MS Marketplace and then install it.

## Troubleshooting

* Using the **Command Palette** to execute **Clean LLDB caches and Oculus Runtime downloads** can be helpful if the debug session is stuck. If the session can't start and it's unclear what the issue is, the problem is sometimes in broken LLDB caches or Meta Quest runtime installations. In those situations clearing them by using this command may help resolve the issue.
* If you are having trouble and wish to file a bug report, you can submit a bug report at (https://github.com/facebookexperimental/oculus-debugger-vscode/). To file a bug from within VS Code, use the links in the Marketplace or use **Help: Report Issue** from the Command Palette. You can also click the **Tweet Feedback** button in the bottom-right corner and select the **Submit a Bug** link.

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)