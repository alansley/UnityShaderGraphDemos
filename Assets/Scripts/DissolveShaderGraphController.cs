using UnityEngine;
using UnityEngine.EventSystems;

using TMPro;
using UnityEngine.Serialization;

/// <summary>
/// Class to allow us to adjust various parameters of the shockwave shader graph at runtime.
/// </summary>
public class DissolveShaderGraphController : MonoBehaviour
{
    /// <summary>
    /// The GameObject to which the shockwave effect is being applied.
    /// </summary>
    [SerializeField] private GameObject _gameObject;

    // ---------- Parameters to control the shader graph's exposed variables ----------

    /// <summary>
    /// How quickly the model rotates.
    /// </summary>
    [SerializeField] private float _rotationSpeedFactor = 0.1f;

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

    [SerializeField] private TMP_Text _rotationSpeedFactorValueText;



    [SerializeField] private TMP_Text _shockwaveStrengthSliderValueText;
    [SerializeField] private TMP_Text _shockwaveSpeedSliderValueText;
    [SerializeField] private TMP_Text _shockwaveAspectRatioSliderValueText;
    [SerializeField] private TMP_Text _shockwaveXOriginSliderValueText;
    [SerializeField] private TMP_Text _shockwaveYOriginSliderValueText;

    /// <summary>
    /// The shockwave material on which we are adjusting exposed variables.
    /// </summary>
    private Material _dissolveMaterial;

    /// <summary>
    /// We need the camera for the screen-space -> model UV lookups so we'll cache it.
    /// </summary>
    private Camera _camera;
    
    /// <summary>
    /// To update the shockwave origin from click or drag we'll keep track of whether there's a pointer down in the flag.
    /// </summary>
    private bool _shouldUpdateShockwaveOrigin = false;

    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        // Grab the camera
        _camera = Camera.main;

        // Grab this GameObject if a specific one wasn't provided
        if (!_gameObject) { _gameObject = this.gameObject; }

        _dissolveMaterial = _gameObject.GetComponent<MeshRenderer>().material;
        if (!_dissolveMaterial) { Debug.LogError("Could not find dissolve material / MeshRenderer!"); }

        // Set all serialized default values on the material (we call the value-changed handlers to so it also updates
        // the UI).
        /*
        OnShockwaveXOriginChanged(_focalPointTS.x);
        OnShockwaveYOriginChanged(_focalPointTS.y);
        OnShockwaveSizeValueChanged(_rippleSize);
        OnShockwaveStrengthValueChanged(_magnificationStrength);
        OnShockwaveSpeedValueChanged(_rippleSpeedFactor);
        OnShockwaveAspectRatioValueChanged(_aspectRatio);
        */
    }

    /*
    /// <summary>
    /// Method to update the size of the shockwave when the relevant slider changes.
    /// </summary>
    /// <param name="value">The new size of the shockwave (-1f to +1f)</param>
    public void OnRotationSpeedChangedShockwaveSizeValueChanged(float value)
    {
        _dissolveMaterial.SetFloat("_RippleSize_TS", value);
        _shockwaveSizeSliderValueText.text = value.ToString("N2");
    }

    /// <summary>
    /// Method to update the strength of the shockwave when the relevant slider changes.
    /// </summary>
    /// <param name="value">The new size of the shockwave (-1f to +1f)</param>
    public void OnShockwaveStrengthValueChanged(float value)
    {
        _dissolveMaterial.SetFloat("_MagnificationStrength_TS", value);
        _shockwaveStrengthSliderValueText.text = value.ToString("N2");
    }

    /// <summary>
    /// Method to update the speed of the shockwave when the relevant slider changes.
    /// </summary>
    /// <param name="value">The new size of the shockwave (-1f to +1f)</param>
    public void OnShockwaveSpeedValueChanged(float value)
    {
        _dissolveMaterial.SetFloat("_RippleSpeedFactor", value);
        _shockwaveSpeedSliderValueText.text = value.ToString("N2");
    }

    /// <summary>
    /// Method to update the aspect ratio of the shockwave when the relevant slider changes.
    /// </summary>
    /// <param name="value">The new size of the shockwave (-1f to +1f)</param>
    public void OnShockwaveAspectRatioValueChanged(float value)
    {
        _dissolveMaterial.SetFloat("_AspectRatio", value);
        _shockwaveAspectRatioSliderValueText.text = value.ToString("N2");
    }
    
    /// <summary>
    /// Method to update the aspect ratio of the shockwave when the relevant slider changes.
    /// </summary>
    /// <param name="value">The new size of the shockwave (-1f to +1f)</param>
    public void OnShockwaveXOriginChanged(float value)
    {
        var origin = _dissolveMaterial.GetVector("_FocalPoint_TS");
        origin.x = value;
        _dissolveMaterial.SetVector("_FocalPoint_TS", origin);
        _shockwaveXOriginSliderValueText.text = value.ToString("N2");
    }

    /// <summary>
    /// Method to update the aspect ratio of the shockwave when the relevant slider changes.
    /// </summary>
    /// <param name="value">The new size of the shockwave (-1f to +1f)</param>
    public void OnShockwaveYOriginChanged(float value)
    {
        var origin = _dissolveMaterial.GetVector("_FocalPoint_TS");
        origin.y = value;
        _dissolveMaterial.SetVector("_FocalPoint_TS", origin);
        _shockwaveYOriginSliderValueText.text = value.ToString("N2");
    }

    
    
    /// <summary>
    /// Method to find the texture coordinate of the point we clicked on in screen-space on the object we clicked on,
    /// and set the origin of the shockwave to be that location! As we implement `IPointerDownHandler` we must implement
    /// this method.
    ///
    /// Code adjusted from: https://docs.unity3d.com/ScriptReference/RaycastHit-textureCoord.html
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        _shouldUpdateShockwaveOrigin = true;
        SetOriginFromEventdata(eventData);
    }

    /// <summary>
    /// Method that actually does the math to find the UV point clicked on. We separate this out so we can call it both
    /// from OnPointerDown and OnPointerMove (if the 'should-update' flag is true).
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    private void SetOriginFromEventdata(PointerEventData eventData)
    {
        // Check if we've hit anything during the 'click' and return if not..
        RaycastHit hit;
        if (!Physics.Raycast(_camera.ScreenPointToRay(eventData.position), out hit))
            return;

        // ..but if we have clicked on our shockwave object set the shockwave origin from the clicked UV
        Vector2 pixelUV = hit.textureCoord;
        //Debug.Log("Pixel UV: " + pixelUV);
        OnShockwaveXOriginChanged(pixelUV.x);
        OnShockwaveYOriginChanged(pixelUV.y);
    }

    /// <summary>
    /// Method to reset the shockwave origin update flag when the pointer is removed (LMB released / finger up).
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerUp(PointerEventData eventData) { _shouldUpdateShockwaveOrigin = false; }

    /// <summary>
    /// Method to update the shockwave origin from the pointer event data's looked up UV if we've clicked + draggeing /
    /// pointer down and moving across touchscreen.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerMove(PointerEventData eventData)
    {
        if (_shouldUpdateShockwaveOrigin) { SetOriginFromEventdata(eventData); }
    }
    */
}