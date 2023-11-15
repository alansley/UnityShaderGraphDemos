using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ShockWaveShaderGraphController : MonoBehaviour
{
    /// <summary>
    /// The GameObject to which the shockwave effect is being applied.
    /// </summary>
    [SerializeField] private GameObject _shockwaveGO;

    /// <summary>
    /// Whether clicking on the model updates the focal point of the shockwave (i.e., where the shockwave originates
    /// from) or not.
    /// </summary>
    [SerializeField] private bool _clickToSetFocalPoint = false;
    
    // ---------- Parameters to control the shader graph's exposed variables ----------
    
    /// <summary>
    /// Where the shockwave originates from. (0.5, 0.5) is the centre of the model (0.0, 0.0) is the bottom-left, and
    /// (1.0, 1.0) is the top-right).
    /// </summary>
    [SerializeField] private Vector2 _focalPointTS = new Vector2(0.5f, 0.5f);

    /// <summary>
    /// How large is the 'ring' of the shockwave ripple. 0.1 means 10% of the model size.
    /// </summary>
    [SerializeField] private float _rippleSize = 0.1f;
    
    /// <summary>
    /// How much the shockwave magnifies the area it's passing over - typically this is a negative value so that it
    /// 'pushes' the pixels it traverses outwards. If a positive value is used then it 'pulls' the pixels it's about to
    /// pass over _into_ the shockwave (like a 'pinch' rather than a 'punch').
    /// </summary>
    [SerializeField] private float _magnificationStrength = -0.1f;

    /// <summary>
    /// How often the shockwave ripples occur. 1f means one shockwave per second.
    /// </summary>
    [SerializeField] private float _rippleSpeedFactor = 1f;

    /// <summary>
    /// The aspect ratio of the shockwave. A value of 1 is round, while values greater than 1 result in a tall oblong
    /// and values less than 1 result in a wide oblong. 
    /// </summary>
    [SerializeField] private float _aspectRatio = 1f;
    
    // ---------- UI Controls ----------
    
    [SerializeField] private TMP_Text _shockwaveSizeSliderValueText;
    
    [SerializeField] private TMP_Text _shockwaveStrengthSliderValueText;
    
    

    // ---------- Private stuff ----------
    
    private Material _shockwaveMaterial;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (!_shockwaveGO) { Debug.LogError("Please attach a shockwave object!"); }
        
        _shockwaveMaterial = _shockwaveGO.GetComponent<MeshRenderer>().material;
        if (!_shockwaveMaterial) { Debug.LogError("Could not find shockwave material / MeshRenderer!"); }
        
        //_shockwaveMaterial.SetFloat("_RippleSize_TS", -0.2f);
        
        //_shockwaveMaterial.SetFloat("_RippleSpeedFactor", 3f);
    }

    /// <summary>
    /// Method to update the size of the shockwave when the relevant slider changes.
    /// </summary>
    /// <param name="value">The new size of the shockwave (-1f to +1f)</param>
    public void OnShockwaveSizeValueChanged(float value)
    {
        _shockwaveMaterial.SetFloat("_RippleSize_TS", value);
        _shockwaveSizeSliderValueText.text = value.ToString("N2");
    }
    
    /// <summary>
    /// Method to update the strength of the shockwave when the relevant slider changes.
    /// </summary>
    /// <param name="value">The new size of the shockwave (-1f to +1f)</param>
    public void OnShockwaveStrengthValueChanged(float value)
    {
        _shockwaveMaterial.SetFloat("_MagnificationStrength_TS", value);
        _shockwaveStrengthSliderValueText.text = value.ToString("N2");
    }
}
