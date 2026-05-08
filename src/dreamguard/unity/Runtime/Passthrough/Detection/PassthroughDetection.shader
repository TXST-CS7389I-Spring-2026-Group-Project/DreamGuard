Shader "DreamGuard/DetectionPassthrough"
{
    // Detection-based partial-passthrough shader.
    //
    // Placed on a single large sphere that surrounds the camera (Cull Front renders the
    // inward-facing surface from inside). The C# side uploads up to MAX_DETECTIONS
    // viewport-space bounding boxes at detection time — one per NMS-surviving detection.
    // For every pixel on the sphere the shader checks whether it falls inside any detection:
    //
    //   Inside  a bbox → write alpha = 0 via ColorMask A → compositor shows passthrough.
    //   Outside all bboxes → discard → VR scene shows through the sphere.
    //
    // Camera-locked, stereo-correct bbox
    // ────────────────────────────────────
    // The C# side stores detection corners as camera-LOCAL direction vectors (converted
    // from world-space passthrough rays by applying the inverse render-camera rotation at
    // detection time). Each frame the shader:
    //   1. Rotates local→world via _CameraLocalToWorld (Camera.transform.localToWorldMatrix,
    //      uploaded from C# every Update) so the direction follows the current camera
    //      orientation → camera-locked. Uses a C# upload instead of unity_CameraToWorld
    //      because unity_CameraToWorld is unreliable in single-pass stereo instancing.
    //   2. Reconstructs a far-plane world point:  _WorldSpaceCameraPos + worldDir * FAR
    //   3. Projects per-eye via unity_StereoMatrixVP[eye] — uses the headset's actual
    //      asymmetric per-eye frustums, matching how vp (from ComputeScreenPos) is
    //      computed. This avoids the divergence caused by Camera.main.WorldToViewportPoint
    //      which uses the symmetric mono projection matrix.
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

            // Camera-local direction vectors for each detection bbox corner.
            // xyz = direction in render-camera transform-local space (+Z = forward).
            // w   = 0 (direction, not position).
            // The shader rotates them to world space each frame via _CameraLocalToWorld
            // (uploaded from C# as Camera.transform.localToWorldMatrix every Update).
            // Using a C#-uploaded matrix avoids relying on unity_CameraToWorld, which is
            // unreliable in single-pass stereo instancing (only valid for eye 0).
            #define MAX_DETECTIONS 16
            float4x4 _CameraLocalToWorld;
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
                float2 vp = i.screenPos.xy / i.screenPos.w;

                // Select this eye's VP matrix. unity_StereoMatrixVP[0] = left, [1] = right.
                // On non-stereo platforms UNITY_MATRIX_VP is used directly.
                #if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_SINGLE_PASS_STEREO)
                    float4x4 eyeVP = unity_StereoMatrixVP[unity_StereoEyeIndex];
                #else
                    float4x4 eyeVP = UNITY_MATRIX_VP;
                #endif

                // Camera-locked, stereo-correct bbox projection.
                //
                // _DetectionDirBL/TR are stored in camera-LOCAL space. Multiplying by
                // unity_CameraToWorld (rotation only, w=0) rotates them to world space using
                // the CURRENT camera orientation each frame — making the bbox follow the
                // camera (camera-locked). The far-plane point is then projected through the
                // per-eye VP matrix, which uses the headset's actual asymmetric frustums and
                // matches the vp computation above — no stereo divergence.
                float3 camPos = _WorldSpaceCameraPos;
                const float FAR = 1000.0;

                for (int d = 0; d < _DetectionCount; d++)
                {
                    // Rotate camera-local direction to world space via the C#-uploaded matrix.
                    float3 worldDirBL = mul((float3x3)_CameraLocalToWorld, _DetectionDirBL[d].xyz);
                    float3 worldDirTR = mul((float3x3)_CameraLocalToWorld, _DetectionDirTR[d].xyz);

                    float4 blClip = mul(eyeVP, float4(camPos + worldDirBL * FAR, 1.0));
                    float4 trClip = mul(eyeVP, float4(camPos + worldDirTR * FAR, 1.0));

                    // Skip if either corner is behind the camera.
                    if (blClip.w <= 0.0 || trClip.w <= 0.0) continue;

                    float2 blVP = blClip.xy / blClip.w * 0.5 + 0.5;
                    float2 trVP = trClip.xy / trClip.w * 0.5 + 0.5;

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
