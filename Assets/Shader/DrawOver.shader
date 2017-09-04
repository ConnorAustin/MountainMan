Shader "Custom/DrawOver" 
{
	Properties 
	{
		_Color ("Color Tint", Color) = (1, 1, 1, 1)
		_MainTex ("Base (RGB) Trans(A)", 2D) = "white" {}
	}
	
	SubShader 
	{
		Tags { "Queue"="Overlay+2001" }
		
		ZTest Always
		ZWrite Off
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha 
		
		Pass
		{
			Lighting On
			Material 
			{
               Emission [_Color]
            }
            
			SetTexture[_MainTex]
			{
				Combine Texture * Primary, Texture * Primary
			}
		}
	}
}
