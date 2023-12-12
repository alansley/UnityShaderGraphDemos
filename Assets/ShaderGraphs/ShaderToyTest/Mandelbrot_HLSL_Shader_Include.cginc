void CalcFractal_float(in float2 UVCoords, in float2 TextureResolution, in float2 RegionSize, in float2 RegionCentre, in float MaxIterations, in float ZoomFactor, in bool CalcJuliaInsteadOfMandelbrot, out float3 FractalGreyscaleColour)
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

	
	float iterations = 0;
	float zx = 0;
	float zy = 0;
	float temp;

	// Mandelbrot rendering loop
	while ((zx * zx + zx * zy < 4.0) && iterations < MaxIterations)
	{
		temp = (zx * zx - zy * zy) + Centre.x;
		zy = (2.0 * zx * zy) + Centre.y;
		zx = temp;
		++iterations;
	}
	
	// Calculate our grey value as the number of iterations it took to exit the above loop..
	float greyscaleValue = iterations / MaxIterations;
	
	// ..and apply it as our outgoing colour.
	FractalGreyscaleColour = float3(greyscaleValue, greyscaleValue, greyscaleValue);
}