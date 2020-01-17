Shader "Custom/Ice"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
	}

		SubShader{
			Tags { "Queue" = "Transparent" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard alpha:fade 
			#pragma target 3.0

			float4 _Color;

			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutputStandard o) {
				o.Albedo = fixed4(_Color);
				o.Alpha = 0.5;
			}
			ENDCG
	}
		FallBack "Diffuse"
}
