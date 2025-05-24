Shader "Custom/URP_PostProcessing"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        
        [Header(Lens Distortion)]
        _LensDistortionIntensity ("Lens Distortion Intensity", Range(-1, 1)) = 0.25
        _LensDistortionXMultiplier ("X Multiplier", Range(0, 1)) = 1
        _LensDistortionYMultiplier ("Y Multiplier", Range(0, 1)) = 1
        _LensDistortionCenterX ("Center X", Range(0, 1)) = 0.5
        _LensDistortionCenterY ("Center Y", Range(0, 1)) = 0.5
        _LensDistortionScale ("Scale", Range(0.01, 3)) = 1
        
        [Header(Chromatic Aberration)]
        _ChromaticAberrationIntensity ("Chromatic Aberration Intensity", Range(0, 1)) = 0.25
        
        [Header(Panini Projection)]
        _PaniniDistance ("Panini Distance", Range(0, 1)) = 0
        _PaniniCropToFit ("Crop To Fit", Range(0, 1)) = 1
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "RenderPipeline"="UniversalPipeline"
        }
        
        Pass
        {
            Name "PostProcessing"
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float _LensDistortionIntensity;
                float _LensDistortionXMultiplier;
                float _LensDistortionYMultiplier;
                float _LensDistortionCenterX;
                float _LensDistortionCenterY;
                float _LensDistortionScale;
                float _ChromaticAberrationIntensity;
                float _PaniniDistance;
                float _PaniniCropToFit;
            CBUFFER_END
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                return output;
            }
            
            // Lens Distortion Function
            float2 DistortUV(float2 uv, float intensity, float2 multiplier, float2 center, float scale)
            {
                float2 centeredUV = (uv - center) * 2.0;
                centeredUV *= multiplier;
                
                float r2 = dot(centeredUV, centeredUV);
                float distortion = 1.0 + intensity * r2;
                
                float2 distortedUV = centeredUV * distortion;
                distortedUV /= scale;
                
                return (distortedUV * 0.5) + center;
            }
            
            // Panini Projection Function
            float2 PaniniProjection(float2 uv, float distance)
            {
                if (distance <= 0.0) return uv;
                
                float2 coords = (uv - 0.5) * 2.0;
                float coordsLength = length(coords);
                
                if (coordsLength <= 0.0) return uv;
                
                float cylCoordsY = coords.y / coordsLength;
                float theta = atan2(coords.y, coords.x);
                
                float sinTheta = sin(theta);
                float cosTheta = cos(theta);
                
                float tanFoV = coordsLength;
                float d = distance;
                
                float S = (d + 1.0) / (d + cosTheta * tanFoV + 1.0);
                
                float2 projectedCoords;
                projectedCoords.x = S * sinTheta * tanFoV;
                projectedCoords.y = S * cylCoordsY * tanFoV;
                
                return (projectedCoords * 0.5) + 0.5;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                float2 uv = input.uv;
                
                // Apply Panini Projection first
                if (_PaniniDistance > 0.0)
                {
                    uv = PaniniProjection(uv, _PaniniDistance);
                    
                    // Crop to fit
                    if (_PaniniCropToFit > 0.0)
                    {
                        float2 cropBounds = lerp(float2(0, 1), float2(0.1, 0.9), _PaniniCropToFit);
                        if (uv.x < cropBounds.x || uv.x > cropBounds.y || 
                            uv.y < cropBounds.x || uv.y > cropBounds.y)
                            return half4(0, 0, 0, 1);
                    }
                }
                
                // Apply Lens Distortion
                float2 center = float2(_LensDistortionCenterX, _LensDistortionCenterY);
                float2 multiplier = float2(_LensDistortionXMultiplier, _LensDistortionYMultiplier);
                
                if (_ChromaticAberrationIntensity > 0.0)
                {
                    // Chromatic Aberration with Lens Distortion
                    float2 redUV = DistortUV(uv, _LensDistortionIntensity * (1.0 + _ChromaticAberrationIntensity * 0.01), 
                                           multiplier, center, _LensDistortionScale);
                    float2 greenUV = DistortUV(uv, _LensDistortionIntensity, 
                                             multiplier, center, _LensDistortionScale);
                    float2 blueUV = DistortUV(uv, _LensDistortionIntensity * (1.0 - _ChromaticAberrationIntensity * 0.01), 
                                            multiplier, center, _LensDistortionScale);
                    
                    // Sample each channel separately
                    half red = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, redUV).r;
                    half green = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, greenUV).g;
                    half blue = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, blueUV).b;
                    half alpha = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, greenUV).a;
                    
                    return half4(red, green, blue, alpha);
                }
                else
                {
                    // Just Lens Distortion
                    float2 distortedUV = DistortUV(uv, _LensDistortionIntensity, multiplier, center, _LensDistortionScale);
                    return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, distortedUV);
                }
            }
            ENDHLSL
        }
    }
    
    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}