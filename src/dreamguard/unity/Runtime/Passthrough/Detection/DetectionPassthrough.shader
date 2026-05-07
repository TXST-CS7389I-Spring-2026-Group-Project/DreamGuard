Shader "DreamGuard/DetectionPassthrough"
{
    // Detection-based partial-passthrough shader.
    //
    // Placed on a single large sphere that surrounds the camera (Cull Front renders the
    // inward-facing surface from inside). The C# side uploads up to MAX_DETECTIONS bounding
    // boxes in normalised viewport space each inference frame. For every pixel on the sphere
    // the shader checks whether its viewport coordinate falls inside any detection bbox:
    //
    //   Inside  a bbox → write alpha = 0 via RevSub blend → compositor shows passthrough.
    //   Outside all bboxes → discard → VR scene shows through the sphere.
    //
    // No per-detection GameObjects are spawned; the single sphere covers the full FOV and
    // the shader drives which regions reveal the real world.
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

            // Detection bounding boxes uploaded by DetectionBasedPassthrough.cs each frame.
            // Each Vector4: (xMin, yMin, xMax, yMax) in normalised viewport coords [0, 1].
            // Y=0 is the bottom of the screen (Unity convention).
            #define MAX_DETECTIONS 16
            float4 _DetectionBboxes[MAX_DETECTIONS];
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

                // Test against every active detection bbox.
                for (int d = 0; d < _DetectionCount; d++)
                {
                    float4 bb = _DetectionBboxes[d]; // xMin, yMin, xMax, yMax
                    if (vp.x >= bb.x && vp.x <= bb.z &&
                        vp.y >= bb.y && vp.y <= bb.w)
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
