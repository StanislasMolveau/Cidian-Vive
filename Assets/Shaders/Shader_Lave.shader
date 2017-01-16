Shader "Custom/Shader_Lave" {
	Properties {
		_MainTex ("_MainTex RGBA", 2D) = "white" {}
		_Distort("_Distort A", 2D) = "white" {}
		_DistortX("Distortion en X", Range(0,2)) = 1
		_DistortY("Distortion en Y", Range(0,2)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert noforwardadd

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Distort;
		fixed _DistortX;
		fixed _DistortY;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed distortion = tex2D(_Distort, IN.uv_MainTex).a;
			fixed2 uv_scroll = fixed2(IN.uv_MainTex.x - distortion * _DistortX, IN.uv_MainTex.y - distortion * _DistortY);
			fixed4 tex = tex2D(_MainTex, uv_scroll);
			c.rgb = lerp(tex.rgb, c.rgb, c.a);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
