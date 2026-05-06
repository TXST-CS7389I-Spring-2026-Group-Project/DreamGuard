# Unity Isdk Create Ui

**Documentation Index:** Learn about unity isdk create ui in this documentation.

---

---
title: "Create a Curved or Flat UI"
description: "Add a curved or flat user interface to your Unity project using Interaction SDK prefabs with poke interactions."
last_updated: "2025-11-03"
---

In this tutorial, you learn how to add a fully functional Interaction SDK User Interface (UI) to your Unity project.  Interaction SDK includes prefabs for a curved UI and flat UI in the [Interaction SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-ovr-integration-265014) package. The UIs look a bit different but function identically. The prefabs include the ability to swipe or scroll and select buttons via a poke interaction. To swap this for other interactions, like a ray interaction, see [Use a Ray Interaction with a UI](/documentation/unity/unity-isdk-use-ray-with-ui/).

To see examples of the UIs in a pre-built scene, see the [RayExamples](/documentation/unity/unity-isdk-example-scenes/#rayexamples) scene or the [PokeExamples](/documentation/unity/unity-isdk-example-scenes/#pokeexamples) scene.

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Choose a UI type

The Interaction SDK includes two types of UI prefab that are ready to use as soon as you drag and drop them into your scene.

1. Open your Unity scene where you completed [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

1. Under **Hierarchy**, add an empty GameObject named **CanvasModule** by right-clicking in the hierarchy and selecting **Create Empty**.

1. Under **Inspector**, add a **Pointable Canvas Module** by clicking the **Add Component** button and searching for _Pointable Canvas Module_. This component tracks the state of each canvas component in the scene.

1. Choose the UI prefab to add to your scene.
    - [Option A: Add a flat UI prefab](#add-a-flat-ui)
    - [Option B: Add a curved UI prefab](#add-a-curved-ui)

### Option A: Add a flat UI prefab {#add-a-flat-ui}

1. Under **Project**, search for _FlatUnityCanvas_. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

1. Drag the **FlatUnityCanvas** prefab from the search results into the **Hierarchy**.

1. To prepare your scene for launch, go to **File** > **Build Profiles**, click **Open Scene List**, and then click **Add Open Scenes**.

    Your scene is now ready to build.

1. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    The flat UI appears in your scene.

    

### Option B: Add a curved UI prefab {#add-a-curved-ui}

1. Under **Project**, search for _CurvedUnityCanvas_. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

1. Drag the **CurvedUnityCanvas** prefab from the search results into the **Hierarchy**.

1. To prepare your scene for launch, go to **File** > **Build Profiles**, click **Open Scene List**, and then click **Add Open Scenes**.

    Your scene is now ready to build.

1. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    The curved UI appears in your scene.

    

## Learn more

### Related topics

- To learn how to interact with a UI via a raycast, see either [Add an Interaction with Quick Actions](/documentation/unity/unity-isdk-quick-actions/) (for Interaction SDK v62+), or [Use a Ray Interaction with UI](/documentation/unity/unity-isdk-use-ray-with-ui/) (for legacy versions).
- To learn about the components of a **Poke** interaction, see [Poke Interactions](/documentation/unity/unity-isdk-poke-interaction/).
- For an interactive example of raycasting with multiple UI prefabs, see the [RayExamples](/documentation/unity/unity-isdk-example-scenes/#rayexamples) scene.
- To learn about the different types of surfaces in Interaction SDK, see [Surfaces](/documentation/unity/unity-isdk-surfaces/).
- To learn about components that establish the interactable surface area of UI elements, see [Surface Patch](/documentation/unity/unity-isdk-test/).

## Learn more

### Design guidelines

#### Controls

- [Buttons](/design/buttons/): Learn about buttons controls for immersive experiences.
- [Dropdown](/design/dropdowns/): Learn about dropdown controls for immersive experiences.
- [Selectors](/design/selectors/): Learn about selectors controls for immersive experiences.
- [Sliders](/design/sliders/): Learn about sliders controls for immersive experiences.
- [Virtual keyboard](/design/virtual-keyboard/): Learn about virtual keyboard controls for immersive experiences.

#### Layout

- [Icons and images](/design/styles_icons_images/): Learn about icons and images for immersive experiences.
- [Typography](/design/styles_typography/): Learn about typography for immersive experiences.
- [Panels](/design/panels/): Learn about panels components for immersive experiences.
- [Windows](/design/windows/): Learn about windows components for immersive experiences.
- [Tooltips](/design/tooltips/): Learn about tooltips components for immersive experiences.
- [Cards](/design/cards/): Learn about cards components for immersive experiences.
- [Dialogs](/design/dialogs/): Learn about dialogs components for immersive experiences.