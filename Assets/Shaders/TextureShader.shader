Shader "Handout/Glow"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SecondTexture("Texture2", 2D) = "white" {}
		_TimeScale("TimeScale", float) = 50
		_Color("Color", Color) = (1,1,0,1)
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#define UNITY_SHADER_NO_UPGRADE 1

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

			sampler2D _MainTex;
			sampler2D _SecondTexture;
			float _TimeScale;
			float4 _Color;

			v2f vert (appdata v)
			{
				v2f o;
				// Transform the point to clip space:
				o.vertex = mul(UNITY_MATRIX_MVP,v.vertex);
				// Copy the UVs:
				o.uv = v.uv;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col2 = (_Color - tex2D(_SecondTexture, i.uv)) * (1 - abs(sin(_Time.x * _TimeScale)));
				//     col 2 = (_color - texture) * [0 - 1];
				//explain this ^^^
				return col + col2;
			}
			ENDCG
		}
	}
}
