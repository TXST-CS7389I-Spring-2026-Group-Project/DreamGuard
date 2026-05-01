Shader "DreamGuard/PassthroughFog"
{
    // Renders a radial fog boundary that gradually reveals Meta Quest passthrough.
    //
    // The mesh this shader sits on should be a large sphere with inward-facing
    // normals (or any convex shape the player stands inside of).
    //
    // Pass 0 - FogColor:
    //   Paints fog-coloured semi-transparent haze in the transition band.
    //   Drawn OVER existing VR geometry without occluding it (ZWrite Off).
    //
    // Pass 1 - AlphaHole:
    //   Writes alpha = 0 to the framebuffer in the outer region (beyond the fog
    //   band), making the Meta Quest compositor reveal passthrough there.
    //   ColorMask A  +  Blend Zero One  preserves RGB, only touches alpha.
    //
    // Compositor model (Meta Quest / OVRPassthroughLayer Underlay Skybox):
    //   alpha == 1  →  show VR content
    //   alpha == 0  →  show passthrough camera feed

    Properties
    {
        _PlayerPos      ("Player World Position (xyz)", Vector) = (0, 0, 0, 0)
        _InnerRadius    ("Inner Radius (full VR)", Float) = 3.0
        _FogBandWidth   ("Fog Band Width", Float) = 2.0
        _FogColor       ("Fog Color", Color) = (0.05, 0.05, 0.08, 1.0)
        _FogMaxAlpha    ("Fog Max Alpha", Range(0, 1)) = 0.92
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            // High queue so this runs after opaque dungeon geometry.
            "Queue" = "Overlay+10"
        }

        // ── Pass 0: Fog colour ────────────────────────────────────────────────
        Pass
        {
            Name "FogColor"
            Cull Front          // Sphere is inside-facing; cull the outside.
            ZWrite Off
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _PlayerPos;
                float  _InnerRadius;
                float  _FogBandWidth;
                float4 _FogColor;
                float  _FogMaxAlpha;
            CBUFFER_END

            struct Attributes { float4 positionOS : POSITION; };
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 worldPos   : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                VertexPositionInputs vpi = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionCS = vpi.positionCS;
                OUT.worldPos   = vpi.positionWS;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Horizontal (XZ-plane) distance from the player.
                float2 delta = IN.worldPos.xz - _PlayerPos.xz;
                float  dist  = length(delta);

                // 0 at inner edge, 1 at outer edge of fog band.
                float t = saturate((dist - _InnerRadius) / max(_FogBandWidth, 0.001));

                // Smooth in/out so fog fades gently at both edges.
                // Peaks at t == 0.6 (slightly weighted toward the outer edge).
                float fogMask = smoothstep(0.0, 0.5, t) * smoothstep(1.0, 0.6, t);

                half  alpha = fogMask * _FogMaxAlpha;
                return half4(_FogColor.rgb, alpha);
            }
            ENDHLSL
        }

        // ── Pass 1: Alpha hole-cutter ─────────────────────────────────────────
        // Writes 0 to the alpha channel beyond the fog band so the Meta Quest
        // compositor shows passthrough there instead of the (now-empty) VR layer.
        Pass
        {
            Name "AlphaHole"
            Cull Front
            ZWrite Off
            ZTest LEqual
            // Preserve RGB entirely; only overwrite alpha.
            ColorMask A
            Blend Zero One

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _PlayerPos;
                float  _InnerRadius;
                float  _FogBandWidth;
                float4 _FogColor;
                float  _FogMaxAlpha;
            CBUFFER_END

            struct Attributes { float4 positionOS : POSITION; };
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 worldPos   : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                VertexPositionInputs vpi = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionCS = vpi.positionCS;
                OUT.worldPos   = vpi.positionWS;
                return OUT;
            }

            // Output alpha:  1 = keep VR,  0 = reveal passthrough.
            half4 frag(Varyings IN) : SV_Target
            {
                float2 delta = IN.worldPos.xz - _PlayerPos.xz;
                float  dist  = length(delta);

                float outerEdge = _InnerRadius + _FogBandWidth;

                // Smoothly cut alpha from 1→0 across a short fringe at the outer edge.
                float cutFringe = 0.3; // metres over which the cut happens
                float keepAlpha = 1.0 - smoothstep(outerEdge - cutFringe, outerEdge, dist);

                // Output: only alpha matters (ColorMask A).
                return half4(0, 0, 0, keepAlpha);
            }
            ENDHLSL
        }
    }

    FallBack Off
}
