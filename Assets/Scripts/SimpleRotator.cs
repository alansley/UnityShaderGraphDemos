using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [SerializeField] private Transform _transformToRotate;
    
    [SerializeField] private Vector3 _rotDegsPerSec = new Vector3(10f, 20f, 30f);
    
    // Start is called before the first frame update
    void Start()
    {
        if (!_transformToRotate) { _transformToRotate = this.transform; }
    }

    // Update is called once per frame
    void Update()
    {
        // Grab the current rotation
        var rotation = _transformToRotate.eulerAngles;

        // Add the x/y/z rotation adjustments
        var deltaTime = Time.deltaTime;
        rotation.x += _rotDegsPerSec.x * deltaTime;
        rotation.y += _rotDegsPerSec.y * deltaTime;
        rotation.z += _rotDegsPerSec.z * deltaTime;

        // Assign the new rotation back to the transform
        _transformToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
