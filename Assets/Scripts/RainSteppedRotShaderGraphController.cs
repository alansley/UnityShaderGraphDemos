using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RainSteppedRotShaderGraphController : MonoBehaviour
{
    [SerializeField] private GameObject _effectGameObject;

    //[SerializeField] private Slider _baseTextureTilingSlider;
    [SerializeField] private TMP_Text _baseTextureTilingSliderHandleText;

    //[SerializeField] private Slider _baseTextureSmoothnessSlider;
    [SerializeField] private TMP_Text _baseTextureSmoothnessSliderHandleText;



    private Material _effectMaterial;

    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        // Grab the camera
        //_camera = Camera.main;

        // Grab this GameObject if a specific one wasn't provided
        if (!_effectGameObject) { _effectGameObject = this.gameObject; }

        _effectMaterial = _effectGameObject.GetComponent<MeshRenderer>().material;
        if (!_effectMaterial) { Debug.LogError("Could not find dissolve material / MeshRenderer!"); }
    }

    // Update is called once per frame
    //void Update() { }

    public void OnBaseTextureTilingSliderValueChanged(float value)
    {
        _effectMaterial.SetFloat("_BaseTextureTilingFactor", value);
        _baseTextureTilingSliderHandleText.text = value.ToString("N2");
    }

    public void OnBaseTextureSmoothnessSliderValueChanged(float value)
    {
        _effectMaterial.SetFloat("_BaseTextureSmoothness", value);
        _baseTextureSmoothnessSliderHandleText.text = value.ToString("N2");
    }
}