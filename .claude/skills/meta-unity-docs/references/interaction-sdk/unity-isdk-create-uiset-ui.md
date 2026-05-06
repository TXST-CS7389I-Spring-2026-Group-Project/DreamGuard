# Unity Isdk Create Uiset Ui

**Documentation Index:** Learn about unity isdk create uiset ui in this documentation.

---

---
title: "Creating UIs with UISet"
description: "Creating user interfaces with Interaction SDK's UISet component library."
last_updated: "2025-11-03"
---

## Set up your UI

1. In the **Project** panel, navigate to the **Packages** > **Meta XR Interaction SDK Essentials** > **Runtime** > **Sample** > **Objects** > **UISet** > **Prefabs** > **Backplate** folder and add a backplate for the UI by dragging the EmptyUIBackplateWithCanvas prefab to the Hierarchy panel.

    

    The backplate prefab contains a **Canvas**, a background for the UI, some basic layout components, and ray and poke interactable components to enable direct touch and raycast interactions with the UI.

    

1. In the **Hierarchy**, select the **CanvasRoot**. In the **Inspector**, under **Rect Transform**, you can use the **Width** and **Height** properties to set the size of the Canvas. In this example, we have use the following settings to scale it to a reasonable size for a few components:

    - **Rect Transform** > **Width**: *500*
    - **Rect Transform** > **Height**: *250*

1. In the **Hierarchy**, select the **UIBackplate**. In the **Inspector**, under **Rect Transform**, set the set the **Width** and **Height** properties to match the **canvas** width and height set in the previous step. In this example, we have use the following settings to match the canvas size:

    - **Rect Transform** > **Width**: *500*
    - **Rect Transform** > **Height**: *250*

1. Add some UI elements to the panel to interact with by dragging and dropping prefabs from the **Packages** > **Meta XR Interaction SDK Essentials** > **Runtime** > **Sample** > **Objects** > **UISet** > **Prefabs** folder. For example, in the **Buttons** > **UnityUIButtonBased** folder, drag the **PrimaryButton_IconAndLabel_UnityUIButton** prefab to the **UIBackplate** object in the **Hierarchy**. The button element appears on the UI. Add as many elements as you desire to create your UI.

    

## Test your interaction by using Meta Horizon Link

Use [Link](/documentation/unity/unity-link) to test your project.

1. Open the **Link** desktop application on your computer.

1. Put on your headset, and, when prompted, enable Link.

1. On your development machine, in Unity Editor, select the **Play** button.

1. In your headset, you can interact with the UI directly with your hand.

## Test your interaction by generating an APK

Build your project into an .apk to test your project.

1. Make sure your headset is connected to your development machine.

1. In Unity Editor, select **File** > **Build Profiles**.

1. Click **Open Scene List** to open the Scene List window.

1. Add your scene to the **Scene List** by dragging it from the Project panel or by clicking **Add Open Scenes**.

1. Click **Build and Run** to generate an .apk and run it on your headset. In the File Explorer that opens, select a location to save the .apk to and give it a name. The build process may take a few minutes.

1. In your headset, you can interact with the UI directly with your hand.

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