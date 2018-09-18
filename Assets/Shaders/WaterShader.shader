Shader "Handout/Water"
{
	Properties
	{
		_TimeScale("TimeScale", float) = 50
		_Strength("Strength", float) = 0.26
		_Scale("Scale", float) = 5
		_Alpha("Alpha", Range(0,1)) = 0.74
		_MainTex("Texture", 2D) = "white" {}
	}

		// why only sometimes transparent?
		// explain the options below
		// add texture
	SubShader
	{
			ZWrite Off // whether new fragments should write their depth to the depth buffer

			ZTest Less //if and how new fragments should be tested against the depth buffer.

			BlendOp Add

			Blend SrcAlpha OneMinusSrcAlpha
			//ResultColor = NewAlpha * NewColor + (1-NewAlpha) * OldColor
		    //ResultColor = [SrcAlpha] * NewColor [BlendOp] ([OneMinusSrcAlpha]) * OldColor

			Tags{
				"Queue" = "Transparent"
				//rendered after 'Geometry' and 'AlphaTest' in back-to-front order.
			}


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#define UNITY_SHADER_NO_UPGRADE 1


			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float4 light : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 diffuseLight : COLOR;
			};
			
			sampler2D _MainTex;
			float _TimeScale;
			float _Strength;
			float _Scale;
			float _Alpha;
			float4 _Color1;

			v2f vert (appdata v)
			{
				v2f o;
				// Transform the point to clip space:
				//o.vertex = mul(UNITY_MATRIX_MVP,v.vertex);
				o.uv = v.uv;
				o.diffuseLight = v.light;

				float4 uvs = float4(sin(v.uv.x + _Time.x * _TimeScale), -cos(v.uv.y + _Time.x * _TimeScale), 0, 0);

				float4 modifiedPosition = v.vertex + v.normal * uvs * _Strength;
				o.vertex = UnityObjectToClipPos(modifiedPosition);


				return o;
			}


			fixed4 frag (v2f i) : SV_Target
			{
				//// rounds scaled coordinates down to integer:
				//float2 roundedPosition = floor(i.uv * _Scale);
				//// modulo 2, so returns -1, 0 or 1 in a 3D checkerboard pattern:
				//int cell = fmod(roundedPosition.x + roundedPosition.y,2);	
				//// Fix negative numbers! Go from -1,0,1 to 0,1:
				//cell = fmod(cell+2,2);	
				//// return color red or green depending on world position:
				//return float4(cell, 1 - cell, 0, 1);

				//float4 col = _Color1;
				//col.a = _Alpha;
				//return col;

				fixed4 col = tex2D(_MainTex, i.uv);
				col.a = _Alpha;
				return col;
			}
			ENDCG
		}
	}
}
