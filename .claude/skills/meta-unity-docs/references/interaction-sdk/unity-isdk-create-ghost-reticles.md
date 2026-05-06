# Unity Isdk Create Ghost Reticles

**Documentation Index:** Learn about unity isdk create ghost reticles in this documentation.

---

---
title: "Create Ghost Reticles"
description: "Add semi-transparent ghost reticles that appear when hovering over or selecting distant objects."
---

In this tutorial, you learn how to create ghost reticles that appear when you hover over a distant cube. Ghost reticles are semi-transparent outlines that indicate when you're currently selecting or hovering over a distant object. The reticles in Interaction SDK are automatically added via QuickActions. The reticles can display cursor icons on objects, outline meshes, and draw lines from your hand to an object's current position.

To try ghost reticles in a pre-built scene, see the [DistanceGrabExamples](/documentation/unity/unity-isdk-example-scenes/#distancegrabexamples) scene.

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Create a distance grabbable GameObject

Ghost reticles are meant to be used with distant objects, so your scene needs a GameObject that's grabbable from a distance. To make that GameObject, you'll use [QuickActions](/documentation/unity/unity-isdk-quick-actions/).

1. Open the Unity scene where you completed [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

1. Create a new GameObject named **Cube** by right-clicking in the **Hierarchy** panel and then clicking **3D Object** > **Cube**.

    A cube appears in the **Scene** view.

1. In the **Hierarchy**, select **Cube**.

1. In the **Inspector**, in the **Transform** component, in the **Position** field, set the axis values to the following.
    - **X**: 0
    - **Y**: 0.7
    - **Z**: 0.5

1. In the **Scale** field, set the axis values to the following.
    - **X**: 0.27
    - **Y**: 0.27
    - **Z**: 0.27

1. In the **Hierarchy**, right-click on **Cube**, and then select **Interaction SDK** > **Add Distance Grab Interaction**.

    The **Distance Grab Wizard** appears.

1. In the **Distance Grab Wizard**, in the **Settings** tab, in the **Add Required Interactor(s)** dropdown, select **Nothing**, since the camera rig includes distance hand grab interactors by default.

1. In the **Distance Grab Type** dropdown, select one of the following options to determine how the cube will respond to distance grabs and what kind of ghost reticle it will use.
    - Grab relative to hand, which displays a reticle on the cube's surface during a hover.
    - Pull interactable to hand, which displays an outline of the cube in your hand during a hover.
    - Manipulate in place, which displays a line between your hand and the cube's surface during a hover.

1. In the **Supported Grab Types** dropdown, select one of the following options. This determines which grab types the cube will respond to.
    - None
    - Grab
    - Everything
    - Pinch
    - Palm
    - All

    In the **Distance Grab Wizard**, every dropdown in your **Settings** tab should now have a value. Depending on your selections, your **Distance Grab Wizard** may look different from the screenshot.

    

1. In the **Distance Grab Wizard**, in the **Required Components** tab, click the **Fix All** button.

    

    The wizard auto-fills the components that don't have a value.

1. (Optional) In the **Distance Grab Wizard**, in the **Optional Components** tab, click the **Fix All** button.

1. At the bottom of the **Distance Grab Wizard**, click the **Create** button.

    In the **Hierarchy**, a new GameObject named **ISDK_DistanceHandGrabInteraction** is added as a child of your **Cube** GameObject.

1. Prepare to launch your scene by going to **File** > **Build Profiles** and clicking the **Add Open Scenes** button.

    Your scene is now ready to build.

1. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    The scene loads. When you hover over the cube, the value you provided in the **Distance Grab Type** dropdown determines which reticle appears and how you can interact with the object.

    If you chose **Grab relative to hand**, when you hover over the **Cube** with either hand, a circular reticle appears.

    <box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
      <section>
        <embed-video width="100%">
          <video-source handle="GGm1UgK5q0PU5OUCAFJUNTOrBM0PbosWAAAF" />
        </embed-video>
      </section>
      <text display="block" color="secondary">
          <b>Video 1</b>: Link view showing the ghost reticles when the hands hover over the cube.
      </text>
    </box>

    If you chose **Pull interactable to hand**, when you hover over the **Cube** with either hand, an outline of the object appears in your hand.

    <box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
      <section>
        <embed-video width="100%">
          <video-source handle="GDKSUwLlWlg0zxABALpDpwRfeNNtbosWAAAF" />
        </embed-video>
      </section>
      <text display="block" color="secondary">
          <b>Video 2</b>: Link view showing the cube outline when each hand hovers over the cube.
      </text>
    </box>

    If you chose **Manipulate in place**, when you hover over the **Cube** with either hand, a line appears between your hand and the object.

    <box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
      <section>
        <embed-video width="100%">
          <video-source handle="GCO0UgLUeUGmUnMAACechsxhFHpibosWAAAF" />
        </embed-video>
      </section>
      <text display="block" color="secondary">
          <b>Video 3</b>: Link view showing a line between the hand and object when hovering over the cube.
      </text>
    </box>

## Related topics

- To learn about the distance hand grab components, see [Distance Grab Interactions](/documentation/unity/unity-isdk-distance-hand-grab-interaction/).