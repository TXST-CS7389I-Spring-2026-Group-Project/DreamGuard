# Unity Depthapi Occlusions Advanced Usage

**Documentation Index:** Learn about unity depthapi occlusions advanced usage in this documentation.

---

---
title: "Occlusions Advanced Usage"
description: "This documentation covers more advanced topics related to implementing occlusions, namely custom occlusion shaders, shadergraph and dealing with z-fighting."
---

This documentation covers more advanced topics related to implementing occlusions, specifically:
* Custom occlusion shaders
* Occlusions using Shadergraph
* Avoiding z-fighting between virtual and real content

## Implementing occlusion in custom shaders

If you have custom shaders, you can convert them to occluded versions by applying some small changes to them.

If your project uses BiRP, use the following include statement:

```hlsl
#include "Packages/com.meta.xr.sdk.core/Shaders/EnvironmentDepth/BiRP/EnvironmentOcclusionBiRP.cginc"
```

For URP:

```hlsl
#include "Packages/com.meta.xr.sdk.core/Shaders/EnvironmentDepth/URP/EnvironmentOcclusionURP.hlsl"
```

**Step 1.** Add occlusion keywords

```hlsl
// DepthAPI Environment Occlusion
#pragma multi_compile _ HARD_OCCLUSION SOFT_OCCLUSION
```

**Step 2**. If the fragment input struct already contains world coordinates, skip this step. Otherwise, use the macro, **_META_DEPTH_VERTEX_OUTPUT_**, to declare the field:

```hlsl
struct v2f
{
   float4 vertex : SV_POSITION;

   float4 someOtherVarying : TEXCOORD0;

   META_DEPTH_VERTEX_OUTPUT(1) // the number should stand for the previous TEXCOORD# + 1

   UNITY_VERTEX_INPUT_INSTANCE_ID
   UNITY_VERTEX_OUTPUT_STEREO // required to support stereo
};
```

**Step 3**. If the struct already contains world coordinates, skip this step. If not, use the macro, **_META_DEPTH_INITIALIZE_VERTEX_OUTPUT_**, like so:

```hlsl
v2f vert (appdata v) {
   v2f o;

   UNITY_SETUP_INSTANCE_ID(v);
   UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); // required to support stereo

   // v.vertex (object space coordinate) might have a different name in your vert shader
   META_DEPTH_INITIALIZE_VERTEX_OUTPUT(o, v.vertex);

   return o;
}
```

**Step 4.** Declare the environment depth bias variable.

```hlsl
float _EnvironmentDepthBias;
```

**Step 5.** Calculate occlusions in fragment shader, with the use of the **_META_DEPTH_OCCLUDE_OUTPUT_PREMULTIPLY _**macro:

```hlsl
half4 frag(v2f i) {
   UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i); required to support stereo

   // this is something your shader will return without occlusions
   half4 fragmentShaderResult = someColor;

   // This will modify the final fragment shader color and add occlusions
   META_DEPTH_OCCLUDE_OUTPUT_PREMULTIPLY(i, fragmentShaderResult, _EnvironmentDepthBias);

   return fragmentShaderResult;
}
```

If you already have a world position varying being passed to your fragment shader, you can use this macro:

**META_DEPTH_OCCLUDE_OUTPUT_PREMULTIPLY_WORLDPOS**(yourWorldPosition, fragmentShaderResult, _EnvironmentDepthBias);

<i>Note: If you only get occlusions working in one eye, make sure that you added the UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i) macro in your fragment shader and the subsequent macros related to stereo rendering as outlined in the above code snippets that have the // required to support stereo comment</i>

For more information, consult the [API reference section](/documentation/unity/unity-depthapi-api-reference)

## Occlusions in Shadergraph

Depth API supports adding occlusions via Shadergraph. A subgraph is provided in the API, called 'OcclusionSubGraph', that exposes occlusion value. This subgraph will output the value 0 if the object is occluded and 1 otherwise.

To use it, multiply the final color and alpha with the output of this subgraph.

## Using Environment depth bias to solve z-fighting in occlusion shaders

Z-fighting issues arise when virtual objects are positioned near real surfaces. The Depth API's shaders feature an _EnvironmentDepthBias property, adjustable at runtime, to modify the environment depth for specific objects.

Reference the material of the object you wish to add this offset to, and alter its value to the desired amount:

```C#
material.SetFloat("_EnvironmentDepthBias", DepthBiasValue);
```

Depth API measurements have an inherent error that scales with distance. The environment depth bias formula is implemented accordingly. This means that the value supplied to _EnvironmentDepthBias represents a virtual depth offset of 1 unit distance away from the camera. The offset is calculated towards the camera; a higher value means the virtual object will be brought closer to the camera. _EnvironmentDepthBias scales linearly with metric distance. Using a value of around 0.06 is recommended, but the value may depend on the type of content that is being placed.

## Using the Depth Mask feature

Starting with v71, a new feature is available that allows developers to cut out meshes from the depth texture. To do this, you can supply a list of mesh filters to the API like so:

```C#
private EnvironmentDepthManager _environmentDepthManager; // Get a reference to the environment depth manager

private void SetDepthMaskMeshFilters(List<MeshFilter> myMeshFilters)
{
   // When MashMeshFilters has any mesh filter in it, the feature will automatically enable itself.
   _environmentDepthManager.MaskMeshFilters = myMeshFilters;

   // Apply an offset by adjusting the Depth Mask bias, if needed
   _environmentDepthManager.MaskBias = _someOffsetFloatValue;
}

private void DisableDepthMasking()
{
   // To disable depth masking, set MaskMeshFilters to null
   _environmentDepthManager.MaskMeshFilters = null;
}
```

An example use case for this feature is to map the walls, with a tool such as MRUK, and then pass those wall mesh filters to the Depth Mask feature. This way, we can "remove the walls" form the depth mask resulting in them no longer contributing to occlusions.