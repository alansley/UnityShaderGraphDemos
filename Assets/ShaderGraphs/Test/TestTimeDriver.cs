using UnityEngine;

public class TestTimeDriver : MonoBehaviour
{
    [SerializeField] private GameObject _go;

    [SerializeField] private bool _autoAnimateByTime = false;

    [SerializeField] private float _speedFactor = 0.1f;

    private Renderer _renderer;

    private Material _testMaterial;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = _go.GetComponent<Renderer>();
        _testMaterial = _renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (_autoAnimateByTime) { _testMaterial.SetFloat("_TextureOffset", Time.time * _speedFactor); }
    }
}