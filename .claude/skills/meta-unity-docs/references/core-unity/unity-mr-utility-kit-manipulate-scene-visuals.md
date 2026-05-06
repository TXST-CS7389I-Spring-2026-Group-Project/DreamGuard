# Unity Mr Utility Kit Manipulate Scene Visuals

**Documentation Index:** Learn about unity mr utility kit manipulate scene visuals in this documentation.

---

---
title: "Mixed Reality Utility Kit – Manipulate Scene Visuals"
description: "Cut holes in geometry, apply selective passthrough, and manipulate scene visuals with MRUK EffectMesh."
last_updated: "2025-09-18"
---

## Learning Objectives

- **Use** **EffectMesh** to cut holes in scene geometry (doors, windows, ceilings).
- **Configure** the **DestructibleGlobalMeshSpawner** for runtime fragmentation and destruction.
- **Apply** the Selective Passthrough shader to hide or reveal real-world surfaces dynamically.
- **Implement** a ceiling-reveal workflow via custom scripts or prefab spawners.

<box height="10px"></box>

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <img
    src="/images/unity-mruk-manipulate-room.png"
    alt="Manipulate Room Sample"
    border-radius="12px"
    width="100%"
  />
</box>

<box height="10px"></box>

## Overview

Beyond placing content, MRUK lets you reshape and visualize the real-world scene itself. You can cut out parts of walls or ceilings, spawn destructible fragments, or use passthrough shaders to reveal portals into VR or AR.

<box height="10px"></box>
---
<box height="10px"></box>

## Cutting Holes with EffectMesh

<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px" padding-end="24">
    <p> The <b>EffectMesh</b> prefab can cut out semantic labels like doors, windows and ceilings by excluding them from the generated mesh. In the inspector, under <b>Cut Holes</b>, select any labels you wish to remove, creating a hole in that real-world surface, such as CEILING or WINDOW_FRAME. </p>
  </box>
  <box>
    <img src="/images/unity-mruk-cutholes.png" alt="EffectMesh Cut Holes" border-radius="12px" width="100%" />
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Destructible Global Mesh

Spawn a runtime, breakable version of the entire Scene Mesh. Fragments can be independently destroyed or manipulated for dramatic effects.

- **Reserved Spaces:** Specify non-fragmentable regions.
- **Segments Density:** Control fragment count vs. performance.
- **Material:** Assign custom materials per fragment.

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img src="/images/unity-mruk-detrglobalmesh.png" alt="Destructible Mesh" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
    <p>Each fragment is a separate GameObject with a <b>DestructibleMeshComponent</b> for debug controls in-Editor. Add <b>DestructibleGlobalMeshSpawner</b> to your SceneMesh; configure reserved volumes, segment density, and materials to get a fully breakable environment.</p>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Selective Passthrough Shader

<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px" padding-end="24">
    <p>The Meta Core SDK’s Selective Passthrough shader and material makes a mesh invisible to reveal the real-world camera feed. Apply it to any <b>EffectMesh</b> or custom mesh to carve portals into the physical environment. Assign the Selective Passthrough material to <b>Mesh Material</b> on your <b>EffectMesh</b> and all generated surfaces become passthrough.</p>
  </box>
  <box>
    <img src="/images/unity-mruk-selectivePassthrough.gif" alt="Selective Passthrough" border-radius="12px" width="100%" />
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Tutorial: Ceiling Sky Reveal

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img src="/images/unity-mruk-ceilingmover.gif" alt="Ceiling Reveal Tutorial" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
    <p>In this step-by-step tutorial, you will learn how to slowly remove the ceiling to reveal a virtual sky behind it, which is a popular use cases in Mixed Reality. When moving parts of your room, it is best practice to not move the MRUK anchor itself but instead move the EffectMesh.</p>
    <p>There are at least two common approaches:</p>
  </box>
</box>

### Option A: Use MRUK and EffectMesh utility functions

1. On the **EffectMesh** component, set **Mesh Material** to a material with the Selective Passthrough shader.
2. Write a script that waits for the EffectMesh to create the ceiling object, locate the ceiling anchor and move it out of view at runtime:

```csharp
using System.Collections;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class CeilingMover : MonoBehaviour
{
    [Tooltip("Speed at which the ceiling moves out")]
    [SerializeField] private float speed = 0.5f;

    [Tooltip("The EffectMesh spawning our ceiling")]
    [SerializeField] private EffectMesh ceilingEffectMesh;

    private void Start()
    {
        StartCoroutine(WaitForEffectMeshAndMove());
    }

    private IEnumerator WaitForEffectMeshAndMove()
    {
        // 1. grab the current room
        var room = MRUK.Instance.GetCurrentRoom();

        // 2. wait until the mesh is spawned
        while (room.CeilingAnchor &&
               !ceilingEffectMesh.EffectMeshObjects.ContainsKey(room.CeilingAnchor))
        {
            yield return null;
        }

        // 3. fetch the wrapper and its real mesh GameObject
        var wrapper = ceilingEffectMesh.EffectMeshObjects[room.CeilingAnchor];
        var meshGo = wrapper.effectMeshGO;
        if (!meshGo)
        {
            Debug.LogError("No effectMeshGO found on wrapper!");
            yield break;
        }

        // 4. detach from the anchor so we only move the mesh
        meshGo.transform.SetParent(null, /* worldPositionStays= */ true);

        // 5. compute how far to move: half-room + half-ceiling + margin
        var roomBounds  = room.GetRoomBounds();
        var meshBounds  = meshGo.GetComponentInChildren&lt;Renderer&gt;().bounds;
        const float MARGIN = 0.1f;
        var distance  = roomBounds.extents.x + meshBounds.extents.x + MARGIN;

        // 6. slide the mesh straight out in world-space
        var moved = 0f;
        var dir = Vector3.right;
        while (moved < distance)
        {
            var step = speed * Time.deltaTime;
            meshGo.transform.Translate(dir * step, Space.World);
            moved += step;
            yield return null;
        }
    }
}
```

3. Attach this component to your scene and assign the EffectMesh component in your scene that is responsible for creating the ceiling mesh.
4. Press play in your editor and see how the room gets created (appears black) and the ceiling moves out of the room's bounds.

### Option B: Use EffectMesh and override the AnchorPrefabSpawner

1. Set **Mesh Material** on **EffectMesh** to Selective Passthrough and deselect **CEILING** in the **Labels** property.
2. Create a custom spawner to size a prefab to the ceiling’s bounds by inheriting from the [AnchorPrefabSpawner](/documentation/unity/unity-mr-utility-kit-content-placement#anchorprefabspawner):

```csharp
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class CeilingPrefabSpawner : AnchorPrefabSpawner
{
    public override Vector2 CustomPrefabScaling(Vector2 localScale)
    {
        return new Vector2(localScale.x, localScale.y);
    }
}
```

3. In your scene, add this spawner and under **Prefabs to Spawn** add a new list item. Select the **CEILING** label under **Labels**, and assign your ceiling prefab under **Prefabs**. For the **Scaling** property, select Custom, so our script can override it.
4. Move or deactivate the spawned prefab at runtime to unveil the sky.

### Tip: When to Use SceneLoaded Callbacks

MRUK provides two ways to react when your scene data is ready:

- **RegisterSceneLoadedCallback:** Use this helper if you want your callback invoked immediately **and** on all future loads. It will fire instantly if the scene is already initialized.
- **SceneLoadedEvent.AddListener:** Use this when you only need to listen from this point forward. If the scene has already loaded, your listener will **not** be called until the next load.

```csharp
using Meta.XR.MRUtilityKit;
using UnityEngine;

private void Awake()
{
    MRUK.Instance.RegisterSceneLoadedCallback(OnSceneReady);
    MRUK.Instance.SceneLoadedEvent.AddListener(OnNextSceneReady);
}

// 1. Runs now (if loaded) and on every future load
private void OnSceneReady()
{
    Debug.Log("Scene is ready right now or was already ready!");
}

// 2. Runs only the next time the scene loads
private void OnNextSceneReady()
{
    Debug.Log("This runs only the next time the scene loads.");
    // If you want it only once, remove the listener:
    MRUK.Instance.SceneLoadedEvent.RemoveListener(OnNextSceneReady);
}
```

If you have dependencies between MonoBehaviours, for example a custom script which depends on Physics colliders spawned by EffectMesh, ensure that you register the callbacks in the same order you want to receive them. Unity allows you to change the [Script Order Execution](https://docs.unity3d.com/Manual/class-MonoManager.html) which can help ensure callbacks are registered in the desired order.

<box height="20px"></box>
---
<box height="10px"></box>

## Related Samples

<box display="flex" flex-direction="row" padding-vertical="16">
  <box width="50%" padding-end="24">
    <img src="/images/unity-mruk-samples-destructiblemesh.gif" alt="DestructibleMesh Sample" border-radius="12px" width="100%" />
  </box>
  <box width="50%" max-width="400px">
    <heading type="title-small-emphasized">DestructibleMesh (Beta)</heading>
    <p>
      Demonstrates the <b>DestructibleGlobalMeshSpawner</b> component. A DestructibleMesh prefab segments and spawns the global mesh when a room is created, parenting it under the <b>GLOBAL_MESH</b> anchor. Each segment is a GameObject with a <b>MeshFilter</b> and <b>MeshRenderer</b>.
    </p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/DestructibleMesh">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box width="50%" max-width="400px">
    <heading type="title-small-emphasized">VirtualHome</heading>
    <p>
      Reconstructs an interior scene using furniture prefabs for each semantic label via <b>AnchorPrefabSpawner</b>. Furniture uses <b>GridsliceResizer</b> to preserve aspect ratios. <b>EffectMesh</b> generates floor and ceiling meshes with carpet and ceiling textures, using Metric UVs for 1 m texture repeats.
    </p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/VirtualHome">View sample</a>
  </box>
  <box width="50%" padding-start="24">
    <img src="/images/unity-mruk-samples-virtualhome2.png" alt="VirtualHome Sample" border-radius="12px" width="100%" />
  </box>
</box>

<box height="30px"></box>
---
<box height="20px"></box>

← **Previous:** [Place Content with Scene](/documentation/unity/unity-mr-utility-kit-content-placement/)

→ **Next:** [Track Objects in MRUK](/documentation/unity/unity-mr-utility-kit-trackables/)

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
- [Track Objects in MRUK](/documentation/unity/unity-mr-utility-kit-trackables)
  Track keyboards using MRUK-trackables.

### Multiuser & Debugging

- [Share Your Space with Others](/documentation/unity/unity-mr-utility-kit-space-sharing)
  Use MRUK’s Space Sharing API to sync scene geometry across multiple users.
- [Debug Your MRUK App](/documentation/unity/unity-mr-utility-kit-debug)
  Use tools like the Scene Debugger and EffectMesh to validate anchor data and scene structure.

### MRUK Samples & Tutorials

- [MRUK Samples](/documentation/unity/unity-mr-utility-kit-samples)
  Explore sample scenes like NavMesh, Virtual Home, Scene Decorator, and more.
- [Mixed Reality Motifs](/documentation/unity/unity-mrmotifs-instant-content-placement)
  Instant Content Placement.

### Mixed Reality Design Principles

- [Mixed Reality Best Practices](/design/mr-overview)
  Design MR apps that are comfortable, intuitive, and immersive.