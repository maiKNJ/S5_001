Shader "Unlit/EarthNightAndDay"
{
   Properties
    {
        _MainTex ("Nighttime Earth", 2D) = "white" {}
        _DecalTex("Daytime Earth", 2D) = "White" {}
        _Color("Nighttime Color Filter", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            Tags { "LightMode"="ForwardBase" } // pass for the first, directional light.
            
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float4 _LightColor0; //color of the light source (from "lighting.cginc")
            uniform sampler2D _MainTex;
            uniform sampler2D _DecalTex;
            uniform float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
                float3 normal : NORMAL;
                
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float levelOfLighting : TEXCOORD1; //level of diffuse lighting computed in vertex shader
            };

            v2f vert (appdata v)
            {
                v2f o;
                float4x4 modelMatrix = unity_ObjectToWorld;
                float4x4 modelMatrixInverse = unity_WorldToObject;

                float3 normalDirection = normalize(mul(float4(v.normal, 0.0), modelMatrixInverse).xyz);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                
                o.levelOfLighting = max(0.0, dot(normalDirection, lightDirection));
                o.uv = v.uv;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : COLOR
            {
                float4 nighttimeColor = tex2D(_MainTex, i.uv.xy) * _Color;
                float4 daytimeColor = tex2D(_DecalTex, i.uv.xy) * _LightColor0;

                // daytimeColor * levelOfLighting + nighttimeColor * (1.0 - levelOfLighting)
                return lerp(nighttimeColor, daytimeColor, i.levelOfLighting);
            }
            ENDCG
        }
    }
}
