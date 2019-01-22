// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TestUI"{   
	Properties{
		_BaseTex("Texture", 2D) = "white" {}
		_KeyColor1("Key Color 1", Color) = (0,0,1,1)
		_KeyColor2("Key Color 2", Color) = (1,1,1,1)
		_KeyColor3("Key Color 3", Color) = (1,0,0,1)
		_ReplaceColor1("Replace Color 1", Color) = (0.5,.2,1,1)
		_ReplaceColor2("Replace Color 2", Color) = (1,0.2,0.5,1)
		_ReplaceColor3("Replace Color 3", Color) = (0,0,0,1)



	}
    SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata{
				float4 vertex : POSITION;
				float2 uv: TEXCOORD0;
			};

			struct v2f{
				float4 vertex: SV_POSITION;
				float2 uv: TEXCOORD0;
			};

			v2f vert(appdata v){
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _BaseTex;

			float4 frag(v2f i) : SV_Target{
				float4 color = tex2D(_BaseTex, i.uv);
				return color;
			}
			ENDCG
		}
    }
}
