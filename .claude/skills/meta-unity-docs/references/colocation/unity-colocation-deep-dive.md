# Unity Colocation Deep Dive

**Documentation Index:** Learn about unity colocation deep dive in this documentation.

---

---
title: "Deep Dive into Discover's Colocation Package"
description: "A deep dive into the code and structure of the Colocation Package used in the Discover Showcase"
last_updated: "2024-12-06"
---

In the [Discover GitHub repo](https://github.com/oculus-samples/Unity-Discover), the `ColocationDriverNetObj` class provides the backbone of the flow and is defined in `/Assets/Discover/Scripts/Colocation/ColocationDriverNetObj.cs`.

The following dissects the key aspects of this flow.

The async `Init()` function represents how the flow starts.

```
        private async void Init()
        {
            m_ovrCameraRigTransform = FindObjectOfType<OVRCameraRig>().transform;
            m_oculusUser = await OculusPlatformUtils.GetLoggedInUser();
            m_headsetGuid = Guid.NewGuid();
            await SetupForColocation();
        }
```

This function is common to both client players and the host player. The `Init` function invokes the `SetupForColocation()` member function of the same class.

```
        private async UniTask SetupForColocation()
        {
            if (HasStateAuthority)
            {
                Debug.Log("SetUpAndStartColocation for host");
                _ = Runner.Spawn(m_networkDataPrefab).GetComponent<PhotonNetworkData>();
                _ = Runner.Spawn(m_networkDictionaryPrefab).GetComponent<PhotonPlayerIDDictionary>();
                _ = Runner.Spawn(m_networkMessengerPrefab).GetComponent<PhotonNetworkMessenger>();
            }

...
```

`SetupForColocation()` is also an async function. It spawns all the necessary Photon pieces if it is invoked by the host. (This status is checked with `HasStateAuthority`.) These are different prefabs that represent the interfaces provided by the Colocation APIs and the Colocation package. The `SetupForColocation()` function spawns those classes and forwards the colocation data through Photon Fusion.

## Spawned data

In the Colocation package, the `INetworkMessenger` interface is defined in `/Packages/com.meta.xr.sdk.core/Scripts/BuildingBlocks/MultiplayerBlocks/Shared/Colocation/ColocationPackage/Runtime/NetworkImpl/INetworkMessenger.cs`. This interface is how Discover sends and receives messages to and from the network.

```
namespace ColocationPackage {

  public interface INetworkMessenger {

    public void SendMessageUsingOculusId(byte eventCode, ulong oculusId, object messageData = null);
    public void SendMessageUsingNetworkId(byte eventCode, int networkId, object messageData = null);
    public void SendMessageUsingHeadsetId(byte eventCode, Guid headsetId, object messageData = null);
    public void SendMessageToAll(byte eventCode, object messageData = null);
    public void RegisterEventCallback(byte eventCode, Action<object> callback);
    public void UnregisterEventCallback(byte eventCode);

  }
}
```

In the Colocation package, there is also the `INetworkData` interface. It gets the data that passes through the networking system. It’s defined in `/Packages/com.meta.xr.sdk.colocation/NetworkUtils/NetworkData/INetworkData.cs`.

For example, the first class members represent and retrieve information about players:

```
  public interface INetworkData {
    public void AddPlayer(Player player);
    public void RemovePlayer(Player player);
    public Player? GetPlayer(ulong oculusId);
    public List<Player> GetAllPlayers();

    public Player? GetFirstPlayerInColocationGroup(uint colocationGroup);

    public void AddAnchor(Anchor anchor);
    public void RemoveAnchor(Anchor anchor);

    public Anchor? GetAnchor(FixedString64Bytes uuid);
    public List<Anchor> GetAllAnchors();

    public uint GetColocationGroupCount();

    public void IncrementColocationGroupCount();
  }
}
```

## Photon Fusion attributes to RPCs

The `PhotonNetworkMessenger` class is defined in `/Assets/Discover/Scripts/Colocation/PhotonNetworkMessenger.cs`. It  contains the `FindRPCToCallServerRPC` member function, which relates to the host player (also called the server here).

```
        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]

        private void FindRPCToCallServerRPC(byte eventCode, int playerId, ulong oculusIdAnchorOwner,
            ulong oculusIdAnchorRequester, Guid headsetIdRequester, string uuid,
            NetworkBool anchorFlowSucceeded, RpcInfo info = default)
        {
            Debug.Log("FindRPCToCallServerRPC");
            PlayerRef playerRef = playerId;
            FindRPCToCallClientRPC(playerRef, eventCode, oculusIdAnchorOwner, oculusIdAnchorRequester, headsetIdRequester, uuid, anchorFlowSucceeded);
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]

        private void FindRPCToCallServerRPC(byte eventCode, int playerId)
        {
            Debug.Log("FindRPCToCallServerRPC: Null");
            PlayerRef playerRef = playerId;
            FindRPCToCallClientRPC(playerRef, eventCode);
        }
```

There is also the corresponding `FindRPCToCallClientRPC` function for client players:

```
        [Rpc(RpcSources.All, RpcTargets.All)]

        private void FindRPCToCallClientRPC(
            [RpcTarget] PlayerRef player,
            byte eventCode,
            ulong oculusIdAnchorOwner, ulong oculusIdAnchorRequester, Guid headsetIdRequester,
            string uuid, NetworkBool anchorFlowSucceeded)
        {
            Debug.Log("FindRPCToCallClientRPC");
            var data = new ShareAndLocalizeParams(oculusIdAnchorOwner, oculusIdAnchorRequester, headsetIdRequester, uuid)
            {
                anchorFlowSucceeded = anchorFlowSucceeded
            };
            m_callbackDictionary[eventCode](data);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]

        private void FindRPCToCallClientRPC(

            [RpcTarget] PlayerRef player,
            byte eventCode
        )
        {
            Debug.Log("FindRPCToCallClientRPC: null");
            m_callbackDictionary[eventCode](null);
        }
```
Depending on the messages, these are all Photon Fusion attributes to RPCs.

### Server and client messages

The `INetworkMessenger` interface invokes the `SendMessageUsingOculusId` function. This is a member function of the `PhotonNetworkMessenger` class, as defined in `/Assets/Discover/Scripts/Colocation/PhotonNetworkMessenger.cs`. It uses a dictionary and implements the `PhotonPlayerIDDictionary` player ID dictionary class.

When using the `SendMessageUsingOculusId` function, the following translates the association between the Oculus ID and the Photon Fusion ID:

```
var networkId = (int)m_idDictionary.GetNetworkId(oculusId);
```

By using this, Discover sends any referenced RPC by this message data object (called `messageData`):

```
            if (messageData != null)
            {
                var data = (ShareAndLocalizeParams)messageData;
                FindRPCToCallServerRPC(eventCode, networkId, data.oculusIdAnchorOwner, data.oculusIdAnchorRequester, data.headsetIdAnchorRequester, data.uuid.ToString(), data.anchorFlowSucceeded);
            }
```

This forwards all networking information through the network.

## ColocationDriverNetObj initialization walkthrough

Focusing back on the `ColocationDriverNetObj` class discussed above, here is a more in-depth look of the process followed in the class.

1. It sets up the following objects:

- `m_ovrCameraRigTransform`
- `m_oculusUser`
- `m_headsetGuid`

2. It adds the current Oculus user ID to the dictionary:

```
AddToIdDictionary(m_oculusUser?.ID ?? default, Runner.LocalPlayer.PlayerId, m_headsetGuid);
```

This is run either by the client or the host. This gets the Oculus user ID and associates it with the local player ID, as well as with a headset `Guid`, which is also used for referencing the Oculus player.

3. It initializes the `Messenger` class with:

```
messenger.Init(PlayerIDDictionary);
```

4. It then starts the colocation flow.
   1. It creates an alignment anchor manager called `m_alignmentAnchorManager` with the following:

      ```
      m_alignmentAnchorManager = Instantiate(m_alignmentAnchorManagerPrefab).GetComponent<AlignmentAnchorManager>();
      ```
   2. It then creates a colocation launcher as follows:

      ```
      m_colocationLauncher = new ColocationLauncher();
      ```
   3. This is initialized with this invocation with the `Init` function of the `ColocationLauncher` class from the Colocation package:

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

## Using the AlignmentAnchorManager class

The `AlignmentAnchorManager` class plays a significant role in the overall flow because it handles player alignment to the spatial anchor. It is defined in `/Packages/com.meta.xr.sdk.core/Scripts/BuildingBlocks/MultiplayerBlocks/Shared/Colocation/ColocationPackage/Runtime/AlignmentAnchorManager.cs`.

The `AlignmentAnchorManager` class contains the `AlignPlayerToAnchor` function, as shown here:

```
    public void AlignPlayerToAnchor(OVRSpatialAnchor anchor) {
      Debug.Log("AlignmentAnchorManager: Called AlignPlayerToAnchor");
      if (_alignmentCoroutine != null) {
        StopCoroutine(_alignmentCoroutine);
        _alignmentCoroutine = null;
      }

      _alignmentCoroutine = StartCoroutine(AlignmentCoroutine(anchor, 2));
    }
```

Invoking the `AlignPlayerToAnchor` function gets the spatial anchor used by the Colocation package. It connects to the Spatial Anchors API in order to align a player to the anchor.

## ColocationLauncher class

Back in the Colocation package, in the `ColocationLauncher` class, defined in `/Packages/com.meta.xr.sdk.core/Scripts/BuildingBlocks/MultiplayerBlocks/Shared/Colocation/ColocationPackage/Runtime/ColocationLauncher.cs`.

### ExecuteAction function

This class contains the `ExecuteAction` function:

```
    private void ExecuteAction(ColocationMethod colocationMethod) {

      switch (colocationMethod) {
        case ColocationMethod.ColocateAutomatically:
          ColocateAutomaticallyInternal();
          break;
        case ColocationMethod.ColocateByPlayerWithOculusId:
          ColocateByPlayerWithOculusIdInternal(_oculusIdToColocateTo);
          break;
        case ColocationMethod.CreateColocatedSpace:
          CreateColocatedSpaceInternal();
          break;
        default:
          Debug.LogError($"ColocationLauncher: Unknown action: {colocationMethod}");
          break;
      }
    }
```

### ColocationAutomaticallyInternal function

The `ExecuteAction` function can, in turn, call the `ColocateAutomaticallyInternal()` function.  This is where the shared spatial anchors come to play:

```
    private async void ColocateAutomaticallyInternal() {

      Debug.Log("ColocationLauncher: Called Init Anchor Flow");
      var successfullyAlignedToAnchor = false;

      List<Anchor> alignmentAnchors = GetAllAlignmentAnchors();
      foreach (var anchor in alignmentAnchors)
        if (await AttemptToShareAndLocalizeToAnchor(anchor)) {
          successfullyAlignedToAnchor = true;
          Debug.Log($"ColocationLauncher: successfully aligned to anchor with id: {anchor.uuid}");
          _networkData.AddPlayer(new Player(_myOculusId, anchor.colocationGroupId));
          AlignPlayerToAnchor();
          break;
        }

      if (!successfullyAlignedToAnchor) {
        if (CreateAnchorIfColocationFailed)
        {
          CreateNewColocatedSpace().Forget();
        }
        else
        {
          OnAutoColocationFailed?.Invoke();
        }
      }
    }
```

This gets all the alignment anchors by calling the `List<Anchor> GetAllAlignmentAnchors()` function, as shown here:

```
    private List<Anchor> GetAllAlignmentAnchors() {

      var alignmentAnchors = new List<Anchor>();
      List<Anchor> allAnchors = _networkData.GetAllAnchors();
      foreach (var anchor in allAnchors)
        if (anchor.isAlignmentAnchor) {
          alignmentAnchors.Add(anchor);
        }

      return alignmentAnchors;
    }
```

The important line here is:

```
List<Anchor> allAnchors = _networkData.GetAllAnchors();
```

This line, in turn, calls `GetAllAnchors()` and gets the anchors list.

### The GetAllAnchors function

The `GetAllAnchors` function retrieves data from the `AnchorList` and is defined in `/Assets/Discover/Scripts/Colocation/PhotonNetworkData.cs` as part of the `PhotonNetworkData` class:

```
        public List<Anchor> GetAllAnchors()

        {
            var anchors = new List<Anchor>();
            foreach (var photonAnchor in AnchorList) anchors.Add(photonAnchor.Anchor);

            return anchors;
        }
```

This function retrieves the data from the `AnchorList`, already populated by previously calling the `AddAnchorRpc` function:

```
        private void AddAnchorRpc(PhotonNetAnchor anchor)
        {
            AddNetAnchor(anchor);
        }
```

Which in turn calls the `AddNetAnchor` function:

```
       private void AddNetAnchor(PhotonNetAnchor anchor)
        {
            if (HasStateAuthority)
            {
                AnchorList.Add(anchor);
            }
            else
            {
                AddAnchorRpc(anchor);
            }
        }
```

This produces all the spatial anchors in a list.

### AttemptToSharedAndLocalizeToAnchor function

The `AttemptToShareAndLocalizeToAnchor` function is defined in `/Packages/com.meta.xr.sdk.core/Scripts/BuildingBlocks/MultiplayerBlocks/Shared/Colocation/ColocationPackage/Runtime/ColocationLauncher.cs` of the Colocation package, within the `ColocateAutomaticallyInternal()` definition. The following part is critical:

```
      foreach (var anchor in alignmentAnchors)

        if (await AttemptToShareAndLocalizeToAnchor(anchor)) {
          successfullyAlignedToAnchor = true;
          Debug.Log($"ColocationLauncher: successfully aligned to anchor with id: {anchor.uuid}");
          _networkData.AddPlayer(new Player(_myOculusId, anchor.colocationGroupId));
          AlignPlayerToAnchor();
          break;
        }
...
```

The invoked `AttemptToShareAndLocalizeToAnchor` function is defined as:

```
    private UniTask<bool> AttemptToShareAndLocalizeToAnchor(Anchor anchor) {

      Debug.Log(
        $"ColocationLauncher: Called AttemptToShareAndLocalizeToAnchor with id: {anchor.uuid} and oculusId: {_myOculusId}"
      );
      _alignToAnchorTask = new UniTaskCompletionSource<bool>();
      var anchorOwner = anchor.ownerOculusId;
      if (anchorOwner == _myOculusId)
      {
        // In the case a player returns and wants to localize to an anchor they created
        // we simply localize to that anchor
        var sharedAnchorId = new Guid(anchor.uuid.ToString());
        LocalizeAnchor(sharedAnchorId);
        return _alignToAnchorTask.Task;
      }
```

Which, in turn, calls the `LocalizeAnchor` function defined as:

```
    private async void LocalizeAnchor(Guid anchorToLocalize) {

      Debug.Log($"ColocationLauncher: Localize Anchor Called id: {_myOculusId}");
      IReadOnlyList<OVRSpatialAnchor> sharedAnchors = null;
      Guid[] anchorIds = {anchorToLocalize};
      sharedAnchors = await _sharedAnchorManager.RetrieveAnchors(anchorIds);

      if (sharedAnchors == null || sharedAnchors.Count == 0) {
        Debug.LogError("ColocationLauncher: Retrieving Anchors Failed");
        _alignToAnchorTask.TrySetResult(false);
      } else {
        Debug.Log("ColocationLauncher: Localizing Anchors is Successful");
        // For now we will only take the first anchor that was shared
        // This should be refactored later to be more generic or for cases where we have multiple alignment anchors if that case comes up
        _myAlignmentAnchor = sharedAnchors[0];
        _alignToAnchorTask.TrySetResult(true);
      }
    }
```

This gets a list of all the anchors with this line:

```
sharedAnchors = await _sharedAnchorManager.RetrieveAnchors(anchorIds);
```

### SharedAnchorManager class

The `SharedAnchorManager` class retrieves the anchors. This class is defined in `/Packages/com.meta.xr.sdk.core/Scripts/BuildingBlocks/MultiplayerBlocks/Shared/Colocation/ColocationPackage/Runtime/SharedAnchorManager.cs` in the Colocation package.

```
    public async UniTask<IReadOnlyList<OVRSpatialAnchor>> RetrieveAnchors(Guid[] anchorIds) {

      Assert.IsTrue(anchorIds.Length <= OVRPlugin.SpaceFilterInfoIdsMaxSize, "SpaceFilterInfoIdsMaxSize exceeded.");

      UniTaskCompletionSource<IReadOnlyList<OVRSpatialAnchor>> utcs = new();
      Debug.Log($"{nameof(SharedAnchorManager)}: Querying anchors: {string.Join(", ", anchorIds)}");

      OVRSpatialAnchor.LoadUnboundAnchors(
        new OVRSpatialAnchor.LoadOptions {
          StorageLocation = OVRSpace.StorageLocation.Cloud,
          Timeout = 0,
          Uuids = anchorIds
        },
        async unboundAnchors => {
          if (unboundAnchors == null) {
            Debug.LogError(
              $"{nameof(SharedAnchorManager)}: Failed to query anchors - {nameof(OVRSpatialAnchor.LoadUnboundAnchors)} returned null."
            );
            utcs.TrySetResult(null);
            return;
          }

          if (unboundAnchors.Length != anchorIds.Length) {
            Debug.LogError(
              $"{nameof(SharedAnchorManager)}: {anchorIds.Length - unboundAnchors.Length}/{anchorIds.Length} anchors failed to relocalize."
            );
          }

          var createdAnchors = new List<OVRSpatialAnchor>();
          var createTasks = new List<UniTask>();
          // Bind anchors
          foreach (var unboundAnchor in unboundAnchors) {
            var anchor = InstantiateAnchor();
            try {
              unboundAnchor.BindTo(anchor);
              _sharedAnchors.Add(anchor);
              createdAnchors.Add(anchor);
              createTasks.Add(UniTask.WaitWhile(() => anchor.PendingCreation, PlayerLoopTiming.PreUpdate));
            } catch {
              Object.Destroy(anchor);
              throw;
            }
          }

          // Wait for anchors to be created
          await UniTask.WhenAll(createTasks);

          utcs.TrySetResult(createdAnchors);
        }
      );

      return await utcs.Task;
    }
```

The call to `OVRSpatialAnchor.LoadUnboundAnchors()` loads any spatial anchors given by this list of IDs:

```
Uuids = anchorIds
```

The host player sends these IDs as a list and the clients can retrieve them.

## AlignmentAnchorManager class deep dive

The Colocation package picks the first spatial anchor and uses it to do the alignment. This assumes that any given anchor is visible to both players (host and client).

The `AlignmentAnchorManager` class is defined in `/Packages/com.meta.xr.sdk.core/Scripts/BuildingBlocks/MultiplayerBlocks/Shared/Colocation/ColocationPackage/Runtime/AlignmentAnchorManager.cs` of the Colocation package. Once the Colocation package retrieves the shared spatial anchors to use and create alignment, it calls this alignment coroutine:

```
    private IEnumerator AlignmentCoroutine(OVRSpatialAnchor anchor, int alignmentCount) {

      Debug.Log("AlignmentAnchorManager: called AlignmentCoroutine");

      while (alignmentCount > 0) {

        if (_anchorToAlignTo != null) {

          _cameraRigTransform.position = Vector3.zero;
          _cameraRigTransform.eulerAngles = Vector3.zero;

          yield return null;
        }

        var anchorTransform = anchor.transform;
        if (_cameraRigTransform != null) {
          Debug.Log("AlignmentAnchorManager: CameraRigTransform is valid");
          _cameraRigTransform.position = anchorTransform.InverseTransformPoint(Vector3.zero);
          _cameraRigTransform.eulerAngles = new Vector3(0, -anchorTransform.eulerAngles.y, 0);
        }

        if (_playerHandsTransform != null) {
          _playerHandsTransform.localPosition = -_cameraRigTransform.position;
          _playerHandsTransform.localEulerAngles = -_cameraRigTransform.eulerAngles;
        }

        _anchorToAlignTo = anchor;
        alignmentCount--;
        yield return new WaitForEndOfFrame();
      }

      Debug.Log("AlignmentAnchorManager: Finished Alignment!");
      OnAfterAlignment.Invoke();
    }
```

This moves the player so that wherever the shared spatial anchor object is, it ensures that the player is relative to that object, the same way that the player in OVR Space is relative to the spatial anchor.