
using System;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UnityEngine.Serialization;

public class MandelbrotAndJuliaFractalShaderGraphController : MonoBehaviour
{
    [SerializeField] private GameObject _shaderGraph_GO;

    /// <summary>
    /// UI Controls to manipulate the ShaderGraph / Material.
    /// </summary>
    //[SerializeField] private Slider _horizResolutionSlider;
    [SerializeField] private TMP_Text _resolutionSliderLabel;
    [SerializeField] private TMP_Text _maxIterationsSliderLabel;
    [SerializeField] private TMP_Text _zoomFactorSliderLabel;
    [SerializeField] private TMP_InputField _horizRegionSizeInputField;
    [SerializeField] private TMP_InputField _vertRegionSizeInputField;
    [SerializeField] private TMP_InputField _horizRegionCentreInputField;
    [SerializeField] private TMP_InputField _vertRegionCentreInputField;
    [SerializeField] private TMP_Text _rotationDegsSliderLabel;
    [SerializeField] private TMP_Text _rotationHorizPivotSliderLabel;
    [SerializeField] private TMP_Text _rotationVertPivotSliderLabel;
    
    
    //[SerializeField] private Slider _zoomFactorSlider;
    
    private Material _shaderGraphMaterial;

    /// <summary>
    /// Shader property IDs.
    /// </summary>
    private int _textureResolutionID;
    private int _regionSizeID;
    private int _regionCentreID;
    private int _maxIterationsID;
    private int _zoomFactorID;
    private int _rotationDegsID;
    private int _rotationPivotID;
    private int _applyColourGradientID;
    private int _autoZoomID;
    private int _autoRotateID;

    /// <summary>
    /// Vectors for properties that we can adjust via separate sliders but are actually the same piece of data - we're
    /// just modifying the .x or .y parts of it per slider.
    /// </summary>
    private Vector2 _textureResolution;
    private float _maxIterations;
    private float _zoomFactor;
    private Vector2 _regionSize;
    private Vector2 _regionCentre;
    private float _rotationDegs;
    private Vector2 _rotationPivotUV;
    private float _autoZoom;
    private float _autoRotate;
    
    
    // While `_ApplyColourGradient` is declared in the ShaderGraph as a Boolean - internally to the shader it sees it as a Float!
    // Further reading: https://docs.unity3d.com/Packages/com.unity.shadergraph@14.0/manual/Property-Types.html
    private float _applyColourGradient;
    
    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        // Warn if there's name ShaderGraph GameObject attached
        if (!_shaderGraph_GO) { Debug.LogWarning("Please connect the ShaderGraph GameObject!"); }

        // Grab the material on the ShaderGraph GameObject and moan if we couldn't find one
        _shaderGraphMaterial = _shaderGraph_GO.GetComponent<MeshRenderer>().material;
        if (!_shaderGraphMaterial) { Debug.LogWarning("Could not obtain ShaderGraph Material!"); }

        // Get the shader property IDs
        _textureResolutionID = Shader.PropertyToID("_TextureResolution");
        _regionSizeID        = Shader.PropertyToID("_RegionSize");
        _regionCentreID      = Shader.PropertyToID("_RegionCentre");
        _maxIterationsID     = Shader.PropertyToID("_MaxIterations");
        _zoomFactorID        = Shader.PropertyToID("_ZoomFactor");
        _rotationDegsID      = Shader.PropertyToID("_RotationDegs");
        _rotationPivotID     = Shader.PropertyToID("_RotationPivotUV");
        _applyColourGradientID = Shader.PropertyToID("_ApplyColourGradient");
        _autoZoomID = Shader.PropertyToID("_AutoZoom");
        _autoRotateID = Shader.PropertyToID("_AutoRotate");
        
        // Grab some initial values
        _textureResolution = _shaderGraphMaterial.GetVector(_textureResolutionID);
        _regionSize = _shaderGraphMaterial.GetVector(_regionSizeID);
        _regionCentre = _shaderGraphMaterial.GetVector(_regionCentreID);
        _rotationDegs = _shaderGraphMaterial.GetFloat(_rotationDegsID);
        _rotationPivotUV = _shaderGraphMaterial.GetVector(_rotationPivotID);
        _applyColourGradient = _shaderGraphMaterial.GetFloat(_applyColourGradientID);
        _autoZoom = _shaderGraphMaterial.GetFloat(_autoZoomID);
        _autoRotate = _shaderGraphMaterial.GetFloat(_autoRotateID);
    }

    public void OnResolutionValueChanged(float value)
    {
        _textureResolution.x = _textureResolution.y = value;
        _shaderGraphMaterial.SetVector(_textureResolutionID, _textureResolution);
        _resolutionSliderLabel.text = value.ToString();
    }
    
    public void OnMaxIterationsValueChanged(float value)
    {
        _maxIterations = value;
        _shaderGraphMaterial.SetFloat(_maxIterationsID, _maxIterations);
        _maxIterationsSliderLabel.text = value.ToString();
    }
    
    public void OnZoomFactorValueChanged(float value)
    {
        _zoomFactor = value;
        _shaderGraphMaterial.SetFloat(_zoomFactorID, _zoomFactor);
        _zoomFactorSliderLabel.text = value.ToString("N2");
    }

    public void OnHorizRegionSizeValueChanged()
    {
        try
        {
            string s = _horizRegionSizeInputField.text;
            float f = float.Parse(s);
            _regionSize.x = f;
            _shaderGraphMaterial.SetVector(_regionSizeID, _regionSize);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Horizontal region size value must be a float.");
        }
    }
    
    public void OnVertRegionSizeValueChanged()
    {
        try
        {
            string s = _vertRegionSizeInputField.text;
            float f = float.Parse(s);
            _regionSize.y = f;
            _shaderGraphMaterial.SetVector(_regionSizeID, _regionSize);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Vertical region size value must be a float.");
        }
    }
    
    public void OnHorizRegionCentreValueChanged()
    {
        try
        {
            string s = _horizRegionCentreInputField.text;
            float f = float.Parse(s);
            _regionCentre.x = f;
            _shaderGraphMaterial.SetVector(_regionCentreID, _regionCentre);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Horizontal region centre value must be a float.");
        }
    }
    
    public void OnVertRegionCentreValueChanged()
    {
        try
        {
            string s = _vertRegionCentreInputField.text;
            float f = float.Parse(s);
            _regionCentre.y = f;
            _shaderGraphMaterial.SetVector(_regionCentreID, _regionCentre);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Vertical region centre value must be a float.");
        }
    }

    public void OnRotationDegsValueChanged(float value)
    {
        _rotationDegs = value;
        _shaderGraphMaterial.SetFloat(_rotationDegsID, _rotationDegs);
        _rotationDegsSliderLabel.text = _rotationDegs.ToString("N1");
    }
    
    
    public void OnRotationHorizPivotValueChanged(float value)
    {
        _rotationPivotUV.x = value;
        _shaderGraphMaterial.SetVector(_rotationPivotID, _rotationPivotUV);
        _rotationHorizPivotSliderLabel.text = _rotationPivotUV.x.ToString("N2");
    }
    
    public void OnRotationVertPivotValueChanged(float value)
    {
        _rotationPivotUV.y = value;
        _shaderGraphMaterial.SetVector(_rotationPivotID, _rotationPivotUV);
        _rotationVertPivotSliderLabel.text = _rotationPivotUV.y.ToString("N2");
    }

    public void OnApplyColourGradientValueChanged(bool value)
    {
        _applyColourGradient = value ? 1f : 0f;
        _shaderGraphMaterial.SetFloat(_applyColourGradientID, _applyColourGradient);
    }
    
    public void OnAutoZoomToggleValueChanged(bool value)
    {
        _autoZoom = value ? 1f : 0f;
        _shaderGraphMaterial.SetFloat(_autoZoomID, _autoZoom);
    }
    
    public void OnAutoRotateToggleValueChanged(bool value)
    {
        _autoRotate = value ? 1f : 0f;
        _shaderGraphMaterial.SetFloat(_autoRotateID, _autoRotate);
    }

}
