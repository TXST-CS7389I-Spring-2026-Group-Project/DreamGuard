# Unity Shared Spatial Anchors Walkthrough

**Documentation Index:** Implement shared spatial anchors in Unity apps using Photon Unity Networking for multi-player experiences.

---

---
title: "Shared Spatial Anchors Walkthrough"
description: "A walkthrough of how shared spatial anchors are implemented in the Discover Sample."
last_updated: "2024-11-25"
---

## Overview

The [Unity-Discover](https://github.com/oculus-samples/Unity-Discover) showcase app is an open source application that highlights Meta Quest Mixed Reality APIs. Among the features it highlights is the use of shared spatial anchors (SSAs). In this walkthrough we will examine the Unity-Discover application's SSA implementation.

After reading this page, you should be able to:

- Outline the steps to create a new room as a host in the Unity-Discover app.
- Identify the actions performed by the Unity-Discover app when a new room is created by a host.
- Explain the procedure for a player to join an existing room and the subsequent actions taken by the Unity-Discover app.

## How to get the sample

You do not need to clone or run the Unity-Discover showcase app to go through this walkthrough. After you finish this walkthrough, you can download the running app from [Discover](https://www.meta.com/experiences/discover/7041851792509764/). If you want to dig further into the app, start with [How to Run the Project in Unity](https://github.com/oculus-samples/Unity-Discover#how-to-run-the-project-in-unity) in GitHub.

Unity-Discover makes use of [Photon Unity Networking](https://www.photonengine.com/pun) as its spatial anchor networking application. For information on why a spatial anchor networking utility is necessary, see [Use Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/).

## How to use the sample

When you run the Unity-Discover app, you have the option to create a new room as a host, or to join an existing room as a player.

{:width="550px"}

If you choose to be a host, you provide a room name. Unity-Discover then:

- Creates a room.
- Loads in the initial scene.
- Creates a spatial anchor to serve as the origin for the application.
- Establishes a connection to Photon Fusion.
- Shares the spatial anchor to Photon Fusion, establishing it as the SSA.
- Creates a colocated space within the room with the SSA as the origin.
- Aligns the host (as a player) to the SSA.

If you choose to be a player and join an existing room, Unity-Discover:

- Establishes a connection to Photon Fusion.
- Queries the SSA for the named room from Photon Fusion.
- Adds the new player to the colocated space.
- Aligns the player to the SSA.

Once the SSA is set up, all subsequent actions that start (Bicycle Mechanic or Drone Rage) are relative to that SSA.

If the host leaves the room, and anyone else is still present, one of those players becomes the host.

There are no additional SSAs in Unity-Discover. Because the app is designed to have everything relative to the one SSA, no others are needed.

## Code-level application flow

Let's take a look at how this plays out in the Unity Project. Unity-Discover is a large app, but we will only talk about the parts that help us understand the SSA implementation.

### Key prefabs and scripts

Several prefabs play important roles in getting this part of the Unity-Discover app to work:

- **NetworkModalWindow** prefab. This prefab instantiates the `NetworkModalWindowController.cs` script, which displays and handles the startup options.
- **DiscoverAppController** prefab. This prefab is the central hub for the Unity-Discover app. It brings together the following:
  - `DiscoverAppController.cs` script, which manages the connections and sessions for players
  - `NetworkSceneManagerDefault.cs` script, which loads and switches among available scenes. This is part of the Fusion assembly.
  - `MRSceneLoader` script, which loads the OVRSceneManager.
  - `ColocationDriverNetObj.cs` script, which sets up the conditions for creating a colocated space.
  - **NetworkRunner** prefab, which carries communications between the Unity-Discover app and the Photon Fusion network. This is part of the Fusion assembly.
  - `ColocationLauncher.cs` script. This script creates the colocated spaces for both host and players.
  - `SharedAnchorManager.cs` script, which creates and shares the SSA.
  - `AlignmentAnchorManager.cs` script, which aligns players to the SSA.
  - `NetworkApplicationManager.cs` script, which launches the games available in the room.
  - **Player** prefab, which handles the player characteristics within the games. This is part of the Fusion assembly.

{:width="550px"}

### A host creates a new room

This section follows the Unity-Discover application as it starts up and creates the colocated space for gameplay by one or more players.

#### Get user input

At program start, the `NetworkModalWindowController.cs` script `ShowNetworkSelectionMenu` method displays the user's selection options. Among these is whether it's a host or player request. This decision governs whether to create a new room, or look for the room specified by the user. This is important, because the path for a host includes most of the preliminary room setup.

```csharp
// NetworkModalWindowController.cs
30    public void ShowNetworkSelectionMenu(
31        Action<string> hostAction, // roomName
32        Action<string, bool> joinAction, // roomName, isRemote
33        Action singlePlayerAction,
34        Action<string> onRegionSelected,
35        string defaultRoomName = null
36    )
37    {
38        m_networkSelectionMenu
39        .Initialize(hostAction, joinAction, singlePlayerAction, ShowSettingsPage, defaultRoomName);
40        m_settingsPage.OnNetworkRegionSelected = onRegionSelected;
41        m_otherActive = true;
42        m_uiParent.SetActive(true);
43        m_networkSelectionMenu.gameObject.SetActive(true);
44    }
```

Then, in the `DiscoverAppController.cs Start()` method, the options are displayed:

```csharp
// NetworkModalWindowController.cs
54  private void Start()
55  {
56      MainMenuController.Instance.EnableMenuButton(false);
57      ShowNetworkNux(); //present the user options
58  }
```

#### Start the connection and initial scene

After the user has opted to be a host or a player, the `DiscoverAppController.cs` script starts the next steps. If the user is a host, `StartHost()` is called, but if the user is a player, `StartClient()` is called instead. We describe the player path later in [A Player Joins an Existing Room](#a-player-joins-an-existing-room).

```csharp
// DiscoverAppController.cs
 98  public void StartHost()
 99  {
100      NUXManager.Instance.StartNux(
101          NUX_EXPERIENCE_KEY,
102          () => StartConnection(true));
103  }
```

`StartHost()` makes a call to the `NUXController.cs` script `StartNux()` method, which is itself dependent on the `DiscoverAppController.cs` script `StartConnectionAsync()` method:

```csharp
// DiscoverAppController.cs
125  private async void StartConnectionAsync(bool isHost, GameMode mode = GameMode.Shared)
126  {
127      if (isHost)   //if this is a host, create a new room
128      {
129          NetworkModalWindowController.Instance.ShowMessage("Loading Room");
130          _ = await m_mrSceneLoader.LoadScene();
131      }
132
133      SetupForNetworkRunner();    // DiscoverAppController.cs (this file), line 247, instantiate and configures the NetworkRunner
134      NetworkModalWindowController.Instance.ShowMessage("Connecting to Photon...");
135      ColocationDriverNetObj.OnColocationCompletedCallback += OnColocationReady;
136      ColocationDriverNetObj.SkipColocation = AvatarColocationManager.Instance.IsCurrentPlayerRemote;
137      await Connect(isHost, mode);  // DiscoverAppController.cs (this file), line 142
138  }
```

The call to `m_mrSceneLoader.LoadScene()` is to the `MRSceneLoader.cs` script, which creates the initial scene.

```csharp
// MRSceneLoader.cs
17   public async UniTask<bool> LoadScene()
18   {
19       if (!m_sceneLoaded)
20       {
21           m_sceneLoadingTask = new();
```

#### Set up the session

The `Connect(isHost, mode)` method in `DiscoverAppController.cs` script sets up the session, sets up the game arguments as per the user selections, and then attempts to start the game using `Runner.StartGame()` (in this case, _game_ refers to the initial room):

```csharp
// DiscoverAppController.cs
139  await Connect(isHost, mode);
...
168  var joined = await Runner.StartGame(args);
```

#### Set up for colocation

The `Runner` object is the **NetworkRunner** prefab, and part of the Fusion assembly. The `OnConnectedToServer` action in the `DiscoverAppController.cs` script spawns the `ColocationDriverNetObj` prefab (m_colocationPrefab).

```csharp
// DiscoverAppController.cs
296  public void OnConnectedToServer(NetworkRunner runner)
297  {
298      NetworkModalWindowController.Instance.ShowMessage(
299         $"Connected To Photon Session: {runner.SessionInfo.Name}");
...
310         Debug.Log("Spawn Colocation Prefab");
311          _ = Runner.Spawn(m_colocationPrefab);
```

When spawned, the `ColocationDriverNetObj.cs` script issues an `Init()` call, which awaits completion of the `SetUpForColocation()` method.

```csharp
// ColocationDriverNetObj.cs
72   private async void Init()
73   {
74       m_ovrCameraRigTransform = FindObjectOfType<OVRCameraRig>().transform;
75       m_oculusUser = await OculusPlatformUtils.GetLoggedInUser();
76       m_headsetGuid = Guid.NewGuid();
77       await SetupForColocation();
78   }
79
80   private async UniTask SetupForColocation()
81   {
82       if (HasStateAuthority)
83       {
84           Debug.Log("SetUpAndStartColocation for host");
```

The `SetUpForColocation()` method does a lot. Among other things, it kicks off and waits for the Photon Network. When that is ready, the method instantiates a `SharedAnchorManager` and an `AlignmentAnchormanager`, which are both key components in setting up the SSA.

```csharp
// ColocationDriverNetObj.cs
100  var sharedAnchorManager = new SharedAnchorManager
101  {
102      AnchorPrefab = m_anchorPrefab
103  };
104
105  m_alignmentAnchorManager =
106      Instantiate(m_alignmentAnchorManagerPrefab).GetComponent<AlignmentAnchorManager>();
107
108  m_alignmentAnchorManager.Init(m_ovrCameraRigTransform);
```

With these available, it sets up the `ColocationLauncher` object:

```csharp
// ColocationDriverNetObj.cs
116  m_colocationLauncher = new ColocationLauncher();
117  m_colocationLauncher.Init(
118      m_oculusUser?.ID ?? default,
119      m_headsetGuid,
120      NetworkAdapter.NetworkData,
121      NetworkAdapter.NetworkMessenger,
122      sharedAnchorManager,
123      m_alignmentAnchorManager,
124      overrideEventCode
125  );
```

Finally, it calls the method `CreateColocatedSpace()`

```csharp
// ColocationDriverNetObj.cs
128  if (HasStateAuthority)
129  {
130      m_colocationLauncher.CreateColocatedSpace();
131  }
```
#### Launch the colocated space and establish the shared spatial anchor

The `m_colocationLauncher` variable is a `ColocationLauncher` object. In the `ColocationLauncher.cs` script, the `CreateColocatedSpace()` method calls `CreateAlignmentAnchor`, which creates the spatial anchor. This is addressed in more detail in the following sections.

```csharp
// ColocationLauncher.cs
213  private async UniTaskVoid CreateNewColocatedSpace() {
214    _myAlignmentAnchor = await CreateAlignmentAnchor();
215    if (_myAlignmentAnchor == null) {
216       Debug.LogError("ColocationLauncher: Could not create the anchor");
217       return;
218    }
```
##### Create the anchor

Here's how the anchor is created. `CreateAlignmentAnchor` is also in the `ColocationLauncher.cs` script. It immediately calls the `SharedAnchorManager.cs` script `CreateAnchor` method, which in turn calls `InstantiateAnchor` right away. `InstantiateAnchor` uses the game object to create a new anchor based on the **AnchorPrefab** prefab.

```csharp
// ColocationLauncher.cs
316  private async UniTask<OVRSpatialAnchor> CreateAlignmentAnchor() {
315    var anchor = await _sharedAnchorManager.CreateAnchor(Vector3.zero, Quaternion.identity);
316    if (anchor == null) {
317      Debug.Log("ColocationLauncher: _sharedAnchorManager.CreateAnchor returned null");
318    }
```

```csharp
// SharedAnchorManager.cs
31  public async UniTask<OVRSpatialAnchor> CreateAnchor(Vector3 position, Quaternion orientation) {
32    Debug.Log("CreateAnchor: Attempt to InstantiateAnchor");
33    var anchor = InstantiateAnchor();
34    Debug.Log("CreateAnchor: Attempt to Set Position and Rotation of Anchor");
...
153  private OVRSpatialAnchor InstantiateAnchor() {
154    GameObject anchorGo;
155    if (AnchorPrefab != null) {
156      anchorGo = Object.Instantiate(AnchorPrefab);
157    } else {
158      anchorGo = new GameObject();
159      anchorGo.AddComponent<OVRSpatialAnchor>();
160    }
```

##### Save the anchor

After creating the anchor, you must save it.

```csharp
// ColocationLauncher.cs
316  private async UniTask<OVRSpatialAnchor> CreateAlignmentAnchor() {
...
322    Debug.Log($"ColocationLauncher: Anchor created: {anchor?.Uuid}");
323
324    bool isAnchorSaved = await _sharedAnchorManager.SaveAnchors();
```

In the `SharedAnchorManager.cs` script, the `SaveAnchors` method is where we finally see the call to `OVRSpatialAnchor.SaveAnchorAsync`, which saves the new anchor to persistent storage.

```csharp
// SharedAnchorManager.cs
public async UniTask<bool> SaveAnchors() {
...
    var result = await OVRSpatialAnchor.SaveAnchorsAsync(_anchors);
    return result.Success;
```

The anchor is now ready to be shared to other players.

##### Align the host and anchor

Earlier we described how in the `ColocationLauncher.cs` script, the `CreateColocatedSpace()` method calls `CreateAlignmentAnchor`. Later in that same method, it calls `AlignPlayerToAnchor`:

```csharp
// ColocationLauncher.cs
220        uint newColocationGroupdId = _networkData.GetColocationGroupCount();
221        _networkData.IncrementColocationGroupCount();
222        _networkData.AddAnchor(new Anchor(true, _myAlignmentAnchor.Uuid.ToString(), _myOculusId, newColocationGroupdId));
223        _networkData.AddPlayer(new Player(_myOculusId, newColocationGroupdId));
224        AlignPlayerToAnchor();
225        await UniTask.Yield();
226    }
```

This method is part of the `AlignmentAnchorManager.cs` script, and is called for every player, including the host. This is an async operation, so the actual work falls to the `AlignmentCoroutine` method to align both the user camera and hands.

```csharp
// AlignmentAnchorManager.cs
62  private IEnumerator AlignmentCoroutine(OVRSpatialAnchor anchor, int alignmentCount) {
63    Debug.Log("AlignmentAnchorManager: called AlignmentCoroutine");
...
66      _cameraRigTransform.position = anchorTransform.InverseTransformPoint(Vector3.zero);
66      _cameraRigTransform.eulerAngles = new Vector3(0, -anchorTransform.eulerAngles.y, 0);
...
75      _playerHandsTransform.localPosition = -_cameraRigTransform.position;
76      _playerHandsTransform.localEulerAngles = -_cameraRigTransform.eulerAngles;
```

At this point, the game options are displayed to the host. In the following graphic, you can see the location of the SSA (the 0,0,0 point for all room activity). This is the point where the host was standing when the spatial anchor was created.

{:width="550px"}

##### Sharing the anchor

Each of the colocation methods in the `ColocationLauncher.cs` script make a call to `AttemptToShareAndLocalizeToAnchor` (also in `ColocationLauncher.cs`). At the end of this method, a call to `TellOwnerToShareAnchor` transfers control to that method later in the script.

```csharp
// ColocationLauncher.cs
244  private UniTask<bool> AttemptToShareAndLocalizeToAnchor(Anchor anchor) {
245    Debug.Log(
246      $"ColocationLauncher: Called AttemptToShareAndLocalizeToAnchor with id: {anchor.uuid} and oculusId: {_myOculusId}"
247    );
...
277   _networkMessenger.SendMessageUsingOculusId(
278     _caapEventCodeDictionary[CaapEventCode.TellOwnerToShareAnchor],
279     anchorOwner,
280     data
281     );
```

The `TellOwnerToShareAnchor` method calls the `SharedAnchorManager.cs` script `ShareAnchorsWithUser` method.

```csharp
// ColocationLauncher.cs
286  private async void TellOwnerToShareAnchor(object data) {
287    Debug.Log($"ColocationLauncher: TellOwnerToShareAnchor with oculusId: {_myOculusId}");
288    var shareAndLocalizeParams = (ShareAndLocalizeParams) data;
289    ulong requestedAnchorOculusId = shareAndLocalizeParams.oculusIdAnchorRequester;
290    bool isAnchorSharedSuccessfully = await _sharedAnchorManager.ShareAnchorsWithUser(requestedAnchorOculusId);
```

### A player joins an existing room

When a player chooses to join an existing room, the flow is much simpler. If the user is a player, the `DiscoverAppController.cs` script `StartClient()` method is called and runs the `NUXController.cs` method `StartNux()`.

```csharp
// DiscoverAppController.cs
105  public void StartClient()
106  {
...
114     NUXManager.Instance.StartNux(
115         NUX_EXPERIENCE_KEY,
116         () => StartConnection(false));
117      }
118  }
```

`StartNux` is dependent on the `DiscoverAppController.cs` script method `StartConnectionAsync()`. As with the host, the `OnConnectedToServer` action is invoked. However, only the player portions are executed:

```csharp
// DiscoverAppController.cs
317    m_playerObject = runner.Spawn(
318        m_playerPrefab, onBeforeSpawned: (_, obj) =>
319        {
320            obj.GetComponent<DiscoverPlayer>().IsRemote =
321                AvatarColocationManager.Instance.IsCurrentPlayerRemote;
322        });
323    runner.SetPlayerObject(runner.LocalPlayer, m_playerObject);
324    MainMenuController.Instance.EnableMenuButton(true);
```

#### Colocate the player

For the player joining a room, the `ColocationLauncher.cs` script `ColocateAutomaticallyInternal()` method gets all known alignment anchors (for Unity-Discover, there is just one), and aligns the player to them.

```csharp
// ColocationLauncher.cs
144    List<Anchor> alignmentAnchors = GetAllAlignmentAnchors();
145    foreach (var anchor in alignmentAnchors)
146    if (await AttemptToShareAndLocalizeToAnchor(anchor)) {
147        successfullyAlignedToAnchor = true;
148        Debug.Log($"ColocationLauncher: successfully aligned to anchor with id: {anchor.uuid}");
149        _networkData.AddPlayer(new Player(_myOculusId, anchor.colocationGroupId));
150        AlignPlayerToAnchor();
151        break;
152    }
```

We will look at how the player is aligned to the SSA next.

##### Aligning the player and loading the shared spatial anchor

As we learned in [Sharing the Anchor](#sharing-the-anchor), each of the colocation methods in the `ColocationLauncher.cs` script makes a call to `AttemptToShareAndLocalizeToAnchor`. For a joining player, the method calls `LocalizeAnchor`:

```csharp
// ColocationLauncher.cs
244  private UniTask<bool> AttemptToShareAndLocalizeToAnchor(Anchor anchor) {
245    Debug.Log(
246      $"ColocationLauncher: Called AttemptToShareAndLocalizeToAnchor with id: {anchor.uuid} and oculusId: {_myOculusId}"
247    );
...
254     var sharedAnchorId = new Guid(anchor.uuid.ToString());
255     LocalizeAnchor(sharedAnchorId);
256     return _alignToAnchorTask.Task;
```

The `LocalizeAnchor` method calls the `SharedAnchorManager.cs` script method `RetrieveAnchors`:

```csharp
// ColocationLauncher.cs
334  private async void LocalizeAnchor(Guid anchorToLocalize) {
335    Debug.Log($"ColocationLauncher: Localize Anchor Called id: {_myOculusId}");
336    IReadOnlyList<OVRSpatialAnchor> sharedAnchors = null;
337    Guid[] anchorIds = {anchorToLocalize};
338    sharedAnchors = await _sharedAnchorManager.RetrieveAnchors(anchorIds);
```

In `RetrieveAnchors`, the anchors are loaded and bound.

```csharp
// SharedAnchorManager.cs
public async UniTask<IReadOnlyList<OVRSpatialAnchor>> RetrieveAnchors(Guid[] anchorIds)
{
...
  OVRSpatialAnchor.LoadUnboundSharedAnchorsAsyc().ContinueWith(result => {
    foreach (var unboundAnchor in unboundAnchors) {
      var anchor = InstantiateAnchor();
      try {
          unboundAnchor.BindTo(anchor);
          _sharedAnchors.Add(anchor);
          createdAnchors.Add(anchor);
          createTasks.Add(UniTask.WaitWhile(() => anchor.PendingCreation, PlayerLoopTiming.PreUpdate));
      }

})
```

At this point the user can participate in any multiuser activity. All gameplay is relative to the one SSA.

## Learn more
Continue learning about spatial anchors on the other pages of this documentation:

- [Troubleshooting](/documentation/unity/unity-ssa-ts/)
- [Best Practices](/documentation/unity/unity-spatial-anchors-best-practices/)

The GitHub page [Unity-Discover Documentation](https://github.com/oculus-samples/Unity-Discover/tree/main/Documentation) provides information on building, using, and understanding the app.

You can find more examples of using spatial anchors with Meta Quest in the oculus-samples GitHub repository:

- [Unity-Discover](https://github.com/oculus-samples/Unity-Discover)
- [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors)
- [Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples)

For API information, see [Unity API Reference](/reference/unity/latest).

To get started with Meta Quest Development in Unity, see [Get Started with Meta Quest Development in Unity](/documentation/unity/unity-gs-overview/)