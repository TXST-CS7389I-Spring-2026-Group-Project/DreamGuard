Shader "DreamGuard/DetectionPassthrough"
{
    // Detection-based partial-passthrough shader.
    //
    // Placed on a single large sphere that surrounds the camera (Cull Front renders the
    // inward-facing surface from inside). The C# side uploads up to MAX_DETECTIONS world-space
    // bounding-box corners each inference frame. For every pixel on the sphere the shader
    // checks whether it falls inside any detection region:
    //
    //   Inside  a bbox → write alpha = 0 via ColorMask A → compositor shows passthrough.
    //   Outside all bboxes → discard → VR scene shows through the sphere.
    //
    // Stereo correctness
    // ──────────────────
    // Previous approach: bboxes were pre-projected to viewport space on the CPU using
    // Camera.WorldToViewportPoint — which only produces left-eye coordinates, causing the
    // left box to appear only in the left eye and the right box only in the right eye.
    //
    // Fix: upload the two world-space corner positions (_DetectionWorldBL / _DetectionWorldTR)
    // and project them in the fragment shader using unity_StereoMatrixVP[unity_StereoEyeIndex].
    // The GPU selects the correct per-eye VP matrix for each draw-call instance, so both eyes
    // independently compute the right screen-space bbox without any CPU-side stereo logic.
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

            // World-space bottom-left and top-right corners for each detection.
            // Uploaded by DetectionBasedPassthrough.cs each inference frame.
            // xyz = world position at estimatedObjectDepth along the passthrough-camera ray.
            // w   = 1 (homogeneous).
            #define MAX_DETECTIONS 16
            float4 _DetectionWorldBL[MAX_DETECTIONS];
            float4 _DetectionWorldTR[MAX_DETECTIONS];
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
                float2 vp = i.screenPos.xy / i.screenPos.w;

                // In single-pass stereo instanced mode (Quest standard), the combined render
                // texture is twice as wide: eye 0 occupies x=[0,0.5], eye 1 x=[0.5,1.0].
                // Normalise to per-eye [0,1] so it matches the projected corner coords below.
                #if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_SINGLE_PASS_STEREO)
                    vp.x = vp.x * 2.0 - unity_StereoEyeIndex;
                #endif

                // Select this eye's VP matrix. unity_StereoMatrixVP[0] = left, [1] = right.
                // On non-stereo platforms UNITY_MATRIX_VP is used directly.
                #if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_SINGLE_PASS_STEREO)
                    float4x4 eyeVP = unity_StereoMatrixVP[unity_StereoEyeIndex];
                #else
                    float4x4 eyeVP = UNITY_MATRIX_VP;
                #endif

                // Test against every active detection bbox.
                for (int d = 0; d < _DetectionCount; d++)
                {
                    // Project world-space corners into this eye's clip space, then to [0,1].
                    float4 blClip = mul(eyeVP, _DetectionWorldBL[d]);
                    float4 trClip = mul(eyeVP, _DetectionWorldTR[d]);
                    float2 blVP   = blClip.xy / blClip.w * 0.5 + 0.5;
                    float2 trVP   = trClip.xy / trClip.w * 0.5 + 0.5;

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
