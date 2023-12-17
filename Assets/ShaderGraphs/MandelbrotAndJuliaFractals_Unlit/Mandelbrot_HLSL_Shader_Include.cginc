/*
float2 rotatePoint(float2 p, float2 pivot, float angleDegs)
{
	float sinDegs = sin(angleDegs);
	float cosDegs = cos(angleDegs);
	
	p -= pivot;
	p = float2(p.x * cosDegs - p.y * sinDegs, p.x * sinDegs + p.y * cosDegs);
	p += pivot;
	
	return p;
}
*/

void CalcFractal_float(in float2 UVCoords, in float2 TextureResolution, in float2 RegionSize, in float2 RegionCentre, in float MaxIterations, in float ZoomFactor, in float AngleDegs, in bool CalcJuliaInsteadOfMandelbrot, out float3 FractalGreyscaleColour)
{
	// Reminder: UVCoords go (0,0) at bottom-left to (1,1) at top-right!

	// 'Bin' the UV to get a pixel position by multiplying the UV by the resolution and rounding the result
	float2 pixelPos = round(UVCoords * TextureResolution);	

	// Apply the zoom factor to the rendered region size (i.e., decrease the region size at higher zoom levels)
	RegionSize /= ZoomFactor;
	
	// Get a render scale (i.e., a multiplying factor so that rendering this region stretches to fit our texture size)
	float2 RenderScale = RegionSize / TextureResolution;	
	
	// Get the center of our rendered region
	float2 Centre = pixelPos * RenderScale + (RegionCentre - (RegionSize / 2));

	//pixelPos = rotatePoint(pixelPos, Centre, AngleDegs);	

	// Mandelbrot rendering loop
	float iterations = 0;
	float2 z = float2(0, 0);
	while (length(z) < 2.0 && iterations < MaxIterations)
	{
		z = float2(z.x * z.x - z.y * z.y, 2.0 * z.x * z.y) + Centre;
		++iterations;
	}
	
	// Calculate our grey value as the number of iterations it took to exit the above loop..
	float greyscaleValue = iterations / MaxIterations;
	
	// ..and apply it as our outgoing colour.
	FractalGreyscaleColour = float3(greyscaleValue, greyscaleValue, greyscaleValue);
}