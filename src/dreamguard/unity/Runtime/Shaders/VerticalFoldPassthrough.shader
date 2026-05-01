Shader "DreamGuard/VerticalFoldPassthrough"
{
    // Tears the virtual world open horizontally, revealing Meta Quest passthrough
    // through the gap.  A large inward-facing cylinder centred on the player carries
    // this shader.
    //
    // The gap is a world-space horizontal band around _GapCenterY.  In the fringe
    // zone either side, Worley cellular noise creates a jagged, chunk-shaped edge so
    // the tear looks like the virtual world is fragmenting and folding away.
    //
    // Pass 0 – FoldVisuals :  glowing seam + fragment chunk colour.
    // Pass 1 – AlphaHole   :  writes alpha = 0 (passthrough) inside the gap and
    //                         the noise-shaped fringe, leaving VR content elsewhere.
    //
    // Meta Quest compositor: alpha == 0 → passthrough, alpha == 1 → VR.

    Properties
    {
        _GapCenterY        ("Gap Centre Y (world)",         Float)       = 1.5
        _GapHalfWidth      ("Gap Half-Width (m)",           Float)       = 0.3
        _EdgeFringe        ("Fragment Fringe Width (m)",    Float)       = 0.35
        _EdgeGlowColor     ("Seam Glow Colour",             Color)       = (0.35, 0.85, 1, 1)
        _EdgeGlowWidth     ("Glow Width (m)",               Float)       = 0.10
        _GlowIntensity     ("Glow Intensity",               Range(0, 8)) = 2.5
        _NoiseScale        ("Fragment Cell Scale",          Float)       = 4.0
        _NoiseSpeed        ("Animation Speed",              Float)       = 0.4
        _FragmentRoughness ("Fragment Edge Roughness",      Range(0, 1)) = 0.65
    }

    SubShader
    {
        Tags
        {
            "RenderType"     = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            // After all opaque and other passthrough overlays.
            "Queue"          = "Overlay+30"
        }

        // ── Pass 0: Seam glow + fragment chunk colour ─────────────────────────────

        Pass
        {
            Name "FoldVisuals"
            Cull Off
            ZWrite Off
            // ZTest Always so the glow stamps over dungeon geometry inside the gap band.
            ZTest Always
            // Premultiplied alpha — lets glow add on bright surfaces.
            Blend One OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float  _GapCenterY;
                float  _GapHalfWidth;
                float  _EdgeFringe;
                float4 _EdgeGlowColor;
                float  _EdgeGlowWidth;
                float  _GlowIntensity;
                float  _NoiseScale;
                float  _NoiseSpeed;
                float  _FragmentRoughness;
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

            // ── Noise helpers ─────────────────────────────────────────────────────

            float2 hash2(float2 p)
            {
                p = float2(dot(p, float2(127.1, 311.7)), dot(p, float2(269.5, 183.3)));
                return frac(sin(p) * 43758.5453);
            }

            // Worley cellular noise.  Returns distance to the nearest cell feature
            // point: ~0 at cell centres, ~1 toward cell edges.
            float worley(float2 p)
            {
                float2 pi = floor(p);
                float2 pf = frac(p);
                float  md = 8.0;
                for (int xi = -1; xi <= 1; xi++)
                for (int yi = -1; yi <= 1; yi++)
                {
                    float2 c  = float2(xi, yi);
                    float  d  = length(pf - hash2(pi + c) - c);
                    md = min(md, d);
                }
                return md;
            }

            // Two-octave cellular noise with slow temporal drift.
            float cellNoise(float2 p, float t)
            {
                float2 drift = float2(t * 0.31, t * 0.19);
                float  n0    = worley(p + drift);
                float  n1    = worley(p * 1.9 + drift * 1.4);
                return saturate(n0 * 0.65 + n1 * 0.35);
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float absY     = abs(IN.worldPos.y - _GapCenterY);
                float gapOuter = _GapHalfWidth + _EdgeFringe;

                // Completely outside the effect band — produce nothing.
                if (absY > gapOuter + _EdgeGlowWidth) discard;

                float t     = _Time.y * _NoiseSpeed;
                float noise = cellNoise(IN.worldPos.xz * _NoiseScale, t);

                // Noisy boundary divides passthrough zone from VR-side fragments.
                // noise ∈ [0,1]:  low noise → boundary pushed further out
                //                 high noise → boundary pulled back toward gap centre
                float boundary = _GapHalfWidth + noise * _FragmentRoughness * _EdgeFringe;

                // ── Seam glow: exponential halo on the VR side of the boundary ────
                float distVR   = absY - boundary;                      // >0 on VR side
                float seamGlow = exp(-max(distVR, 0.0) / max(_EdgeGlowWidth, 0.001) * 5.0)
                               * step(0.0, distVR);

                // ── Fragment chunks: VR-side cells inside the fringe zone ─────────
                float inChunk  = step(boundary, absY) * step(absY, gapOuter);
                // Fade toward the outer fringe edge.
                float chunkT   = saturate((absY - boundary) / max(_EdgeFringe, 0.001));
                // Cell centres (low noise = low worley dist) glow brightest.
                float chunkLum = inChunk * (1.0 - chunkT) * (0.4 + 0.6 * (1.0 - noise));

                // ── Pulse: keeps the seam energetic ──────────────────────────────
                float pulse = 0.75 + 0.25 * sin(_Time.y * 3.7
                            + IN.worldPos.x * 0.5 + IN.worldPos.z * 0.3);

                float alpha = saturate(max(seamGlow, chunkLum) * _GlowIntensity * pulse);
                clip(alpha - 0.01);

                // Premultiplied output.
                float3 col = _EdgeGlowColor.rgb * _GlowIntensity * pulse;
                return half4(col * alpha, alpha);
            }
            ENDHLSL
        }

        // ── Pass 1: Alpha hole-cutter ─────────────────────────────────────────────
        // Writes 0 to the alpha channel inside the gap and the noise-shaped fringe,
        // so the Meta Quest compositor reveals passthrough there.
        // ZTest Always so this stamps over dungeon walls that fill the gap band.

        Pass
        {
            Name "AlphaHole"
            Cull Off
            ZWrite Off
            ZTest Always
            ColorMask A
            Blend Zero One

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float  _GapCenterY;
                float  _GapHalfWidth;
                float  _EdgeFringe;
                float4 _EdgeGlowColor;
                float  _EdgeGlowWidth;
                float  _GlowIntensity;
                float  _NoiseScale;
                float  _NoiseSpeed;
                float  _FragmentRoughness;
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

            float2 hash2(float2 p)
            {
                p = float2(dot(p, float2(127.1, 311.7)), dot(p, float2(269.5, 183.3)));
                return frac(sin(p) * 43758.5453);
            }

            float worley(float2 p)
            {
                float2 pi = floor(p);
                float2 pf = frac(p);
                float  md = 8.0;
                for (int xi = -1; xi <= 1; xi++)
                for (int yi = -1; yi <= 1; yi++)
                {
                    float2 c = float2(xi, yi);
                    md = min(md, length(pf - hash2(pi + c) - c));
                }
                return md;
            }

            float cellNoise(float2 p, float t)
            {
                float2 drift = float2(t * 0.31, t * 0.19);
                return saturate(worley(p + drift) * 0.65 + worley(p * 1.9 + drift * 1.4) * 0.35);
            }

            // alpha output: 1 = keep VR, 0 = reveal passthrough.
            half4 frag(Varyings IN) : SV_Target
            {
                float absY     = abs(IN.worldPos.y - _GapCenterY);
                float gapOuter = _GapHalfWidth + _EdgeFringe;

                // Beyond fringe + glow — leave VR alpha intact.
                if (absY > gapOuter + _EdgeGlowWidth)
                    return half4(0, 0, 0, 1.0);

                float t        = _Time.y * _NoiseSpeed;
                float noise    = cellNoise(IN.worldPos.xz * _NoiseScale, t);
                float boundary = _GapHalfWidth + noise * _FragmentRoughness * _EdgeFringe;

                // Below boundary (closer to gap centre) → passthrough (keepAlpha = 0).
                // Above boundary (toward VR) → keep VR (keepAlpha = 1).
                float keepAlpha = step(boundary, absY);

                return half4(0, 0, 0, keepAlpha);
            }
            ENDHLSL
        }
    }

    FallBack Off
}
