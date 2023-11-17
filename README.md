# UnityShaderGraphDemos
A suite of example Unity URP Shader Graph effects - just because I wanted to learn how to use shader graph.

~~All examples have UI 'twiddle' controls so you can adjust them in-app rather than just in the Unity editor.

Tested and working in Unity 2022.3.13f1 on Windows, Linux, and Android*.

* = Full-screen shader graphs don't seem to work on Android, not sure why atm - will have to look into it.~~

## Shader Graph Effects
All effects built from scratch using the provided 'based on' source material - massive thanks to the authors of these wonderful resources! `<3` 

### Shockwave
	A screen-space shockwave / ripple effect.
	Based on: GameDevBill - "Shockwave Shader Graph - How to make a show wave shader in Unity URP/HDRP" - https://www.youtube.com/watch?v=dFDAwT5iozo
	
### Dissolve
	A model-space noisy dissolve with coloured edges. TODO: Make this directional!
	Based on: Brackeys - "Dissolve using Unity Shader Graph" - https://www.youtube.com/watch?v=taMp1g1pBeE

### Hologram
	A model-space transparent hologram effect with animated scanlines and noise.
	Based on: TODO: Look it up - can't remember!

### Force-field / Intersection
	A model-space "glowing-intersection" effect where a model intersects with another. Useful for force-fields / holograms and such.
	Based on a combination of:
		Gabriel Anguiar Prod. - "Unity Shader Graph - Intersection Effect Tutorial" - https://www.youtube.com/watch?v=Uyw5yBFEoXo
		Brackeys - "Force Field in Unity" - https://www.youtube.com/watch?v=NiOGWZXBg4Y
	Note: Requires "Depth Texture" and "Opaque Texture" to be enabled in the URP settings (in 'Assets\Settings\URP-HighFidelity.asset' etc.)
	
	
## Third-Party Assets
This project uses the following (FOSS) third-party assets (thank you, devs!):

- `UnityColorPicker` by `mmaletin` - https://github.com/mmaletin/UnityColorPicker	
