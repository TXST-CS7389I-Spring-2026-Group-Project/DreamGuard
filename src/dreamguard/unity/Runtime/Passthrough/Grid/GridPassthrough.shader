Shader "DreamGuard/GridPassthrough"
{
    // Renders a world-aligned grid on horizontal surfaces (floor / ceiling) with a
    // distance-based passthrough gradient.
    //
    // The mesh this shader sits on should be a large flat plane (Unity Plane
    // primitive, scaled to cover the level).
    //
    // Visual behaviour
    // ────────────────
    // • Within innerRadius of the player  → grid fully opaque (VR content visible).
    // • Between innerRadius and innerRadius + gradientWidth
    //     – Fill between grid lines fades to passthrough first.
    //     – Grid lines themselves fade out more slowly.
    // • Beyond innerRadius + gradientWidth → full passthrough.
    //
    // Two-pass compositor model (Meta Quest / OVRPassthroughLayer Underlay Skybox):
    //   alpha == 1  →  show VR content
    //   alpha == 0  →  show passthrough camera feed
    //
    // Pass 0 – GridColor:
    //   Paints the grid (lines + fill) with distance-based alpha over the existing
    //   VR geometry.  ZWrite Off so it doesn't depth-fight with the floor/ceiling.
    //
    // Pass 1 – AlphaHole:
    //   Writes alpha = 0 to the framebuffer in the fill areas at distance, revealing
    //   passthrough there.  ColorMask A + Blend Zero One preserves RGB.

    Properties
    {
        _PlayerPos      ("Player World Position (xyz)", Vector) = (0, 0, 0, 0)
        _InnerRadius    ("Inner Radius – full VR (m)", Float) = 4.0
        _GradientWidth  ("Gradient Width (m)", Float) = 6.0
        _GridSpacing    ("Grid Cell Size (m)", Float) = 1.0
        _LineWidth      ("Line Width (fraction of cell)", Range(0.01, 0.49)) = 0.04
        _GridColor      ("Grid Line Color", Color) = (0.3, 0.8, 1.0, 1.0)
        _GridAlpha      ("Grid Base Alpha", Range(0, 1)) = 0.8
    }

    SubShader
    {
        Tags
        {
            "RenderType"     = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            // Render after opaque geometry so alpha writes land on top of it.
            "Queue"          = "Overlay+20"
        }

        // ── Pass 0: Grid colour ───────────────────────────────────────────────────
        Pass
        {
            Name "GridColor"
            Cull Off        // Visible from above (floor) and below (ceiling).
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
                float  _GradientWidth;
                float  _GridSpacing;
                float  _LineWidth;
                float4 _GridColor;
                float  _GridAlpha;
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

                // Passthrough gradient: 0 = full VR, 1 = full passthrough.
                float passT = saturate((dist - _InnerRadius) / max(_GradientWidth, 0.001));

                // World-space grid pattern.
                float2 cellFrac  = frac(IN.worldPos.xz / _GridSpacing);
                float2 distToLine = min(cellFrac, 1.0 - cellFrac);   // 0 = on line, 0.5 = cell centre

                float lineHalf = _LineWidth * 0.5;

                // Screen-space anti-aliased line edges via derivatives.
                float2 fw = fwidth(distToLine);
                float isLineX = 1.0 - smoothstep(lineHalf - fw.x, lineHalf + fw.x, distToLine.x);
                float isLineZ = 1.0 - smoothstep(lineHalf - fw.y, lineHalf + fw.y, distToLine.y);
                float isLine  = max(isLineX, isLineZ);

                // Fill fades out first (linearly with passthrough gradient).
                float fillAlpha = (1.0 - passT) * _GridAlpha;
                // Lines are more persistent — fade with passT^2 so they stay visible longer.
                float lineAlpha = (1.0 - passT * passT) * _GridAlpha;

                float alpha = lerp(fillAlpha, lineAlpha, isLine);

                // Skip invisible fragments to save fill rate.
                clip(alpha - 0.004);

                return half4(_GridColor.rgb, alpha);
            }
            ENDHLSL
        }

        // ── Pass 1: Alpha hole-cutter ─────────────────────────────────────────────
        // Writes 0 to the alpha channel in fill areas at distance so the Meta Quest
        // compositor reveals passthrough through the floor / ceiling holes there.
        Pass
        {
            Name "AlphaHole"
            Cull Off
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
                float  _GradientWidth;
                float  _GridSpacing;
                float  _LineWidth;
                float4 _GridColor;
                float  _GridAlpha;
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

                float passT = saturate((dist - _InnerRadius) / max(_GradientWidth, 0.001));

                // Grid pattern (same logic as Pass 0).
                float2 cellFrac   = frac(IN.worldPos.xz / _GridSpacing);
                float2 distToLine = min(cellFrac, 1.0 - cellFrac);

                float lineHalf = _LineWidth * 0.5;
                float2 fw = fwidth(distToLine);
                float isLineX = 1.0 - smoothstep(lineHalf - fw.x, lineHalf + fw.x, distToLine.x);
                float isLineZ = 1.0 - smoothstep(lineHalf - fw.y, lineHalf + fw.y, distToLine.y);
                float isLine  = max(isLineX, isLineZ);

                // Fill alpha decreases linearly — holes open up as passT grows.
                float fillKeep = 1.0 - passT;
                // Lines stay opaque longer (quadratic fade).
                float lineKeep = 1.0 - passT * passT;

                float keepAlpha = lerp(fillKeep, lineKeep, isLine);

                // Only alpha channel matters (ColorMask A).
                return half4(0, 0, 0, keepAlpha);
            }
            ENDHLSL
        }
    }

    FallBack Off
}
