Shader "DreamGuard/DetectionPassthrough"
{
    // Detection-based partial-passthrough shader.
    //
    // Placed on a single large sphere that surrounds the camera (Cull Front renders the
    // inward-facing surface from inside). The C# side uploads up to MAX_DETECTIONS
    // world-space direction vectors each inference frame — one pair (BL/TR) per detection.
    // For every pixel on the sphere the shader checks whether it falls inside any detection:
    //
    //   Inside  a bbox → write alpha = 0 via ColorMask A → compositor shows passthrough.
    //   Outside all bboxes → discard → VR scene shows through the sphere.
    //
    // Angular tracking (direction-based, not position-based)
    // ───────────────────────────────────────────────────────
    // Directions are captured at inference time (when the passthrough camera frame is
    // grabbed). The shader reconstructs a world point each frame as:
    //   camPos + dir * 1000 m
    // Because camPos (_WorldSpaceCameraPos) updates every frame, the bbox hole follows
    // the player's view. Because dir is a unit vector fixed at inference time, the hole
    // stays on the real-world object's angular direction as the player rotates. The 1000 m
    // FAR value reduces translation-induced parallax to < 0.06 deg — imperceptible.
    //
    // Stereo correctness
    // ──────────────────
    // Each detection corner is projected per-eye using unity_StereoMatrixVP[eye]. In
    // single-pass stereo instanced mode (Quest/OpenXR default), unity_MatrixMVP and
    // unity_StereoMatrixVP[eye] both produce per-eye [0,1] viewport coordinates — no
    // x-axis stereo correction is applied to the fragment screen position.
    //
    // Requirements: camera clearFlags=SolidColor, backgroundColor=(0,0,0,1) and an
    // enabled OVRPassthroughLayer set to Underlay on the same GameObject.

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }

        Pass
        {
            ColorMask A     // write only the alpha channel
            ZWrite Off
            ZTest Always
            Cull Front  // camera is inside the sphere; render inward-facing surface

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            // World-space direction vectors for each detection bbox corner.
            // Uploaded by DetectionBasedPassthrough.cs each inference frame.
            // xyz = unit direction from the passthrough camera (at capture time) toward
            //       the bottom-left / top-right corner of the detection.
            // w   = 0 (direction, not a homogeneous position).
            // The shader reconstructs a far-plane world point per eye as:
            //   camPos + dir * FAR  (FAR = 1000 m)
            // This makes the passthrough hole follow the angular direction of the detected
            // object regardless of player translation, with negligible parallax.
            #define MAX_DETECTIONS 16
            float4 _DetectionDirBL[MAX_DETECTIONS];
            float4 _DetectionDirTR[MAX_DETECTIONS];
            int    _DetectionCount;

            struct appdata
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex    : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert(appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex    = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                // Normalised viewport coordinate [0,1] for this pixel.
                // ComputeScreenPos(UnityObjectToClipPos(v)) uses unity_MatrixMVP which is
                // already the per-eye matrix in single-pass instanced stereo (Quest/OpenXR).
                // Both vp and the projected bbox corners below come out in the same per-eye
                // [0,1] space — no x-axis stereo correction is needed or correct here.
                // (The old vp.x * 2 - eye correction assumed a double-wide side-by-side
                // render target, which Quest does NOT use in instanced stereo mode.)
                float2 vp = i.screenPos.xy / i.screenPos.w;

                // Select this eye's VP matrix. unity_StereoMatrixVP[0] = left, [1] = right.
                // On non-stereo platforms UNITY_MATRIX_VP is used directly.
                #if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_SINGLE_PASS_STEREO)
                    float4x4 eyeVP = unity_StereoMatrixVP[unity_StereoEyeIndex];
                #else
                    float4x4 eyeVP = UNITY_MATRIX_VP;
                #endif

                // Reconstruct a far-plane world point for each corner direction so that
                // the bbox tracks the angular direction of the detected object (not a
                // fixed 3-D position). Using FAR = 1000 m makes player translation cause
                // < 0.06 deg of parallax, which is imperceptible.
                float3 camPos = _WorldSpaceCameraPos;
                const float FAR = 1000.0;

                // Test against every active detection bbox.
                for (int d = 0; d < _DetectionCount; d++)
                {
                    float4 blWorld = float4(camPos + _DetectionDirBL[d].xyz * FAR, 1.0);
                    float4 trWorld = float4(camPos + _DetectionDirTR[d].xyz * FAR, 1.0);
                    float4 blClip  = mul(eyeVP, blWorld);
                    float4 trClip  = mul(eyeVP, trWorld);
                    float2 blVP    = blClip.xy / blClip.w * 0.5 + 0.5;
                    float2 trVP    = trClip.xy / trClip.w * 0.5 + 0.5;

                    float xMin = min(blVP.x, trVP.x);
                    float xMax = max(blVP.x, trVP.x);
                    float yMin = min(blVP.y, trVP.y);
                    float yMax = max(blVP.y, trVP.y);

                    if (vp.x >= xMin && vp.x <= xMax &&
                        vp.y >= yMin && vp.y <= yMax)
                    {
                        // Inside a detection — ColorMask A writes alpha = 0.
                        // The Meta compositor shows the passthrough underlay here.
                        return fixed4(0, 0, 0, 0);
                    }
                }

                // Outside all detections — discard so the VR scene shows through.
                clip(-1);
                return fixed4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}
