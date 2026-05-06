# Unity Isdk Uiset

**Documentation Index:** Learn about unity isdk uiset in this documentation.

---

---
title: "UI Set"
description: "Build customizable, interaction-ready VR user interfaces using pre-built UI Set components in Interaction SDK."
last_updated: "2025-03-18"
---

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
      <video-source handle="GE1jNxw-p_cZRTsCABzsdBtuYbg1bosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      <b>Video</b>: Interactions with several Meta Horizon OS UI Set components.
  </text>
</box>

This page provides guidance on using the Meta Horizon OS UI Set available in Meta XR Interaction SDK Unity.

For design guides and resources, please refer to <a href="/design/buttons" target="_blank">Components documentation</a> and <a href="https://www.figma.com/community/file/1509641173090552632/meta-horizon-os-ui-set" target="_blank">UI Set Figma design file available on Meta's Figma page</a>. To try out the UI Set on your device, you can download the <a href="https://www.meta.com/experiences/interaction-sdk-samples/5605166159514983/" target="_blank">Interaction SDK Samples app</a> and select UI Set from the main menu located on the left side of the scene.

## What is the UI Set?

The Meta Horizon OS UI Set is a collection of essential user interface components designed for immersive experiences. It is available for download in both Meta XR Interaction SDK Unity and Figma, allowing for streamlined design and development processes.

### Build high-quality UIs with ease

By utilizing the UI Set, you can efficiently create widely used user interface patterns, ensuring a consistent and cohesive interaction experience across immersive apps. This enables designers and developers to concentrate on crafting compelling content, without worrying about integrating proper input modalities or visual feedback for various interaction states.

### Achieve consistent input and interaction support

The UI Set supports various input modalities, accommodating both direct and indirect interactions. Empowering users to interact with your UI in their preferred method, whether that's directly touching the UI or indirectly interacting with hands or controllers.

### Customize with your own brand identity

The UI Set includes built-in theming capabilities, making it easy to integrate your brand identity while maintaining consistent input modality and interaction support.

## Where to find UI Set

UI Set is available in Meta XR Interaction SDK Essentials package. When you install [Meta XR Interaction SDK package](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-265014) it automatically installs Essentials package together. UI Set folder is located at **Packages > Meta XR Interaction SDK Essentials > Runtime > Sample > Objects > UISet**

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Set example scenes"
      src="/images/isdk-uiset-folder.png"
      border-radius="12px"/>
</box>

## Start with example scenes

Interaction SDK provides these example scenes to help you jumpstart your UI projects.

### UISet

An example scene that shows the entire UI library.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Set component list"
      src="/images/isdk-uiset-examples-library.png"
      border-radius="12px"/>
</box>

### UISetPatterns

Common UI pattern examples built with UI set components.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Set pattern examples"
      src="/images/isdk-uiset-examples-patterns.png"
      border-radius="12px"/>
</box>

Due to Unity's restrictions, example scenes located in the Packages folder cannot be opened directly. To access and modify these scenes, simply drag and drop them into the Assets folder. This will enable you to open and edit the scenes as needed.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Set example scenes"
      src="/images/isdk-uiset-examplescenes.png"
      border-radius="12px"/>
</box>

### UISetExamples

An example scene that can be experienced in the headset.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Set example scene for device experience"
      src="/images/isdk-uiset-examples-runtime.png"
      border-radius="12px"/>
</box>

This scene can be imported through Package Manager.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Set example scenes import"
      src="/images/isdk-uiset-examplescenes-import.png"
      border-radius="12px"/>
</box>

## List of components

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Set component list"
      src="/images/isdk-uiset-component-list.png"
      border-radius="12px"/>
</box>

- **Buttons**: Primary, Secondary, Borderless, Destructive, Tile, Shelf
- **Sliders**: Small, Medium, Large
- **Controls**: Toggle switch, Checkbox, Radio
- **Dropdown**: Single line, double line, Icon and text
- **Tooltip**
- **Text input field, Search bar**
- **Dialogs**: Single button, double buttons, With image/video

## Unity canvas UI

UI Set components are built on Unity UI which uses Canvas. By using Canvas and RectTransform, you can leverage rich layout features - such as Grid, Horizontal, Vertical Layout Group, reflow, anchoring, and margin/padding support - that are crucial for common UI layout scenarios and patterns.

To support various input modalities of Meta Horizon OS, the root container object includes Interaction SDK components such as **PokeInteractable**, **RayInteractable**, and **PointableCanvas**.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Unity canvas UI"
      src="/images/isdk-uiset-canvasui.png"
      border-radius="12px"/>
</box>

## Structure of the UI components

### UI Backplate

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Backplate"
      src="/images/isdk-uiset-cover-backplate.png"
      border-radius="12px"/>
</box>

To secure legibility of UI and content, UI components should always be displayed on top of a backplate. The UI backplate is using **RoundedBoxUIProperties.cs** with Unity Image component to achieve rounded corners. Additionally, there is another Image object with a material called **MultiGradientUI.mat** which provides a subtle gradient effect on the backplate, mixed with the backplate color.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Backplate Structure"
      src="/images/isdk-uiset-backplate1.png"
      border-radius="12px"/>
</box>

You can use **EmptyUIBackplateWithCanvas** prefab to get started with UI layout. It contains all necessary components such as Unity canvas, Backplate, PokeInteractable for building UI.

### Buttons

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Button examples"
      src="/images/isdk-uiset-cover-buttons.png"
      border-radius="12px"/>
</box>

Buttons are made with Unity UI's **Toggle** component to support both regular and toggle button functionality. **Animator** is used to provide visual feedback such as scaling on press and release and color changes. From v74 release, Button prefabs made with Unity UI **Button** component have been added.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Buttons with Animator"
      src="/images/isdk-uiset-button-animator.gif"
      border-radius="12px"/>
    <text display="block" color="secondary">
      Color and scale changes with Animator
    </text>
</box>

Animator for toggle buttons have additional layer **Selected Layer** which shows different color for toggle state while performing same scaling animation for pressed state. **AimatorOverrideLayerWeigth.cs** scipt handles the layer switching between Base Layer and Selected Layer.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Animator Layer"
      src="/images/isdk-uiset-button-animator-layer.png"
      border-radius="12px"/>
    <text display="block" color="secondary">
      Animator Layers and AimatorOverrideLayerWeigth script
    </text>
</box>

You can customize the color or scale of a button's interaction states by modifying the animation clips. Alternatively, use the **Theme Manager** to efficiently modify colors across multiple UI components. We'll explore Theme Manager in more detail in the next section.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Modifying colors"
      src="/images/isdk-uiset-button-animator-modify.png"
      border-radius="12px"/>
    <text display="block" color="secondary">
      Under Animation panel, you can select an animation clip for speific interaction state and modify color or scale
    </text>
</box>

If you want to make an image button, you can assign image sprite texture onto the Content > Background > Image's 'Source Image' field. With default animator **ToggleButton_Dark**, the image would look too dark.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Making image buttons - Before"
      src="/images/isdk_uiset_imgbutton_before.gif"
      border-radius="12px"/>
    <text display="block" color="secondary">
      Image buttons with default animator
    </text>
</box>

You can duplicate the animator and customize the color for interaction states. In this example, assigned a duplicated animator **ImageButton** and increased the r, g, b values. To provide different visual feedback for hover and pressed state, still differentiated values a little bit. Below are before (with default ToggleButton_Dark animator) and after (with new ImageButton animator)

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Making image buttons - After"
      src="/images/isdk_uiset_imgbutton_after.gif"
      border-radius="12px"/>
    <text display="block" color="secondary">
      Image buttons with modified animator
    </text>
</box>

### Dropdown

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Dropdown example"
      src="/images/isdk-uiset-cover-dropdown.png"
      border-radius="12px"/>
</box>

The Dropdown component is comprised of two sub-components - **DropDownListButton** and **DropDownList**. **DropDownListButton** works as a toggle button to show and hide the **DropDownList** which is the collection of the buttons. When **DropDownListButton** is pressed, it runs **CanvasGroupAlphaToggle.cs** script's ToggleVisible() under **DropDownList** to toggle visiblity. It modifies the Canvas Group's alpha value to show or hide the child button components.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Dropdown List visibility"
      src="/images/isdk-uiset-dropdown-visibility.gif"
      border-radius="12px"/>
</box>

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Dropdown structure"
      src="/images/isdk-uiset-dropdown-structure.png"
      border-radius="12px"/>
</box>

### Controls

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Controls examples"
      src="/images/isdk-uiset-cover-controls.png"
      border-radius="12px"/>
</box>

Toggle Switch, Checkbox, and Radio buttons share a common structure with Toggle buttons, but with distinct visual representations. Notably, Radio buttons are designed for exclusive selection within a group, and are therefore often used in conjunction with Unity UI's **Toggle Group** component to manage group behavior.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Radio button group"
      src="/images/isdk-uiset-radio-group.gif"
      border-radius="12px"/>
</box>

### Sliders

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Sliders examples"
      src="/images/isdk-uiset-cover-sliders.png"
      border-radius="12px"/>
</box>

Three Slider components are available, offering small, medium, and large variants to accommodate different design requirements. Optional components such as text labels and icons can be displayed to suit your specific needs.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Sliders examples and optional components"
      src="/images/isdk-uiset-slider-components.png"
      border-radius="12px"/>
</box>

### Text input field, Search bar

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Text input field"
      src="/images/isdk-uiset-cover-textinputfield.png"
      border-radius="12px"/>
</box>
Both the Text Input Field and Search Bar components offer the same basic input field functionality, built upon Unity UI's Text Mesh Pro Input Field. The focus visual elements are dynamically toggled based on the focus state, providing a clear visual indicator of the current interaction.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Keyboard input with text input field"
      src="/images/isdk-uiset-input-field-focus.gif"
      border-radius="12px"/>
</box>

To use Text input field with system keyboard, check **Requires System Keyboard** under **OVRManager**. See [System Keyboard Overlay](/documentation/unity/unity-keyboard-overlay) for more details.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Keyboard input with text input field"
      src="/images/isdk-uiset-input-field.gif"
      border-radius="12px"/>
</box>

### Dialogs

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Dialog examples"
      src="/images/isdk-uiset-cover-dialogs.png"
      border-radius="12px"/>
</box>

Three variants with different button configurations are provided. Dialogs are constructed with backplate, buttons, and images.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Dialog examples with optional configurations"
      src="/images/isdk-uiset-dialog-structure.png"
      border-radius="12px"/>
</box>

## Theme Manager

**Theme Manager** allows you to easily customize the visual design and bring your own brand identity into the UI components through a customizable color palette and font while preserving the core functionality and input interaction quality.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="UI Backplate"
      src="/images/isdk-uiset-theme-manager-overview.gif"
      border-radius="12px"/>
</box>

When you add **ThemeManager.cs** to an object, it searches child objects and reassigns following elements based on Tags:

- Image color
- TextMeshPro font, text color
- Animatior Controllers

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Theme Manager script"
      src="/images/isdk-uiset-theme-tags.png"
      border-radius="12px"/>
    <text display="block" color="secondary">
      List of Tags used in UI Set components to identify various elements
    </text>
</box>

You can find an example of using **ThemeManager** in **UISet.unity** example scene. It contains **ThemeManager.cs** script in **ContentRoot** object.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Theme Manager script"
      src="/images/isdk-uiset-thememanager.png"
      border-radius="12px"/>
</box>

## Theme

The **Theme** scriptable object enables you to define style elements, including shared elements like backplate color and text colors, as well as detailed colors for various components and their interaction states. You can think of it as a style sheet. As previously mentioned, since Animators control the color and scale of UI components, they need to be updated to reflect the new theme styles. Conveniently, the Theme inspector features a **Save Theme Updates** button at the bottom, which updates the Animators accordingly.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Theme assets"
      src="/images/isdk-uiset-theme-assets.png"
      border-radius="12px"/>
</box>

You can see the examples of corresponding color palette in Meta Horizon OS Figma file.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Theme file"
      src="/images/isdk-uiset-theme-colorpalette.png"
      border-radius="12px"/>
</box>

The **Color Path** field describes the hierarchy of the UI object where the color can be updated.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Theme file"
      src="/images/isdk-uiset-theme-colorpath.png"
      border-radius="12px"/>
</box>

## How to build a custom theme

<box display="flex" flex-direction="row" padding-vertical="16">
  <box display="flex" padding-vertical="12" padding-end="20" min-width="400px" max-width="400px">
      <img src="/images/isdk-uiset-theme-customization_01.png" alt="Selecting existing theme file and animations folder" border-radius="12px"/>
   </box>
   <box display="block" flex-wrap="wrap" justify-content="flex-start" align-self="center">
      <text type="body2-emphasized">
         1. Create a new theme
      </text>
      <br/>
      <text>
         Duplicate one of the existing theme files and animations folder by selecting them and CTRL+D.
      </text>
</box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box display="flex" padding-vertical="12" padding-end="20" min-width="400px" max-width="400px">
      <img src="/images/isdk-uiset-theme-customization_02.png" alt="Renamed themes and assigned animation controllers" border-radius="12px"/>
   </box>
   <box display="block" flex-wrap="wrap" justify-content="flex-start" align-self="center">
      <text type="body2-emphasized">
         2. Assign new Animator Controllers
      </text>
      <br/>
      <text>
         In this example, renamed them with suffix _MyTheme to make it easier to distinguish.
      </text>
 </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box display="flex" padding-vertical="12" padding-end="20" min-width="400px" max-width="400px">
      <img src="/images/isdk-uiset-theme-customization_03.png" alt="Assigning custom font" border-radius="12px"/>
   </box>
   <box display="block" flex-wrap="wrap" justify-content="flex-start" align-self="center">
      <text type="body2-emphasized">
         3. Assign custom font (optional, Unity’s Liberation Sans assigned by default)
      </text>
      <br/>
      <text>
         Drag & drop new font file into Unity. Right click font file and Create > Text Mesh Pro > Font Asset. Assign SDF files onto the theme, under Fonts.
      </text>
 </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box display="flex" padding-vertical="12" padding-end="20" min-width="400px" max-width="400px">
   <img src="/images/isdk-uiset-theme-customization_04.png" alt="Update colors" border-radius="12px"/>
   </box>
   <box display="block" flex-wrap="wrap" justify-content="flex-start" align-self="center">
      <text type="body2-emphasized">
         4. Update colors
      </text>
      <br/>
      <text>
         Adjust colors for various components and interaction states.
      </text>
 </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box display="flex" padding-vertical="12" padding-end="20" min-width="400px" max-width="400px">
      <img src="/images/isdk-uiset-theme-customization_05.png" alt="Save theme updates button" border-radius="12px"/>
   </box>
   <box display="block" flex-wrap="wrap" justify-content="flex-start" align-self="center">
      <text type="body2-emphasized">
         5. Save theme
      </text>
      <br/>
      <text>
         Click ‘Save Theme Updates’ button. This updates color schemes in all animation clips.
      </text>
 </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box display="flex" padding-vertical="12" padding-end="20" min-width="400px" max-width="400px">
      <img src="/images/isdk-uiset-theme-customization_06.png" alt="Assigning theme in UIThemeManager" border-radius="12px"/>
   </box>
   <box display="block" flex-wrap="wrap" justify-content="flex-start" align-self="center">
      <text type="body2-emphasized">
         6. Assign your theme in UIThemeManager
      </text>
      <br/>
      <text>
         In your scene, add UIThemeManager script to the root of your content that includes all UI elements. Theme will be applied to all UI components under this object.
         Add your theme.
         UISet scene is recommended to test out new theme since it allows you easily see the changes in all UI library
      </text>
 </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box display="flex" padding-vertical="12" padding-end="20" min-width="400px" max-width="400px">
      <img src="/images/isdk-uiset-theme-customization_07.png" alt="applying theme" border-radius="12px"/>
   </box>
   <box display="block" flex-wrap="wrap" justify-content="flex-start" align-self="center">
      <text type="body2-emphasized">
         7. Apply the theme
      </text>
      <br/>
      <text>
         Use dropdown to select your theme.
      </text>
 </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box display="flex" padding-vertical="12" padding-end="20" min-width="400px" max-width="400px">
      <img src="/images/isdk-uiset-theme-customization_08.png" alt="Applying theme on runtime" border-radius="12px"/>
   </box>
   <box display="block" flex-wrap="wrap" justify-content="flex-start" align-self="center">
      <text type="body2-emphasized">
         8. Apply the theme on runtime
      </text>
      <br/>
      <text>
         Call UIThemeManager’s ApplyTheme(int index) function from your button or script to apply a theme.
      </text>
 </box>
</box>

## Unity UI and canvas performance considerations

According to Unity's official documentation, Canvas regeneration and draw calls to the GPU occur when UI Elements change. To optimize performance, Unity recommends:

- Splitting up canvases to create isolated "islands" for each canvas, separating elements from other canvases.
- Nesting canvases, as child canvases maintain their own geometry and batching, isolating content from parent and sibling canvases.

By implementing these strategies, you can improve UI performance and reduce unnecessary draw calls.

To learn more about Canvas optimization, please visit [Optimization tips for Unity UI](https://unity.com/how-to/unity-ui-optimization-tips)

## Related topics

- To learn about general Unity UI integration with Interaction SDK, see [Create UI](/documentation/unity/unity-isdk-create-ui).
- To learn how to interact with a UI via a raycast, see either [Add an Interaction with Quick Actions](/documentation/unity/unity-isdk-quick-actions/) (for Interaction SDK v62+), or [Use a Ray Interaction with UI](/documentation/unity/unity-isdk-use-ray-with-ui/) (for legacy versions).
- To learn about the components of a **Poke** interaction, see [Poke Interactions](/documentation/unity/unity-isdk-poke-interaction/).
- For an interactive example of raycasting with multiple UI prefabs, see the [RayExamples](/documentation/unity/unity-isdk-example-scenes/#rayexamples) scene.
- To learn about the different types of surfaces in Interaction SDK, see [Surfaces](/documentation/unity/unity-isdk-surfaces/).
- To learn about components that establish the interactable surface area of UI elements, see [Surface Patch](/documentation/unity/unity-isdk-test/).