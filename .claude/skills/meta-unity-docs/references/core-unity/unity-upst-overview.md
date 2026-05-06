# Unity Upst Overview

**Documentation Index:** Learn about unity upst overview in this documentation.

---

---
title: "Project Setup Tool"
description: "Configure your Unity project for Meta Quest development using automated configuration tasks and rules."
last_updated: "2025-01-22"
---

The Unity Project Setup Tool is designed to help developers quickly configure projects using the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/). It guides you through the necessary steps so you can start developing more quickly. It tests a registry of rules called Configuration Tasks. We provide default rules to make your project Meta Quest ready. But you can also add your own rules through Configuration Tasks.

## Prerequisites

The Unity Project Setup Tool requires [Oculus Integration SDK v49 to v57](/downloads/package/unity-integration) or the [Meta XR Core SDK v59 or higher](/downloads/package/meta-xr-core-sdk/) to function. This can be imported as a standalone package or bundled as part of the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm).

## Opening the Project Setup Tool

To open the Project Setup Tool in the Unity Editor, navigate to **Meta** > **Tools** > **Project Setup Tool**.

**Note**: You can also open the Project Setup Tool from the menu by navigating to **Edit** > **Project Settings** > **Meta XR**.

<image alt="Project Setup Tool checklist panel showing configuration tasks grouped by category and status." style="width: 600px;" src="/images/upst-checklist.png"/>
This is the main panel on which all the Configuration Tasks are displayed, per target platform and per categories and/or levels. It provides a dashboard showing off any outstanding issues that have been detected on the project, as well as listing all those that have been validated or ignored.

### Actions
The following actions are available for this tool:
- **Target Group**: Switch between the different build target groups to see the list of tasks that apply to each target group.
- **Filter by Group**: Lets you filter the tasks by group such as packages, compatibility, features, and rendering.
- **Fix All**: It goes through all the outstanding settings that are necessary for a Meta Quest app.

Under the cog menu in the upper right, the following actions are available:
- **Background Checks**: A toggle that enables continuous background checks on the project's status and rules.
- **Required throw errors**: Uncheck to ignore failing tasks when building/running.
- **Log outstanding issues**: Uncheck to remove any logs in the console. This would prevent log spam if any.
- **Show Status Icon**: Uncheck to hide the status icon on the bottom right of the editor.
- **Produce Report on Build**: Generate a json report which lists all the rules and their status for the current build target.

## Configuration tasks

The pane lists all the configuration tasks for a given project. The default settings will configures Meta Quest application to be able to develop in XR.

<image alt="A single configuration task entry in the Project Setup Tool with Fix, Docs, and Ignore actions." style="width: 600px;" src="/images/upst-task.png"/>
Configuration Tasks are atomic requirements that can be checked and validated with one single action. They can be seen as technical requirements, recommendations, best practice and any individual check that can be statically checked in the project. Those tasks are registered to the tool and checked at various points. If the conditions are not met to validate the task, the task will be marked as outstanding.

### Actions
A Configuration Task is regularly checked for its validation status. The user can, however, interact directly with a Task in the following ways:
- **Fix/Apply**: Manually call the fix delegate for this Task in order to resolve the issue. This action is only available for tasks that are not already validated.
- **Documentation**: Open the related documentation following the specified URL in the Configuration Task description. If no URL has been specified, this option is not visible.
- **Ignore / Unignore**: This moves the task to another category that will get ignored for both checks and fixes. This gives the control back to developers who may not want to be forced to follow some guidelines or even requirements in some specific cases.

## Implementing your own configuration task with `AddTask`
*From `v50` and higher*

You can add a new Configuration Task with a single function call in your code. Use configuration tasks to create custom setup tasks, configure custom tooling, or set up custom technology in every project or scene.

At any point in your code, you can add a new task that will get registered to the tool via the `OVRProjectSetup.AddTask` method. There are plenty of parameters that can be filled to customize your task. A Configuration Task is an atomic requirement that can be statically checked in the project. The task's validation is binary (true or false), and the developer must provide an `isDone` function to test it. Similarly, a `fix` function can be provided to make the necessary changes for the task to pass.

```csharp
public static void OVRProjectSetup.AddTask(
        OVRProjectSetup.TaskGroup group,
        Func<BuildTargetGroup, bool> isDone,
        BuildTargetGroup platform = BuildTargetGroup.Unknown,
        Action<BuildTargetGroup> fix = null,
        OVRProjectSetup.TaskLevel level = OVRProjectSetup.TaskLevel.Recommended,
        Func<BuildTargetGroup, OVRProjectSetup.TaskLevel> conditionalLevel = null,
        string message = null,
        Func<BuildTargetGroup, string> conditionalMessage = null,
        string fixMessage = null,
        Func<BuildTargetGroup, string> conditionalFixMessage = null,
        string url = null,
        Func<BuildTargetGroup, string> conditionalUrl = null,
        bool validity = true,
        Func<BuildTargetGroup, bool> conditionalValidity = null);
```
### Description
Add an `OVRConfigurationTask` to the Setup Tool.

### Remarks
- This method creates, adds and registers an `OVRConfigurationTask` to the Setup Tool.
Note that the `Message` or `ConditionalMessage` parameters must be unique, as they are hashed to generate a Unique ID for the task.

- Once added, tasks are not meant to be removed from the Setup Tool and will be checked at key points.

- This method is the entry point for developers to add sanity checks, technical requirements, or recommendations.

- Use conditional parameters with lambdas or delegates for more complex behaviors if needed.

### Parameters
- `group`: Category that fits the task. Feel free to add more to the enum if relevant. Do not use "All".
- `isDone`: Delegate that checks if the Configuration Task is validated or not.
- `platform`: Platform for which this Configuration Task applies. Use "Unknown" for any.
- `fix`: Delegate that validates the Configuration Task.
- `level`: Severity (or behaviour) of the Configuration Task.
- `conditionalLevel`: Use this delegate for more control or complex behaviours over the `level` parameter.
- `message`: Description of the Configuration Task.
- `conditionalMessage`: Use this delegate for more control or complex behaviours over the `message` parameter.
- `fixMessage`: Description of the actual fix for the Task.
- `conditionalFixMessage`: Use this delegate for more control or complex behaviours over the `fixMessage` parameter.
- `url`: Url to more information about the Configuration Task.
- `conditionalUrl`: Use this delegate for more control or complex behaviours over the `url` parameter.
- `validity`: Checks if the task is valid. If not, it will be ignored by the Setup Tool.
- `conditionalValidity`: Use this delegate for more control or complex behaviours over the `validity` parameter.

### Exceptions
- `ArgumentNullException`: Possible causes:
  - If either `message` or `conditionalMessage` do not provide a valid non-null string.
  - `isDone` is null.
  - `fix` is null.
- `ArgumentException`: Possible causes:
  - `group` is set to "All". This category is not meant to be used to describe a task.
  - A task with the same unique ID already has been registered (conflict in hash generated from description message).

### Examples
Here is an example of the implementation of a very simple Configuration Task with the following parameters:
- This task is a *Recommendation*.
- It will be shown in the *Quality* category.
- Its description message is: *Set Texture Quality to Full Res*.
- Both `isDone` and `fix` functions are pretty straightforward and directly test or alter the `QualitySettings` of the project.

```csharp
OVRProjectSetup.AddTask(
    level: OVRConfigurationTask.TaskLevel.Recommended,
    group: OVRConfigurationTask.TaskGroup.Quality,
    message: "Set Texture Quality to Full Res",
    isDone: buildTargetGroup => QualitySettings.masterTextureLimit == 0,
    fix: buildTargetGroup => QualitySettings.masterTextureLimit = 0
);
```

### Validity

Call the method as early as possible. Although it's not mandatory, this ensures the task is tested early and shown to the user in the task list.

If the task is not valid in some states, use the `conditionalValidity` parameter to ignore it if conditions are not met.

In this example, the tasks cannot be checked before getting the list from the Package Manager, so the `conditionalValidity` parameter checks availability.

```csharp
OVRProjectSetup.AddTask(
    conditionalValidity: buildTargetGroup => OVRProjectSetupUtils.PackageManagerListAvailable,
    level: OVRConfigurationTask.TaskLevel.Required,
    group: OVRConfigurationTask.TaskGroup.XR,
    isDone: buildTargetGroup => OVRProjectSetupUtils.IsPackageInstalled(OculusXRPackageName),
    message: "The Oculus XR Plug-in package must be installed",
    fix: buildTargetGroup => OVRProjectSetupUtils.InstallPackage(OculusXRPackageName)
);
```

### Conditional parameters

If you need more control, most parameters have a conditional alternative parameter that lets the developer provide a delegate instead of a fixed value. This can help change the behavior of the task in more elaborated ways if needed.

This next example requires a different pixel light count for each build target group. We use the `conditionalMessage` parameter to communicate the correct amount to the user.

```csharp
OVRProjectSetup.AddTask(
    level: OVRConfigurationTask.TaskLevel.Recommended,
    group: OVRConfigurationTask.TaskGroup.Quality,
    isDone: buildTargetGroup => QualitySettings.pixelLightCount <= GetRecommendedPixelLightCount(buildTargetGroup),
    conditionalMessage: buildTargetGroup => $"Set maximum pixel lights count to {GetRecommendedPixelLightCount(buildTargetGroup)}",
    fix: buildTargetGroup => QualitySettings.pixelLightCount = GetRecommendedPixelLightCount(buildTargetGroup)
);
```

## Generated report
*From `v52` and higher*

It is possible to generate a .json report containing the digest of the Unity Project Setup Tool checklist. This report can be a good indicator of the health of a given project and can easily be parsed and/or processed by external tools.

### Format
```json
{
  "createdAt": "[Date]",
  "platform" : "[PlatformName]",
  "projectName": "[ProjectName]",
  "unityVersion": "[UnityVersion]",
  "projectUrl": "[ProjectPath]",
  "tasksStatus":
  [
    {
      "uid": "[Hash]",
      "group": "[TaskGroup]",
      "message": "[TaskMessage]",
      "level": "[TaskLevel]",
      "isDone": bool
    },
    {...}
  ]
}
```

### Usage
- **On Demand** : Click on the menu icon within the Checklist, under the platform tab to generate a report for the selected platform. The .json report will be generated on the specified folder in the folder selection dialog box.
- **On Build** : Click the cog icon in the Unity Project Setup Tool window to toggle on/off the automatic generation of a report on build. The .json file will be generated on the same folder as the build.
- **In Batchmode** : Start the editor in batchmode and execute the following method:
```csharp
public static void GenerateProjectSetupReport()
```

### Description
Generate a project setup report and write it to a file.
### Remarks
This generates a project setup report for the active platform build target and write it to a file. The active platform build target may be specified using the "-buildTarget" CLI argument. The output file can be specified using the "-reportFile" CLI argument. If not specified, a file with a generated name will be created in the current folder.
### Example
```batch
> [UnityPath] -project [ProjectPath] -executeMethod OVRProjectSetupCLI.GenerateProjectSetupReport -reportFile [ReportFileName] buildTarget [PlatformName]
```