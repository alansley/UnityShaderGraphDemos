using System;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomMatrixFunctionsShaderGraphController : MonoBehaviour
{
    [SerializeField] private GameObject _go;

    
    
    [SerializeField] private bool _autoAnimateByTime = false;

    [SerializeField] private float _speedFactor = 0.1f;

    [SerializeField] private Slider _textureOffsetSlider;
    [SerializeField] private TMP_Text _textureOffsetSliderHandleText;

    //[SerializeField] private TMP_Dropdown _effectDropdown;
    
    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private TMP_Text _brightnessSliderHandleText;
    
  //  [SerializeField] private TMP_InputField _inputField;
//    
    [SerializeField] private TMP_InputField _m00Text;
    [SerializeField] private TMP_InputField _m10Text;
    [SerializeField] private TMP_InputField _m20Text;
    [SerializeField] private TMP_InputField _m01Text;
    [SerializeField] private TMP_InputField _m11Text;
    [SerializeField] private TMP_InputField _m21Text;
    [SerializeField] private TMP_InputField _m02Text;
    [SerializeField] private TMP_InputField _m12Text;
    [SerializeField] private TMP_InputField _m22Text;
    
    private Renderer _renderer;

    private Material _testMaterial;

    /// <summary>
    /// Three vector3's to represent as 3x3 matrix (we can't pass a 3x3 matrix directly to shader graph! Boo!)
    /// </summary>
    private Vector3 m0 = new Vector3(1f, 1f, 1f);
    private Vector3 m1 = new Vector3(1f, 1f, 1f);
    private Vector3 m2 = new Vector3(1f, 1f, 1f);
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = _go.GetComponent<Renderer>();
        _testMaterial = _renderer.material;
        
        // Set texture offset slider details on the material and slider UI
        _testMaterial.SetFloat("_TextureOffset", _textureOffsetSlider.value);
        _textureOffsetSliderHandleText.text = _textureOffsetSlider.value.ToString("N4");
        
        // Set us to standard display by default
        _testMaterial.SetInt("_PerformDilate", 0);
        _testMaterial.SetInt("_PerformErode", 0);
        _testMaterial.SetInt("_PerformEmboss", 0);
        
        // Set our initial matrix values
        _testMaterial.SetVector("_Mat3_Row0", m0);
        _testMaterial.SetVector("_Mat3_Row1", m1);
        _testMaterial.SetVector("_Mat3_Row2", m2);
    }

    // Update is called once per frame
    void Update()
    {
        if (_autoAnimateByTime) { _testMaterial.SetFloat("_TextureOffset", Time.time * _speedFactor); }
    }

    public void OnTextureOffsetValueChanged(float value)
    {
        _testMaterial.SetFloat("_TextureOffset", value);
        _textureOffsetSliderHandleText.text = value.ToString("N4");
    }

    public void OnBrightnessSliderValueChanged(float value)
    {
        _testMaterial.SetFloat("_Brightness", value);
        _brightnessSliderHandleText.text = value.ToString("N2");
    }

    public void OnMatrixPresetChanged(int dropdownIndex)
    {
        Debug.Log("Matrix preset changed to index: " + dropdownIndex);
        
        switch (dropdownIndex)
        {
            // Standard
            case 0:
                _testMaterial.SetInt("_PerformDilate", 0);
                _testMaterial.SetInt("_PerformErode", 0);
                _testMaterial.SetInt("_PerformEmboss", 0);
                break;
            
            // Dilate
            case 1:
                _testMaterial.SetInt("_PerformDilate", 1);
                _testMaterial.SetInt("_PerformErode", 0);
                _testMaterial.SetInt("_PerformEmboss", 0);
                break;
            
            // Erode
            case 2:
                _testMaterial.SetInt("_PerformDilate", 0);
                _testMaterial.SetInt("_PerformErode", 1);
                _testMaterial.SetInt("_PerformEmboss", 0);
                break;
            
            // Emboss
            case 3:
                _testMaterial.SetInt("_PerformDilate", 0);
                _testMaterial.SetInt("_PerformErode", 0);
                _testMaterial.SetInt("_PerformEmboss", 1);
                break;
        }
        
        /*
        // Update the material with the new matrix values
        _testMaterial.SetVector("_Mat3_Row0", m0);
        _testMaterial.SetVector("_Mat3_Row1", m1);
        _testMaterial.SetVector("_Mat3_Row2", m2);

        // Update the InputFields w/ the new values, too
        UpdateMatrixTextInputFieldValues();
        */
    }
    
    // ----- Row 0 -----
    public void OnM00ValueChanged(string valueString)
    {
        try
        {
            m0.x = float.Parse(valueString);
            _testMaterial.SetVector("_Mat3_Row0", m0);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnM10ValueChanged(string valueString)
    {
        try
        {
            m0.y = float.Parse(valueString);
            _testMaterial.SetVector("_Mat3_Row0", m0);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnM20ValueChanged(string valueString)
    {
        try
        {
            m0.z = float.Parse(valueString);
            _testMaterial.SetVector("_Mat3_Row0", m0);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    // ----- Row 1 -----
    public void OnM01ValueChanged(string valueString)
    {
        try
        {
            m1.x = float.Parse(valueString);
            _testMaterial.SetVector("_Mat3_Row1", m1);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnM11ValueChanged(string valueString)
    {
        try
        {
            m1.y = float.Parse(valueString);
            _testMaterial.SetVector("_Mat3_Row1", m1);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnM21ValueChanged(string valueString)
    {
        try
        {
            m1.z = float.Parse(valueString);
            _testMaterial.SetVector("_Mat3_Row1", m1);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    // ----- Row 2 -----
    public void OnM02ValueChanged(string valueString)
    {
        try
        {
            m2.x = float.Parse(valueString);
            _testMaterial.SetVector("_Mat3_Row2", m2);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnM12ValueChanged(string valueString)
    {
        try
        {
            m2.y = float.Parse(valueString);
            _testMaterial.SetVector("_Mat3_Row2", m2);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }
    
    public void OnM22ValueChanged(string valueString)
    {
        try
        {
            m2.z = float.Parse(valueString);
            _testMaterial.SetVector("_Mat3_Row2", m2);
        }
        catch (Exception e) { Debug.LogWarning(e); }
    }

    private void UpdateMatrixTextInputFieldValues()
    {
        Debug.Log("Attempting update of IF!");
        
        // Row 0
        _m00Text.text = m0.x.ToString(CultureInfo.InvariantCulture);
        _m10Text.text = m0.y.ToString(CultureInfo.InvariantCulture);
        _m20Text.text = m0.z.ToString(CultureInfo.InvariantCulture);
        
        // Row 1
        _m01Text.text = m1.x.ToString(CultureInfo.InvariantCulture);
        _m11Text.text = m1.y.ToString(CultureInfo.InvariantCulture);
        _m21Text.text = m1.z.ToString(CultureInfo.InvariantCulture);
        
        // Row 2
        _m02Text.text = m2.x.ToString(CultureInfo.InvariantCulture);
        _m12Text.text = m2.y.ToString(CultureInfo.InvariantCulture);
        _m22Text.text = m2.z.ToString(CultureInfo.InvariantCulture);
    }
    
    
}