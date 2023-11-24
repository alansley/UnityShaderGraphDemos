
# Unity Shader Graph Demos
A suite of example Unity URP Shader Graph effects - just because I wanted to learn how to use shader graph =D

**Unity version**: 2022.3.13f1
**URP & Shader Graph version**: 14.0.9

*Note*: Full-screen shader graphs don't seem to work on Android, not sure why atm - will have to look into it.

## Shader Graph Effects (in no particular order)
All effects built from scratch using the provided '*based on*' source material - massive thanks to the authors of these wonderful resources! <3

#### Shockwave
- Shockwave / ripple effect with push/pull parameters
- Based on: GameDevBill - "Shockwave Shader Graph - How to make a show wave shader in Unity URP/HDRP" - https://www.youtube.com/watch?v=dFDAwT5iozo	
	
#### Dissolve
- Noise threshold dissolve with coloured edges. TODO: Make this directional!
- Based on: Brackeys - "Dissolve using Unity Shader Graph" - https://www.youtube.com/watch?v=taMp1g1pBeE

#### Hologram
- A model-space transparent hologram effect with animated scanlines and noise.
- Based on: TODO: Look it up - can't remember!

#### Force-field / Intersection
- A "glowing-intersection" effect where a model intersects with another. Useful for force-fields / holograms and such.
Based on a combination of:
	- Gabriel Anguiar Prod. - "Unity Shader Graph - Intersection Effect Tutorial" - https://www.youtube.com/watch?v=Uyw5yBFEoXo
	- Brackeys - "Force Field in Unity" - https://www.youtube.com/watch?v=NiOGWZXBg4Y
	Note: Requires "Depth Texture" and "Opaque Texture" to be enabled in the URP settings (in 'Assets\Settings\URP-HighFidelity.asset' etc.)

#### Matrix Weighting Effects
- A selection of 3x3 matrix convolution effects such as:
	- Box Blur,
	- Gaussian Blur,
	- Sharpen, and
	- Laplacian Edge Detection.
- Based on: GLSL code I'd previously written.

#### 1-Pass Matrix Sampling w/ Custom Function Effects
- A selection of effects that use custom functions following 3x3 sampling such as:
	- Dilate,
	- Erode, and
	- Emboss. TODO: Rotatable emboss direction.

#### 2-Pass Matrix Sampling Effects
- A selection of effects where we run both a horizontal pass and a vertical pass across 3x3 matrices to find edges, such as:
	- Sobel edge detection,
	- Prewitt edge detection, and
	- Scharr edge detection.
- Based on: 
	- Some GLSL code I'd previously written, and
	- pintusaini - "Edge detection using Prewitt, Scharr and Sobel Operator" - https://www.geeksforgeeks.org/edge-detection-using-prewitt-scharr-and-sobel-operator/
	- Tom Crossland - Canny Edge Detector - https://tomcrossland.com/edgedetection/

#### Rain
- A sprite-sheet animation of normals based rain effect.
- Based on: PolyToot - "Unity Shadergraph: Rain Drop Ripples!" - https://www.youtube.com/watch?v=R6EX6dN1BOs
	
## Third-Party Assets
This project uses the following (FOSS) third-party assets (thank you, devs!):

- `UnityColorPicker` by `mmaletin` - https://github.com/mmaletin/UnityColorPicker

- Cloudy normal texture (used in the rain effect shader graph) by Calimou - https://opengameart.org/node/36296
