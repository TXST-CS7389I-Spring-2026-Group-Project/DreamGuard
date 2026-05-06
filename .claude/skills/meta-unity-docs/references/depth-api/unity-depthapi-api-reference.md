# Unity Depthapi Api Reference

**Documentation Index:** Learn about unity depthapi api reference in this documentation.

---

---
title: "Depth API shader library reference"
description: "Shader library API reference for environment occlusion and hand removal using the Depth API."
---

## EnvironmentOcclusion shader library API reference
***

### META_DEPTH_VERTEX_OUTPUT(number)
#### Description
 Used in the fragment shader input struct. It is equivalent to: <br> `float3 posWorld : TEXCOORD##number;`
#### Parameters:
  * `number` type: `int`.
   Value applied to TEXCOORD##number

***

### META_DEPTH_INITIALIZE_VERTEX_OUTPUT(output, vertex)
#### Description
Used in the vertex shader. Converts from object space to world space and passes it to the vertex output.
#### Parameters:
  * `output` type: `struct`. Represents the vertex output struct.
  * `input` type: `float3`. Represents the SV_POSITION of the vertex input struct.

***

### META_DEPTH_GET_OCCLUSION_VALUE_WORLDPOS(posWorld, zBias)
#### Description
Used in the fragment shader. Calculates the occlusion value at position ‘posWorld’.
#### Parameters:
  * `posWorld` type: `float3`. Represents the world position value of the fragment.
  * `zBias` type: `float`. Represents amount of environment depth bias applied to the occlusion.

***

### META_DEPTH_GET_OCCLUSION_VALUE(input, zBias)
#### Description
Used in the fragment shader. Calculates the occlusion value where input is assumed to have the world position ‘posWorld’.
#### Parameters:
  * `input` type: `struct`. Represents the input struct of the fragment shader.
  * `zBias` type: `float`. Represents amount of environment depth bias applied to the occlusion.

***

### META_DEPTH_OCCLUDE_OUTPUT_PREMULTIPLY_WORLDPOS(posWorld, output, zBias)
#### Description
Used in the fragment shader. Will automatically modify the output color to account for the occlusion value at position ‘posWorld’.
#### Parameters:
  * `posWorld` type: `float3`. Represents the world position value of the fragment.
  * `output` type: `float4`. Represents the final fragment shader color that will be modified by the occlusion value.
  * `zBias` type: `float`. Represents amount of environment depth bias applied to the occlusion.

***

### META_DEPTH_OCCLUDE_OUTPUT_PREMULTIPLY(input, output, zBias)
#### Description
Used in the fragment shader. Will automatically modify the output color to account for the occlusion value at position ‘posWorld’.
#### Parameters:
  * `input` type: `struct`. Represents the input struct of the fragment shader.
  * `output` type: `float4`. Represents the final fragment shader color that will be modified by the occlusion value.
  * `zBias` type: `float`. Represents amount of environment depth bias applied to the occlusion.

***

## Example usage

```hlsl
Shader "Depth/Unlit"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        // 0. It's important to have One OneMinusSrcAlpha so it blends properly against transparent background (passthrough)
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            // 1. Keywords are used to enable different occlusions
            #pragma multi_compile _ HARD_OCCLUSION SOFT_OCCLUSION

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // 2. Include the file with utility functions
            #include "Packages/com.meta.xr.sdk.core/Shaders/EnvironmentDepth/URP/EnvironmentOcclusionURP.hlsl"

            // if your shaders are in a BiRP project, you would add this include instead:
            //#include "Packages/com.meta.xr.sdk.core/Shaders/EnvironmentDepth/BiRP/EnvironmentOcclusionBiRP.hlsl"

            struct Attributes
            {
                float4 vertex : POSITION;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;

                // 3. This macro adds 'posWorld' to the varyings struct.
                //    The subsequent macros require this field to be named as such.
                //    The number has to be filled with the recent TEXCOORD number + 1
                //    Or 0 as in this case, if there are no other TEXCOORD fields
                META_DEPTH_VERTEX_OUTPUT(0)

                UNITY_VERTEX_INPUT_INSTANCE_ID
                // 4. The fragment shader needs to understand to which eye it's currently
                //    rendering, in order to get depth from the correct texture.
                UNITY_VERTEX_OUTPUT_STEREO
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;

                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);

                output.positionCS = TransformObjectToHClip(input.vertex.xyz);

                // 5. World position is required to calculate the occlusions.
                //    This macro will calculate and set world position value in the output Varyings structure.
                META_DEPTH_INITIALIZE_VERTEX_OUTPUT(output, input.vertex);

                // 6. Passes stereo information to frag shader
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                // 7. Initializes global stereo constant for the frag shader
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                half4 finalColor = _BaseColor;

                // 8. A third macro required to enable occlusions.
                //    It requires previous macros to be there as well as the naming behind the macro is strict.
                //    It will enable soft or hard occlusions depending on the current keyword set.
                //    finalColor value will be multiplied by the occlusion visibility value.
                //    Occlusion visibility value is 0 if virtual object is completely covered by environment and vice versa.
                //    Fully occluded pixels will be discarded
                META_DEPTH_OCCLUDE_OUTPUT_PREMULTIPLY(input, finalColor, 0);

                return finalColor;
            }
            ENDHLSL
        }
    }
}

```