
Shader "Custom/MenuBackground"
{

	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			float2 cmul(float2 c1, float2 c2)
			{
				return float2(
					c1.x * c2.x - c1.y * c2.y,
					c1.x * c2.y + c1.y * c2.x
				);
			}

			fixed4 CalColor(float2 pixelCoord, float2 resolution, float time)
			{
				float2 uPos = (pixelCoord.xy / resolution.y);
				uPos -= float2((0.5 * resolution.x / resolution.y), 0.5);
				uPos *= 3.0;
		
				float3 color = float3(0, 0, 0);
				float vertColor = 0.0;
				for( float i = 0.0; i < 3.0; ++i )
				{
					float t = time * 0.6 + 1.0;
	
					float2 origPos = uPos;
					uPos.y += sin( uPos.y / 2.0 + uPos.x / 2.0 * (i / 0.5 + 1.0) + t ) * 1.1;
		
					uPos =  cmul(origPos, uPos);
		
					float fTemp = abs(9.0 / uPos.y / 0.09 / 100.0);
					vertColor += fTemp;
					color += float3( fTemp * i / 5.0, fTemp * i / 40.0, pow(fTemp, i / 24.0) / 64.0 );
				}
	
				return float4(color, 1.0);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float x = i.uv.x;
				float y = i.uv.y;
				float w = _ScreenParams.x;
				float h = _ScreenParams.y;

				return CalColor(float2(x * w, y * h), float2(w, h), _Time.y);
			}
			ENDCG
		}
	}

}
