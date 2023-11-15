using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RendererFeatureController : MonoBehaviour
{
    [SerializeField] private UniversalRendererData _universalRendererData;

    [SerializeField] private Color _blackAndWhiteColour = Color.white;

    [SerializeField] private Color _sepiaToneColour = new Color(0.439f, 0.259f, 0.078f, 1.0f);
    
    private FullScreenPassRendererFeature _colourChangeRendererFeature;
    
    private const string ColourTintShaderGraphVariableName = "_TintColour";
    
    private Color _originalColour;
    
    
    void Awake()
    {
        // Grab the renderer feature
        _colourChangeRendererFeature = _universalRendererData.rendererFeatures.OfType<FullScreenPassRendererFeature>().FirstOrDefault();

        // Take a copy of the original tint colour
        _originalColour = _colourChangeRendererFeature.passMaterial.GetColor(ColourTintShaderGraphVariableName);
        
        /*
        // Set the new tint colour
        _colourChangeRendererFeature.passMaterial.SetColor(ColourTintShaderGraphVariableName, Color.blue);
        
        _colourChangeRendererFeature.SetActive(false);
        
        // Set the renderer data as dirty to indicate something has changed and any pre-calculated data should be re-done
        _universalRendererData.SetDirty();

        PrintRendererFeatures();
        */
    }

    /// <summary>
    /// Method where we can enable/disable renderer features via querying for them rather than requiring a serialized field.
    /// Just for demonstration purposes.
    /// </summary>
    private void PrintRendererFeatures()
    {
        // Get the current renderer
        var renderer = (GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset).GetRenderer(0);
        
        // Get the 'renderFeatures' property
        var property = typeof(ScriptableRenderer).GetProperty("rendererFeatures", BindingFlags.NonPublic | BindingFlags.Instance);
 
        // Get a list of all scriptable renderer features from the renderer features property of the renderer... JFC...
        List<ScriptableRendererFeature> features = property.GetValue(renderer) as List<ScriptableRendererFeature>;
 
        foreach (var feature in features)
        {
            Debug.Log(feature);

            if (feature.GetType() == typeof(FullScreenPassRendererFeature))
            {
                feature.SetActive(true);
                
                (feature as FullScreenPassRendererFeature).passMaterial.SetColor(ColourTintShaderGraphVariableName, Color.red);
            }
        }
    }

    /// <summary>
    /// Dropdown handler method.
    /// </summary>
    /// <param name="index">Index of the dropdown.</param>
    public void OnDropDownValueChanged(int index)
    {
        switch (index)
        {
            case 0:
                _colourChangeRendererFeature.SetActive(false);
                break;
            case 1:
                _colourChangeRendererFeature.SetActive(true);
                _colourChangeRendererFeature.passMaterial.SetColor(ColourTintShaderGraphVariableName, _blackAndWhiteColour);
                break;
            case 2:
                _colourChangeRendererFeature.SetActive(true);
                _colourChangeRendererFeature.passMaterial.SetColor(ColourTintShaderGraphVariableName, _sepiaToneColour);
                break;
        }
        
        // Set the renderer data as dirty to indicate something has changed and any pre-calculated data should be re-done
        _universalRendererData.SetDirty();
    }
    
    private void OnApplicationQuit()
    {
        _colourChangeRendererFeature.SetActive(false);
        
        // Restore the original tint colour when the app stops playing (this works in the editor)
        _colourChangeRendererFeature.passMaterial.SetColor(ColourTintShaderGraphVariableName, _originalColour);
    }
}
