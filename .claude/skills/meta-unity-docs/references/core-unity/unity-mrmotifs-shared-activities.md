# Unity Mrmotifs Shared Activities

**Documentation Index:** Learn about unity mrmotifs shared activities in this documentation.

---

---
title: "Shared Activities in Mixed Reality Motif"
description: "Create shared MR activities with aligned virtual objects across multiple users using spatial anchors and multiplayer frameworks."
last_updated: "2026-04-23"
---

<box display="flex" flex-direction="column" align-items="center">
  <a href="https://www.youtube.com/watch?v=ZaW47wZJb0k">
    <img alt="Shared Activities Thumbnail" src="/images/unity-mrmotifs-2-thumbnail.png" border-radius="12px" width="100%" />
  </a>
</box>

Create convincing shared activities in MR that encourage authentic, intuitive interactions with the Shared Activities in Mixed Reality motif. This project uses the [`Multiplayer Building Blocks`](/documentation/unity/bb-multiplayer-blocks) to quickly and effortlessly set up a networked experience using the [`Networked Meta Avatars`](/documentation/unity/meta-avatars-networking). This motif shows developers how to easily extend Building Blocks and build custom shared experiences, like chess and movie co-watching, on top of them.

## Requirements

Below are the requirements for this project. The requirement versions are minimums. Later versions are also compatible.

<oc-devui-collapsible-card heading="Requirements List">
    <table>
        <thead>
            <tr>
                <th>Package</th>
                <th>Minimum Version</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td><a href="https://github.com/oculus-samples/Unity-MRMotifs">Unity-MRMotifs samples on GitHub</a></td>
                <td>Latest</td>
            </tr>
            <tr>
                <td><a href="https://unity.com/releases/editor/whats-new/6000.0.25">Unity 6</a></td>
                <td>Unity 6 or later</td>
            </tr>
            <tr>
                <td>URP or BiRP</td>
                <td>Recommended: URP</td>
            </tr>
            <tr>
                <td>OpenXR Plugin - <code>com.unity.xr.openxr</code></td>
                <td>1.15.0</td>
            </tr>
            <tr>
                <td><a href="https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169">Meta XR Core SDK</a> - <code>com.meta.xr.sdk.core</code></td>
                <td>78.0.0</td>
            </tr>
            <tr>
                <td><a href="https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-265014">Meta XR Interaction SDK</a> - <code>com.meta.xr.sdk.interaction.ovr</code></td>
                <td>78.0.0</td>
            </tr>
            <tr>
                <td><a href="https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-essentials-264559">Meta XR Interaction SDK Essentials</a> - <code>com.meta.xr.sdk.interaction</code></td>
                <td>78.0.0</td>
            </tr>
            <tr>
                <td><a href="https://assetstore.unity.com/packages/tools/integration/meta-avatars-sdk-271958">Meta Avatars SDK</a> - <code>com.meta.xr.sdk.avatars</code></td>
                <td>38.0.1</td>
            </tr>
            <tr>
                <td><a href="https://assetstore.unity.com/packages/tools/integration/meta-avatars-sdk-sample-assets-272863">Meta Avatars SDK Sample Assets</a> - <code>com.meta.xr.sdk.avatars.sample.assets</code></td>
                <td>38.0.1</td>
            </tr>
            <tr>
                <td><a href="https://assetstore.unity.com/packages/tools/integration/meta-xr-platform-sdk-262366">Meta XR Platform SDK</a> - <code>com.meta.xr.sdk.platform</code></td>
                <td>78.0.0</td>
            </tr>
            <tr>
                <td><a href="https://assetstore.unity.com/packages/tools/integration/meta-xr-simulator-266732">Meta XR Simulator</a> - <code>com.meta.xr.simulator</code></td>
                <td>78.0.0</td>
            </tr>
            <tr>
                <td><a href="https://assetstore.unity.com/packages/tools/network/photon-fusion-267958">Photon Fusion</a></td>
                <td>2.0.6</td>
            </tr>
            <tr>
                <td><a href="https://assetstore.unity.com/packages/tools/audio/photon-voice-2-130518">Photon Voice</a></td>
                <td>2.59</td>
            </tr>
            <tr>
                <td><a href="https://doc.photonengine.com/voice/current/getting-started/voice-for-fusion">Import Photon Voice Setup Guide</a></td>
                <td>Required</td>
            </tr>
            <tr>
                <td><a href="https://github.com/VeriorPies/ParrelSync">ParrelSync</a></td>
                <td>1.5.2 (Optional)</td>
            </tr>
        </tbody>
    </table>
</oc-devui-collapsible-card>

## The concept of Shared Activities in MR

The animation below illustrates the Shared Activities MR Motif. In MR, each user wants to place their own local board or screen without causing misalignment for others. Instead of moving the anchor (e.g., a chessboard), the solution is to adjust remote avatars’ position and rotation. This allows each player to position the board as needed while keeping others correctly aligned.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <img
    alt="Shared Activities Visualization"
    src="/images/unity-mrmotifs-2-ConceptVisualization.gif"
    border-radius="12px"
    width="100%" />
</box>

For example, if **`Client B`** is one meter behind the board, facing 45 degrees away, **`Client A`** can move their board freely while still seeing **`Client B`** in the correct relative position and rotation.

## Shared Activities sample scenes

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img alt="Chess" src="/images/unity-mrmotifs-2-Chess.gif" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
    <br/>
    <text>
      The <b>Chess scene</b> updates chess piece positions and rotations, similar to what the <b>AvatarMovementHandlerMotif</b> does. The <b>ChessBoardHandlerMotif</b> assigns State Authority to the player moving a piece and updates networked positions and rotations relative to the board. It switches each chess piece's Rigidbody to physics for the State Authority and kinematic for others, ensuring all clients sync with the State Authority.
    </text>
  </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px">
    <br/>
    <text>
      The <b>Movie Cowatching scene</b> contains the <b>MovieControlsHandlerMotif</b> which differs from the previous Chess sample by synchronizing UI elements instead of transforms, such as button and toggle states. It uses Networked Properties like <b>NetworkBools</b> and <b>NetworkedFloats</b> to track slider values and toggle states.
    </text>
  </box>
  <box padding-start="24">
    <img alt="Movie Cowatching" src="/images/unity-mrmotifs-2-Movie.gif" border-radius="12px" width="100%" />
  </box>
</box>

Notably, both samples integrate Photon Fusion’s <b>IStateAuthorityChanged</b> interface, allowing the system to wait for State Authority transfer before a player interacts with a component.

## Multiplayer setup & achieving successful entitlement checks

<p>To build a multiplayer experience with Meta XR SDKs, there are really just a couple of steps to keep in mind. Make sure to go through each step.</p>

<p><strong>1.</strong> After installing all <a href="#requirements">requirements from above</a>, set up a multiplayer scene with the following Building Blocks:</p>

<oc-devui-collapsible-card heading="Core Building Blocks Overview">
    <ul>
        <li>
            <strong>[BuildingBlock] Camera Rig</strong>: On the Camera Rig feel free to add any additional Building Blocks such as grab and ray interaction or hands and controller tracking. Or simply use the <strong>OVRCameraRig</strong> prefab located at <code>Assets/MRMotifs/Shared Assets/Prefabs</code>.
        </li>
        <li>
            <strong>[BuildingBlock] Passthrough</strong>: Make sure the <code>CenterEyeAnchor</code>'s <code>Background Type</code> is set to <strong>Solid Color</strong> and set the <code>Background Color</code>'s alpha to <code>0</code>.
        </li>
        <li>
            <strong>[BuildingBlock] Network Manager</strong>: Contains the heart of any multiplayer session: <a href="https://doc.photonengine.com/fusion/current/manual/network-runner">Network Runner</a> and Network Events. The Multiplayer Building Blocks use <code>FusionBBEvents</code> instead of direct Network Events. <code>FusionBBEvents</code> wraps Photon Fusion’s <code>INetworkRunnerCallbacks</code> and exposes them as static events, allowing multiple classes to subscribe without implementing the interface directly. This simplifies event handling, promotes modularity, and keeps network logic separate from other game systems for cleaner code.
        </li>
        <li>
            <strong>[BuildingBlock] Platform Init</strong>: This component is responsible for initializing the Meta platform, fetching the access token, and checking if the user is entitled to access the platform. This is later necessary to load the avatar, based on the user’s account and if they are entitled.
        </li>
        <li>
            <strong>[BuildingBlock] Auto Matchmaking</strong>: Here sits the component which is responsible for connecting the player to the right game mode (shared) and room. By default every client will simply be spawned in the same room.
        </li>
        <li>
            <strong>[BuildingBlock] Networked Avatar</strong>: Responsible for spawning the local and remote Meta Avatars and syncing their movement over the network.
        </li>
        <li>
            <strong>[BuildingBlock] Player Name Tag</strong>: Responsible for spawning a name tag above the avatar with the user’s name or, if the user is not logged in, a random name.
        </li>
        <li>
            <strong>[BuildingBlock] Player Voice Chat</strong>: Responsible for setting up the voice chat and creating a speaker for each avatar.
        </li>
    </ul>
</oc-devui-collapsible-card>

<p><strong>2.</strong> A crucial part of a multiplayer setup, using Meta Avatars, is the <a href="/documentation/unity/unity-platform-entitlements"><code>entitlement check</code></a>, which enables Meta Platform features like avatars:</p>

<oc-devui-collapsible-card heading="Meta Platform Setup Instructions">
    <ol>
        <li>Create a developer account on the <a href="/manage">Meta Quest Dashboard</a>, and either create or join a team and create your app (Meta Horizon Store).</li>
        <li>Retrieve its App ID by navigating to the <strong>Development</strong> > <strong>API</strong> section. Find the ID under “App ID - Used to initialize the Platform SDK”.</li>
        <li>Add this App ID to the Unity project through the Unity Editor under <strong>Meta</strong> → <strong>Platform</strong> → <strong>Edit Settings</strong>, under Application ID.</li>
        <li>
            Complete the Data Use Check Up for your app, which can be found under <strong>Requirements</strong> → <strong>Data Use Check Up</strong> in the Dashboard.
            For using the <strong>Meta Avatars</strong>, fill the usage for <strong>User ID</strong>, <strong>User Profile</strong>, and <strong>Avatars</strong>.
            Make sure that for all of them <strong>Use Avatars</strong> under <strong>Usage</strong> is selected and an arbitrary description, such as
            <em>Using Meta Avatars in my multiplayer experience</em>, under <strong>Description</strong>, is written.
        </li>
        <li>
            To use other platform features such as the <strong>Friends Invite</strong> feature included in this MR Motif, then additional Data Use Check Ups have to be filled out, such as
            <strong>Deep Linking</strong>, <strong>Friends</strong>, and <strong>Invites</strong>. Do not forget in this case, to
            <a href="/documentation/unity/ps-destinations-overview#create-a-single-destination-in-the-developer-dashboard">create one or more destinations</a> first.
            Later, the API name and Deep Link Message are needed to invite friends to the experience.
        </li>
        <li>If everything was filled out correctly, the request should be accepted in a few minutes in most cases.</li>
        <li>
            In the Unity Editor under the platform settings, check the <strong>Use Standalone Platform</strong> checkbox. Then go back to the Developer Dashboard.
            Under <strong>Development</strong> > <strong>Test Users</strong>, create a new Test User. This helps developers test the app with another account besides their own,
            which can be helpful for debugging. Make sure to at least fill in the prefixes and the password.
            Now, in the Unity Editor under <strong>Meta</strong> > <strong>Platform</strong> > <strong>Edit Settings</strong>, under <strong>Unity Editor Settings</strong>,
            click on the checkbox for <strong>Use Standalone Platform</strong> and fill in the Test User credentials, and then log in.
        </li>
        <li>
            Lastly, <a href="/documentation/unity/unity-prepare-for-publish">upload</a> an APK of the app to a release channel, e.g. the Alpha channel.
            It is recommended to use the <a href="/documentation/unity/ts-odh">Meta Quest Developer Hub</a> for this.
            Under App Distribution, select the team and the application to upload the APK to.
            Make sure everyone testing the app is part of the team or is invited as alpha tester.
            To be extra sure, the same APK version can also be installed <em>additionally</em> on the devices through the Device Manager in the Meta Quest Developer Hub.
        </li>
    </ol>
</oc-devui-collapsible-card>

<p><strong>3.</strong> After setting up the scene with the Multiplayer Building Blocks, and making sure that the entitlement check passes successfully, everything is ready to build truly shared experiences.</p>

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <img
    alt="Movie Cowatching in XR Simulator"
    src="/images/unity-mrmotifs-2-MovieWatching.png"
    border-radius="12px"
    width="100%" />
</box>

## How to move Avatars and other GameObjects in a Shared Mixed Reality Activity

<p>In Shared Mixed Reality Activities, networking a single object (e.g., a chessboard) can cause misalignment when moved. Instead, each player maintains a local copy of the board and shares their avatar's position relative to it. The <code>State Authority</code> manages <a href="https://doc.photonengine.com/fusion/current/manual/data-transfer/networked-properties">networked arrays</a> of positions and rotations, updated as players move their boards. This ensures all avatars stay correctly aligned by reading from these arrays each frame or as needed.</p>

<p><strong>1.</strong> At the core of avatar movement is the <code>AvatarEntity</code>, which manages loading, configuration, and synchronization of Meta Avatars in multiplayer by streaming state data like position and rotation.</p>

<p>The <code>AvatarBehaviourFusion</code> class integrates this with Photon Fusion, networking avatar states across clients to keep remote avatars updated. It is attached to the <code>FusionAvatar</code> prefab, which is spawned into the Fusion scene by the <code>AvatarSpawnerFusion</code> class. This class handles spawning and entitlement checks, ensuring avatars are dynamically loaded and synchronized in real-time.</p>

   2. Retrieve its App ID by navigating to the **Development** > **API** section. Find the ID under “App ID - Used to initialize the Platform SDK”.
   3. Add this App ID to the Unity project through the Unity Editor under **Meta** > **Platform** > **Edit Settings**, under Application ID.
   4. Complete the Data Use Check Up for your app, which can be found under **Requirements** > **Data Use Check Up** in the Dashboard. For using the **Meta Avatars**, fill the usage for **User ID**, **User Profile**, and **Avatars**. Make sure that for all of them **Use Avatars** under **Usage** is selected and an arbitrary description, such as **Using Meta Avatars in my multiplayer experience**, under **Description**, is written.
   5. To use other platform features such as the **Friends Invite** feature included in this MR Motif, then additional Data Use Check Ups have to be filled out, such as **Deep Linking**, **Friends**, and **Invites**. Do not forget in this case, to [create one or more destinations](/documentation/unity/ps-destinations-overview#create-a-single-destination-in-the-developer-dashboard) first. Later, the API name and Deep Link Message are needed, in order to invite friends to the experience.
   6. If everything was filled out correctly, the request should be accepted in a few minutes in most cases.
   7. In the Unity Editor under the platform settings, check the **Use Standalone Platform** check box. Then go back to the Developer Dashboard. Under **Development** > **Test Users**, create a new Test User. This helps developers test the app with another account besides their own, which can be helpful for debugging. Make sure to at least fill in the prefixes and the password. Remember or note down the password. Now, in the Unity Editor under **Meta** > **Platform** > **Edit Settings**, under **Unity Editor Settings**, click on the check box for **Use Standalone Platform** and fill in the Test User credentials, and then log in.
   8. Lastly, [upload](/documentation/unity/unity-prepare-for-publish) an APK of the app to a release channel, e.g. the Alpha channel. It is recommended to use the [Meta Quest Developer Hub](/documentation/unity/ts-mqdh) for this. Under App Distribution, select the team and the application to upload the APK to. Make sure everyone testing the app is part of the team or is invited as alpha tester. To be extra sure, the same APK version can also be installed *additionally* on the devices through the Device Manager in the Meta Quest Developer Hub.

<ul>
    <li><strong>Components That Can’t Be Manipulated</strong>:
        <ul>
            <li><strong>AvatarEntity</strong>: Controls the avatar’s head and hands based on the main camera and body tracking via OVR Manager. Since this ensures natural movement, the remote avatar’s head or chest cannot be moved.</li>
            <li><strong>CenterEyeCamera</strong>: The main camera's transform is controlled by the headset and cannot be set at runtime.</li>
            <li><strong>OVR Camera Rig</strong>: Moving this rig affects the local avatar. It is used in <code>AvatarSpawnerHandlerMotif</code> for initial positioning but is left untouched afterward, as direct access to remote players' rigs is not possible, making updates difficult.</li>
        </ul>
    </li>
    <li><strong>Components That Can Be Manipulated</strong>: The <code>AvatarBehaviourFusion</code> parent object can be moved safely without causing issues. In <code>AvatarMovementHandlerMotif</code>, this acts as the "remote version" of another player's OVR Camera Rig. When a player moves, their position and rotation are sent to a networked list. Other players read from this list, ensuring each avatar appears in the correct relative position to their object while allowing personal placement of elements like a chessboard.</li>
</ul>

<p>Since remote avatars are moved using networked arrays of positions and rotations, the <code>Networked Transform</code> component of Photon Fusion is no longer needed. In the <code>AvatarMovementHandlerMotif</code> class, this component is disabled for each remote avatar to prevent Fusion from applying additional offsets, which could misalign avatars from the local chess board.</p>

## Troubleshooting Guide

Multiplayer development is not easy. However, following the setup guide should make sure that the app runs as expected. Below is a list of issues developers commonly face:

<oc-devui-collapsible-card heading="OvrAvatarAnimationBehavior.cs script missing in project">
    <p>Unfortunately this appears because the <code>OvrAvatarAnimationBehavior.cs</code> is located in the Avatars samples. To install the samples go to the Package Manager > Meta Avatars SDK > Go to the samples tab and import the samples.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="An avatar appears blue/purple">
    <p>This indicates that the avatar asset could not be loaded. Make sure there are either avatar sample assets assigned in Unity or the user is entitled to use this app (see setup above). The issue could also be caused by the number of avatar presets the app is trying to load. On the Avatar Spawner Fusion class, located on the Networked Avatar Building Block, make sure the Preloaded Sample Avatar Size is 17 or less for the Avatar SDK v28 or higher, or 32 or less for older versions.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="My avatar is not moving">
    <p>Most likely the issue here is how the Building Blocks are trying to assign the input of the OVR Rig to the Avatars. Please stick to v31 or v38.0.1 or later.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="I cannot see other avatars">
    <p>This is a known issue. If this is happening in the editor, make sure to pan the camera around and move to where the other player is expected to be for its visuals to show up. On the headset, move around or simply wait a few seconds for the other clients to show up. If they still cannot be seen, it is most likely that the entitlement check failed on a client.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Can't copy CoreAssets from package folder to Avatar2 Assets folder error">
    <p>Simply delete the <code>CoreAssets</code> folders found in the Avatar2 folder under <strong>Assets</strong> → <strong>Oculus</strong> → <strong>Avatar2</strong> and restart the editor for the package to correctly copy the right core assets into the project.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading=".zip assets missing error from the Meta Avatar SDK Sample Assets package">
    <p>Copy the missing files from the packages (Meta Avatars SDK and Meta Avatars SDK Sample Assets) into the corresponding Assets folders, or reinstall the Meta Avatars SDK Sample Assets.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Missing Fusion namespaces">
    <p>Close Unity, delete the Photon folder under Assets, open Unity and reimport Fusion 2 as well as Photon Voice.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Missing BuildingBlock namespaces, even though Meta Core SDK is installed">
    <p>This means some of the scripts are throwing errors before the packages have had a chance to compile. This usually happens when adding and removing the Photon folder during the development process. It most likely means the build settings did not clear the <strong>Scripting Define Symbols</strong> in the <strong>Player Settings</strong>. In that case make sure there are no symbols such as <code>FUSION_WEAVER</code>, <code>FUSION2</code>, or <code>PHOTON_VOICE_DEFINED</code>, before Photon Fusion 2 is imported!</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="I cannot upload my APK through the Meta Quest Developer Hub">
    <p>This can have various reasons, indicated on the error message. More often than not, the upload fails because of a <strong>missing or unnecessary permission in the Android Manifest</strong>. To make the Android manifest compatible with the store, an editor extension can be found under Meta, then Tools, and then click on <a href="/documentation/unity/unity-prepare-for-publish#generate-manifest-file">Create store-compatible AndroidManifest.xml</a>.</p>
    <p>However, if there are still unwanted Android permissions present, that cannot be removed either manually or with the editor extension, then it can be explicitly removed directly within the Android Manifest file. This is how a permission that is unexpectedly appearing might be removed. Simply add this line to the AndroidManifest.xml file in Unity:</p>
    <pre><code>&lt;uses-permission android:name="android.permission.THE_UNWANTED_PERMISSION" tools:node="remove" /&gt;</code></pre>
</oc-devui-collapsible-card>

## Resources

- [Unity-MRMotifs samples on GitHub](https://github.com/oculus-samples/Unity-MRMotifs)
- [Shared Activities in MR YouTube Tutorial](https://www.youtube.com/watch?v=ZaW47wZJb0k)
- [Shared Activities in MR Blog Post](/blog/shared-activities-mixed-reality-motif-multiplayer-meta-quest-building-blocks)
- [Multiplayer Building Blocks Setup Guide](/documentation/unity/bb-multiplayer-blocks)
- [Multiplayer Overview](/documentation/unity/ps-multiplayer-overview)
- [Multiplayer Enablement: Platform SDK implementation (Destinations, Invite to App, and more)](/documentation/unity/ps-multiplayer-enablement#platform-sdk-implementation)