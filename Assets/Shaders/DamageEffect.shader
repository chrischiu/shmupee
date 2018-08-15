Shader "Custom/DamageEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_OffsetU("OffsetU", Range(0, 1)) = 0
		_OffsetV("OffsetV", Range(0, 1)) = 0
		_Intensity("Intensity", Range(0, 1)) = 0
		_MaxDistortion("MaxDistortion", Range(0, 1)) = 0.2
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _NoiseTex;

			uniform float _OffsetU;
			uniform float _OffsetV;
			uniform float _Intensity;
			uniform float _MaxDistortion;

			fixed4 frag (v2f_img i) : COLOR
			{
				float4 r = tex2D(_NoiseTex, i.uv + half2(_OffsetU, _OffsetV) * 0.1);

				half displacementX = -_MaxDistortion * (1 + 2 * (r % 8));
				half displacementY = -_MaxDistortion * (1 + 2 * (r / 8));
				
				fixed4 col1 = tex2D(_MainTex, i.uv);
				fixed4 col2 = tex2D(_MainTex, i.uv - half2(displacementX, displacementY));

				fixed4 col = lerp(col1, col2, _Intensity);

				return col;
			}
			ENDCG
		}
	}
}


