using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRotator : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectToRotate;

    [SerializeField] private float _degsPerSecond = 10f;
    
    private Quaternion _rotation;

    private Vector3 _centralBoundsPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!_gameObjectToRotate) { _gameObjectToRotate = this.gameObject; }
        
        // Grab the center of the GameObject / 3D Model / Whatever via its bound
        _centralBoundsPosition = _gameObjectToRotate.GetComponent<Renderer>().bounds.center;
    }

    // Update is called once per frame
    void Update()
    {
        //var eulers = this.transform.localRotation.eulerAngles;
        //eulers.x += _degsPerSecond * Time.deltaTime;
        //this.transform.localRotation = Quaternion.Euler(eulers);

        
        _gameObjectToRotate.transform.RotateAround(_centralBoundsPosition, Vector3.up, _degsPerSecond * Time.deltaTime);
    }
}
