# Unity Dronerage Scene Integration

**Documentation Index:** Learn about unity dronerage scene integration in this documentation.

---

---
title: "Scene Integration In DroneRage"
description: "DroneRage demonstrates how Scene API maps real-world furniture and surfaces into mixed reality gameplay."
last_updated: "2024-11-04"
---

DroneRage has drone ships coming above from the seat, from behind the walls, into the hole of the player’s ceiling. The app needs to know where these physical walls are. Both the enemies and the lasers interact with the walls and any other scene objects in the physical environment. So, if you shoot your desk or if the enemy shoots your desk, you see the bullet hole decals. Or if one of the drones crashes over a desk, it will fall and hit the desk.

This logic uses Discover’s integration of the [Scene API](/documentation/unity/unity-scene-overview/). The Scene API is used to spawn Photon network objects, and then everything is handled completely in the Discover level, like tracking and synchronizing the objects.

All these objects are also already set up at the Discover level to do all things necessary in all the full game experiences contained within Discover. Theoretically, you could swap these objects out depending on the app selection. For example, you don’t need the Interaction SDK’s `RayInteractibles` in DroneRage, because you are not pointing at the drones, but Discover has all these as a mega object because it’s easier than creating and de-spawning objects.

## Adding scene to DroneRage

The `SceneElement` class can be found in `/Assets/Discover/Scripts/SceneElement.cs` and `SceneElementsManager` in `/Assets/Discover/Scripts/SceneElementsManager.cs`.

To enable DroneRage access to Scene elements, the objects first need to be registered with a Scene Elements Manager in the `Awake` function of the `SceneElement` class:

```
        private void Awake()
        {
            m_highlightObject.SetActive(false);
            SceneElementsManager.Instance.RegisterElement(this);
        }
```

Let’s take a look at Unity Editor to better understand how this Scene API integration works. This is the `DiscoverAppController` prefab.

{:width="650px"}

In its hierarchy, the `MRUK` object is part of the [Meta MR Utility Kit](/downloads/package/meta-xr-mr-utility-kit-upm/).

[`MRUK`](/documentation/unity/unity-mr-utility-kit-overview/) uses the Scene API from the Unity Editor’s perspective. Here objects are set up and will be spawned related to the type of anchor.

{:width="650px"}

When a ceiling object is detected by the Scene API, it spawns the `CeilingPhotonInstantiator` object. This uses the Photon Instantiator component which is part of Discover and instantiates this network object.

{:width="650px"}

The reason for that is that it instantiates network objects through Photon Fusion, rather than using `Object.Instantiate`. Then, there is the `SceneCeilingPhoton` class which is a Photon Fusion net object.

{:width="650px"}

This has the Scene Element component. This is a Discover class, which means the Scene Element Manager tracks it automatically. Since it's a network object, the colocated player automatically spawns that object because it’s a Photon Fusion network object. This is not special in terms of Scene API or colocation. Rather, it’s like any other network GameObject.

### Scene Elements

In the `SceneElementsManager` class, in `/Assets/Discover/Scripts/SceneElementsManager.cs`, there is this line:

```
        public IEnumerable<SceneElement> GetElementsByLabel(string label) =>
            Instance.SceneElements.Where(e => e.ContainsLabel(label));
```

That label is a string (such as “CEILING”, and so on). You can see, for example, in class `EnvironmentSwapper` of DroneRage in `/Discover/DroneRage/Scripts/Scene/EnvironmentSwapper.cs` how it changes the materials of the walls:

```
        private async void SwapWalls(bool toAlt)
        {
            await UniTask.WaitUntil(() => SceneElementsManager.Instance.AreAllElementsSpawned());
            foreach (var wall in SceneElementsManager.Instance.GetElementsByLabel(MRUKAnchor.SceneLabels.WALL_FACE))
            {
                if (toAlt)
                {
                    m_originalWallMaterials ??= wall.Renderer.sharedMaterials;
                    wall.Renderer.sharedMaterials = m_altWallMaterials;
                }
                else
                {
                    wall.Renderer.sharedMaterials = m_originalWallMaterials;
                }
            }
        }

```

By using `SceneElementsManager.Instance.GetElementsByLabel(MRUKAnchor.SceneLabels.WALL_FACE))`, it gets elements by label (WALL_FACE) and then swaps on that wall.

**Note**: `CEILING` is a string and not an enum. This is because it’s part of the Scene API, which is frequently updated. Having that as a string helps with backwards compatibility.

A similar flow is followed for the ceiling swap:

```
        private async void SwapCeiling(bool toAlt)
        {
            await UniTask.WaitUntil(() => SceneElementsManager.Instance.AreAllElementsSpawned());
            var ceiling = SceneElementsManager.Instance.
                GetElementsByLabel(MRUKAnchor.SceneLabels.CEILING).
                FirstOrDefault()?.
                Renderer;
            if (ceiling == null)
                return;

            var block = new MaterialPropertyBlock();
            block.SetFloat(s_visibility, toAlt ? 0 : 1);
            ceiling.SetPropertyBlock(block);
        }
```

### Enemies spawn

In the `Spawner` class,  as defined in `/Assets/Discover/DroneRage/Scripts/Enemies/Spawner.cs`, enemies are initially spawned outside the room, behind the wall. To do that, there is a need to first figure out the extent of the wall.

This is how the room extent is calculated:

```
        private void CalculateRoomExtents()
        {
            var anchors = SceneElementsManager.Instance.GetElementsByLabel(MRUKAnchor.SceneLabels.WALL_FACE).ToList();
            if (anchors.Any())
            {
                m_roomMinExtent = anchors[0].transform.position;
                m_roomMaxExtent = anchors[0].transform.position;
                m_roomSize = Vector3.zero;
            }

            foreach (var anchor in anchors)
            {
                var anchorTransform = anchor.transform;
                var position = anchorTransform.position;
                var scale = anchorTransform.lossyScale;
                var right = anchorTransform.right * scale.x * 0.5f;
                var up = anchorTransform.up * scale.y * 0.5f;
                var wallPoints = new Vector3[]
                {
                    position - right - up,
                    position - right + up,
                    position + right - up,
                    position + right + up,
                };

                foreach (var wp in wallPoints)
                {
                    Debug.Log($"wall point {wp}");

                    m_roomMinExtent = Vector3.Min(m_roomMinExtent, wp);
                    m_roomMaxExtent = Vector3.Max(m_roomMaxExtent, wp);

                    m_roomSize.y = Mathf.Max(m_roomSize.y, transform.lossyScale.y);
                }
            }

            m_roomSize = m_roomMaxExtent - m_roomMinExtent;
            Debug.Log("Room Size: " + m_roomSize + " Room Min Extents: " + m_roomMinExtent + " Room Max Extents: " + m_roomMaxExtent);
        }
```

The first gets the list of all wall faces with the following line:

```
            var anchors = SceneElementsManager.Instance.GetElementsByLabel(MRUKAnchor.SceneLabels.WALL_FACE).ToList();
```

Then, it creates a min and max extent.

```
                    m_roomMinExtent = anchors[0].transform.position;
                    m_roomMaxExtent = anchors[0].transform.position;
```

This essentially gets a bounding box that represents the room and stores that. The following logic finds a point that is outside that bounding box which represents the user’s real-life room.

```
        public Vector3 GetClosestSpawnPoint(Vector3 position)

        {
            var spawnOffset = position - 0.5f * (m_roomMinExtent + m_roomMaxExtent);
            spawnOffset.y = 0f;
            spawnOffset = spawnOffset.normalized;

            if (spawnOffset.sqrMagnitude == 0f)
            {
                spawnOffset = Vector3.forward;
            }

            return 2f * (RoomSize.magnitude + 1f) * spawnOffset + new Vector3(0f, m_roomMaxExtent.y - 1f, 0f);
        }
```

The Scene API is used to spawn all these Photon network objects, and then everything is handled completely in the Discover level, like tracking and synchronizing these objects.