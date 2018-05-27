Shader "Custom/BravoMike" {
	Properties {
		_MainTex ("Texture (RGBA)", 2D) = "white" {}
		_Color ("Color (RGBA)", Color) = (1,1,1,1)
		_Metallic ("Light Reflection", Range(0, 1)) = 0.0
		_Glossiness ("Smoothness", Range(0, 1)) = 0.0

		_OutlineColor ("Outline Color", Color) = (1,1,1,1)
		_OutlineWidth ("Outline Width", Range(1, 2)) = 0.0 
	}
	SubShader {
		//Handles Outline
		CGINCLUDE //Tells CG to include these files, since they are not default
		struct appdata{
			float4 vertex : POSITION;
		};

		struct v2f{
			float4 pos : POSITION;
		};

		float _OutlineWidth;
		float4 _OutlineColor;

		v2f vert(appdata v)
		{
			v.vertex.xyz *= _OutlineWidth;

			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			return o;
		}
		ENDCG
		Pass//Renders outline
		{
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : COLOR
			{
				return _OutlineColor;
			}
			ENDCG
		}//Really wish I could tell you what all of above does, however I really have no clue, I only understand positioning. ~ Michael

	    //Tags {"RenderType" = "Opaque"}
		//Makes the game look "Pretty"
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows //Required to run textures

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		half _Metallic;
		half _Glossiness;

		void surf (Input IN, inout SurfaceOutputStandard s) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color; //Allows textures to be colored
			s.Albedo = c.rgb; 
			s.Alpha = c.a; //Draws Alpha on textures, above draws RGB on textures
			s.Metallic = _Metallic; //Draws light reflection
			s.Smoothness = _Glossiness; //Draws smoothness, compliments light reflection
		}
		ENDCG
	}
	FallBack "Diffuse" //Prevents shadows from drawing through object
}