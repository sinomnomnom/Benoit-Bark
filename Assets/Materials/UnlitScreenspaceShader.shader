Shader "Tutorial/039_ScreenspaceTextures/Unlit_Blend"{
    Properties{
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture A", 2D) = "white" {}
        _BlendTex ("Texture B", 2D) = "white" {}
        _Blend ("Blend", Range(0,1)) = 0
    }

    SubShader{
        Tags{
            "Queue"="Background"
            "RenderType"="Background"
            "PreviewType"="Skybox"
        }


        Pass{
            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            sampler2D _BlendTex;

            float4 _MainTex_ST;
            float4 _BlendTex_ST;

            fixed4 _Color;
            float _Blend;

            struct appdata{
                float4 vertex : POSITION;
            };

            struct v2f{
                float4 position : SV_POSITION;
                float4 screenPosition : TEXCOORD0;
            };

            v2f vert(appdata v){
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.position);
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET{
                float2 uv = i.screenPosition.xy / i.screenPosition.w;

                float aspect = _ScreenParams.x / _ScreenParams.y;
                uv.x *= aspect;

                float2 uvA = TRANSFORM_TEX(uv, _MainTex);
                float2 uvB = TRANSFORM_TEX(uv, _BlendTex);

                fixed4 colA = tex2D(_MainTex, uvA);
                fixed4 colB = tex2D(_BlendTex, uvB);

                fixed4 col = lerp(colA, colB, _Blend);
                col *= _Color;

                return col;
            }
            ENDCG
        }
    }
}
