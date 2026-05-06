# Spirit Sling

**Documentation Index:** Learn about spirit sling in this documentation.

---

---
title: "Spirit Sling tabletop showcase"
description: "Spirit Sling is a social tabletop mixed reality game showcasing Avatars, Interaction SDK, Passthrough, and colocation."
last_updated: "2025-10-08"
---

## Building a social tabletop game with Avatars Style 2.0 and Presence Platform capabilities

Spirit Sling is a social mixed reality app created to show developers how to build exciting tabletop games that give users a reason to be together in VR. Tabletop games are a popular social gaming category today, but friends can only play when they are able to be together physically. With our new and improved Avatars, and the power of mixed reality, users can now place a fun tabletop experience on a surface and invite a friend into their space to play with them.

You can [download Spirit Sling on the Meta Store](https://www.meta.com/experiences/spirit-sling-tabletop/26801347429479910/) and [download its project files from GitHub](https://github.com/oculus-samples/Unity-SpiritSling).

This app shows you:
* How to use Mixed Reality Utility Kit (MRUK) to ensure the board is placed somewhere in your room that works for the gameplay.
* How to use Platform SDK and its social features to build a rich multiplayer experience.
* Interactable objects with physics, so you can enhance traditional tabletop gameplay mechanics with the power of virtual objects.

There are several different developer guidelines related to reproducing compelling productivity applications below:
1. [Contextual board placement: Mixed Reality Utility Kit & other tips](#contextual-board-placement-mixed-reality-utility-kit--other-tips)
2. [Integration with Platform SDK and Multiplayer](#integration-with-platform-sdk-and-multiplayer)
3. [Intractable virtual objects: Using ISDK and physics to enhance gameplay](#intractable-virtual-objects-using-isdk-and-physics-to-enhance-gameplay)

## Contextual board placement: Mixed reality utility kit & other tips

When developing experiences with passthrough, seamless integration of the game into the real-world environment is crucial. Virtual and real objects coexist in the same physical space, posing unique challenges for developers. One key challenge involves strategically placing virtual content to ensure all gameplay elements remain accessible to the player. Spirit Sling, which primarily uses hand tracking for input, requires that the game board be immediately accessible by hand and maintain visibility above real-world objects upon first use. To access data about real-world objects, developers should utilize the [Scene API](/documentation/unity/unity-scene-overview/) with assistance from [MRUK](/documentation/unity/unity-mr-utility-kit-overview/), which offers extensive utility features for enhancing MR experiences.

First, the game should wait for the MRUK to load the data. This can be done by either waiting for `MRUK.Instance.IsInitialized` or by subscribing to the `MRUK.Instance.SceneLoadedEvent`.
```csharp
// Wait until MRUK loads the Scene API data
while (!MRUK.Instance.IsInitialized)
{
    yield return null;
}
```

After MRUK has initialized, the game starts polling the Scene API data to find the board spawn position. This checks whether the positions are inside the room and not inside any volume:
```csharp
var currentRoom = MRUK.Instance.GetCurrentRoom();
if (currentRoom.IsPositionInRoom(position) && !currentRoom.IsPositionInSceneVolume(position))
{

}
```

This code checks that the board and its grabbable handles are reachable by hand so that user can interact with the most important elements of the board:
```csharp
// Check that the line that connects the center of the board with the player board (grabbable handles) doesn't hit any scene anchor.
const float playerBoardOffset = 0.5f;
if (currentRoom.Raycast(new Ray(position, rotation * Vector3.back), playerBoardOffset, out RaycastHit hit))
{
    Log.Debug("GameVolumeSpawner can't spawn because the player board is not reachable by hand.");
    return false;
}
```

Next is the placement code. The actual Spirit Sling placement code is quite complex. Here is a simplified version, with all the specifics of the Spirit Sling removed:
```csharp
[SerializeField] private GameObject _objectToSpawn;

private IEnumerator Start()
{
    // Wait until MRUK loads the Scene API data
    while (!MRUK.Instance.IsInitialized)
    {
        yield return null;
    }

    while (true)
    {
        if (TryGetSpawnPosition(out Vector3 position, out Quaternion rotation))
        {
            // Spawn the board in front of the player if the spawn position was found
            var spawnedGameVolume = Instantiate(_objectToSpawn, position, rotation);
            SaveSpatialAnchorGuid(spawnedGameVolume.GetComponent<OVRSpatialAnchor>());
            SceneManager.MoveGameObjectToScene(spawnedGameVolume, gameObject.scene);
            GameVolumeSpawned?.Invoke(spawnedGameVolume);
            break;
        }

        // Wait until the board can be spawned in front of the player
        await Task.Yield();
    }
}

private static bool TryGetSpawnPosition(out Vector3 position, out Quaternion rotation)
{
    var cameraRig = OVRManager.instance.GetComponentInChildren<OVRCameraRig>();
    Transform centerEyeAnchor = cameraRig.centerEyeAnchor;
    bool isUserInsideRoom = MRUK.Instance.GetCurrentRoom().IsPositionInRoom(centerEyeAnchor.position);
    if (!isUserInsideRoom)
    {
        // The user is not inside the room. In this case it's recommended to show a UI
        // message asking the user to return to the room or rescan their environment.
        position = default;
        rotation = default;
        return false;
    }

    Vector3 lookDir = Vector3.ProjectOnPlane(centerEyeAnchor.forward, Vector3.up);

    // Calculate the position in front of the player and slightly lowered from the head position
    const float distanceToTheContent = 0.9f;
    const float downOffset = 0.4f;
    position = centerEyeAnchor.position + lookDir * distanceToTheContent + Vector3.down * downOffset;

    // Align the content's forward vector with the look direction of the player
    rotation = Quaternion.LookRotation(lookDir);

    return IsValidPlacementPose(position, rotation);
}

private static bool IsValidPlacementPose(Vector3 position, Quaternion rotation)
{
    // Check if the position is inside the room and isn't inside any volume
    var currentRoom = MRUK.Instance.GetCurrentRoom();
    if (!currentRoom.IsPositionInRoom(position) || currentRoom.IsPositionInSceneVolume(position))
    {
        Log.Debug("GameVolumeSpawner can't spawn because position is outside the current room or inside the anchor volume.");
        return false;
    }

    // Check that the line that connects the center of the board with the player board (grabbable handles) doesn't hit any scene anchor.
    const float playerBoardOffset = 0.5f;
    if (currentRoom.Raycast(new Ray(position, rotation * Vector3.back), playerBoardOffset, out RaycastHit hit))
    {
        Log.Debug("GameVolumeSpawner can't spawn because the player board is not reachable by hand.");
        return false;
    }

    return true;
}
```

## Integration with platform SDK and multiplayer

Meta XR Platform SDK provides features to create engaging and social game experiences. Spirit Sling uses Platform SDK in combination with [Photon Fusion](https://doc.photonengine.com/fusion/current/fusion-intro#) to make the experience multiplayer.

### Initializing platform SDK

Before starting to use Platform SDK, it should be initialized first. The initialization part consists of calling the `Core.AsyncInitialize` method followed by the app entitlement check `Entitlements.IsUserEntitledToApplication`.

``` csharp
private void Awake()
{
    // Initialize Platform SDK.
    // Set the App ID  in the 'Meta / Platform / Edit Settings' menu before calling Core.AsyncInitialize().
    Core.AsyncInitialize().OnComplete(initMsg =>
    {
        if (initMsg.IsError)
        {
            Debug.LogError("Failed to initialize Platform SDK");
            return;
        }

        // Check app entitlement
        Entitlements.IsUserEntitledToApplication().OnComplete(entitlementMsg =>
        {
            if (entitlementMsg.IsError)
            {
                Debug.LogError("The user has no entitlement to use this app.");
                Application.Quit();
            }
            else
            {
                OnUserEntitlementChecked();
            }
        });
    });
}
```
### Creating a multiplayer session

Once the Platform SDK is initialized, the app can create a multiplayer room by calling `Fusion.NetworkRunner.StartGame(...)`. If the room is private, the app calls `GroupPresence.Set(...)` to share the session via private lobby with friends.

```csharp
[SerializeField] private Fusion.NetworkRunner _networkRunner;

public async void CreateRoom(bool isPublic)
{
    while (!_networkRunner.IsCloudReady)
    {
        await Task.Yield();
    }

    StartGameResult result = await _networkRunner.StartGame(new StartGameArgs
    {
        GameMode = GameMode.Shared,
        PlayerCount = 4,
        SessionName = $"{Application.version}-{Guid.NewGuid()}",
        CustomLobbyName = "Spirit Sling",
        IsVisible = isPublic,
        SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>()
    });

    if (!result.Ok)
    {
        Debug.LogError($"CreateRoom failed: {result.ErrorMessage}");
        return;
    }

    if (!isPublic)
    {
        string roomName = _networkRunner.SessionInfo.Name;
        var options = new GroupPresenceOptions();
        options.SetDestinationApiName("Spirit Sling Private Room");
        options.SetMatchSessionId(roomName);
        options.SetLobbySessionId(roomName);
        options.SetIsJoinable(true);
        GroupPresence.Set(options).OnComplete(msg =>
        {
            Debug.Log($"Private room created successfully: {!msg.IsError}");
        });
    }
}
```

### Joining a multiplayer session

Once the multiplayer session is created by other players, the app can join the session either by joining a public room or by accepting the invitation from the private lobby.

To join a public room:
1. The app first gathers all publicly available sessions by subscribing to the `NetworkEvents.OnSessionListUpdate` event.
2. Then, the app joins the session lobby with `NetworkRunner.JoinSessionLobby(...)`.
3. Finally, the app finds the first non-full public room and joins the session by calling `NetworkRunner.StartGame(...)`.

Here is an example:
```csharp
[SerializeField] private NetworkEvents _networkEvents;
private readonly List<SessionInfo> m_cachedSessionList = new List<SessionInfo>();

private void OnEnable() => _networkEvents.OnSessionListUpdate.AddListener(OnSessionListUpdate);
private void OnDisable() => _networkEvents.OnSessionListUpdate.RemoveListener(OnSessionListUpdate);

private void OnSessionListUpdate(NetworkRunner _, List<SessionInfo> updatedSessions)
{
    // Save the list of all available sessions
    m_cachedSessionList.Clear();
    m_cachedSessionList.AddRange(updatedSessions);
}

public async Task JoinPublicRoom()
{
    while (!_networkRunner.IsCloudReady)
    {
        await Task.Yield();
    }

    if (_networkRunner.LobbyInfo == null)
    {
        // Join the session lobby
        StartGameResult joinSessionResult = await _networkRunner.JoinSessionLobby(SessionLobby.Custom, "Spirit Sling");
        if (!joinSessionResult.Ok)
        {
            Debug.LogError($"JoinSessionLobby failed: {joinSessionResult.ErrorMessage}");
            return;
        }
    }

    // Find the first non-full room
    var session = m_cachedSessionList.Find(static s =>
    {
        return s.IsOpen && s.PlayerCount < 4 && s.Name.StartsWith(Application.version, StringComparison.Ordinal);
    });
    if (session == null)
    {
        Debug.LogError("Public session not found.");
        return;
    }

    // Join the session
    var startGameResult = await _networkRunner.StartGame(new StartGameArgs
    {
        GameMode = GameMode.Shared,
        SessionName = session.Name,
        SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>()
    });

    if (startGameResult.Ok)
    {
        // Open the main menu and start the game
        MenuStateMachine.ChangeState(MenuStateMachine.mainMenuState);
    }
    else
    {
        Debug.LogError($"Failed to join room: {startGameResult.ErrorMessage}, {startGameResult.ShutdownReason}");
    }
}
```

Next, to join a session from a private lobby, the app reads `ApplicationLifecycle.GetLaunchDetails()` once Platform SDK is initialized to accept the invite on the app start. Also, the app subscribes to the `ApplicationLifecycle.SetLaunchIntentChangedNotificationCallback()` callback to listen for the launch intent changes:

```csharp
private void OnUserEntitlementChecked()
{
    // Try to accept the invitation once the Platform SDK is initialized
    TryAcceptInvitationFromDeepLink();

    // Subscribe to the launch intent changes and accept the invitation in the callback
    ApplicationLifecycle.SetLaunchIntentChangedNotificationCallback(intentChangeMsg =>
    {
        if (!intentChangeMsg.IsError)
        {
            TryAcceptInvitationFromDeepLink();
        }
    });

    async void TryAcceptInvitationFromDeepLink()
    {
        while (!_networkRunner.IsCloudReady)
        {
            await Task.Yield();
        }

        LaunchDetails launchDetails = ApplicationLifecycle.GetLaunchDetails();
        if (string.IsNullOrEmpty(launchDetails.TrackingID))
        {
            return;
        }

        StartGameResult startGameResult = await _networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = launchDetails.MatchSessionID,
            SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>()
        });

        if (startGameResult.Ok)
        {
            // Open the main menu and start the game
            MenuStateMachine.ChangeState(MenuStateMachine.mainMenuState);
        }
        else
        {
            Debug.LogError($"Failed to join room: {startGameResult.ErrorMessage}, {startGameResult.ShutdownReason}");
        }
    }
}
```

## Intractable virtual objects: Using ISDK and physics to enhance gameplay

Spirit Sling uses hand tracking as the primary input method. To simplify the integration of hand tracking, this project uses Meta Interaction SDK (ISDK). The fastest way to get started with ISDK is to use the [Building Blocks](/documentation/unity/bb-overview/) feature.

To add the Grab Interaction Building Block to your project:
1. Navigate to **Meta** > **Tools** > **Building Blocks** to open the **Building Blocks** window.
2. Select **Grab Interaction** to add the interaction and its components to your project.

### Using ISDK to control gameplay elements

After adding the ‘Grab Interaction’ building block to the scene, the `[BuildingBlock] Cube` example game object is added to the scene with these essential components needed to detect the grab gesture:
* `Grabbable`
* `HandGrabInteractable`
* `GrabInteractable`

The `Grabbable` component tells ISDK that the object can respond to the grab gesture. The `HandGrabInteractable` is responsible for detecting the gesture when hand tracking is enabled and the player doesn’t use controllers. The `GrabInteractable` is responsible for grabbing objects with controllers.

Spirit Sling uses the `Grabbable.WhenPointerEventRaised` event to listen to the grab gesture events in all its three interactable gameplay elements:
* Kodama
* SlingBall
* Slingshot

Let’s cover the `SlingBallShootController` as the most complex component out of three:
```csharp
public class SlingBallShootController : MonoBehaviour
{
    [SerializeField] private Grabbable _grabbable;

    /// Initial position when the ball is pulled back.
    /// HasValue only when the player is performing the drag gesture.
    private Vector3? _pullInitialPosition;

    private void OnEnable() => _grabbable.WhenPointerEventRaised += GrabbableOnWhenPointerEventRaised;
    private void OnDisable() => _grabbable.WhenPointerEventRaised -= GrabbableOnWhenPointerEventRaised;

    private void GrabbableOnWhenPointerEventRaised(PointerEvent pointerEvent)
    {
        switch (pointerEvent.Type)
        {
            case PointerEventType.Select:
                // Record the initial position of the ball
                _pullInitialPosition = transform.parent.position;
                break;
            case PointerEventType.Cancel:
                // Reset the ball when the grab gesture is cancelled
                _pullInitialPosition = null;
                ResetBall();
                break;
            case PointerEventType.Unselect:
                if (_pullInitialPosition.HasValue)
                {
                    float pullDistance = Vector3.Distance(_pullInitialPosition.Value, transform.position);
                    const float minPullDistance = 0.03f;
                    // Launch the ball only when the current pull distance is big enough
                    if (pullDistance < minPullDistance)
                    {
                        CancelDrag();
                    }
                    else
                    {
                        LaunchBall();
                    }
                }
                break;
        }
    }

    private void Update()
    {
        if (_pullInitialPosition.HasValue)
        {
            // Update the ball while the player is performing the drag gesture
            UpdateBallVisual();
            UpdateAimRotation();
            UpdateTrajectory();
        }
    }
}
```

### Manual board adjustment with hand tracking

An important aspect of virtual content placement that contributes to a good user experience is to give the user the ability to re-adjust the board after the initial placement. Spirit Sling uses the `Grabbable` component from ISDK and custom `One/TwoGrabGameVolumeTransformer` components to achieve that.

```csharp
public class TwoGrabGameVolumeTransformer : MonoBehaviour, ITransformer
{
    void ITransformer.UpdateTransform()
    {
        var grabbable = GetComponent<Grabbable>();
        var grabA = grabbable.GrabPoints[0];
        var grabB = grabbable.GrabPoints[1];
        // Calculate new board pose from two grab points...
        Vector3 position = ...;
        Quaternion rotation = ...;
        transform.SetPositionAndRotation(position, rotation);
        if (IsValidPlacementPose(position, rotation))
        {
            // Hide the invalid placement hint
            HideGhost();
        }
        else
        {
            // Display the hint to the user that the current position is invalid
            DisplayGhost(position, inVolume: true);
        }
    }
```
To integrate a poke interaction into your scene:
1. Navigate to **Meta** > **Tools** > **Building Blocks** to open the **Building Blocks** window.
2. Select **Poke Interaction** to add the interaction and its components to your project. This action automatically generates two `PokeInteractor` objects within the OVRCameraRig, equipped with all necessary components for the poke gesture.
4. Similar to the `SlingBallShootController` example, attach the `SpiritSlingButton` script to the game object with the `PokeInteractable` component to handle `PokeInteractable.WhenPointerEventRaised` events.

### Adding hand interaction to interface buttons

To integrate poke interaction into your scene:
1. Start by adding the **Meta** / **Tools** / **Building Blocks** / **Poke Interaction** block. This action automatically generates two `PokeInteractor` objects within the OVRCameraRig, equipped with all necessary components for the poke gesture.
2. Create a `SpiritSlingButton` script and attach it to a game object that includes the `PokeInteractable` component. Use the `BuildingBlock` Poke Interaction game object, which is added automatically, as the foundation for your button supporting the poke gesture.
3. Finally, similar to the `SlingBallShootController` example, attach the `SpiritSlingButton` script to the game object with the `PokeInteractable` component to handle `PokeInteractable.WhenPointerEventRaised` events.

```csharp
public class SpiritSlingButton : MonoBehaviour
{
    [SerializeField] private PokeInteractable _poke;

    private void OnEnable() => _poke.WhenPointerEventRaised += WhenPointerEventRaised;
    private void OnDisable() => _poke.WhenPointerEventRaised -= WhenPointerEventRaised;

    private void WhenPointerEventRaised(PointerEvent pointerEvent)
    {
        switch (pointerEvent.Type)
        {
            case PointerEventType.Select:
                Debug.Log("Button pressed.");
                break;
        }
    }
}
```