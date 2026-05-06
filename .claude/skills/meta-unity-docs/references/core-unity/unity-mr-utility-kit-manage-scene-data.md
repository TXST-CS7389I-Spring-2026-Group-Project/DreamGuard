# Unity Mr Utility Kit Manage Scene Data

**Documentation Index:** Learn about unity mr utility kit manage scene data in this documentation.

---

---
title: "Mixed Reality Utility Kit – Manage and query scene data"
description: "Setup, stabilization, data sources, scripting, and scene querying using MRUK, MRUKRoom, MRUKAnchor, and trackables"
last_updated: "2026-01-27"
---

## Learning objectives

- **Configure the MRUK prefab** to bootstrap scene data and define application behavior.
- **Stabilize your virtual scene** using World Locking to minimize drift and simplify anchor workflows.
- **Select and combine data sources** (Device, Prefab, JSON) with fallback strategies for development and production.
- **Control script execution order** on scene-loaded events to manage complex initialization.
- **Leverage MRUK, MRUKRoom, MRUKAnchor, and MRUKTrackables APIs** for spatial queries.

<box height="10px"></box>
---
<box height="10px"></box>

## Scene capture and startup

To enable MRUK’s spatial queries, you must first generate a **Scene Model** using the Space Setup flow. This process captures planes, volumes, and mesh geometry of the physical environment.

<!-- Image on the right, text on the left -->
<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px">
    <br/>
    <text>
      Follow the on‑device Space Setup by navigating to <b>Settings → Environment Setup → Space Setup</b>. The guided scan walks users around their environment to capture necessary data, with a manual capture fallback if automatic detection struggles. You can also enable <b>Load Scene on Startup</b> in the MRUK prefab to auto‑trigger this flow, or call <b>OVRScene.RequestSpaceSetup()</b> programmatically.
    </text>
  </box>
  <box padding-start="24">
    <img alt="Scene Capture Flow" src="/images/unity-mruk-room-scan.gif" border-radius="12px" width="100%" />
  </box>
</box>

1. **On-device Flow:**
   - Navigate to **Settings → Physical Space → Space Setup** on the headset.
   - Follow the guided scan; if automatic detection struggles, switch to manual capture to outline key surfaces.

2. **Auto-trigger:**
   - In the MRUK prefab inspector, enable **Load Scene on Startup**. If no scene exists, MRUK will prompt Space Setup at app launch.

3. **Programmatic Invocation:**

   ```csharp
   OVRScene.RequestSpaceSetup();
   ```

<box height="10px"></box>

<oc-devui-note type="important" heading="Link Limitation">
  Space Setup must run on‑device. You cannot perform Space Setup via Meta Horizon Link.
</oc-devui-note>

<box height="10px"></box>
---
<box height="10px"></box>

## MRUK class

Place the **MRUK.prefab** (`Core/Prefabs/MRUK.prefab`) into your scene and configure its sections:

### World Locking

<!-- Image on the right, text on the left -->
<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px">
    <br/>
    <text>
      <b>World Locking</b> keeps virtual content stationary relative to the real world without manually parenting every object to anchors. With <code>EnableWorldLock</code> (default <code>true</code>), MRUK applies subtle tracking‑space adjustments so anchors remain aligned. This allows your virtual objects to stay “static” in world space, improving stability and reducing the need to parent to each anchor.
    </text>
  </box>
  <box padding-start="24">
    <img alt="World Locking Demo" src="/images/unity-mruk-world-locking.gif" border-radius="12px" width="100%" />
  </box>
</box>

- `EnableWorldLock` (default: `true`): MRUK applies subtle camera-rig adjustments to maintain alignment.
- When implementing custom camera control and World Locking is enabled, use the TrackingSpaceOffset transform matrix field in MRUK to move the tracking space.

### Data source selection

MRUK supports three scene data inputs. Choose one or combine them via inspector dropdown or API:

- **Device:** Live scan via OpenXR or XR Simulator. Reflects the user’s actual room.
- **Prefab:** ~50 built-in sample rooms (`Core/Rooms/Prefabs`) for quick editor testing without scanning.
- **JSON:** Pre-captured Scene Models (`Core/Rooms/Json`) for sharing or multiplayer synchronization.

Example API calls:
```csharp
MRUK.Instance.LoadSceneFromDevice();
MRUK.Instance.LoadSceneFromPrefab("Office_Small");
MRUK.Instance.LoadSceneFromJson("MySavedScene.json");
```

### Script execution order

Many initialization scripts depend on scene data. Under `Script Execution`, list your MonoBehaviours to fire on `SceneLoadedEvent` in the correct sequence. Here is the ordering of the [FloorZone example](https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/FloorZone):

1. **BasicWallDecorator:** Apply color schemes or tags to primary walls.
2. **VolumeDecorator:** Highlight furniture volumes or other semantic zones.
3. **[FindSpawnPositions](/documentation/unity/unity-mr-utility-kit-content-placement#findspawnpositions):** Compute valid floor or surface points for gameplay elements.

This ordering guarantees that decorations are applied before spawn calculations run.

<box height="10px"></box>
---
<box height="10px"></box>

## Working with scene data

After loading, MRUK exposes comprehensive APIs for querying rooms, anchors, and [trackables](/documentation/unity/unity-mr-utility-kit-trackables).

- **MRUK:** Singleton manager. Loads, clears, and raises global events.
- **MRUKRoom:** Represents one scanned room—contains anchors and spatial utilities.
- **MRUKAnchor:** Planes (walls, floor, ceiling), volumes (furniture), and mesh geometry with semantic labels.
- **MRUKTrackable:** Dynamic anchors (e.g., keyboards) with detection events.

The rooms, anchors, and trackables that have been loaded by MRUK should never be deleted or modified from the outside. Doing so will lead to undefined behavior. See [Effect Mesh](#visualizing-the-scene-with-the-effect-mesh-class) and [AnchorPrefabSpawner](#anchorprefabspawner) for more information on how to only visualize a specific room.

<box height="10px"></box>

<oc-devui-note type="note" heading="Events &amp; Callbacks">
Subscribe to:
<ul>
  <li><code>MRUK.Instance.SceneLoadedEvent</code> when all rooms finish loading.</li>
  <li><code>RoomCreatedEvent</code> / <code>RoomUpdatedEvent</code> as rooms change.</li>
  <li><code>AnchorCreatedEvent</code> / <code>AnchorUpdatedEvent</code> per‑anchor updates.</li>
  <li><code>TrackableAdded</code> / <code>TrackableRemoved</code> for dynamic trackables.</li>
</ul>
</oc-devui-note>

### Typical workflow

Example: Placing a virtual billboard on the largest unobstructed wall.

```csharp
// 1. Retrieve the current room
var currentRoom = MRUK.Instance.GetCurrentRoom();

// 2. Find the key wall (longest, unobstructed)
Vector2 wallScale;
var keyWall = currentRoom.GetKeyWall(out wallScale);

// 3. Create and align a quad
var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
quad.transform.SetPositionAndRotation(
    keyWall.transform.position,
    keyWall.transform.rotation
);

quad.transform.localScale = new Vector3(wallScale.x, wallScale.y, 1f);
```

### Core API reference

<box height="10px"></box>

<oc-devui-collapsible-card heading="☕ MRUK Methods">
  <pre><code>
  // Scene loading & management
  void LoadSceneFromDevice();
  void LoadSceneFromPrefab(string prefabName);
  void LoadSceneFromJson(string jsonPath);
  void ClearScene();

  // Room queries
  List&lt;MRUKRoom&gt; GetRooms();
  MRUKRoom GetCurrentRoom();
  </code></pre>
</oc-devui-collapsible-card>

<box height="10px"></box>

<oc-devui-collapsible-card heading="☕ MRUKRoom Utilities">
  <pre><code>
  // Anchor collections
  List&lt;MRUKAnchor&gt; GetRoomAnchors();
  MRUKAnchor GetFloorAnchor();
  MRUKAnchor GetCeilingAnchor();
  List&lt;MRUKAnchor&gt; GetWallAnchors();

  // Spatial queries
  Vector2[] GetRoomOutline();
  MRUKAnchor GetKeyWall(out Vector2 scale);
  RaycastHit Raycast(Ray ray);
  bool IsPositionInRoom(Vector3 pos);
  Vector3 GenerateRandomPositionInRoom();
  </code></pre>
</oc-devui-collapsible-card>

<box height="10px"></box>

<oc-devui-collapsible-card heading="☕ MRUKAnchor Queries">
  <pre><code>
  // Hit testing
  bool Raycast(Ray ray);
  bool IsPositionInBoundary(Vector3 pos);

  // Geometry info
  float GetDistanceToSurface(Vector3 pos);
  Vector3 GetClosestSurfacePosition(Vector3 pos);
  Vector3 GetAnchorCenter();
  Vector3 GetAnchorSize();

  // Semantic labels
  string[] GetLabelsAsEnum();
  bool HasLabel(string label);
  </code></pre>
</oc-devui-collapsible-card>

<box height="10px"></box>

<oc-devui-note type="note" heading="Best Practices">
<ul>
  <li>Always check <code>MRUK.IsSupported</code> before using MRUK APIs.</li>
  <li>Cache room and anchor references when accessed frequently to reduce overhead.</li>
  <li>Unsubscribe from events in <code>OnDestroy()</code> to prevent unexpected behaviour.</li>
  <li>Never delete or modify MRUK room, anchor, or trackables. Instead use the functionality on EffectMesh and AnchorPrefabSpawner to only visualize a specific room.</li>
</ul>
</oc-devui-note>

<box height="10px"></box>
---
<box height="10px"></box>

## High-Fidelity scene

MRUK supports the [High-Fidelity Scene API](/documentation/unity/unity-scene-roommesh/).
HiFi scene provides a more detailed version of the room layout that allows features such as multiple floors, columns, and slanted ceilings to be queried by the developer.
To use HiFi Scene in MRUK, use the checkbox in MRUK **Enable High Fidelity Scene**.

### New features

**Inner Wall Faces** - A room can now have inner wall faces to represent complex architectural features such as columns, pillars, or other structural elements. These are represented by `INNER_WALL_FACE`, the new [semantic label](/reference/unity/latest/struct_o_v_r_semantic_labels/).

**Multiple Floors** - A room can now have multiple floors if detected by the space setup. This feature allows for more accurate representation of multi-story buildings or rooms with varying floor levels. The Scene API will provide separate floor planes for each distinct floor level, enabling developers to create more immersive and realistic experiences.

**Multiple Ceilings** - A room can now have multiple ceilings if detected by the space setup. This feature supports complex ceiling geometries, such as slanted or dropped ceilings, and allows developers to create more detailed and realistic environments. The Scene API will provide separate ceiling planes for each distinct ceiling, enabling developers to create more immersive and interactive experiences.

<box height="10px"></box>
---
<box height="10px"></box>

## Visualizing the scene with the EffectMesh class

<!-- Image on the right, text on the left -->
<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px">
    <br/>
    <text>
      <b>EffectMesh</b> creates special‑effect meshes from your Scene anchors—planes become triangulated meshes, volumes become cuboids, and the global scene mesh uses its raw geometry. You can apply custom UV mappings, vertex coloring, and optional colliders or hole‑cuts for doors and windows.
    </text>
  </box>
  <box padding-start="24">
    <img alt="World Locking Demo" src="/images/unity-mruk-effect-mesh.jpg" border-radius="12px" width="100%" />
  </box>
</box>

### How to use

- Drag the `EffectMesh` prefab (`Core/Prefabs/EffectMesh.prefab`) into your scene.
- Assign a **Mesh Material** to define colors, outlines, or custom shaders.
- Enable **Add Colliders** to allow physics interactions (for example, bouncing or placement checks).
- Toggle **Hide Mesh** if you only need colliders or shader-driven effects without visible geometry.
- Check **Cut Holes** to automatically remove quads where door and window frames appear.

### EffectMesh settings

- **Spawn On Start**: Controls whether an effect mesh is automatically applied to all rooms, a specific room, or no rooms, allowing for manual application if desired.
- **Mesh Material**: Material applied to all generated primitives. Use multiple EffectMesh instances for layered materials.
- **Border Size**: Thickness of edge bounds. Edge vertices are black, fading to white over this distance—ideal for outline shaders.
- **Frames Offset**: Extrudes co-planar quads (doors/windows) off the wall by this many meters to prevent Z-fighting.
- **Add Colliders**: When `true`, colliders are added to each mesh primitive.
- **Cast Shadows**: Controls whether effect meshes cast shadows in the scene.
- **Hide Mesh**: Hides the visual mesh but retains colliders and shader interactions.
- **Texture Coordinate Mode**:
  - `METRIC`: UVs increase 1 unit per meter.
  - `METRIC_SEAMLESS`: 1 unit per meter, adjusted to end on integers to avoid seams.
  - `MAINTAIN_ASPECT_RATIO`: UVs scaled to preserve texture aspect ratio.
  - `MAINTAIN_ASPECT_RATIO_SEAMLESS`: Aspect-ratio UVs ending on whole numbers.
  - `STRETCH`: UVs mapped from 0 to 1 across each primitive.
- **Labels**: List of semantic labels (for example, `"Wall"`, `"Floor"`, `"GlobalMesh"`) to include in the generated meshes.

<box height="10px"></box>
---
<box height="10px"></box>

## Related samples

<box display="flex" flex-direction="row" padding-vertical="16">
  <box width="50%" padding-end="24">
    <img src="/images/unity-mruk-samples-mrukbase.png" alt="MRUKBase Sample" border-radius="12px" width="100%" />
  </box>
  <box width="50%" max-width="400px">
    <heading type="title-small-emphasized">MRUKBase</heading>
    <p>A minimal scene with <code>MRUK</code>, <code>EffectMesh</code> (using the neon-blue <b>RoomBoxEffects</b> material with 1 m floor/ceiling crosses and stretched UVs), and <code>SceneDebugger</code> to visualize spatial queries.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/Basic">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box width="50%" max-width="400px">
    <heading type="title-small-emphasized">BouncingBall</heading>
    <p>This sample uses <code>EffectMesh</code> colliders (hidden mesh) to let virtual balls bounce off the physical environment. <code>BouncingBallMgr</code> spawns balls on Grab and shoots them on Trigger; <code>BouncingBallLogic</code> plays collision audio and self-destructs after a delay.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/BouncingBall">View sample</a>
  </box>
  <box width="50%" padding-start="24">
    <img src="/images/unity-mruk-samples-bouncingball.gif" alt="BouncingBall Sample" border-radius="12px" width="100%" />
  </box>
</box>

## Related content

Explore more MRUK documentation topics to dive deeper into spatial queries, content placement, manipulation, sharing, and debugging.

### Core concepts

- [Overview](/documentation/unity/unity-mr-utility-kit-overview)
  Get an overview of MRUK's key areas and features.
- [Getting Started](/documentation/unity/unity-mr-utility-kit-gs)
  Set up your project, install MRUK, and understand space setup with available Building Blocks.
- [Place Content without Scene](/documentation/unity/unity-mr-utility-kit-environment-raycast)
  Use Environment Raycasting to place 3D objects into physical space with minimal setup.
- [Place Content with Scene Data](/documentation/unity/unity-mr-utility-kit-content-placement/)
  Use semantic anchors, surface scattering, and raycasting to place content precisely within mixed reality scenes.

### Content and interaction

Track keyboards using MRUK-trackables.

### Multiuser and debugging

- [Share Your Space with Others](/documentation/unity/unity-mr-utility-kit-space-sharing)
  Use MRUK’s Space Sharing API to sync scene geometry across multiple users.
- [Debug Your MRUK App](/documentation/unity/unity-mr-utility-kit-debug)
  Use tools like the Scene Debugger and EffectMesh to validate anchor data and scene structure.

### MRUK samples and tutorials

- [MRUK Samples](/documentation/unity/unity-mr-utility-kit-samples)
  Explore sample scenes like NavMesh, Virtual Home, Scene Decorator, and more.
- [Mixed Reality Motifs](/documentation/unity/unity-mrmotifs-instant-content-placement)
  Instant Content Placement.

### Mixed reality design principles

- [Mixed Reality Best Practices](/design/mr-overview)
  Design MR apps that are comfortable, intuitive, and immersive.