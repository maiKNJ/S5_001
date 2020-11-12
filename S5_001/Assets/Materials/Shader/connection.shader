Shader "Unlit/connection"
{
   Properties
    {
        _Shininess("Shininess", range(0.1, 100)) = 1.
        
    }
    SubShader
    {
        

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            
            float _Shininess;

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
               
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXTCOORD1;
                float3 worldNormal : TEXTCOORD2;
       
            };



            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.worldNormal = normalize(mul(v.normal,(float3x3) unity_WorldToObject)); 
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = fixed4(0,0,1,1);

                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos);
                float3 worldNormal = normalize(i.worldNormal);
                float3 reflectionDir = normalize(reflect(-lightDir, worldNormal));

                //fixed4 NdotL = max(0, dot(worldNormal, lightDir));

                float RdotV = max(0, dot(reflectionDir, viewDir));
                fixed4 specColor = _LightColor0 * pow(RdotV, _Shininess);
                col.rgb += specColor.rgb;
                return col;
            }
            ENDCG
        }
    }
}
