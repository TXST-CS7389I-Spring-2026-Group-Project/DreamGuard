Shader "DreamGuard/WaterRipplePassthrough"
{
    // Passthrough reveal effect that animates like water ripples breaking through the floor.
    //
    // Visual progression (driven by _Progress 0 → 1):
    //   0.0 → 0.25  Single gentle drip; tiny passthrough spot opens at the center.
    //   0.25→ 0.65  Expanding ripple ring with wavy, animated boundary.
    //   0.65→ 1.0   Four secondary sources fire; violent interference waves; full
    //               passthrough floods the area.
    //
    // Two-pass compositor model (Meta Quest / OVRPassthroughLayer Underlay):
    //   framebuffer alpha == 1  →  VR content visible
    //   framebuffer alpha == 0  →  passthrough camera feed
    //
    // Pass 0 – WaterColor : Paints translucent blue-teal water at the boundary.
    // Pass 1 – AlphaHole  : Writes alpha = 0 inside the ripple zone (reveals passthrough).

    Properties
    {
        _RippleCenter    ("Ripple Center (World XYZ)", Vector) = (0, 0, 0, 0)
        _Progress        ("Reveal Progress (0-1)", Range(0, 1)) = 0
        _MaxRadius       ("Max Ripple Radius (m)", Float) = 6.0
        _WaterColor      ("Water Color", Color) = (0.08, 0.35, 0.60, 1.0)
        _HighlightColor  ("Wave Crest Highlight", Color) = (0.60, 0.85, 1.00, 1.0)
        _RippleIntensity ("Ripple Speed Multiplier", Range(0.1, 3.0)) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderType"     = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            "Queue"          = "Overlay+20"
        }

        // ── Pass 0: Water surface colour ─────────────────────────────────────────
        // Renders a translucent water layer near the passthrough boundary so the
        // transition looks like actual water spreading across the floor.
        Pass
        {
            Name "WaterColor"
            Cull Off
            ZWrite Off
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _RippleCenter;
                float  _Progress;
                float  _MaxRadius;
                float4 _WaterColor;
                float4 _HighlightColor;
                float  _RippleIntensity;
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

            // Outward-travelling circular wave from one source point.
            // Returns height in approx [-1, 1] that decays naturally with distance.
            float RippleWave(float2 pos, float2 src,
                             float t, float speed, float freq, float decay)
            {
                float r    = length(pos - src);
                float wave = sin(r * freq - t * speed);
                return wave * exp(-r * decay);
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float progress = _Progress;
                if (progress < 0.005) return half4(0, 0, 0, 0);

                float2 center = _RippleCenter.xz;
                float2 pos    = IN.worldPos.xz;
                float  dist   = length(pos - center);
                float  ptR    = progress * _MaxRadius;

                // Cull fragments more than 3 m outside the active ripple zone.
                if (dist > ptR + 3.0) return half4(0, 0, 0, 0);

                float t     = _Time.y * _RippleIntensity;
                float speed = lerp(2.0,  7.0,  progress);
                float freq  = lerp(3.5,  9.0,  progress);
                float decay = lerp(0.10, 0.35, progress);

                // Primary source (always active).
                float h = RippleWave(pos, center, t, speed, freq, decay);

                // Four secondary sources that phase in above progress 0.5, creating
                // the violent interference pattern.
                float t2 = saturate((progress - 0.5) * 2.0);
                float sr = ptR * 0.5;
                h += RippleWave(pos, center + float2( sr,  0), t * 0.93 + 1.1, speed * 0.85, freq * 1.15, decay * 0.8) * t2 * 0.4;
                h += RippleWave(pos, center + float2(-sr,  0), t * 0.87 + 2.3, speed * 0.90, freq * 1.10, decay * 0.8) * t2 * 0.4;
                h += RippleWave(pos, center + float2( 0,  sr), t * 0.91 + 3.7, speed * 0.80, freq * 1.20, decay * 0.8) * t2 * 0.4;
                h += RippleWave(pos, center + float2( 0, -sr), t * 0.95 + 0.5, speed * 0.95, freq * 1.05, decay * 0.8) * t2 * 0.4;

                // Normalise to [0, 1] for colour blending (0 = trough, 1 = crest).
                float hN = saturate(h * 0.5 + 0.5);

                // Colour: deep blue → mid blue → bright highlight at crests.
                float3 deepColor = _WaterColor.rgb * 0.5;
                float3 midColor  = _WaterColor.rgb;
                float3 highColor = _HighlightColor.rgb;
                float3 color     = lerp(deepColor, midColor, saturate(hN * 2.0));
                color            = lerp(color, highColor, saturate((hN - 0.65) * 3.5));

                // The water layer is only visible near the passthrough boundary:
                // fades in as the wave front approaches and fades out well inside the
                // passthrough zone (where the real world takes over).
                float outerFade = saturate((ptR + 2.5 - dist) / 2.5);
                float innerFade = saturate((dist - (ptR - 1.5)) / 1.5);
                float alpha     = outerFade * innerFade * lerp(0.5, 0.75, progress);
                alpha          *= (0.4 + 0.6 * hN);

                clip(alpha - 0.005);
                return half4(color, alpha);
            }
            ENDHLSL
        }

        // ── Pass 1: Alpha hole (passthrough reveal) ───────────────────────────────
        // Writes alpha = 0 inside the ripple zone so the OVR compositor reveals the
        // passthrough camera feed there.  The zone boundary is distorted by the live
        // wave field, giving the wavy organic edge that escalates to violent chaos.
        Pass
        {
            Name "AlphaHole"
            Cull Off
            ZWrite Off
            ZTest LEqual
            ColorMask A
            Blend Zero One

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _RippleCenter;
                float  _Progress;
                float  _MaxRadius;
                float4 _WaterColor;
                float4 _HighlightColor;
                float  _RippleIntensity;
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

            float RippleWave(float2 pos, float2 src,
                             float t, float speed, float freq, float decay)
            {
                float r    = length(pos - src);
                float wave = sin(r * freq - t * speed);
                return wave * exp(-r * decay);
            }

            // Output alpha: 1 = keep VR, 0 = reveal passthrough.
            half4 frag(Varyings IN) : SV_Target
            {
                float progress = _Progress;
                float2 center = _RippleCenter.xz;
                float2 pos    = IN.worldPos.xz;
                float  dist   = length(pos - center);
                float  ptR    = progress * _MaxRadius;

                // Well outside the active zone: preserve VR content entirely.
                if (dist > ptR + 3.5) return half4(0, 0, 0, 1.0);

                float t     = _Time.y * _RippleIntensity;
                float speed = lerp(2.0,  7.0,  progress);
                float freq  = lerp(3.5,  9.0,  progress);
                float decay = lerp(0.10, 0.35, progress);

                float h = RippleWave(pos, center, t, speed, freq, decay);

                float t2 = saturate((progress - 0.5) * 2.0);
                float sr = ptR * 0.5;
                h += RippleWave(pos, center + float2( sr,  0), t * 0.93 + 1.1, speed * 0.85, freq * 1.15, decay * 0.8) * t2 * 0.4;
                h += RippleWave(pos, center + float2(-sr,  0), t * 0.87 + 2.3, speed * 0.90, freq * 1.10, decay * 0.8) * t2 * 0.4;
                h += RippleWave(pos, center + float2( 0,  sr), t * 0.91 + 3.7, speed * 0.80, freq * 1.20, decay * 0.8) * t2 * 0.4;
                h += RippleWave(pos, center + float2( 0, -sr), t * 0.95 + 0.5, speed * 0.95, freq * 1.05, decay * 0.8) * t2 * 0.4;

                // Ripple height distorts the boundary: crests push passthrough outward,
                // troughs pull VR content back in.  Distortion amplitude scales with
                // progress for the transition from gentle → violent.
                float distortAmt = lerp(0.3, 1.6, progress);
                float distortedDist = dist - h * distortAmt;

                // Smooth boundary transition (0 = passthrough, 1 = VR).
                float keepAlpha = smoothstep(ptR - 0.4, ptR + 0.4, distortedDist);

                // At high progress (> 0.75), intense secondary waves punch extra passthrough
                // holes slightly beyond the main radius, creating a chaotic "breaking" effect.
                float outerPunch = saturate((progress - 0.75) * 4.0);
                float punchMask  = saturate((ptR + 2.0 - dist) / 2.0);
                float extraHole  = saturate(-h * 1.5) * outerPunch * punchMask;
                keepAlpha        = min(keepAlpha, 1.0 - extraHole);

                return half4(0, 0, 0, keepAlpha);
            }
            ENDHLSL
        }
    }

    FallBack Off
}
