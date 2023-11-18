Shader "Custom/RedOverlay"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _Color ("Overlay Color", Color) = (.7,0,0,1)
        _Radius ("Circle Radius", Range(0.0, 0.5)) = 0.2
    }
    SubShader
    {
        CGINCLUDE
        #include "UnityCG.cginc"
    CGINCLUDE
    }
    FallBack "Diffuse"
}
