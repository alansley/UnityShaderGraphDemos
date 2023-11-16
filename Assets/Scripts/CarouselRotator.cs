using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarouselRotator : MonoBehaviour
{
    [SerializeField] private int _numShaderGraphsOnCarousel = 8;
    
    private Quaternion _currentRotation;

    /// <summary>
    /// How many degrees we step to show the next shader graph when rotating the carousel.
    /// </summary>
    private float _degStep;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentRotation = this.transform.rotation;

        
        _degStep = 360f / _numShaderGraphsOnCarousel;

    }

    // Update is called once per frame
    void Update2()
    {
        _currentRotation = this.transform.rotation;
        float currentYRot = _currentRotation.eulerAngles.y;

        float rotDegsPerFrame = Time.deltaTime * 10f;
        
        _currentRotation = Quaternion.Euler(0f, currentYRot + rotDegsPerFrame, 0f);
        
        this.transform.rotation = _currentRotation;
    }

    /// <summary>
    /// Method to rotate the carousel to show the shader graph to the left.
    /// </summary>
    public void OnRotateToShowLeft()
    {
        _currentRotation = this.transform.rotation;
        float currentYRot = _currentRotation.eulerAngles.y;
        currentYRot -= _degStep;
        _currentRotation = Quaternion.Euler(0f, currentYRot, 0f);
        this.transform.rotation = _currentRotation;
    }
    
    /// <summary>
    /// Method to rotate the carousel to show the shader graph to the right.
    /// </summary>
    public void OnRotateToShowRight()
    {
        _currentRotation = this.transform.rotation;
        float currentYRot = _currentRotation.eulerAngles.y;
        currentYRot += _degStep;
        _currentRotation = Quaternion.Euler(0f, currentYRot, 0f);
        this.transform.rotation = _currentRotation;
    }
}
