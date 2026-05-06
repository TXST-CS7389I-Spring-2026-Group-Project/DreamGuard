# Unity Isdk Create Pokeable Ui

**Documentation Index:** Learn about unity isdk create pokeable ui in this documentation.

---

---
title: "Creating Pokeable UIs"
description: "Utility to automate setting up a UI to be pokeable."
last_updated: "2025-11-03"
---

## Set up your UI

1. In the **Hierarchy** panel, add a Canvas by right-clicking and selecting **UI** > **Canvas**. The canvas appears in the **Hierarchy**.

1. In the **Hierarchy**, select the **Canvas**. In the **Inspector**, under **Rect Transform**, set the following settings to position the Canvas where it can be easily interacted with and scales it to a manageable size:

    - **Canvas** > **Render Mode**: *World Space*
    - **Rect Transform** > **PosX**: *0*
    - **Rect Transform** > **PosY**: *1.15*
    - **Rect Transform** > **PosZ**: *1.5*
    - **Rect Transform** > **Width**: *480*
    - **Rect Transform** > **Height**: *720*
    - **Rect Transform** > **Scale**: *[0.0005, 0.0005, 0.0005]*

1. Add a panel to the Canvas by right-clicking on the **Canvas** in the **Hierarchy** and selecting **UI** > **Panel**. The panel appears in the **Hierarchy**.

1. Add some UI elements to the panel to interact with. For example, right-click on the **Panel** in the **Hierarchy** and select **UI** > **Button - TextMeshPro**. The button element appears on the **Panel**. Add as many elements as you desire to create your UI.

    

## Add the Poke Interaction

1. Right-click on the **Canvas**, and select **Interaction SDK** > **Add Poke Interaction to Canvas**. The Poke wizard appears.

    

3. In the Poke Canvas wizard, you may see a message about a missing PointableCanvasModule. Select **Fix**. This will add the missing PointableCanvasModule.

    

4. If you want to further customize the interaction, adjust the interaction's settings in the wizard. For details on the available options, please see the [Poke Canvas Quick Action](/documentation/unity/unity-isdk-poke-quick-action) documentation.

5. Select **Create**. The wizard automatically adds the required components for the interaction to the Canvas. It also adds components to the camera rig if those components weren't already there.

    

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

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.

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