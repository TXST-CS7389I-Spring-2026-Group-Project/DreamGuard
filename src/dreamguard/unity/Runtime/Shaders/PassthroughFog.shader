Shader "DreamGuard/PassthroughFog"
{
    // Renders a radial fog boundary that gradually reveals Meta Quest passthrough.
    //
    // Meta Quest compositor model:
    //   framebuffer alpha == 1  →  show VR
    //   framebuffer alpha == 0  →  show passthrough (underlay)
    //   framebuffer alpha in (0,1) → linear blend of VR + passthrough
    //
    // Pass 0 – FogColor:       paints semi-transparent haze in the transition band.
    // Pass 1 – PassthroughCut: writes vrAmount directly into the alpha channel
    //   (ColorMask A, ZTest Always).  Inner zone → vrAmount=1 (VR),
    //   outer zone → vrAmount=0 (passthrough), transition → smooth gradient.
    //
    // Distance model
    // ──────────────
    // Both passes sample the URP scene depth buffer (_CameraDepthTexture) at the
    // screen position of each dome fragment to find the depth of VR geometry
    // rendered before the dome.  They then reconstruct the world-space position
    // and compute xzDist = length(reconstructed.xz - _PlayerPos.xz).
    //
    //   xzDist < _InnerRadius              → full VR     (vrAmount = 1)
    //   xzDist > _InnerRadius+_FogBandWidth → passthrough (vrAmount = 0)
    //   in between                          → smooth gradient + fog haze
    //
    // Sky / empty pixels (no VR geometry) hit the far plane.  rawDepth is
    // converted to linear [0=near, 1=far] via Linear01Depth/_ZBufferParams,
    // which handles both reversed-Z (Quest/Vulkan) and standard-Z automatically.
    // Far-plane pixels (linearDepth > 0.9999) are treated as xzDist=0 (inner VR
    // zone) so the VR skybox and ceiling remain visible rather than becoming passthrough.
    //
    // Prerequisites (already configured):
    //   • OVRPassthroughLayer (Underlay) on this GameObject.
    //   • Cameras clear to SolidColor, backgroundColor.a = 0 while fog is active.
    //   • m_AllowPostProcessAlphaOutput: 1 in UniversalRP.asset so URP preserves
    //     alpha through to the XR swapchain.

    Properties
    {
        _PlayerPos    ("Player World Position (xyz)", Vector)       = (0, 0, 0, 0)
        _InnerRadius  ("Inner Radius (full VR)",      Float)        = 3.0
        _FogBandWidth ("Fog Band Width",              Float)        = 2.0
        _FogColor     ("Fog Color",                   Color)        = (0.05, 0.05, 0.08, 1.0)
        _FogMaxAlpha  ("Fog Max Alpha",               Range(0, 1)) = 0.92
    }

    SubShader
    {
        Tags
        {
            "RenderType"     = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            // Render after all opaque geometry so _CameraDepthTexture is fully
            // populated with VR scene depth before we sample it.
            "Queue"          = "Overlay+10"
        }

        // ── Pass 0: Fog colour haze ───────────────────────────────────────────
        Pass
        {
            Name "FogColor"
            Cull Front
            ZWrite Off
            ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _PlayerPos;
                float  _InnerRadius;
                float  _FogBandWidth;
                float4 _FogColor;
                float  _FogMaxAlpha;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                // ComputeScreenPos handles stereo eye layout and Vulkan y-flip correctly.
                // positionCS.xy / _ScreenParams.xy does not — it breaks depth UV in
                // single-pass stereo instancing and reversed-Z on Quest/Vulkan.
                float4 screenPos  : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.positionCS = GetVertexPositionInputs(IN.positionOS.xyz).positionCS;
                OUT.screenPos  = ComputeScreenPos(OUT.positionCS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
                float  rawDepth = SampleSceneDepth(screenUV);

                // Convert raw device depth to linear [0=near, 1=far] using _ZBufferParams.
                // This handles both reversed-Z (Quest/Vulkan: near=1, far=0) and standard-Z.
                float linearDepth = Linear01Depth(rawDepth, _ZBufferParams);

                // Sky / empty VR space → far plane (linearDepth ≈ 1).
                // Treat as inner zone so the VR sky/ceiling stays visible.
                float xzDist;
                if (linearDepth > 0.9999)
                {
                    xzDist = 0.0;
                }
                else
                {
                    float3 sceneWS = ComputeWorldSpacePosition(screenUV, rawDepth, UNITY_MATRIX_I_VP);
                    xzDist = length(sceneWS.xz - _PlayerPos.xz);
                }

                // Bell-curve mask: zero at inner edge, peaks mid-band, zero at outer edge.
                float  t       = saturate((xzDist - _InnerRadius) / max(_FogBandWidth, 0.001));
                float  fogMask = smoothstep(0.0, 0.5, t) * smoothstep(1.0, 0.6, t);
                half   alpha   = fogMask * _FogMaxAlpha;
                return half4(_FogColor.rgb, alpha);
            }
            ENDHLSL
        }

        // ── Pass 1: Passthrough cut ───────────────────────────────────────────
        // Writes vrAmount into the alpha channel (ColorMask A, ZTest Always),
        // overriding whatever alpha URP wrote for opaque/transparent geometry.
        Pass
        {
            Name "PassthroughCut"
            Cull Front
            ZWrite Off
            ZTest Always
            ColorMask A

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _PlayerPos;
                float  _InnerRadius;
                float  _FogBandWidth;
                float4 _FogColor;
                float  _FogMaxAlpha;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 screenPos  : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.positionCS = GetVertexPositionInputs(IN.positionOS.xyz).positionCS;
                OUT.screenPos  = ComputeScreenPos(OUT.positionCS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
                float  rawDepth = SampleSceneDepth(screenUV);

                // Convert raw device depth to linear [0=near, 1=far] using _ZBufferParams.
                // This handles both reversed-Z (Quest/Vulkan: near=1, far=0) and standard-Z.
                float linearDepth = Linear01Depth(rawDepth, _ZBufferParams);

                // Sky / empty VR space → far plane (linearDepth ≈ 1).
                // Treat as inner zone (vrAmount = 1) so the VR skybox/ceiling stays visible.
                float xzDist;
                if (linearDepth > 0.9999)
                {
                    xzDist = 0.0;
                }
                else
                {
                    float3 sceneWS = ComputeWorldSpacePosition(screenUV, rawDepth, UNITY_MATRIX_I_VP);
                    xzDist = length(sceneWS.xz - _PlayerPos.xz);
                }

                float outerEdge = _InnerRadius + _FogBandWidth;
                // 1 = keep VR,  0 = reveal passthrough.
                float vrAmount  = 1.0 - smoothstep(_InnerRadius, outerEdge, xzDist);
                return half4(0, 0, 0, vrAmount);
            }
            ENDHLSL
        }
    }

    FallBack Off
}
