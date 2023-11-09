Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _TargetX ("Target X UV Position", float) = 0.5
        _TargetY ("Target Y UV Position", float) = 0.5
        _Strength ("Fade Strength", Range(0.0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _TargetX;
            float _TargetY;
            float _Strength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 col = float4(0, 0, 0, 1);
                float2 target = float2(_TargetX - 0.5, _TargetY - 0.5) * 2;
                float dist = _Strength / 2  - length(target- i.uv);
                float res = clamp(1 * dist, 0, 1);
                col.w = res;
                return col;
            }
            ENDCG
        }
    }
}
