Shader "Custom/Shader"
{
    Properties
    {
        _Closeness("Closeness", Float) = 1
        _Curvature("Curvature", Float) = 1
        _Color("Color", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline"}
        Pass
        {
            Name "FullscreenExample"
            ZWrite Off
            Cull Off
            Blend Off 
            
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _Closeness;
            float _Curvature;
            float4 _Color;

            float4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                float2 uv = UnityStereoTransformScreenSpaceTex(input.texcoord);
                half4 color = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_PointClamp, uv);
                float2 uv_position = uv;
                float cutoff = 1-(_Closeness / 2);
                float bend_power = _Curvature * (uv.x - 0) * (uv.y -1);
                float bend_cutoff = bend_power;
                
                if (uv.y > bend_cutoff && uv.y < -bend_cutoff)
                {
                      color.rgb = _Color;   
                }

                return color;
            }
            ENDHLSL
        }
    }
}