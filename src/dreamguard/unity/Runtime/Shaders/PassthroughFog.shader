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
    // Both passes determine the XZ player distance by:
    //   1. Projecting the dome fragment through _EnvironmentDepthReprojectionMatrices
    //      to obtain the corresponding UV in the environment depth camera.
    //   2. Sampling SampleEnvironmentDepthLinear for the real-world depth (metres).
    //   3. xzDist = envDepth * length(normalize(domeFrag - camera).xz)
    //      — the horizontal distance of the real-world surface from the player.
    //
    // This means the boundary follows real-world geometry rather than a fixed
    // analytical radius, which is the correct model for a passthrough MR effect.
    //
    // Prerequisites (already configured):
    //   • OVRPassthroughLayer (Underlay) on this GameObject.
    //   • EnvironmentDepthManager on this GameObject — populates
    //     _EnvironmentDepthTexture and _EnvironmentDepthReprojectionMatrices.
    //   • m_AllowPostProcessAlphaOutput: 1 in UniversalRP.asset so URP preserves
    //     alpha through to the XR swapchain.

    Properties
    {
        _PlayerPos            ("Player World Position (xyz)", Vector) = (0, 0, 0, 0)
        _PlayerEyeHeight      ("Player Eye Height (metres)",  Float)  = 1.6
        _InnerRadius          ("Inner Radius (full VR)",      Float)  = 3.0
        _FogBandWidth         ("Fog Band Width",              Float)  = 2.0
        _FogColor             ("Fog Color",                   Color)  = (0.05, 0.05, 0.08, 1.0)
        _FogMaxAlpha          ("Fog Max Alpha",               Range(0, 1)) = 0.92
        _EnvironmentDepthBias ("Depth Bias",                  Float)  = 0.06
    }

    SubShader
    {
        Tags
        {
            "RenderType"     = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            // Render after all opaque geometry so the depth texture and environment
            // depth are fully available.
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
            #pragma multi_compile _ HARD_OCCLUSION SOFT_OCCLUSION
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.meta.xr.sdk.core/Shaders/EnvironmentDepth/URP/EnvironmentOcclusionURP.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _PlayerPos;
                float  _PlayerEyeHeight;
                float  _InnerRadius;
                float  _FogBandWidth;
                float4 _FogColor;
                float  _FogMaxAlpha;
                float  _EnvironmentDepthBias;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 worldPos   : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                VertexPositionInputs vpi = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionCS = vpi.positionCS;
                OUT.worldPos   = vpi.positionWS;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                // Reproject dome fragment into environment depth camera space.
                float4 depthSpace = mul(_EnvironmentDepthReprojectionMatrices[unity_StereoEyeIndex],
                                        float4(IN.worldPos, 1.0));
                float2 uvCoords   = (depthSpace.xy / depthSpace.w + 1.0f) * 0.5f;

                // Real-world linear depth in this direction (metres from camera).
                float  envDepth   = SampleEnvironmentDepthLinear(uvCoords);

                // Horizontal distance of the real-world surface from the player.
                float3 dir        = normalize(IN.worldPos - _WorldSpaceCameraPos);
                float  xzDist     = envDepth * length(dir.xz);

                float  t          = saturate((xzDist - _InnerRadius) / max(_FogBandWidth, 0.001));
                // Bell-curve mask: peaks at the centre of the transition band.
                float  fogMask    = smoothstep(0.0, 0.5, t) * smoothstep(1.0, 0.6, t);
                half   alpha      = fogMask * _FogMaxAlpha;
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
            #include "Packages/com.meta.xr.sdk.core/Shaders/EnvironmentDepth/URP/EnvironmentOcclusionURP.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _PlayerPos;
                float  _PlayerEyeHeight;
                float  _InnerRadius;
                float  _FogBandWidth;
                float4 _FogColor;
                float  _FogMaxAlpha;
                float  _EnvironmentDepthBias;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 worldPos   : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                VertexPositionInputs vpi = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionCS = vpi.positionCS;
                OUT.worldPos   = vpi.positionWS;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                // Reproject dome fragment into environment depth camera space.
                float4 depthSpace = mul(_EnvironmentDepthReprojectionMatrices[unity_StereoEyeIndex],
                                        float4(IN.worldPos, 1.0));
                float2 uvCoords   = (depthSpace.xy / depthSpace.w + 1.0f) * 0.5f;

                // Real-world linear depth in this direction (metres from camera).
                float  envDepth   = SampleEnvironmentDepthLinear(uvCoords);

                // Horizontal distance of the real-world surface from the player.
                float3 dir        = normalize(IN.worldPos - _WorldSpaceCameraPos);
                float  xzDist     = envDepth * length(dir.xz);

                float  outerEdge  = _InnerRadius + _FogBandWidth;
                // 1 = keep VR,  0 = reveal passthrough.
                float  vrAmount   = 1.0 - smoothstep(_InnerRadius, outerEdge, xzDist);
                return half4(0, 0, 0, vrAmount);
            }
            ENDHLSL
        }
    }

    FallBack Off
}
