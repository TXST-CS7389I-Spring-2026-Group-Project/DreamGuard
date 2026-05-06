# Unity Mrmotifs Colocated Experiences

**Documentation Index:** Learn about unity mrmotifs colocated experiences in this documentation.

---

---
title: "Colocated Experiences Motif"
description: "Build colocated MR experiences using Spatial Anchors, Colocation Discovery, and Shared Spaces for multi-user interactions."
last_updated: "2025-10-01"
---

<box display="flex" flex-direction="column" align-items="center">
  <a href="https://www.youtube.com/watch?v=39u3GQuNK6Y">
    <img alt="Video Thumbnail" src="/images/unity-mrmotifs-4-thumbnail.png" border-radius="12px" width="100%" />
  </a>
</box>

This MR Motif is a guide through everything from the very basics of [Spatial Anchors](/documentation/unity/unity-spatial-anchors-persist-content/), through how to share them over the network, to how to set up co-located experiences using [Colocation Discovery](/documentation/unity/unity-colocation-discovery/). Furthermore, the project includes a guide on how to set up the new [Space Sharing API](/documentation/unity/space-sharing-overview/), allowing developers to build colocated apps where all participants can leverage detailed information about their physical surroundings with MRUK. The project also contains the new [Microgestures](/documentation/unity/unity-microgestures), allowing users to interact with the shared Whiteboard sample in the project, using controllers and hands equally.

Colocation is used to streamline multi-user experiences, making social and collaborative play more immersive and natural. By using technologies such as the bluetooth-based [Colocation Discovery](/documentation/unity/unity-colocation-discovery/) and [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/), developers can synchronize virtual content with the real world, allowing users to interact with the same digital objects in the same physical space.

## Requirements

- [Unity 6](https://unity.com/releases/editor/whats-new/6000.0.25) or later
- URP (Recommended) or BiRP
- OpenXR Plugin (`1.15.0`) - com.unity.xr.openxr
- [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169) (`78.0.0`) - com.meta.xr.sdk.core
- [Meta XR Interaction SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-265014) (`78.0.0`) - com.meta.xr.sdk.interaction.ovr
- [Meta XR Interaction SDK Essentials](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-essentials-264559) (`78.0.0`) - com.meta.xr.sdk.interaction
- [Meta XR Platform SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-platform-sdk-262366) (`78.0.0`) - com.meta.xr.sdk.platform
- [Meta MR Utility Kit](https://assetstore.unity.com/packages/tools/integration/meta-mr-utility-kit-272450) (`78.0.0`) - com.meta.xr.mrutilitykit

## Sample scenes

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img alt="Depth Effects" src="/images/unity-mrmotifs-4-ColocationDiscovery.gif" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
    <br/>
    <text>
      The <b>Colocation Discovery scene</b> contains a shared whiteboard, where users can draw and write on the same surface. Furthermore, this scene contains the new <b><a href="/documentation/unity/unity-microgestures">Microgestures</a></b>, allowing users to move and scale the whiteboard using their hands and not just controllers. This scene teaches you how to use <b><a href="/documentation/unity/unity-colocation-discovery/">Colocation Discovery</a></b> and how to share anchors between users.
    </text>
  </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px">
    <br/>
    <text>
      The <b>Space Sharing scene</b> demonstrates how to use the new <b><a href="/documentation/unity/space-sharing-overview">Space Sharing API</a></b> to share the room layout with other users. This scene contains a bouncing ball spawner, where users can spawn balls in the same physical space and see how they bounce off the shared room meshes.
    </text>
  </box>
  <box padding-start="24">
    <img alt="Grounding Shadow" src="/images/unity-mrmotifs-4-SpaceSharing.gif" border-radius="12px" width="100%" />
  </box>
</box>

## Spatial Anchors Basics

<p>An anchor is a world-locked frame of reference that gives a position and orientation to a virtual object in the real world. Applications can use one anchor per virtual object, or choose to have multiple virtual objects use the same anchor as long as those objects are within its coverage area of three meters.</p>

<p>The Anchors API offers several key features:</p>
<ul>
    <li>Persistence across sessions: The pose of an anchor can be persisted.</li>
    <li>Discovery across sessions: Anchors can be discovered and reused.</li>
    <li>Sharing with other users: Anchors can be shared synchronously and asynchronously.</li>
</ul>

<p>For managing anchors, the MR Motifs project contains the SpatialAnchorManager, SpatialAnchorStorage, and SpatialAnchorLoader classes. These classes are samples of how the Anchors API could be used and enable basic operations such as creating, saving, loading, and erasing anchors.</p>

### Basic Anchor Operations

<oc-devui-collapsible-card heading="📍 Create Anchor">
    <p>Creating an anchor can be done by simply instantiating a prefab that contains the OVRSpatialAnchor component, or taking an existing object in the scene and adding the OVRSpatialAnchor component to it. This assigns a UUID and creates the anchor asynchronously.</p>

    <pre><code>var anchor = gameObject.AddComponent&lt;OVRSpatialAnchor&gt;();

while (!anchor.Created)
{
    await Task.Yield();
}</code></pre>

    <p>The anchor contains a property called <code>Created</code> to check if the creation is already successfully completed. It is good practice to wait until the anchor has been created before continuing.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="💾 Save Anchor">
    <p>The anchor can be saved by calling <a href="/reference/unity/v74/class_o_v_r_spatial_anchor">OVRSpatialAnchor.SaveAnchorAsync</a>.</p>

    <pre><code>await anchor.SaveAnchorAsync();
SpatialAnchorStorage.SaveUuidToPlayerPrefs(anchor.Uuid);</code></pre>

    <p>The ColocatedExperiences project contains a static SpatialAnchorStorage class, which allows the user to save the anchors to the <a href="https://docs.unity3d.com/6000.0/Documentation/ScriptReference/PlayerPrefs.html">Unity PlayerPrefs</a>. We can simply call the SaveUuidToPlayerPrefs method of the sample project, to store the spatial anchor on the device.</p>

    <pre><code>public static void SaveUuidToPlayerPrefs(Guid uuid)
{
    var count = PlayerPrefs.GetInt(NumUuidsKey, 0);
    PlayerPrefs.SetString($"{UuidKeyPrefix}{count}", uuid.ToString());
    PlayerPrefs.SetInt(NumUuidsKey, count + 1);
    PlayerPrefs.Save();
}</code></pre>

    <p>This method adds the new UUID to the PlayerPrefs with a count. This way, one can easily keep track of multiple UUIDs and access them later.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="📦 Load Anchor">
    <p>For loading anchors there is a class called SpatialAnchorLoader, as this involves three separate steps: Loading, Localizing, and Binding. When loading an anchor they are initially unbound. When an anchor is unbound, it means the anchor is not yet connected to its intended GameObject’s OVRSpatialAnchor component. The anchor must be bound to manage its lifecycle and to provide access to other features such as save and erase.</p>

    <p>To load an anchor, its UUID is needed. As all UUIDs are stored in the PlayerPrefs, the user can query a list of UUIDs from the static SpatialAnchorStorage class.</p>

    <pre><code>var uuids = SpatialAnchorStorage.LoadAllUuidsFromPlayerPrefs();</code></pre>

    <p>Next, the <a href="/reference/unity/v74/class_o_v_r_spatial_anchor">LoadUnboundAnchorsAsync</a> method is called. Also here, it is best-practice to await a successful result, before the anchor is then localized by calling LocalizeAsync. Localizing an anchor causes the system to determine the anchor’s pose in the world.</p>

    <pre><code>var unboundAnchors = new List&lt;OVRSpatialAnchor.UnboundAnchor&gt;();
await OVRSpatialAnchor.LoadUnboundAnchorsAsync(uuids, unboundAnchors);

foreach (var unboundAnchor in unboundAnchors)
{
    if (await unboundAnchor.LocalizeAsync())
    {
        // Bind anchor here
    }
}</code></pre>

    <p>We await the successful localization before binding the anchor. To bind the unbound anchor to its OVRSpatialAnchor component, the built-in BindTo method can be called.</p>

    <pre><code>if (!unboundAnchor.TryGetPose(out var pose))
{
    return;
}

var anchorObject = Instantiate(anchorPrefab.gameObject, pose.position, pose.rotation);
var spatialAnchor = anchorObject.GetComponent&lt;OVRSpatialAnchor&gt;();

unboundAnchor.BindTo(spatialAnchor);</code></pre>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="🗑️ Erase Anchor">
    <p>We use the <a href="/reference/unity/v74/class_o_v_r_spatial_anchor">OVRSpatialAnchor.EraseAnchorAsync</a> method to erase a spatial anchor from persistent storage. After that, it is best-practice to destroy the anchor GameObject to stop tracking it in the runtime.</p>

    <pre><code>await anchor.EraseAnchorAsync();
Destroy(anchor.gameObject);</code></pre>

    <p>And with this how anchors are easily created, saved, loaded, and erased. To create colocated experiences it is also important to know how to share anchors and align other users to their pose.</p>
</oc-devui-collapsible-card>

## Share an Anchor & Colocation Setup

<p>To set up colocation, it is recommended to use <a href="/documentation/unity/unity-colocation-discovery">Colocation Discovery</a> since version 71 of the Meta XR SDK in Unity. This is a feature that allows users to advertise to and discover nearby users via Bluetooth. The class it belongs to is called <a href="/reference/unity/latest/class_o_v_r_colocation_session"><code>OVRColocationSession</code></a>. Typically, the advertising clients seek to act as the host for a multi-user experience, and the discovering clients are interested in joining a hosted experience.</p>

### Requirements

>To enable Colocation Discovery, the <a href="/documentation/unity/unity-colocation-discovery#enable-via-ovrmanager">Colocation Session Support</a> on the OVR Manager needs to be **Required**.

<p>Colocation Discovery requires one of the following configurations:</p>
<ul>
    <li>The user is a member of a verified developer team.</li>
    <li>The user is a test user from the developer team owning the app.</li>
    <li>The user is invited by a developer team to a release channel (except for production).</li>
</ul>

See <a href="/documentation/unity/unity-colocation-discovery#configurations">Configurations</a> for more information.

For <a href="/documentation/unity/unity-shared-spatial-anchors">shared spatial anchors</a>, also the **Shared Spatial Anchor Support** permission needs to be set to "Required". Furthermore, the device must be connected to the internet. Lastly, **Enhanced Spatial Services** must be enabled on the Meta Quest device.

To turn on <a href="/documentation/unity/unity-shared-spatial-anchors#enhanced-spatial-services">Enhanced Spatial Services</a>, go to **Settings** > **Privacy and Safety** > **Device Permissions**, and enable **Enhanced Spatial Services**.
The app will detect when this setting is disabled and inform users to turn it on.

<h2>Colocation Discovery & Group-Based Anchor Sharing</h2>

<p>As of v71, <a href="/documentation/unity/unity-shared-spatial-anchors#understanding-group-based-vs-user-based-spatial-anchor-sharing-and-loading">Spatial Anchor sharing and loading is groups instead of user based</a>. This eliminates the need for users to be entitled to the app and therefore for developers to manage user IDs through their verified app on the Developer Dashboard. Instead, an arbitrary group UUID is used for sharing and loading anchors, making Group Sharing the recommended approach.</p>

<p>Before sharing a spatial anchor with a group, one of the participants, usually the host, must create a single UUID representing the group and communicate it to the others. The method of that communication can be either via an app-managed network connection, such as <a href="https://docs-multiplayer.unity3d.com/netcode/current/about/">Unity Netcode</a> or <a href="https://www.photonengine.com/fusion">Photon Fusion</a>, or via <a href="/documentation/unity/unity-colocation-discovery">Colocation Discovery</a>, which greatly reduces end-user friction around setting up colocated experiences.</p>

<oc-devui-collapsible-card heading="📢 Colocation Discovery: Session Advertisement & Anchor Sharing">
    <p>The group ID is automatically generated as the result of advertising. In the code below, the host starts the advertisement. After successful advertisement the user can read the group ID from the advertisement result.</p>

    <pre><code>var advertisementResult = await OVRColocationSession.StartAdvertisementAsync(null);
_groupId = advertisementResult.Value;

// Create and save anchor here</code></pre>

    <p>Instead of sending null in the StartAdvertisementAsync method, the host can also send some session information in the form of a string, such as a session name or simply a message to other users. We can send a maximum of 1024 bytes of data.</p>

    <p>After creating and saving the spatial anchor as seen in the previous section, the host is then able to call the group-based function for sharing anchors.</p>

    <pre><code>OVRSpatialAnchor.ShareAsync(new List&lt;OVRSpatialAnchor&gt; { anchor }, _groupId);</code></pre>

    <p>For the full anchor creation flow, see the <a href="#basic-anchor-operations">Spatial Anchors Basics</a> section above.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="🔍 Colocation Discovery: Session Discovery & Anchor Loading">
    <p>After the host has started advertising the session and has successfully shared the anchor, including the created group ID, all other users are ready to discover the session. Users can subscribe to the OnColocationSessionDiscovered event and then start the Discovery with StartDiscoveryAsync.</p>

    <pre><code>OVRColocationSession.ColocationSessionDiscovered += OnColocationSessionDiscovered;
OVRColocationSession.StartDiscoveryAsync();</code></pre>

    <p>Once the colocation session has been discovered, the OnColocationSessionDiscovered callback is activated. We can then read the group ID from the discovered session and use it to load the anchor.</p>

    <pre><code>private void OnColocationSessionDiscovered(OVRColocationSession.Data sessionData)
{
    _groupId = session.AdvertisementUuid;
    // Load anchor here
}</code></pre>

    <p>The user can now load the anchor but this time the function is slightly different and is called LoadUnboundSharedAnchorsAsync and takes the group ID as input.</p>

    <pre><code>var unboundAnchors = new List&lt;OVRSpatialAnchor.UnboundAnchor&gt;();
await OVRSpatialAnchor.LoadUnboundSharedAnchorsAsync(_groupId, unboundAnchors);</code></pre>

    <p>After successfully loading, keep in mind to localize and bind the anchor to an OVRSpatialAnchor component as shown previously.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="📐 Anchor Alignment">
    <p>Next, the users are ready to be aligned to the anchor’s pose. This is necessary for a truly colocated experience and so that the users have the same tracking space as the host. To do this the app can simply adjust the position and rotation of the user's Camera Rig to the anchor’s pose.</p>

    <pre><code>cameraRig.Transform.position = anchor.Transform.InverseTransformPoint(Vector3.zero);
cameraRig.Transform.eulerAngles = new Vector3(0, -anchor.Transform.eulerAngles.y, 0);</code></pre>
</oc-devui-collapsible-card>

<section>
    <p>This is all that is needed to create a seamless colocated experience in just a few lines of code.</p>
</section>

<h2>Space Sharing</h2>

<p>A popular use case when doing colocation is to also share the room layout with other users. With version 74 of the Meta XR SDK a powerful <a href="/documentation/unity/space-sharing-overview">Space Sharing API</a> has been introduced, making it extremely easy and seamless to share rooms across clients in a co-located experience.</p>

<h3>Requirements and Limitations</h3>

<ul>
  <li>Only the host needs to have scanned the room beforehand or at the start of the experience.</li>
  <li>The APK must be uploaded to a release channel on the Developer Dashboard and all users or test users must be invited to that channel or be part of the team.</li>
  <li>It is not possible to share a space between two devices with the same account. Therefore, there are two ways to test space sharing during development:
    <ul>
      <li>Use an additional device of a friend/colleague and share a space between the two. Make sure they are <a href="/documentation/unity/unity-platform-entitlements">entitled</a> to use the app.</li>
      <li><a href="/resources/test-users/">Create a test user</a> and log in with that test user on a second device. In this case, also make sure the test user email is invited to the release channel and entitled to use the app.</li>
    </ul>
  </li>
</ul>

<h3>Space Sharing Setup</h3>

<p>We can combine the Space Sharing API with Colocation Discovery, which makes this whole setup extremely straightforward. To share a room, all the host has to do is to talk to the MRUK singleton instance, get a list of MRUK rooms, and call the built-in <code>ShareRoomsAsync</code> method.</p>

<pre><code>// For sharing multiple rooms
var rooms = MRUK.Instance.Rooms;
MRUK.Instance.ShareRoomsAsync(rooms, _groupId);

// For sharing a single (current) room
var room = MRUK.Instance.GetCurrentRoom();
room.ShareRoomAsync(_groupId);
</code></pre>

<p>Similarly, it is possible for the other users to load all rooms shared with the group ID and align themselves to the room’s floor world pose.</p>

<pre><code>MRUK.Instance.LoadSceneFromSharedRooms(null, _groupId, alignmentData: (roomUuid, remoteFloorWorldPose));
</code></pre>

<p>For more detailed code and information on how to get all room information such as the floor world pose, check out the sample code in the <strong>SpaceSharingManager</strong> class of the Colocated Experiences MR Motif!</p>

<h2>Troubleshooting Guide</h2>

<oc-devui-collapsible-card heading="Result Unsupported">
    <p>This error can occur when using Colocation Discovery and indicates that the correct permissions are not set. Make sure to set the <strong>Colocation Session Support</strong> and <strong>Shared Spatial Anchor Support</strong> permissions to <strong>Required</strong> in the <code>OVRManager</code>.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Failure_GroupNotFound">
    <p>This error can occur when using Space Sharing and indicates that no group ID can be found. Make sure to use the correct group ID when sharing and loading anchors, and that the group ID is actually shared with the other users trying to load the anchor. Sharing anchors between two apps with different package names is not supported.</p>
</oc-devui-collapsible-card>

<h2>Resources</h2>

<ul>
  <li><a href="https://github.com/oculus-samples/Unity-MRMotifs">Unity-MRMotifs samples on GitHub</a></li>
  <li><a href="https://www.youtube.com/watch?v=ZaW47wZJb0k">Colocated Experiences YouTube Tutorial</a></li>
  <li><a href="/blog/shared-activities-mixed-reality-motif-multiplayer-meta-quest-building-blocks">Colocated Experiences Blog Post</a></li>
  <li><a href="/documentation/unity/unity-spatial-anchors-overview/">Spatial Anchors Overview</a></li>
  <li><a href="/documentation/unity/unity-colocation-discovery/">Colocation Discovery</a></li>
  <li><a href="/documentation/unity/space-sharing-overview/">Space Sharing API</a></li>
  <li><a href="/documentation/unity/unity-microgestures">Microgestures</a></li>
</ul>