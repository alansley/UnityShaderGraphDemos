using UnityEngine;

public class SimpleSpinner : MonoBehaviour
{
    [SerializeField] private Transform _transformToRotate;
    
    [SerializeField] private Vector3 eulerDegsPerSec = new Vector3(10f, 20f, 30f);
    
    // Start is called before the first frame update
    void Start()
    {
        if (!_transformToRotate) { _transformToRotate = this.transform; }
    }

    // Update is called once per frame
    void Update()
    {
        _transformToRotate.Rotate(eulerDegsPerSec * Time.deltaTime);
    }
}