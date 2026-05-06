# Unity Set Up Colocation Package

**Documentation Index:** Learn about unity set up colocation package in this documentation.

---

---
title: "Set up Colocation Package"
description: "Set up the Colocation package for your multiplayer app"
last_updated: "2024-12-16"
---

When the scene sets up, players are synchronized. Everything else is normal Unity networking.

The most important part of a colocated experience is ensuring the client player is in the right place relative to the scene transmitted by the host player.

To do this, the host player accesses the Scene API and spawns all the objects in the scene. When the client player then joins that session, they can start interacting with these objects. The rest of this logic is a matter of placing the client players in the correct spot relative to the host player, and that's where the shared spatial anchors come from. Once these are set up, everything else falls into place and it all converts to a network scene.

From the client player's perspective, the host player sends them two things:

- An object (such as a Unity transform) that represents it relative to the spatial anchor. In Discover, this is sent through Photon Fusion.
- An Anchor ID that the host player is using through the Shared Spatial Anchors API to get its transform relative to the headset.

The Colocation package then uses these to reorient every player’s rig to the transform that the host has sent to the client players. This means that the server sends a transform and the ID, first. And then, by using that ID, there is another transform that represents where the player actually is in the first player’s space. Under the hood, the Spatial Anchors API returns the transform relative to the headset.

In this way,  there is an absolute coordinate system which is the common world space used by the host and client players.  Because of this, when you replicate the digital objects in an app by using the Colocation package, you don't need to align the objects to the player as well. You are simply moving the root of the player’s camera rig.

## Outline of how the Colocation package is set up

This process is demonstrated by the `ColocationDriverNetObj` class in `/Assets/Discover/Scripts/Colocation/ColocationDriverNetObj.cs`.
Here is how the flow works in this class.

1. Set up objects
    In the `UniTask SetupForColocation()` function, the class sets up all the objects in the scene, as seen in the following code snippet:

    ```
                m_colocationLauncher.Init(
                m_oculusUser?.ID ?? default,
                m_headsetGuid,
                NetworkAdapter.NetworkData,
                NetworkAdapter.NetworkMessenger,
                sharedAnchorManager,
                m_alignmentAnchorManager,
                overrideEventCode
            );
    ```

2. Initialize colocation launcher
    After the objects are set up, it initializes the colocation launcher:

    ```
            m_colocationLauncher.ColocationReady += OnColocationReady;
            m_colocationLauncher.ColocationFailed += OnColocationFailed;

            if (HasStateAuthority)
            {
                m_colocationLauncher.CreateColocatedSpace();
            }
            else
            {
                // Don't try to colocate if we join remotely
                if (SkipColocation)
                {
                    OnColocationCompletedCallback?.Invoke(false);
                }
                else
                {
                    m_colocationLauncher.ColocateAutomatically();
                }
            }
    ```
    This provides a `OnAfterColocationReady` callback. It also checks to see if the connecting device is the host device with the `HasStateAuthority`, and if so, it creates the collocated space with:

    ```
    m_colocationLauncher.CreateColocatedSpace();
    ```

    Otherwise, it calls the `ColocateAutomatically` method.

    This ensures the spatial anchors are set up from both the Meta Quest cloud side (with the Shared Spatial Anchor API), and the Unity objects network side (as with a networking solution like Photon Fusion).

When the client player connects, the class calls the `ColocateAutomatically` method. This gets the information from the host, which then sets up everything to ensure players are oriented correctly.