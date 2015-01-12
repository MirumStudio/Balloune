
//http://forum.unity3d.com/threads/anyway-to-change-from-greyscale-to-colour.221341/
//http://answers.unity3d.com/questions/583960/apply-shader-to-a-sprite.html as References

Shader "Sprites/GrayScaler" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_GrayScl ("GrayScale",Range(0.0,1.0)) = 1.0
	}
	SubShader {
		 Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }
 
    ZWrite Off
 
    Blend SrcAlpha OneMinusSrcAlpha
         Tags 
         { 
             "RenderType" = "Opaque" 
             "Queue" = "Transparent+1" 
         }
 
         Pass
         {
             ZWrite Off
             Blend SrcAlpha OneMinusSrcAlpha 
  
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile DUMMY PIXELSNAP_ON
  
             sampler2D _MainTex;
             float _GrayScl;
 
             struct Vertex
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float2 uv2 : TEXCOORD1;
             };
     
             struct Fragment
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float2 uv2 : TEXCOORD1;
             };
  
             Fragment vert(Vertex v)
             {
                 Fragment o;
     
                 o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                 o.uv_MainTex = v.uv_MainTex;
                 o.uv2 = v.uv2;
     
                 return o;
             }
                                                     
             float4 frag(Fragment IN) : COLOR
             {
                fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
	            fixed4 mainColor = fixed4(1,1,1,1);
	            fixed4 fragColor;
	            fixed4 tempColor;
	            tempColor.rgb = dot(mainTex.rgb, fixed3(.222, .707, .071));
	            fragColor = ((_GrayScl * mainTex) + ((1 - _GrayScl) * tempColor))/ (2 - _GrayScl);
	            fragColor.a = mainTex.a;
	            return fragColor;  
             }
 
             ENDCG
		} 
	}
	FallBack "Sprites/Default"
}
