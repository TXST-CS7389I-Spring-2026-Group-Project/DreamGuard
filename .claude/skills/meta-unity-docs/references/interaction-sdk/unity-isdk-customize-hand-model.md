# Unity Isdk Customize Hand Model

**Documentation Index:** Learn about unity isdk customize hand model in this documentation.

---

---
title: "Use Custom Hand Model"
description: "Replace the default Interaction SDK hands with custom hand models by parenting meshes or retargeting joints."
last_updated: "2025-11-06"
---

In this tutorial, you learn how to replace the default Interaction SDK hands with your own set of custom hands. There are two ways to add a custom hand model. You can either parent additional meshes to the synthetic hand (Option A), or you can retarget the joints of the synthetic hand to a custom mesh (Option B).

When possible, use Option A since it's simpler and faster than Option B.

## Before you begin

Complete the steps in [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Option A: Parenting additional meshes

This approach parents additional meshes to the default hand using joint position data from the **HandJoint** component. You can also retexture the hand to further customize it. In this section, you'll parent a ring mesh to one of your fingers.

1. Open the Unity scene where you completed [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

1. In the **Project** window search bar, search for _ButtonRing_ and drag it into **Hierarchy**. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

   A ring object appears in your scene.

1. Under **Hierarchy**, select the **ButtonRing** you just added.

1. Under **Inspector**, add a **Hand Joint** component by clicking the **Add Component** button and searching for _Hand Joint_.

1. In the **Hand Joint** component, set **Hand** to the **LeftHandSynthetic** GameObject. This is the hand data source used by the Interaction SDK rig. If you are using a non-Interaction SDK setup, use **LeftHand** instead.

1. Set **Hand Joint Id** to **Hand Index 2**. This is the joint the ring will track.

   If the ring appears offset from the finger, select **ButtonRing** in the **Hierarchy** and reset its **Transform** position to `(0, 0, 0)`.

   

1. In the **Project** window search bar, search for _GoldMat_, and then drag the **GoldMat** material onto **ButtonRing** in the **Inspector**.

1. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

   When the current scene loads, you'll see the ring attached to your index finger. It will move with your index joint.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
      <video-source handle="GK-TUgIbJq_mxf8CAJ65ufnKtW10bosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      <b>Video 1</b>: Ring attached to the user's finger. Note that this video shows the right hand, but this tutorial uses the left hand.
  </text>
</box>

## Option B: Retargeting to a custom mesh

This approach completely replaces the default hand with a custom mesh. Although this method offers the most customization, it can also require more time than parenting meshes to the existing hand mesh. It also requires a basic understanding of rigging and 3D modeling. To retarget to a custom mesh, you need to delete the existing hand mesh but keep the armature, then weight paint your custom mesh to the armature, and finally import the custom mesh into Unity.

### Export the hand model

Before you can bind your custom mesh to the SDK's armature, you need to export the hand file from Unity.

1. Open the Unity scene where you completed [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

1. In the **Project** window search bar, search for _OculusHand_L_. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

   

1. In the search results, right-click on the file and select **Show in Explorer**.

   Windows File Explorer opens to the location of the file.

1. In **File Explorer**, make a copy of the file so you have a backup of the original hand.

1. Save the copy on your computer. It doesn't have to stay in the original folder.

1. Open the copy of **OculusHand_L.fbx** in your preferred 3D modeling software, such as Blender or Maya.

### Retarget armature to new mesh

To make your custom mesh move in sync with your physical hands, you have to retarget the armature from the default hand to your new mesh.

1. With your copy of **OculusHand_L.fbx** open in your 3D modeling software, import your custom mesh so they're both in the same file.

1. Scale your custom mesh so it roughly matches the size of the hand.

   <oc-devui-note type="warning">Do not apply scale to the custom mesh. Its scale should match the hand's scale, which in Blender is 0.010 on the X, Y, and Z axes. If you apply scale, it may cause animation issues when you import your custom mesh into Unity.</oc-devui-note>

1. Align your custom mesh with the armature. This makes it easier to weight paint, especially if you decide to use automatic weight painting.

   

   _A custom mesh aligned with the armature._

1. Delete the existing hand mesh so only the armature remains.

   <box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
     <section>
       <embed-video width="100%">
         <video-source handle="GGe0UgJ0Kx4jlEoHAEq1ALITGrtfbosWAAAF" />
       </embed-video>
     </section>
     <text display="block" color="secondary">
         <b>Video 2</b>: Deletion of the hand mesh.
     </text>
   </box>

1. Weight paint your mesh so it moves in sync with the hand bones.

   <oc-devui-note type="warning">In the next step, export your FBX file with the X-axis as the forward axis and do not add any leaf bones.</oc-devui-note>

1. Rename your mesh (but not the armature) to **l_handMeshNode**. This will help you find your mesh once you import it into Unity.

1. Export the mesh and armature together as an FBX file. When exporting, set the X-axis as the forward axis, and do **not** add any leaf bones.

### Import custom hand to Unity

With your new mesh bound to the armature, it's ready to be imported to Unity.

1. In Unity, open the scene where you want to use your custom hand.

1. To import your custom hand, in the toolbar at the top of the screen, select **Asset** > **Import New Asset**. If that option is greyed out, it means the **Package** folder is selected in the **Project** window. In that case, click the **Assets** folder in the **Project** window and try again.

   The file browser appears.

   

1. Locate your custom mesh file, then click **Import**.

   Under **Project**, your custom hand appears in the **Assets** folder.

### Link HandVisual to the custom hand

Since the **HandVisual** component controls the appearance of the hand, you have to update it to use your custom mesh.

1. Drag your custom hand from the **Assets** folder onto **OVRLeftHandVisual** in the **Hierarchy**. If you're using **SyntheticHand**, **OVRLeftHandVisual** is under the **LeftHandSynthetic** GameObject, otherwise it's under the **HandVisualsLeft** GameObject.

   

   _Placing the custom hand onto **OVRLeftHandVisual**._

1. In the **Project** window, search for _OculusHand_LAvatar_. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

1. Under **Hierarchy**, select your custom hand.

1. Under **Inspector**, add an **Animator** component.

1. Set the **Animator** component's **Avatar** field to the **OculusHand_LAvatar** file from the **Project** window search results by dragging the file onto the **Avatar** field.

1. Under **Hierarchy**, select **l_handMeshNode**, which is a child of your custom hand GameObject. If you don't see a GameObject with the name **l_handMeshNode**, check what the mesh name is in your 3d modeling file, then search your Unity hierarchy for that name.

1. Under **Inspector**, add a **Material Property Block Editor** component.

1. In the **Material Property Block Editor** component, make sure **Element 0** is set to the GameObject's **Skinned Mesh Renderer** component.

   

   _**Element 0** set to the **Skinned Mesh Renderer** component._

1. Under **Hierarchy**, select **OVRLeftHandVisual**.

1. Under **Inspector**, in the **Hand Visual** component, set **Skinned Mesh Renderer** to **l_handMeshNode**. This tells the SDK to use your custom mesh as the hand mesh.

1. Set **Hand Material Property Block Editor** to **l_handMeshNode**.

1. Click the **Auto Map Joints** button. This ensures that each bone in your custom mesh will align itself with the matching bone during hand tracking.

   The list of joints automatically updates. The joints with names ending in **...Tip** will be empty, but that's alright.

_Updating the **Hand Visual** component._

1. In the **Hand Confidence Visual** component, set **Hand Material Property Block Editor** to **l_handMeshNode**.

1. (Optional) Repeat the entire **Retargeting to a Custom Mesh** section for the right hand.

1. Prepare to launch your scene by going to **File** > **Build Profiles** and clicking the **Add Open Scenes** button.

   Your scene is now ready to build.

1. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

   When the current scene loads, lift your hands. Your custom hand model is now visible.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
      <video-source handle="GO6zUgIfPmuVgloCAFACDInIks00bosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      <b>Video 3</b>: Hand tracking shown on a custom hand model.
  </text>
</box>

## Learn more

### Design guidelines

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.