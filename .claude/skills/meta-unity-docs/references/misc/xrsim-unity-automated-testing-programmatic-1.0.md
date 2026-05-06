# Xrsim Unity Automated Testing Programmatic 1.0

**Documentation Index:** Learn about xrsim unity automated testing programmatic 1.0 in this documentation.

---

---
title: "Programmatic Automation Testing"
description: "Write script-based tests that validate dynamic scenes and hidden objects using the Meta XR Simulator."
last_updated: "2024-06-10"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

Programmatic automation testing offers a more precise examination of your application. For instance, scenarios where an object is obscured behind another object cannot be captured through screenshots alone but can be validated using this method. Additionally, this approach can handle dynamic situations that the zero-code approach may struggle with, such as randomly spawned game objects.

In this guide, you will use [Oculus Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples) as our project to be tested. You will programmatically load the **SceneManager** scene and check whether the app successfully loaded room data: walls and ceiling.

The test itself is simple, but because this project doesn't have existing play mode tests, we will need to spend some extra time setting up the test. Below are the outlined steps:

1. Clone [Oculus Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples).
2. Download Meta XR Simulator.
3. Create test assembly definition.
4. Write the test.
5. Add test scene to build settings.
6. Prepare Meta XR Simulator.
7. Run the test.
8. Check the result.

## Complete script

Preparing the project and Meta XR Simulator are similar to [Zero-code Automation Testing In Action](/documentation/unity/xrsim-unity-automated-testing-in-action-pt-1.0/).

Creating and setting up tests (Steps 3, 4, 5) are the key parts, which you can do manually in the Unity Editor. However, to ensure you can easily replicate exactly what we did in the guide, we will do all that via scripts.

```
# 1. Clone Oculus Unity-StarterSamples

$testRootPath = "C:\tmp"
$xrSimulatorPath = "$testRootPath\MetaXRSimulator"
$projectPath = "$testRootPath\Unity-StarterSamples"
$testFolderPath = "$projectPath\Assets\Tests"
$playModeFolderPath = "$testFolderPath\PlayMode"
$assemblyDefinitionPath = "$playModeFolderPath\PlayModeTests.asmdef"
$testScriptPath = "$playModeFolderPath\ExamplePlayModeTest.cs"

mkdir $testRootPath
cd $testRootPath
git clone https://github.com/oculus-samples/Unity-StarterSamples.git

# 2. Download and Unzip Meta XR Simulator [Start]
# If you already have Meta XR Simulator install, skip to the next step of this script. (Note: the download progress can take up to 5 minutes. Alternatively you can manually download it from https://developers.meta.com/horizon/downloads/package/meta-xr-simulator/)

cd $testRootPath

Invoke-WebRequest -Uri https://npm.developer.oculus.com/com.meta.xr.simulator/-/com.meta.xr.simulator-65.0.0.tgz -OutFile MetaXRSimulator.tgz

tar -xvzf "MetaXRSimulator.tgz"

Start-Sleep 2

Rename-Item -Path "package" -NewName "MetaXRSimulator"

# Download and Unzip Meta XR Simulator [End]

# 3. Create test assembly definition

if (!(Test-Path -Path $testFolderPath)) {
    New-Item -ItemType Directory -Path $testFolderPath
}

if (!(Test-Path -Path $playModeFolderPath)) {
    New-Item -ItemType Directory -Path $playModeFolderPath
}

$assemblyDefinitionContent = @"
{
    "name": "PlayModeTests",
    "references": [
        "Oculus.VR",
        "UnityEngine.TestRunner",
        "UnityEditor.TestRunner"
    ],
    "includePlatforms": [],
    "excludePlatforms": [],
    "allowUnsafeCode": false,
    "overrideReferences": true,
    "precompiledReferences": [
        "nunit.framework.dll"
    ],
    "autoReferenced": false,
    "defineConstraints": [
        "UNITY_INCLUDE_TESTS"
    ],
    "versionDefines": [],
    "noEngineReferences": false
}
"@

if (!(Test-Path -Path $assemblyDefinitionPath)) {
    $assemblyDefinitionContent | Out-File -FilePath $assemblyDefinitionPath -Encoding utf8
    Write-Output "Assembly definition created at $assemblyDefinitionPath"
} else {
    Write-Output "Assembly definition already exists at $assemblyDefinitionPath"
}

# 4. Create a test script

$testScriptContent = @"
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Assertions;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.SceneManagement;
using System.Linq;

public class ExamplePlayModeTest
{
    [UnityTest]
    public IEnumerator CheckSceneDataLoadedSuccessfully()
    {

        var scene = "Assets/StarterSamples/Usage/SceneManager.unity";

        NUnit.Framework.Assert.IsTrue(OVRPlugin.initialized, "OVRPlugin failed to initialize. " +
                                             "This could be caused by the XR simulator not finding/loading a .dll " +
                                             "dependency at runtime");

        yield return LoadTestScene(scene);

        yield return new WaitForSeconds(10);

        UnityEngine.Assertions.Assert.AreEqual(FindSceneAnchorsBySemanticClassificationLabel(OVRSceneManager.Classification.Ceiling).Length, 1);
        UnityEngine.Assertions.Assert.AreEqual(FindSceneAnchorsBySemanticClassificationLabel(OVRSceneManager.Classification.WallFace).Length, 8);// Bedroom has 8 wallfaces.
        UnityEngine.Assertions.Assert.AreEqual(FindSceneAnchorsBySemanticClassificationLabel(OVRSceneManager.Classification.DoorFrame).Length, 1);
        UnityEngine.Assertions.Assert.AreEqual(FindSceneAnchorsBySemanticClassificationLabel(OVRSceneManager.Classification.WindowFrame).Length, 2);

        UnityEngine.Assertions.Assert.IsTrue(DoesSceneAnchorExistInHierarchy(OVRSceneManager.Classification.Floor), "Find Floor in the Scene");

        yield return null;
    }

    protected OVRSemanticClassification[] FindSceneAnchorsBySemanticClassificationLabel(string substring)
    {
        OVRSemanticClassification[] allObjects = (OVRSemanticClassification[])GameObject.FindObjectsOfType(typeof(OVRSemanticClassification));
        var matchingObjects = allObjects.Where(obj => obj.Labels.Where(label => label.ToLower().Contains(substring.ToLower())).Any()).ToArray();
        return matchingObjects;
    }

    protected bool DoesSceneAnchorExistInHierarchy(string semanticClassificationLabel)
    {
        OVRSemanticClassification[] allObjects = (OVRSemanticClassification[])GameObject.FindObjectsOfType(typeof(OVRSemanticClassification));
        var matchingObjects = allObjects.Where(obj => obj.Labels.Where(label => label.Equals(semanticClassificationLabel)).Any()).ToArray();
        return matchingObjects.Any();
    }

    public static IEnumerator LoadTestScene(string scene)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(scene);
        yield return new WaitUntil(() => asyncOperation.isDone);
    }

    public static IEnumerator UnloadTestScene(string scene)
    {
        var asyncOperation = SceneManager.UnloadSceneAsync(scene);
        yield return new WaitUntil(() => asyncOperation.isDone);
    }
}
"@

if (!(Test-Path -Path $testScriptPath)) {
    $testScriptContent | Out-File -FilePath $testScriptPath -Encoding utf8
    Write-Output "Test script created at $testScriptPath"
} else {
    Write-Output "Test script already exists at $testScriptPath"
}

# 5. Add test scene to build settings

$editorBuildSettingsPath = "$projectPath\ProjectSettings\EditorBuildSettings.asset"

$targetScenePath = "Assets/StarterSamples/Usage/SceneManager.unity"
$editorBuildSettingsContent = Get-Content -Path $editorBuildSettingsPath
$sceneFound = $false

for ($i = 0; $i -lt $editorBuildSettingsContent.Length; $i++) {
    if ($editorBuildSettingsContent[$i].Trim() -eq "path: $targetScenePath") {
        # The scene path is found, now look for the 'enabled' line above it
        if ($i -ge 1 -and $editorBuildSettingsContent[$i - 1].Trim() -eq "- enabled: 0") {
            # Change the enabled status from 0 to 1
            $editorBuildSettingsContent[$i - 1] = "  - enabled: 1"
            $sceneFound = $true
            break
        }
    }
}

if ($sceneFound) {
    Set-Content -Path $editorBuildSettingsPath -Value $editorBuildSettingsContent -Force
    Write-Output "Scene '$targetScenePath' has been enabled in the build settings."
} else {
    Write-Output "Scene '$targetScenePath' not found or is already enabled in the build settings."
}

# 6. Launch a Synthetic Environment
Start-Process -FilePath "$xrSimulatorPath/MetaXRSimulator\.synth_env_server\synth_env_server.exe" -ArgumentList "Bedroom" -PassThru

# 7. Activate Meta XR Simulator

& "$xrSimulatorPath/MetaXRSimulator/activate_simulator.ps1"

# Construct the argument list with proper variable interpolation
$argumentList = "-runTests -projectPath $projectPath -testResults $projectPath\test.xml -testPlatform PlayMode -logFile $projectPath\log.log"

# 8. Run the Unity editor with the specified arguments
Start-Process -FilePath "C:\Program Files\Unity\Hub\Editor\2022.3.20f1\Editor\Unity.exe" -ArgumentList $argumentList -Wait

# 9. Deactivate Meta XR Simulator
& "$xrSimulatorPath\MetaXRSimulator\deactivate_simulator.ps1"

# 10. Stop Synthetic Environment
Get-Process -Name "synth_env_server" | Stop-Process

# 11. Check Test result
Start-Process -FilePath "$projectPath\test.xml"

echo done

```

## Recommendations:

1. If running the script for the first time fails, manually opening the project in the Unity Editor might be necessary to view errors. Most often, these are dependency issues caused by network failures.