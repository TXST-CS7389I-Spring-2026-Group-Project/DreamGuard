# Unity Mrmotifs Passthrough Transitioning

**Documentation Index:** Learn about unity mrmotifs passthrough transitioning in this documentation.

---

---
title: "Passthrough Transitioning Motif"
description: "Transition between fully immersive VR and passthrough mixed reality experiences using the Passthrough API."
last_updated: "2025-09-04"
---

<box display="flex" flex-direction="column" align-items="center">
  <a href="https://www.youtube.com/watch?v=C9PFg-XfQcA">
    <img alt="Passthrough Transitioning Thumbnail" src="/images/unity-mrmotifs-1-thumbnail.png" border-radius="12px" width="100%" />
  </a>
</box>

<box padding="16px">
  <p>
    The <b>Passthrough Transitioning motif</b> demonstrates the transition from fully immersive VR experiences to passthrough mixed reality experiences using the <a href="/documentation/unity/unity-passthrough">Passthrough API</a>. In this sample project, you can adjust the visibility of your surroundings by manipulating a slider, which regulates the level of passthrough. Alternatively, you can directly switch from one mode to another with the press of a button.
  </p>
  <p>
    The project also shows you how to use the <a href="/documentation/unity/unity-boundaryless">Boundary API</a> to disable the guardian while in passthrough mode for a seamless MR experience.
  </p>
</box>

<h2>Requirements</h2>

<ul>
  <li><a href="https://github.com/oculus-samples/Unity-MRMotifs">Unity-MRMotifs samples on GitHub</a></li>
  <li><a href="https://unity.com/releases/editor/whats-new/6000.0.25">Unity 6</a> or later</li>
  <li>URP (Recommended) or BiRP</li>
  <li>OpenXR Plugin (<code>1.15.0</code>) - <code>com.unity.xr.openxr</code></li>
  <li><a href="https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169">Meta XR Core SDK</a> (<code>v74 or later</code>) - <code>com.meta.xr.sdk.core</code></li>
  <li><a href="https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-265014">Meta XR Interaction SDK</a> (<code>v74 or later</code>) - <code>com.meta.xr.sdk.interaction.ovr</code></li>
  <li><a href="https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-essentials-264559">Meta XR Interaction SDK Essentials</a> (<code>v74 or later</code>) - <code>com.meta.xr.sdk.interaction</code></li>
</ul>

<h2>Project Setup</h2>

<ol>
  <li>Import the Passthrough Transitioning motif assets, including scripts, shaders, prefabs, and materials.</li>
  <li>With the XR Camera Rig selected in the <b>Hierarchy</b>, go to the <b>Inspector</b>, and under <b>OVR Manager → Quest Features</b>, set <b>Passthrough Support</b> to <b>Supported</b>, and under <b>OVR Manager → Insight Passthrough & Guardian Boundary</b>, check <b>Enable Passthrough</b>.</li>
  <li>Add an <b>OVR Passthrough Layer</b> component, and set its <b>Compositing &gt; Placement</b> to <b>Underlay</b>.</li>
  <li>Add the <b>Passthrough Fader</b> prefab to the <b>CenterEyeAnchor</b>.</li>
  <li>Reference the <b>OVR Passthrough Layer</b> on the <b>Passthrough Fader</b> component.</li>
  <li>Set up a way to call the public <b>TogglePassthrough</b> method of the <b>PassthroughFader</b> class, with either the <b>Controller Buttons Mapper</b> Building Block or the <b>Menu Panel</b> prefab.</li>
</ol>

<p>To transition between VR and passthrough, place a sphere as a child of the main camera. Use a shader, configured at runtime with a custom fader script, to manipulate the sphere. This enables adjusting fade speed, direction, distance, and effects like dissolving the sphere in a random pattern (Perlin noise). All you need to set this up in your project is the custom <b>PassthroughFader</b> shader from this project and the <b>PassthroughFader</b> class or a similar class to manipulate the shader's properties.</p>

<h2>Contextual Passthrough</h2>

<p>
  Contextual passthrough means that passthrough should be enabled based on <a href="/documentation/unity/unity-passthrough-gs#enable-based-on-system-recommendation">system recommendations</a>. In other words, if the user is in passthrough mode in their home environment, the operating system can detect this and display the system splash screen and the Unity scene in passthrough. In VR mode, the system splash screen will be shown with a black background like before.
</p>

<p>
  If you have enabled contextual passthrough and still cannot see the effect being applied, try updating your <code>AndroidManifest.xml</code> by navigating to <b>Meta</b> → <b>Tools</b> → <b>Update Android Manifest file</b> in the Unity Editor.
</p>

<box display="flex" flex-direction="row" padding-vertical="16" margin-bottom="16">
  <box padding-end="12" width="50%">
    <text align-items="center"><b>Splash Screen (Black)</b></text>
    <img alt="Splash Screen (Black)" src="/images/unity-mrmotifs-1-SplashScreenBlack.gif" border-radius="12px" width="100%" />
  </box>
  <box padding-start="12" width="50%">
    <text align-items="center"><b>Splash Screen (Passthrough Contextual)</b></text>
    <img alt="Splash Screen (Passthrough Contextual)" src="/images/unity-mrmotifs-1-SplashScreenPassthrough.gif" border-radius="12px" width="100%" />
  </box>
</box>

<oc-devui-note type="note">
  Contextual PT for the system splash screen can only be enabled with Unity 6 or a Unity Pro license.
</oc-devui-note>

<h2>Conditional Passthrough</h2>

<p>
  Passthrough can also be used to switch between MR and VR game modes or to conditionally show parts of the environment, such as when opening menus or changing scenes. This allows players to feel more comfortable and immersed, leading to longer play sessions.
</p>

<p>
  Keep in mind that enabling passthrough is asynchronous. System resources like cameras can take a few hundred milliseconds to be activated, during which time passthrough is not yet rendered and experienced as black flicker. You can avoid this by using the <a href="/documentation/unity/unity-passthrough/#wait-until-passthrough-is-ready">passthroughLayerResumed</a> event, which is emitted once the layer is fully initialized and passthrough is visible. Additionally, you don’t just want to immediately go from passthrough to VR but rather use a shader to smoothly transition between the two.
</p>

<h2>Passthrough Transitioning Sample Scenes</h2>

<p>
  Both scenes come with a <b><code>Passthrough Fader</code></b> prefab, which is located on the <b><code>CenterEyeAnchor</code></b>. It contains the <b><code>PassthroughFader</code></b> class. The prefab also contains an audio source used to play audio clips whenever you fade in or out.
</p>

<box display="flex" flex-direction="row" padding-vertical="16" margin-bottom="16">
  <box padding-end="12" width="50%">
    <text align-items="center"><b>PassthroughFader Underlay</b></text>
    <img alt="PassthroughFader Underlay" src="/images/unity-mrmotifs-1-PassthroughFaderUnderlay.gif" border-radius="12px" width="100%" />
  </box>
  <box padding-start="12" width="50%">
    <text align-items="center"><b>PassthroughFader Selective</b></text>
    <img alt="PassthroughFader Selective" src="/images/unity-mrmotifs-1-PassthroughFaderSelective.gif" border-radius="12px" width="100%" />
  </box>
</box>

<p>
  The passthrough fader slider scene comes with a <b><code>PassthroughFaderSlider</code></b> prefab, which is located on the <b><code>CenterEyeAnchor</code></b>. It contains the <b><code>PassthroughFaderSlider</code></b> component. The passthrough dissolver scene comes with a <b><code>PassthroughDissolver</code></b> prefab, which is located <i>outside</i> the <b><code>CenterEyeAnchor</code></b>, so that the dissolution pattern does not move with your head but instead stays anchored in the scene. It contains the <b><code>PassthroughDissolver</code></b> class.
</p>

<box display="flex" flex-direction="row" padding-vertical="16" margin-bottom="16">
  <box padding-end="12" width="50%">
    <text align-items="center"><b>PassthroughFaderSlider</b></text>
    <img alt="PassthroughFaderSlider" src="/images/unity-mrmotifs-1-PassthroughFaderSlider.gif" border-radius="12px" width="100%" />
  </box>
  <box padding-start="12" width="50%">
    <text align-items="center"><b>PassthroughFaderDissolve</b></text>
    <img alt="PassthroughFaderDissolve" src="/images/unity-mrmotifs-1-PassthroughFaderDissolveSG.gif" border-radius="12px" width="100%" />
  </box>
</box>

<p>
  The <b><code>PassthroughDissolver</code></b> shader is applied to the sphere, and therefore, you need the <b><code>PerlinNoiseTexture</code></b> script to generate a texture, which you can easily modify by changing the values in the inspector. If you were to use the <b><code>PassthroughDissolverSG</code></b> ShaderGraph shader, you can simply remove the <b><code>PerlinNoiseTexture</code></b> component since the texture is already generated within the ShaderGraph. Everything else works the same.
</p>

<h2>Passthrough Fading Scripts</h2>

<p>
  The <b><code>PassthroughFader</code></b> class handles smooth transitions between VR and Passthrough. It includes logic to:
</p>
<ul>
  <li>Check if <a href="/documentation/unity/unity-passthrough/#enable-based-on-system-recommendation">Passthrough is recommended</a></li>
  <li><a href="/documentation/unity/unity-passthrough/#wait-until-passthrough-is-ready">Wait until Passthrough is ready</a></li>
  <li>Toggle VR/Passthrough via the <code>PassthroughToggle</code> method</li>
</ul>

<p>
  This script supports both <b>Underlay</b> and <b>Selective</b> modes, configurable via the Inspector.
</p>

<box display="flex" flex-direction="row" padding-vertical="16" margin-bottom="16">
  <box padding-end="12" width="50%">
    <text align-items="center"><b>PassthroughFader Underlay</b></text>
    <img alt="PassthroughFader Underlay Script" src="/images/unity-mrmotifs-1-PassthroughFaderUnderlayScript.png" border-radius="12px" width="100%" />
  </box>
  <box padding-start="12" width="50%">
    <text align-items="center"><b>PassthroughFader Selective</b></text>
    <img alt="PassthroughFader Selective Script" src="/images/unity-mrmotifs-1-PassthroughFaderSelectiveScript.png" border-radius="12px" width="100%" />
  </box>
</box>

<h3>Key Properties</h3>
<ul>
  <li><b>Fade Speed</b> & <b>Fade Direction</b> – for both modes</li>
  <li><b>Selective Distance</b> – limits visible content in selective mode (e.g., tabletop games)</li>
  <li><b>Unity Events</b> – triggers for fade in/out start & complete (e.g., audio cue triggers)</li>
</ul>

<h3>Other Scripts</h3>
<ul>
  <li><b><code>PassthroughFaderSlider</code></b> – enables manual fading via UI slider + optional guardian toggle</li>
  <li><b><code>PassthroughDissolver</code></b> – uses dissolve level instead of inverted alpha</li>
  <li><b><code>AudioController</code></b> – adjusts audio volume based on dissolve state</li>
  <li><b><code>PerlinNoiseTexture</code></b> – generates procedural texture for the <b>PassthroughDissolver</b> shader. <a href="https://docs.unity3d.com/ScriptReference/Mathf.PerlinNoise.html">Learn more</a>.</li>
</ul>

<h2>Passthrough Transitioning Shaders</h2>

<p>
  Passthrough Transitioning utilizes a custom HLSL shader to smoothly fade between VR and passthrough modes. The main shader used by both the <b>Passthrough Fader</b> and <b>Slider</b> is called <b><code>PassthroughFader</code></b>.
</p>

<h3>Shader Highlights</h3>
<ul>
  <li><b><code>_InvertedAlpha</code></b>: Inverts alpha transparency to produce fade effects</li>
  <li><b><code>_FadeDirection</code></b>: Controls direction of fade using UV coordinates
    <ul>
      <li>0 – red channel</li>
      <li>1 – right to left</li>
      <li>2 – top to bottom</li>
      <li>3 – center outwards</li>
    </ul>
  </li>
  <li>Smooth transitions via <code>smoothstep</code></li>
</ul>

<oc-devui-note type="note">
  On the sphere using the shader, set <b><code>Cull Off</code></b> and <b>render queue to <code>Transparent-1 (2999)</code></b> to avoid z-fighting and ensure proper depth layering behind other materials.
</oc-devui-note>

<h3>Dissolve Effects</h3>
<p>
  To create stylish dissolves between VR and passthrough, the <b><code>PassthroughFaderDissolve</code></b> scene is included. It uses the <b><code>PassthroughDissolver</code></b> class to manipulate shader parameters for a patterned transition.
</p>

<ul>
  <li><b><code>PassthroughDissolver</code></b>: Uses external <b><code>PerlinNoiseTexture</code></b> script to generate dissolve patterns</li>
  <li><b><code>PassthroughDissolverSG</code></b>: ShaderGraph variant with built-in noise pattern, no extra script required</li>
</ul>

<h2>Resources</h2>

<ul>
  <li><a href="https://github.com/oculus-samples/Unity-MRMotifs">Unity-MRMotifs samples on GitHub</a></li>
  <li><a href="https://www.youtube.com/watch?v=C9PFg-XfQcA">Passthrough Transitioning YouTube Tutorial</a></li>
  <li><a href="/blog/mixed-reality-motifs-passthrough-transitioning-meta-quest-horizon">Passthrough Transitioning Blog Post</a></li>
  <li><a href="/documentation/unity/unity-passthrough">Passthrough API Overview</a></li>
  <li><a href="/documentation/unity/unity-passthrough-gs">Get Started with Passthrough</a></li>
  <li><a href="/documentation/unity/unity-boundaryless">Boundaryless</a></li>
</ul>