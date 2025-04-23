Shader "Custom/SpaceGrid" {
    Properties {
        _GridSize ("Grid Size", Float) = 10.0
        _GridColor ("Grid Color", Color) = (0.2, 0.4, 0.8, 0.2)
        _LineThickness ("Line Thickness", Range(0.001, 0.1)) = 0.03
        _FadeDistance ("Fade Distance", Float) = 100.0
    }
    
    SubShader {
        Tags { "Queue"="Background" "RenderType"="Transparent" }
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        
        Pass {
            Tags { "DisableBatching" = "False" }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setupInstancedGrid
            
            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct v2f {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float distFromCamera : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            float _GridSize;
            float4 _GridColor;
            float _LineThickness;
            float _FadeDistance;
            
            float4x4 _GridInstanceMatrices[512]; // Maximum number of instances
            
            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            void setupInstancedGrid() {
                unity_ObjectToWorld = _GridInstanceMatrices[unity_InstanceID];
                
                unity_WorldToObject = unity_ObjectToWorld;
                unity_WorldToObject._14_24_34 *= -1;
                unity_WorldToObject._11_22_33 = 1.0f / unity_WorldToObject._11_22_33;
            }
            #endif
            
            v2f vert (appdata v) {
                v2f o;
                
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.distFromCamera = distance(o.worldPos, _WorldSpaceCameraPos);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                UNITY_SETUP_INSTANCE_ID(i);
                
                float2 grid = frac(i.worldPos.xy / _GridSize);
                
                float2 gridLines = step(grid, _LineThickness) + 
                                  step(1.0 - _LineThickness, grid);
                
                float distanceFade = 1.0 - saturate(i.distFromCamera / _FadeDistance);
                float alpha = saturate(gridLines.x + gridLines.y) * distanceFade * _GridColor.a;
                
                return float4(_GridColor.rgb, alpha);
            }
            ENDCG
        }
    }
}