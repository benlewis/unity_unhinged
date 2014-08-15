Shader "Custom/VisNorm" {
SubShader {
    Pass {
        Fog { Mode Off }
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
//From: http://docs.unity3d.com/Documentation/Components/SL-VertexProgramInputs.html
// vertex input: position, normal
struct appdata {
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f {
    float4 pos : SV_POSITION;
    fixed4 color : COLOR;
};
v2f vert (appdata v) {
    v2f o;
    o.pos = mul( UNITY_MATRIX_MVP, v.vertex );
    o.color.xyz = v.normal * 0.5 + 0.5;
    o.color.w = 1.0;
    return o;
}
fixed4 frag (v2f i) : COLOR0 { return i.color; }
ENDCG
    }
}
}