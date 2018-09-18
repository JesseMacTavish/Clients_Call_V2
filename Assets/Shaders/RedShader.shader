Shader "Handout/Red" {
	Properties
	{
		_Color1("Color1", Color) = (1,0.5,0,1)
		_Color2("Color2", Color) = (0.5,0.25,0,1)
		Size("RingSize", float) = 4.38
	}
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex myVertexShader
			#pragma fragment myFragmentShader
			#define UNITY_SHADER_NO_UPGRADE 1

			struct vertexInput {
				float4 vertex : POSITION;
			};

			struct vertexToFragment {
				float4 vertex : SV_POSITION;
				float4 UV : TEXCOORDS0;
			};

			float MandelBrot(float2 cCoord) {
				int iteration = 0;
				float2 zCoord = float2(0, 0);
				while (zCoord.x * zCoord.x + zCoord.y * zCoord.y < 3 & iteration < 100) {
					zCoord = float2(
						zCoord.x * zCoord.x - zCoord.y * zCoord.y + cCoord.x,
						2 * zCoord.x * zCoord.y + cCoord.y
						);
					iteration++;
				}
				return iteration / 100.0;
			}

			float PingPong(float value, float length)
			{
				if (value > length) //check if we are over our limit
				{
					float a = value % length; //grab the decimal number
					uint b = value / length; //check how many times we are over our limit
					uint c = b % 2; //see if it's an even number
					if (c == 0) //even
					{
						return a;
					}
					else //not even
					{
						return length - a;
					}
				}
				else
				{
					return value;
				}
			}

			float4 _Color1;
			float4 _Color2;
			float Size;

			vertexToFragment myVertexShader (vertexInput v) {
				vertexToFragment o;
				// Transform the point to clip space:
				o.vertex = mul(UNITY_MATRIX_MVP,v.vertex);

				o.UV = mul(UNITY_MATRIX_P, v.vertex);
				return o;
			}
			
			fixed4 myFragmentShader (vertexToFragment i) : SV_Target {
				// return color red (1,0,0), with alpha=1:
				//return float4(1,0,0,1);

				//chessboard
				//float parity = fmod(floor(i.UV.x * 10) + floor(i.UV.y * 10), 2);
				//return float4(parity, parity, parity, 1);

				//circle:
				//float distanceFromCenter = length(i.UV - float2(0, 0));
				//float colorValue = saturate(1 - distanceFromCenter * 2);
				//return float4(colorValue, colorValue, colorValue, 1);

				//stripes:
				//float value = fmod(i.UV.x * 10 + i.UV.y * 5, 1);
				//return float4(value, value, 0, 1);

				//UV:
				//return float4(i.UV.x, i.UV.y, 0, 1);

				//MantelBrot:
				float val = MandelBrot(i.UV * 3);
				return float4(val, val, val, 1);
			}
			ENDCG
		}
	}
}

