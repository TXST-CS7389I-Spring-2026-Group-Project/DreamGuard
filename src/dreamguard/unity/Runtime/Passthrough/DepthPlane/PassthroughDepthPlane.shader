Shader "Custom/PassthroughDepthPlane"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EnvironmentDepthBias ("Environment Depth Bias", Float) = 0.0
        _DreamGuardDepthThreshold ("Depth Threshold (m)", Float) = 1.5

        [Header(Pixelation)]
        _BlockSize ("Block Size (UV)", Float) = 0.03
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }
        LOD 100

        Pass
        {
           BlendOp RevSub
           Blend One Zero, Zero Zero
           ZWrite Off
           ZTest Always
           Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.meta.xr.sdk.core/Shaders/EnvironmentDepth/BiRP/EnvironmentOcclusionBiRP.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                // World position variable must have this name to work with EnvironmentDepth Macros
                float3 posWorld : TEXCOORD1;

                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _EnvironmentDepthBias;
            float _DreamGuardDepthThreshold;
            float _BlockSize;

            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.posWorld = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                const float4 depthSpace =
                    mul(_EnvironmentDepthReprojectionMatrices[unity_StereoEyeIndex], float4(i.posWorld, 1.0));
                float2 uvCoords = (depthSpace.xy / depthSpace.w + 1.0f) * 0.5f;

                // Snap UVs to a coarse block grid so each spatial block samples one
                // depth value — gives the pixelated look.
                float2 snappedUV = (floor(uvCoords / _BlockSize) + 0.5) * _BlockSize;

                float depth = SampleEnvironmentDepthLinear(snappedUV) + _EnvironmentDepthBias;

                // Passthrough where real-world surfaces are within the threshold;
                // dungeon everywhere else.
                clip(depth < _DreamGuardDepthThreshold ? 1 : -1);

                return float4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}
