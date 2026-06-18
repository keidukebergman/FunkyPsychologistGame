Shader "Custom/Shader"
{
    Properties
    {
        _Separation("Separation", Float) = 1
        _Curvature("Curvature", Float) = 1
        _Color("Color", Color) = (0,0,0,0)
        _Softness("softness", Float) = 0.5
        [MaterialToggle] _Active("Active", Float) = 0
        _FadeColor("FadeColor", Color) = (0,0,0,0)
        _FadeAmount("FadeAmount", Float) = 1
        _RorshachTexture("RorshachTexture", 2D)  = ""{}
        _TextureScale("TextureScale", Float) = 1
        [ShowAsVector2] _ScrollSpeed("ScrollSpeed", Vector) = (1,0,0,0)
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline"}
        Pass
        {
            Name "Fullscreen"
            ZWrite Off
            Cull Off
            Blend Off 
            
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _Separation;
            float _Curvature;
            float4 _Color;
            float _Active;
            float _Softness;
            float4 _FadeColor;
            float _FadeAmount;
            texture2D _RorshachTexture;
            float _TextureScale;
            float2 _ScrollSpeed;


            float4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                float2 uv = UnityStereoTransformScreenSpaceTex(input.texcoord);
                half4 color = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_PointClamp, uv);
                float2 uv_position = uv;
                float bend_power = _Curvature * (uv.x - 0) * (uv.x -1);
                float bend_cutoff = bend_power;
                
                if (_Active == 1)
                {
                    float upper_edge = bend_power + 0.4999 + _Separation;
                    float lower_edge = 0.5  - bend_power - _Separation;
                    float dist_from_upper = upper_edge - uv.y;
                    float dist_from_lower = uv.y - lower_edge;

                    float inside = min(dist_from_upper, dist_from_lower);

                    float softness = _Softness; 

                    float t = saturate(inside / softness);
                    float blend = smoothstep(0.0, 1.0, t);

                    color.rgb = lerp(color.rgb, _FadeColor.rgb, _FadeAmount);
                    color.rgb = lerp(_Color.rgb, color.rgb, blend); 

                    float aspect = _BlitTexture_TexelSize.y / _BlitTexture_TexelSize.x;

                    float2 texuv = uv;
                    texuv.x *= aspect;                       
                    texuv /= max(_TextureScale, 0.0001);

                    float wrappedTime = fmod(_Time.y, 4096.0);
                    texuv += _ScrollSpeed.xy * wrappedTime;

                    float4 sampled_texture = SAMPLE_TEXTURE2D(_RorshachTexture, sampler_PointRepeat, texuv);

                    if (sampled_texture.a != 0 && blend < 0.9)
                    {
                        color.rgb = lerp(color.rgb, sampled_texture.rgb, saturate(1.25 * (_FadeAmount - 0.2)));
                    }
 
                }
                return color;

            }
            ENDHLSL
        }
    }
}