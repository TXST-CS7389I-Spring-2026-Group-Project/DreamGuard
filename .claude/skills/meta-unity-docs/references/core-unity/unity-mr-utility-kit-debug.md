# Unity Mr Utility Kit Debug

**Documentation Index:** Learn about unity mr utility kit debug in this documentation.

---

---
title: "Mixed Reality Utility Kit – Debug and Test your MRUK app"
description: "Use SceneDebugger and the Spatial Testing framework to visualize and verify MRUK scene data in Unity."
last_updated: "2026-02-16"
---

## Learning Objectives

- **Enable** and configure the Immersive Debugger for MRUK in Editor and Development Builds.
- **Visualize** scene data such as meshes, NavMesh and anchors via the **SceneDebugger** prefab.
- **Interact** with scene queries (raycasts, key walls, surface finds) in real time to verify behavior.
- **Integrate** the debugger into your build pipeline to test on-device without leaving the headset.
- **Automate** spatial testing across multiple room configurations using the MRUK Spatial Testing framework.
- **Analyze** test results with the integrated Test Report Viewer.

<box height="10px"></box>
---
<box height="10px"></box>

## Scene Debugger Overview

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img
      src="/images/MRUK-Scene-Debugger.png"
      alt="MRUK Scene Debugger"
      border-radius="12px"
      width="100%"
    />
  </box>
  <box max-width="400px">
    <p>
      The <b>SceneDebugger</b> is a suite of visualization tools within the Immersive Debugger that lets you inspect MRUK scene data and observe how spatial queries operate live in your app. The <b>SceneDebugger</b> prefab provides a live, in-headset visualization of MRUK scene data such as meshes, NavMesh and anchors. This lets you run spatial queries interactively via the Immersive Debugger menu.
    </p>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Setup

1. **Enable Immersive Debugger:** In Unity, go to **Meta → Tools → Immersive Debugger** and check **Enable**.
2. **Development Build:** In **File** > **Build Profiles**, toggle **Development Build** on for your target platform.
3. **Assign Prefab:** In your scene’s MRUK component, set **ImmersiveSceneDebuggerPrefab** to `MetaMRUtilityKit/Core/Tools/ImmersiveSceneDebugger.prefab`.

<box height="10px"></box>
---
<box height="10px"></box>

## Debugger Menus & Features

<box display="flex" flex-direction="row" padding-vertical="16">
  <box width="50%" padding-end="16">
    <ul>
      <li>Toggle visualization of the Scene Mesh.</li>
      <li>Inspect any baked or runtime NavMesh.</li>
      <li>Show/hide semantic anchors (walls, floors, volumes).</li>
    </ul>
  </box>
  <box width="50%" padding-start="16">
    <ul>
      <li>Run individual queries <b>GetKeyWall</b>, <b>GetLargestSurface</b>, <b>IsPositionInRoom</b>, etc.</li>
      <li>See real-time overlays of hit points, normals, and boundary outlines.</li>
      <li>Combine toggles and tests to fully validate your placement logic.</li>
    </ul>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## On-Device Debugging

With the Immersive Debugger enabled in a Development Build, all debugger menus are accessible in-headset via the system menu. Test without Editor tethering, adjusting settings and running queries directly on the device to validate real-world behavior.

<box height="10px"></box>
---
<box height="20px"></box>

## MRUK Spatial Testing

The MRUK Spatial Testing framework provides automated testing capabilities for validating spatial-dependent functionality across multiple room configurations. This framework enables developers to ensure robust behavior of mixed reality features across different physical environments by running tests against various room layouts automatically.

### Overview

The MRUK Spatial Testing framework is built on top of Unity's standard test runner, allowing tests to be easily upgraded to make use of extra scene-specific testing features. Key benefits include:

- **Multi-Room Testing**: Automatically run tests across all configured room prefabs or JSON scenes
- **Comprehensive Reporting**: Detailed failure analysis with room-specific insights through the integrated Test Report Viewer
- **CI/CD Compatible**: Works seamlessly in continuous integration environments
- **Custom Configuration**: Support for both project-wide settings and per-test custom configurations
- **Unity Test Runner Integration**: Tests are exposed as standard Unity tests and appear in the **Test Runner** window (**Window > General > Test Runner**), allowing you to run, debug, and view results using Unity's familiar testing interface

For a complete working example of MRUK spatial tests, see the [Unity-MRUtilityKitSample project on GitHub](https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/Tests).

### Setting Up a Tests Assembly Folder

Before writing spatial tests, you need to create a Tests assembly folder and configure the required assembly references.

#### Creating the Tests Folder:

1. In the Unity Project window, right-click in your desired location
2. Navigate to **Create > Testing > Tests Assembly Folder**
3. Name the folder appropriately, for example `Tests`

#### Configuring Assembly References:

After creating the Tests assembly folder, select the generated assembly definition file and add the following **Assembly Definition References**:

| Reference | Description |
|-----------|-------------|
| `meta.xr.mrutilitykit` | Core MRUK functionality |
| `meta.xr.mrutilitykit.tests` | MRUK testing framework (contains `MRUKTestBase`) |
| `Oculus.VR` | Oculus VR SDK integration |

> **Note:** Ensure **Use GUIDs** is checked for more reliable reference resolution across project changes.

For more information about Unity's testing framework, see the [Unity Test Framework documentation](https://docs.unity3d.com/6000.0/Documentation/Manual/testing-editortestsrunner.html).

### Accessing MRUK Test Settings

To configure the MRUK Spatial Testing framework:

1. Open Unity's **Project Settings** window (**Edit > Project Settings**)
2. Navigate to **Meta XR > MRUK Tests Settings** in the left sidebar

<box height="10px"></box>
---
<box height="10px"></box>

### MRUK Tests Settings

The MRUK Tests Settings panel provides comprehensive configuration options for the testing framework.

#### Test Configuration

| Setting | Description |
|---------|-------------|
| **Data Source** | Choose between `Prefab` or `Json` as the source for room configurations. When set to Prefab, tests use Room Prefabs; when set to Json, tests use Scene JSON files. |
| **Load Scene on Startup** | When enabled, automatically loads the configured scene when tests start. |
| **Enable World Lock** | Enables world lock functionality during test execution. |

#### Room Configuration

| Setting | Description |
|---------|-------------|
| **Room Index** | The index of the room to start testing from (0-based). |
| **Seat Width** | The default seat width parameter used during testing (in meters). |

#### Test Assets

Configure the room configurations used for testing:

##### Room Prefabs
- An array of room prefab GameObjects that will be used when Data Source is set to `Prefab`
- Each prefab represents a different room layout for testing
- Add prefabs using the + button or drag and drop from the Project window

##### Scene JSON Files
- An array of TextAsset JSON files that will be used when Data Source is set to `Json`
- Each JSON file contains serialized scene data representing a room configuration
- Useful for testing with real-world captured room data

#### Test Reporting

| Setting | Description |
|---------|-------------|
| **Enable Notifications** | When enabled, displays notifications when tests complete. |
| **Auto-open on Failure** | When enabled, automatically opens the Test Report Viewer window when test failures occur. |
| **Max Reports to Keep** | The maximum number of test reports to retain in history. Older reports are automatically removed. |

#### Actions

##### Initialize from MRUK Prefab
- Populates the test settings with default room prefabs and scene JSON files from the MRUK package
- Useful for quickly setting up a testing environment with built-in room configurations

##### Open Test Report Window
- Opens the MRUK Test Report Viewer window to view test results

<box height="10px"></box>
---
<box height="10px"></box>

### Writing Spatial Tests

#### Creating a Basic Test

To create a spatial test, extend the `MRUKTestBase` class and use the `RunTestOnAllScenes` method:

```csharp
using System.Collections;
using Meta.XR.MRUtilityKit;
using Meta.XR.MRUtilityKit.Tests;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace MyProject.Tests
{
    public class MySpatialTests : MRUKTestBase
    {
        [UnityTest]
        [Timeout(DefaultTimeoutMs)]
        public IEnumerator TestRoomHasWalls()
        {
            yield return RunTestOnAllScenes(ExecuteWallTest);
        }

        private IEnumerator ExecuteWallTest(MRUKRoom room)
        {
            // Get all wall anchors in the room
            var walls = room.Anchors.FindAll(x =>
                x.HasAnyLabel(MRUKAnchor.SceneLabels.WALL_FACE));

            // Verify the room has walls
            Assert.IsTrue(walls.Count > 0,
                $"Room {room.name} should have at least one wall");

            yield return null;
        }
    }
}
```

#### Using Setup and Teardown Callbacks

The `MRUKTestBase` class provides lifecycle hooks for per-room setup and teardown:

```csharp
public class MyTestWithSetup : MRUKTestBase
{
    private GameObject _testObject;

    public override IEnumerator SetUp()
    {
        yield return base.SetUp();

        // Per-room setup callback
        RoomSetUp = (roomName, room) =>
        {
            _testObject = new GameObject("TestObject");
        };

        // Per-room teardown callback
        RoomTearDown = (roomName, room) =>
        {
            if (_testObject != null)
            {
                Object.DestroyImmediate(_testObject);
            }
        };
    }

    public override IEnumerator TearDown()
    {
        // Final cleanup
        yield return base.TearDown();
    }

    [UnityTest]
    public IEnumerator TestWithObject()
    {
        yield return RunTestOnAllScenes(ExecuteTest);
    }

    private IEnumerator ExecuteTest(MRUKRoom room)
    {
        Assert.IsNotNull(_testObject, "Test object should exist");
        yield return null;
    }
}
```

#### Using Custom Test Settings

For tests that require specific configurations, you can create custom settings on-the-fly:

```csharp
[UnityTest]
[Timeout(DefaultTimeoutMs)]
public IEnumerator TestWithCustomRooms()
{
    // Create temporary custom settings (no asset file created)
    var customSettings = ScriptableObject.CreateInstance<MRUKTestsSettings>();

    // Configure settings for this specific test
    customSettings.SceneSettings.RoomIndex = 0;
    customSettings.SceneSettings.SeatWidth = 0.7f;
    customSettings.SceneSettings.LoadSceneOnStartup = true;
    customSettings.SceneSettings.DataSource = MRUK.SceneDataSource.Prefab;

    // Specify custom room prefabs for this test
    customSettings.SceneSettings.RoomPrefabs = new[]
    {
        AssetDatabase.LoadAssetAtPath<GameObject>(
            "Packages/com.meta.xr.mrutilitykit/Core/Rooms/Prefabs/Bedroom/Bedroom00.prefab"),
        AssetDatabase.LoadAssetAtPath<GameObject>(
            "Packages/com.meta.xr.mrutilitykit/Core/Rooms/Prefabs/LivingRoom/LivingRoom01.prefab")
    };

    // Run the test with custom settings - this re-initializes MRUK
    yield return RunTestOnAllScenes(ExecuteMyTest, customSettings);

    // Cleanup the temporary settings instance
    Object.DestroyImmediate(customSettings);
}
```

<box height="10px"></box>
---
<box height="10px"></box>

### Test Report Viewer

The MRUK Test Report Viewer provides a comprehensive interface for viewing and analyzing test results.

#### Opening the Test Report Viewer

Open the Test Report Viewer using one of these methods:

- From the MRUK Tests Settings panel, click **Open Test Report Window**
- From the menu bar: **Window > Meta > MRUK Test Report Viewer**

#### View Modes

The Test Report Viewer supports two viewing modes, accessible via the **View Mode** dropdown:

##### By Room View

The **By Room** view groups test results by room configuration:

- **Rooms Panel** (left): Lists all rooms with their pass/fail statistics. Shows the number of passed tests vs total tests for each room. Displays success rate percentage. Click a room to view its detailed results.

- **Room Details Panel** (right): Shows detailed information for the selected room, including Room Summary (total test executions, passed count, failed count, and success rate), Test Results by Type (lists each test method with its pass/fail history), and Recent Failures (shows the most recent failure details including timestamp and error message).

##### By Report View

The **By Report** view shows test results chronologically:

- **Test Reports Panel** (left): Lists all test reports with their success rates. Reports are sorted by most recent first and show overall pass rate for each test execution.

- **Report Details Panel** (right): Shows comprehensive details for the selected report, including Test Summary (test method name, timestamp, total tests, passed/failed counts) and Per-Room Statistics (breakdown of results for each room configuration).

#### Filtering and Search

- **Search**: Use the search field to filter results by test name, room name, or timestamp
- **Show Only Failures**: Toggle this checkbox to display only failed tests

#### Exporting Reports

Click the **Export to File** button to export the current report data to a text file for external analysis or sharing with team members.

#### Clearing Reports

Click the **Clear Reports** button to remove all stored test reports and start fresh.

<box height="10px"></box>
---
<box height="10px"></box>

### Best Practices

#### Test Design

1. **Keep tests focused**: Each test method should verify a single aspect of functionality
2. **Use descriptive assertions**: Include room-specific context in assertion messages
3. **Handle async operations**: Use `yield return` for operations that need time to complete

```csharp
private IEnumerator TestSpawnPositions(MRUKRoom room)
{
    spawner.StartSpawn();

    // Wait for spawning to complete
    yield return new WaitForSeconds(1f);

    Assert.AreEqual(expectedCount, spawner.SpawnedObjects.Count,
        $"Expected {expectedCount} objects in room {room.name}");
}
```

#### Room Configuration Best Practices

1. **Test diverse configurations**: Include rooms with different sizes, shapes, and furniture layouts
2. **Use embedded defaults**: The framework provides built-in room configurations for quick setup
3. **Include edge cases**: Test rooms with minimal furniture, unusual shapes, or specific features

#### Result Analysis

1. **Review per-room failures**: Use the By Room view to identify which room configurations cause failures
2. **Track success rates over time**: Monitor test stability across multiple runs
3. **Export reports for CI/CD**: Use the export functionality to integrate with build pipelines

<box height="10px"></box>
---
<box height="20px"></box>

← **Previous:** [Share Your Space with Others](/documentation/unity/unity-mr-utility-kit-space-sharing/)

→ **Next:** [MRUK Samples](/documentation/unity/unity-mr-utility-kit-samples/)

<box height="20px"></box>

## Related Content

Explore more MRUK documentation topics to dive deeper into spatial queries, content placement, manipulation, sharing, and debugging.

### Core Concepts

- [Overview](/documentation/unity/unity-mr-utility-kit-overview)
  Get an overview of MRUK's key areas and features.
- [Getting Started](/documentation/unity/unity-mr-utility-kit-gs/)
  Set up your project, install MRUK, and understand space setup with available Building Blocks.
- [Place Content without Scene](/documentation/unity/unity-mr-utility-kit-environment-raycast)
  Use Environment Raycasting to place 3D objects into physical space with minimal setup.
- [Manage Scene Data](/documentation/unity/unity-mr-utility-kit-manage-scene-data)
  Work with MRUKRoom, EffectMesh, anchors, and semantic labels to reflect room structure.

### Content & Interaction

- [Place Content with Scene](/documentation/unity/unity-mr-utility-kit-content-placement)
  Combine spatial data with placement logic to add interactive content in the right places.
- [Manipulate Scene Visuals](/documentation/unity/unity-mr-utility-kit-manipulate-scene-visuals)
  Replace or remove geometry, apply effects, and adapt scenes using semantics and EffectMesh.
- [Track Objects in MRUK](/documentation/unity/unity-mr-utility-kit-trackables)
  Track keyboards using MRUK-trackables.

### Multiuser

- [Share Your Space with Others](/documentation/unity/unity-mr-utility-kit-space-sharing)
  Use MRUK’s Space Sharing API to sync scene geometry across multiple users.

### MRUK Samples & Tutorials

- [MRUK Samples](/documentation/unity/unity-mr-utility-kit-samples)
  Explore sample scenes like NavMesh, Virtual Home, Scene Decorator, and more.
- [Mixed Reality Motifs](/documentation/unity/unity-mrmotifs-instant-content-placement)
  Instant Content Placement.

### Mixed Reality Design Principles

- [Mixed Reality Best Practices](/design/mr-overview)
  Design MR apps that are comfortable, intuitive, and immersive.