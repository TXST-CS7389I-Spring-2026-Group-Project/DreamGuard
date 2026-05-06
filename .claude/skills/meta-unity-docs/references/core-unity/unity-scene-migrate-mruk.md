# Unity Scene Migrate Mruk

**Documentation Index:** Learn about unity scene migrate mruk in this documentation.

---

---
title: "Migrating from OVRSceneManager to MRUK"
description: "Replace OVRSceneManager with Mixed Reality Utility Kit in existing Unity projects step by step."
last_updated: "2024-07-22"
---

This page will help you migrate existing apps from OVRSceneManager to MR Utility Kit. It focuses on the differences between them. If you are starting a new project, please instead refer to the [MR Utility Kit documentation](/documentation/unity/unity-mr-utility-kit-overview).

## Prerequisites

### OVRSceneManager:

You would place the OVRSceneManager prefab in your scene.

### MRUK:

Place the MRUK prefab in your scene *instead* of OVRSceneManager. This is a singleton and accessed through `MRUK.Instance`. You’ll also need to add the `using` directive to the top of your C# code that intends to use MRUK:
```
using Meta.XR.MRUtilityKit;
```

## Initialization

### OVRSceneManager:

In your game code, you would normally do this to know when the Scene data has loaded:
```
public OVRSceneManager sceneManager;

void Awake()
{
    sceneManager.SceneModelLoadedSuccessfully += YourStartupCode;
}

void YourStartupCode()
{
    // query for anchors, build your game world, do game initialization
}
```

### MRUK:

In MRUK, you can add your public function to the Scene Loaded Event list on the MRUK prefab:

If you have dependencies, list order matters; if script B requires something on script A to run first, you should put A above B in this list.

You can also subscribe with code instead:
```
void Awake()
{
    MRUK.Instance.SceneLoadedEvent.AddListener(YourStartupCode);
}

public void YourStartupCode()
{
    // query for anchors, build your game world, do game initialization
}
```
After the Scene Loaded Event has triggered your code, you can then query MRUK for all your Scene needs (for example, explore `MRUK.Instance.GetCurrentRoom` and the `MRUKRoom` class)

## Position and rotation

The pose of MRUK anchors remains the same as OVRSceneAnchors:

**Quads** (WALL_FACE, FLOOR, and so on): position is centered, Z-forward is the surface normal

**Cubes** (OTHER, COUCH, and so on): position is centered at the TOP of the volume, Z-forward is UP

To get this in either OVR or MRUK, it’s the same:
```
Vector3 anchorPosition = anchor.transform.position;
Quaternion anchorRotation = anchor.transform.rotation;
```
As a reminder, note that in both OVR and MRUK; for volumes, **Z+ is still the gravity-aligned “up.”** This is unlike Unity volumes, where Y+ is up and the position is centered.

## Scale

### OVRSceneManager:

If you’re starting from the instantiated prefabs in OVRSceneManager, the anchor scale is on the children. So assuming your prefabs in OVRSceneManager have at least one child, you could query its `localScale`. This would give you an error if the anchor has no child, in which case you’d need to add more code to query for OVRScenePlane/OVRSceneVolume components on the anchor itself.
```
Vector3 anchorScale = anchor.transform.GetChild(0).localScale;
```

### MRUK:

Some anchors (TABLE and COUCH) can have **both planes and volumes**, so you’ll have to choose depending on what you want:
```
MRUKAnchor anchor;

if (anchor.PlaneRect.HasValue)
{
    Vector3 anchorScale = new Vector3(anchor.PlaneRect.Value.size.x, anchor.PlaneRect.Value.size.y, 1);
}

if (anchor.VolumeBounds.HasValue)
{
    // this is a cube - note that "height" is the Z component
    Vector3 anchorScale = anchor.VolumeBounds.size;
}
```

## Labels

### OVRSceneManager:

```
OVRSceneAnchor anchor;
OVRSemanticClassification classification = anchor.GetComponent<OVRSemanticClassification>();
// you may first need to do a null check, since not every OVRSceneAnchor has a classification (such as the room)
if (classification != null  && classification.Contains(OVRSceneManager.Classification.WallFace))
{
    // this is a wall
}
```

### MRUK:

```
MRUKAnchor anchor;
if (anchor.HasAnyLabel(MRUKAnchor.SceneLabels.WALL_FACE))
{
    // this is a wall
}
```

## OVRSceneManager prefab

### OVRSceneManager:

The OVRSceneManager prefab is what you normally use to instantiate prefabs for each anchor:

### MRUK:

The equivalent of this in MRUK is the Anchor Prefab Spawner:

Hover over each parameter in the Inspector for useful tool tips.

## World locking

Unlike with OVRSceneManager, the anchors in MRUK do not move in Unity coordinate space once they have been created. This makes keeping content synchronized with the real world much easier. See World Locking in [MR Utility Kit Features](/documentation/unity/unity-mr-utility-kit-features) for more details.

### OVRSceneManager:

You’re expected to query the OVRSceneAnchor pose periodically, to align your content with new data from tracking. Optionally, you can attach your instantiated content as a child of the OVRSceneAnchor, where it benefits automatically from OVRSceneAnchor tracking corrections.

### MRUK:

Querying the MRUKAnchor pose is unnecessary, as the tracking updates the camera instead, leaving the Unity world space to remain fixed. This can be optionally disabled on the MRUK prefab.

## Scene mesh

The Scene Mesh is a high-coverage artifact that represents the scanned room data (available only on Meta Quest 3) as a triangle mesh. While the resolution of the artifact captures a high level of detail, using it comes at a high cost, and it should preferably be used only for fast-paced short-lived physics contacts.

### OVRSceneManager:

You have to use the prefab overrides to get the global mesh, as the anchor doesn’t correspond to either a plane, nor a volume. Furthermore, there is a strict set of components that is required on the prefab in order to retrieve the triangle mesh data.

### MRUK:

On the EffectMesh prefab, you can specify the material and GLOBAL_MESH Label and it will be created:

## Accessing Scene data hierarchy

The Scene hierarchy in the Unity scene is generally flat and matches between OVR and MRUK; anchors in the room are siblings of each other and children of each room, which has its own anchor. Both approaches provide shortcuts to avoid using Unity’s expensive GameObject.Find*() methods.

### OVRSceneManager:

```
// you should really cache the room if you intend to do this every frame
OVRSceneRoom room = FindObjectOfType<OVRSceneRoom>();
foreach (Transform childAnchor in room.transform)
{
    // each child is a scene object belonging to this room, such as a floor, ceiling, or couch
}
```
### MRUK:

The recommended approach to access the scene hierarchy in MRUK is to get the list of rooms from the MRUK instance `Rooms` property, the anchors within the room are accessible via the `Anchors` property:
```
foreach (MRUKRoom room in MRUK.Instance.Rooms)
{
    foreach (MRUKAnchor childAnchor in room.Anchors)
    {
        // each child is a scene object belonging to this room, such as a loor, ceiling, or couch
    }
}
```

## Next steps

The items listed here mainly bring you up to parity with OVRSceneManager. To see all the extra functionality MRUK offers, refer to the [MR Utility Kit documentation](/documentation/unity/unity-mr-utility-kit-overview).