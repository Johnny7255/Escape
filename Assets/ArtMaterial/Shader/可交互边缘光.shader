Shader "Custom/可交互边缘光"
{
    Properties
    {
        _MainColor ("主色调", Color) = (0,0,0,1)
        _InSideRimColor ("边缘光颜色", Color) = (0,0,0,0)
        _InSideRimPower("边缘光强度", Range(0.0,5)) = 0                 // 控制菲涅尔影响范围的大小，这个值越大，效果上越边缘化
        _InSideRimIntensity("边缘光强度系数", Range(0.0, 10)) = 0       // 反射的强度，值越大返回的强度越大，导致边缘的颜色不那么明显  
        [Toggle] _Invert ("可交互?", Float) = 0
        _MainTex("主纹理", 2D) = "white" {}
        _BumpMap ("法线纹理", 2D) = "bump" {}
        _BumpScale ("凹凸程度", Float) = 1.0
        _Specular ("高光颜色", Color) = (1.0, 1.0, 1.0, 1.0)
        _Gloss ("光泽度", Range(8.0, 256)) = 20
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        Pass  //内边缘光pass
        {
            Tags{ "LightMode" = "ForwardBase" }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature _INVERT_ON
            #include "Lighting.cginc"
            
            fixed4 _MainColor;
            uniform float4 _InSideRimColor;
            float  _InSideRimPower;
            float _InSideRimIntensity;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;
			float4 _BumpMap_ST;
			float _BumpScale;
			fixed4 _Specular;
			float _Gloss;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD1;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 uv : TEXCOORD0;
                float3 wold_normal : TEXCOORD1;
                float4 vertexWorld : TEXCOORD2;
                float3 lightDir : TEXCOORD3;
                float3 viewDir : TEXCOORD4;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                o.wold_normal = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                fixed3 worldBinormal = cross(o.wold_normal, worldTangent) * v.tangent.w;
                float3x3 worldToTangent = float3x3(worldTangent, worldBinormal, o.wold_normal);
                o.vertexWorld = mul(unity_ObjectToWorld, v.vertex);
                
                o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
                
				o.lightDir = mul(worldToTangent, WorldSpaceLightDir(v.vertex));
				o.viewDir = mul(worldToTangent, WorldSpaceViewDir(v.vertex));
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed3 tangentViewDir = normalize(i.viewDir);
                fixed3 tangentLightDir = normalize(i.lightDir);

                fixed4 packedNormal = tex2D(_BumpMap, i.uv.zw);
                fixed3 tangentNormal;
                tangentNormal = UnpackNormal(packedNormal);
                tangentNormal.xy *= _BumpScale;
                tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));

                const fixed3 albedo = tex2D(_MainTex, i.uv).rgb * _MainColor.rgb;
                const fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;

                fixed3 halfDir = normalize(tangentLightDir + tangentViewDir);
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(tangentNormal, halfDir)), _Gloss);
                
                i.wold_normal = normalize(i.wold_normal);                                       //下面计算方式套用菲涅尔计算
                float3 worldViewDir = normalize(_WorldSpaceCameraPos.xyz - i.vertexWorld.xyz);  //获取单位视角方向
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                const fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(i.wold_normal, worldLightDir));
                
                _InSideRimIntensity = 4 * sin(_Time.z) + 5;
                
                float3 Emissive = (0, 0, 0);
                
                #if _INVERT_ON
                    half NdotV = max(0, dot(i.wold_normal, worldViewDir));                       // 计算法线方向和视角方向点积,约靠近边缘夹角越大，值约小，那就是会越在圆球中间约亮，越边缘约暗
                    NdotV = 1.0 - NdotV;
                    float fresnel = pow(NdotV,_InSideRimPower) * _InSideRimIntensity;
                    Emissive = _InSideRimColor.rgb * fresnel;      
                #endif
                
                return fixed4(Emissive + diffuse + ambient + specular,1.0);
            }
            ENDCG
        }
    }
}