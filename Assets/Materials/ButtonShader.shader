Shader "Unlit/ButtonShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color ("Mesh color", Color) = (0,0,1,1)
        [Toggle] _enable("Outline enable", Float) = 1
        _outline_thickness ("Outline thickness", Float ) = 0.05
        _outline_color ("Outline color", Color) = (0,0,0,1)
    }
    SubShader
    {
        
        LOD 100

        Pass
        {
            Name "Outline"
            Tags {"RenderType"="Opaque"}
            Cull Front  
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           
            struct vert_in 
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct vert_to_frag
            {
                float4 vertex : SV_POSITION;                
            };

            float _outline_thickness, _enable;
            float4 _outline_color;
                           
            vert_to_frag vert (vert_in v)
            {
                vert_to_frag result;
                result.vertex = UnityObjectToClipPos(float4(v.vertex.xyz + v.normal * _outline_thickness, 1));
                return result;
            }
           
            float4 frag(vert_to_frag v):COLOR
            {
                if (_enable==1)
                {
                    return float4(_outline_color.rgb,0);
                }
                else
                {
                    discard;
                    return 0;
                }                      
            }        
            ENDCG
        }
       
        Pass
        {
            Name "FORWARD"
            Tags {"RenderType"="Transparent"}         
            Blend One Zero
            AlphaToMask On

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
               
            float4 _Color;
                                               
            struct vert_in 
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct vert_to_frag
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;                
            };

            sampler2D _MainTex;
            float _outline_thickness, _enable;
            float4 _outline_color;
            float4 _MainTex_ST;
                           
            vert_to_frag vert (vert_in v)
            {
                vert_to_frag result;
                result.vertex = UnityObjectToClipPos(v.vertex);
                result.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return result;
            }
           
            float4 frag(vert_to_frag v):COLOR
            {

                fixed4 col = tex2D(_MainTex, v.uv);
                if(col.a < 0.5){
                    float a = tex2D(_MainTex, float2(v.uv.x + _outline_thickness, v.uv.y)).a +
			            tex2D(_MainTex, float2(v.uv.x, v.uv.y - _outline_thickness)).a +
			            tex2D(_MainTex, float2(v.uv.x - _outline_thickness, v.uv.y)).a +
			            tex2D(_MainTex, float2(v.uv.x, v.uv.y + _outline_thickness)).a;
		            if (col.a < 1.0 && a > 0.0){
                        return fixed4(0, 0, 0, 0.8);
                    }
                }
                
                return col;
                // float l= length(col.xyz);
                // if(l < 1){
                //     col = _outline_color;
                // } 
                // if(l < 1 - _outline_thickness){
                //     col.w = 0;
                // }
                // return col;          
            } 
            ENDCG
        }
    }
}