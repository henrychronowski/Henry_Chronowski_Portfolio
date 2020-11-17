// Lerning absorbed from https://www.youtube.com/watch?v=EBrAdahFtuo

Shader "Unlit/RainWindow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	    _Size("Size", float) = 1
		_T("Time", float) = 0
		_Distortion("Distortion", range(-5, 5)) = 0
		_Blur("Blur", range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent"}
        LOD 100

		GrabPass { "_GrabTexture"}
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #define S(a, b, t) smoothstep(a, b, t)

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
			float _Size, _T, _Distortion, _Blur;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.grabUv = UNITY_PROJ_COORD(ComputeGrabScreenPos(o.vertex));
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			//=================================================================================================================

			float ranNum(float2 p)
			{
				p = frac(p * float2(123.34, 345.45));
				p += dot(p, p + 34.345);
				return frac(p.x * p.y);
			}

			float3 Layer(float2 UV, float t)
			{
				float2 aspect = float2(2, 1);
				float2 iAspect = float2(1, 2);
				float2 uv = UV * _Size * aspect;
				uv.y += t * .25;
				float2 gv = frac(uv) - .5;
				float2 id = floor(uv);

				float n = ranNum(id); // [0,1]
				t += n * 6.2831;
				float w = UV.y * 10;
				float x = (n - .5) * .8;
				x += (.4 - abs(x)) * sin(3 * w) * pow(sin(w), 6) * .45;
				float y = -sin(t + sin(t + sin(t) * .5)) * .45;
				y -= (gv.x - x) * (gv.x - x);

				float2 dropPos = (gv - float2(x, y)) * iAspect;
				float drop = S(.05, .03, length(dropPos));

				float2 trailPos = (gv - float2(x, t * .25)) * iAspect;
				trailPos.y = (frac(trailPos.y * 8) - .5) * .125;
				float trail = S(.03, .01, length(trailPos));
				float fogTrail = S(-.05, .05, dropPos.y);
				fogTrail *= S(0.5, y, gv.y);
				trail *= fogTrail;
				fogTrail *= S(.05, .04, abs(dropPos.x));

				float2 offs = drop * dropPos + trail * trailPos;// +trail;

				return float3(offs, fogTrail);
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float t = fmod(_Time.y, 7200) + _T;
				float4 col = 0;
				float fade = 1 - saturate(fwidth(i.uv) * 60);

				float3 drops = Layer(i.uv, t);
				drops += Layer(i.uv * 1.23 + 7.54, t);
				drops += Layer(i.uv * 1.35 + 1.54, t);
				drops += Layer(i.uv * 1.57 - 7.54, t);

				float blur = _Blur * 7 * (1 - drops.z*fade);
				blur *= .01;
				
				float2 projUv = i.grabUv.xy / i.grabUv.w;
				projUv += drops.xy * _Distortion * fade;

				const float numSamples = 16; // DON'T GO HIGHER THAN LIKE 32ISH, VERY EXPENSIVE!!!!!!
				float a = ranNum(i.uv) * 6.2831;
				for (float i = 0; i < numSamples; i++)
				{
					float2 offs = float2(sin(a), cos(a))*blur;
					offs *= sqrt(frac(sin((i + 1) * 637) * 5018));
					col += tex2D(_GrabTexture, projUv + offs);
					a++;
				}

				col /= numSamples;

				return col;
            }
            ENDCG
        }
    }
}
