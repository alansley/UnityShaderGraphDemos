using System;
using System.Globalization;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class EdgeDetectionShaderGraphController : MonoBehaviour
{
    [SerializeField] private GameObject _go;

    // Sliders for the texture offset, original image blend factor, and brightness adjustment
    [SerializeField] private Slider _textureOffsetSlider;
    [SerializeField] private TMP_Text _textureOffsetSliderHandleText;

    [SerializeField] private Dropdown _matrixPresetDropdown;
    
    [SerializeField] private Slider _originalBlendFactorSlider;
    [SerializeField] private TMP_Text _originalBlendFactorSliderHandleText;

    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private TMP_Text _brightnessSliderHandleText;
    
    // Horizontal pass 3x3 matrix input fields
    [SerializeField] private TMP_InputField _horizM00InputField;
    [SerializeField] private TMP_InputField _horizM10InputField;
    [SerializeField] private TMP_InputField _horizM20InputField;
    [SerializeField] private TMP_InputField _horizM01InputField;
    [SerializeField] private TMP_InputField _horizM11InputField;
    [SerializeField] private TMP_InputField _horizM21InputField;
    [SerializeField] private TMP_InputField _horizM02InputField;
    [SerializeField] private TMP_InputField _horizM12InputField;
    [SerializeField] private TMP_InputField _horizM22InputField;
    
    // Vertical pass 3x3 matrix input fields
    [SerializeField] private TMP_InputField _vertM00InputField;
    [SerializeField] private TMP_InputField _vertM10InputField;
    [SerializeField] private TMP_InputField _vertM20InputField;
    [SerializeField] private TMP_InputField _vertM01InputField;
    [SerializeField] private TMP_InputField _vertM11InputField;
    [SerializeField] private TMP_InputField _vertM21InputField;
    [SerializeField] private TMP_InputField _vertM02InputField;
    [SerializeField] private TMP_InputField _vertM12InputField;
    [SerializeField] private TMP_InputField _vertM22InputField;
    
    private Renderer _renderer;
    private Material _testMaterial;

    /// <summary>
    /// Initial Sobel edge detection horizontal pass matrix values.
    /// </summary>
    private Vector3 _horizM0 = new Vector3(-1f, 0f, 1f);
    private Vector3 _horizM1 = new Vector3(-2f, 0f, 2f);
    private Vector3 _horizM2 = new Vector3(-1f, 0f, 1f);
    
    /// <summary>
    /// Initial Sobel edge detection vertical pass matrix values.
    /// </summary>
    private Vector3 _vertM0 = new Vector3(-1f, -2f, -1f);
    private Vector3 _vertM1 = new Vector3( 0f,  0f,  0f);
    private Vector3 _vertM2 = new Vector3( 1f,  2f,  1f);
    
    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        _renderer = _go.GetComponent<Renderer>();
        _testMaterial = _renderer.material;
        
        // Set texture offset slider details on the material and slider UI
        _testMaterial.SetFloat("_TextureOffset", _textureOffsetSlider.value);
        _textureOffsetSliderHandleText.text = _textureOffsetSlider.value.ToString("N4");
        
        // Set blend factor slider details on the material and slider UI
        _testMaterial.SetFloat("_OriginalTextureBlendFactor", _originalBlendFactorSlider.value);
        _originalBlendFactorSliderHandleText.text = _originalBlendFactorSlider.value.ToString("N2");
        
        // Set brightness slider details on the material and slider UI
        _testMaterial.SetFloat("_Brightness", _brightnessSlider.value);
        _brightnessSliderHandleText.text = _brightnessSlider.value.ToString("N2");
        
        // Set our initial matrix values in the material
        _testMaterial.SetVector("_HorizMat3_Row0", _horizM0);
        _testMaterial.SetVector("_HorizMat3_Row1", _horizM1);
        _testMaterial.SetVector("_HorizMat3_Row2", _horizM2);
        _testMaterial.SetVector("_VertMat3_Row0", _vertM0);
        _testMaterial.SetVector("_VertMat3_Row1", _vertM1);
        _testMaterial.SetVector("_VertMat3_Row2", _vertM2);
    }

    public void OnTextureOffsetValueChanged(float value)
    {
        _testMaterial.SetFloat("_TextureOffset", value);
        _textureOffsetSliderHandleText.text = value.ToString("N4");
    }

    public void OnOriginalBlendFactorValueChanged(float value)
    {
        _testMaterial.SetFloat("_OriginalTextureBlendFactor", value);
        _originalBlendFactorSliderHandleText.text = value.ToString("N2");
    }
    
    public void OnBrightnessSliderValueChanged(float value)
    {
        _testMaterial.SetFloat("_Brightness", value);
        _brightnessSliderHandleText.text = value.ToString("N2");
    }

    public void OnMatrixPresetDropdownValueChanged(int index)
    {
        switch (index)
        {
            // Sobel edge detection
            case 0:
                _horizM0 = new Vector3(-1f, 0f, 1f);
                _horizM1 = new Vector3(-2f, 0f, 2f);
                _horizM2 = new Vector3(-1f, 0f, 1f);
                _vertM0 = new Vector3(-1f, -2f, -1f);
                _vertM1 = new Vector3( 0f,  0f,  0f);
                _vertM2 = new Vector3( 1f,  2f,  1f);
                break;
            
            // Prewitt edge detection
            case 1: 
                _horizM0 = new Vector3(-1f, 0f, 1f);
                _horizM1 = new Vector3(-1f, 0f, 1f);
                _horizM2 = new Vector3(-1f, 0f, 1f);
                _vertM0 = new Vector3(-1f, -1f, -1f);
                _vertM1 = new Vector3( 0f,  0f,  0f);
                _vertM2 = new Vector3( 1f,  1f,  1f);
                break;
            
            // Scharr edge detection
            case 2:
                _horizM0 = new Vector3(-3f,  0f, 3f);
                _horizM1 = new Vector3(-10f, 0f, 10f);
                _horizM2 = new Vector3(-3f,  0f, 3f);
                _vertM0 = new Vector3(3f, 10f, 3f);
                _vertM1 = new Vector3( 0f,  0f,  0f);
                _vertM2 = new Vector3( -3f,  -10f,  -3f);
                break;
        }
        
        // Update the matrix data in the material
        _testMaterial.SetVector("_HorizMat3_Row0", _horizM0);
        _testMaterial.SetVector("_HorizMat3_Row1", _horizM1);
        _testMaterial.SetVector("_HorizMat3_Row2", _horizM2);
        _testMaterial.SetVector("_VertMat3_Row0", _vertM0);
        _testMaterial.SetVector("_VertMat3_Row1", _vertM1);
        _testMaterial.SetVector("_VertMat3_Row2", _vertM2);
        
        UpdateMatrixTextInputFieldValues();
    }

    // ---------- Matrix Value Setters ----------
    
    // ----- Horizontal Pass Matrix -----
    // ----- Row 0 -----
    public void OnHorizM00ValueChanged(string valueString)
    {
        try
        {
            _horizM0.x = float.Parse(valueString);
            _testMaterial.SetVector("_HorizMat3_Row0", _horizM0);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnHorizM10ValueChanged(string valueString)
    {
        try
        {
            _horizM0.y = float.Parse(valueString);
            _testMaterial.SetVector("_HorizMat3_Row0", _horizM0);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnHorizM20ValueChanged(string valueString)
    {
        try
        {
            _horizM0.z = float.Parse(valueString);
            _testMaterial.SetVector("_HorizMat3_Row0", _horizM0);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    // ----- Row 1 -----
    public void OnHorizM01ValueChanged(string valueString)
    {
        try
        {
            _horizM1.x = float.Parse(valueString);
            _testMaterial.SetVector("_HorizMat3_Row1", _horizM1);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnHorizM11ValueChanged(string valueString)
    {
        try
        {
            _horizM1.y = float.Parse(valueString);
            _testMaterial.SetVector("_HorizMat3_Row1", _horizM1);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnHorizM21ValueChanged(string valueString)
    {
        try
        {
            _horizM1.z = float.Parse(valueString);
            _testMaterial.SetVector("_HorizMat3_Row1", _horizM1);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    // ----- Row 2 -----
    public void OnHorizM02ValueChanged(string valueString)
    {
        try
        {
            _horizM2.x = float.Parse(valueString);
            _testMaterial.SetVector("_HorizMat3_Row2", _horizM2);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnHorizM12ValueChanged(string valueString)
    {
        try
        {
            _horizM2.y = float.Parse(valueString);
            _testMaterial.SetVector("_HorizMat3_Row2", _horizM2);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnHorizM22ValueChanged(string valueString)
    {
        try
        {
            _horizM2.z = float.Parse(valueString);
            _testMaterial.SetVector("_HorizMat3_Row2", _horizM2);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    // ----- Vertical Pass Matrix -----
    // ----- Row 0 -----
    public void OnVertM00ValueChanged(string valueString)
    {
        try
        {
            _vertM0.x = float.Parse(valueString);
            _testMaterial.SetVector("_VertMat3_Row0", _vertM0);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnVertM10ValueChanged(string valueString)
    {
        try
        {
            _vertM0.y = float.Parse(valueString);
            _testMaterial.SetVector("_VertMat3_Row0", _vertM0);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnVertM20ValueChanged(string valueString)
    {
        try
        {
            _vertM0.z = float.Parse(valueString);
            _testMaterial.SetVector("_VertMat3_Row0", _vertM0);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    // ----- Row 1 -----
    public void OnVertM01ValueChanged(string valueString)
    {
        try
        {
            _vertM1.x = float.Parse(valueString);
            _testMaterial.SetVector("_VertMat3_Row1", _vertM1);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnVertM11ValueChanged(string valueString)
    {
        try
        {
            _vertM1.y = float.Parse(valueString);
            _testMaterial.SetVector("_VertMat3_Row1", _vertM1);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnVertM21ValueChanged(string valueString)
    {
        try
        {
            _vertM1.z = float.Parse(valueString);
            _testMaterial.SetVector("_VertMat3_Row1", _vertM1);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    // ----- Row 2 -----
    public void OnVertM02ValueChanged(string valueString)
    {
        try
        {
            _vertM2.x = float.Parse(valueString);
            _testMaterial.SetVector("_VertMat3_Row2", _vertM2);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnVertM12ValueChanged(string valueString)
    {
        try
        {
            _vertM2.y = float.Parse(valueString);
            _testMaterial.SetVector("_VertMat3_Row2", _vertM2);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnVertM22ValueChanged(string valueString)
    {
        try
        {
            _vertM2.z = float.Parse(valueString);
            _testMaterial.SetVector("_VertMat3_Row2", _vertM2);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }

    private void UpdateMatrixTextInputFieldValues()
    {
        Debug.Log("Attempting update of IF!");
        
        // Horizontal 3x3 Pass
        // Row 0
        _horizM00InputField.text = _horizM0.x.ToString(CultureInfo.InvariantCulture);
        _horizM10InputField.text = _horizM0.y.ToString(CultureInfo.InvariantCulture);
        _horizM20InputField.text = _horizM0.z.ToString(CultureInfo.InvariantCulture);
        
        // Row 1
        _horizM01InputField.text = _horizM1.x.ToString(CultureInfo.InvariantCulture);
        _horizM11InputField.text = _horizM1.y.ToString(CultureInfo.InvariantCulture);
        _horizM21InputField.text = _horizM1.z.ToString(CultureInfo.InvariantCulture);
        
        // Row 2
        _horizM02InputField.text = _horizM2.x.ToString(CultureInfo.InvariantCulture);
        _horizM12InputField.text = _horizM2.y.ToString(CultureInfo.InvariantCulture);
        _horizM22InputField.text = _horizM2.z.ToString(CultureInfo.InvariantCulture);
        
        // Vertical 3x3 Pass
        // Row 0
        _vertM00InputField.text = _vertM0.x.ToString(CultureInfo.InvariantCulture);
        _vertM10InputField.text = _vertM0.y.ToString(CultureInfo.InvariantCulture);
        _vertM20InputField.text = _vertM0.z.ToString(CultureInfo.InvariantCulture);
        
        // Row 1
        _vertM01InputField.text = _vertM1.x.ToString(CultureInfo.InvariantCulture);
        _vertM11InputField.text = _vertM1.y.ToString(CultureInfo.InvariantCulture);
        _vertM21InputField.text = _vertM1.z.ToString(CultureInfo.InvariantCulture);
        
        // Row 2
        _vertM02InputField.text = _vertM2.x.ToString(CultureInfo.InvariantCulture);
        _vertM12InputField.text = _vertM2.y.ToString(CultureInfo.InvariantCulture);
        _vertM22InputField.text = _vertM2.z.ToString(CultureInfo.InvariantCulture);
    }
}