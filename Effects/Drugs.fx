sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float Multiplier(int divide) 
{
    return 0.01f / divide;
}
float4 Drugs(float2 coords : TEXCOORD0) : COLOR0
{
    float2 newCoords = coords;
    newCoords.y += sin(coords.x * 1 + uTime) * Multiplier(1);
    newCoords += (cos(coords.y * 1 + uTime) * Multiplier(1), sin(coords.y * 1 + uTime) * Multiplier(1));
    float2 spinCoords = coords;
    spinCoords += (cos(uTime * 0.1f) * Multiplier(1), sin(uTime * 0.1f) * Multiplier(1));
    spinCoords += (sin(coords.y * 1 + uTime) * Multiplier(1), cos(coords.y * 1 + uTime) * Multiplier(1));
    float2 opCoords = coords;
    opCoords += (sin(uTime * 0.1f) * Multiplier(1), cos(uTime * 0.1f) * Multiplier(1));
    opCoords += (cos(coords.y * 1 + uTime) * Multiplier(1), sin(coords.y * 1 + uTime) * Multiplier(1));
    float4 color = tex2D(uImage0, newCoords);
    float4 noise = tex2D(uImage1, spinCoords);
    float4 noise2 = tex2D(uImage2, opCoords);
    color.rgb += (uColor.rgb / 2);
    noise.rgb += (uSecondaryColor.rgb / 2);
    color.rgb += noise2.rgb * 0.10f;
    color.rgb += noise.rgb * 0.15f;
    return color;
}

technique Technique1
{
    pass Drugs
    {
        PixelShader = compile ps_2_0 Drugs();
    }
}