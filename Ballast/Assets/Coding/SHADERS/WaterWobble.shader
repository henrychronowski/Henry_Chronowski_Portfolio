Shader "Unlit/WaterWobble"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	    _Distortion("Distortion", range(-10, 10)) = 0
		_Size("Size", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

		GrabPass {"_GrabTexture" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float4 grabUv : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex, _GrabTexture;
            float4 _MainTex_ST;
			float _Size, _Distortion;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.grabUv = UNITY_PROJ_COORD(ComputeGrabScreenPos(o.vertex));
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			float ranNum(float2 p)
			{
				p = frac(p * float2(123.34, 345.45));
				p += dot(p, p + 34.345);
				return frac(p.x * p.y);
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv * _Size;
				float t = fmod(_Time.y, 7200);
				float2 projUv = i.grabUv.xy / i.grabUv.w;
                // sample the texture
				//projUv += ranNum(sin(t*i.uv.x*i.uv.y)) * _Distortion;
				projUv += ranNum(floor(uv.x)+frac(t)) * _Distortion;
                fixed4 col = tex2D(_GrabTexture, projUv);
				
				//col.xy *= ranNum(sin(t));
                
				// apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
