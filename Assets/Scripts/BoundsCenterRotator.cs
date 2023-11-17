using UnityEngine;

/// <summary>
/// A class to rotate an object around the center of its model-space bounds. This is useful when a 3D model is not
/// precisely centered on the origin (so it would sweep in an arc if rotated) - by finding the center of the model's
/// bounds via the object's Renderer we can rotate it as if it WAS correctly centered.
/// </summary>
public class BoundsCenterRotator : MonoBehaviour
{
    /// <summary>
    /// The GameObject to rotate. If not set then we grab the one this script is attached to.
    /// </summary>
    [SerializeField] private GameObject _gameObjectToRotate;

    /// <summary>
    /// How many degrees per second to rotate on the X/Y/Z axes. Rotation order is X->Y->Z.
    /// </summary>
    [SerializeField] private Vector3 _degsPerSecond = new Vector3(0f, 30f, 0f);

    /// <summary>
    /// Whether we wish to rotate the object in world-space or model-space.
    /// </summary>
    [SerializeField] private Space _coordinateSystem = Space.Self;
    
    /// <summary>
    /// The center position to rotate the object around - populated from the object's Renderer.bounds.center property.
    /// </summary>
    private Vector3 _centralBoundsPosition;
    
    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        // If no specific GameObject has been chosen then we'll use this one
        if (!_gameObjectToRotate) { _gameObjectToRotate = this.gameObject; }
        
        // Grab the center of the GameObject / 3D Model / Whatever via its bound
        _centralBoundsPosition = _gameObjectToRotate.GetComponent<Renderer>().bounds.center;
    }

    /// <summary>
    /// Unity Update hook.
    /// </summary>
    void Update()
    {
        var thisTransform = this.transform;
        float deltaTime = Time.deltaTime;

        if (_coordinateSystem == Space.Self)
        {   
            _gameObjectToRotate.transform.RotateAround(_centralBoundsPosition, thisTransform.right, _degsPerSecond.x * deltaTime);
            _gameObjectToRotate.transform.RotateAround(_centralBoundsPosition, thisTransform.up, _degsPerSecond.y * deltaTime);
            _gameObjectToRotate.transform.RotateAround(_centralBoundsPosition, thisTransform.forward, _degsPerSecond.z * deltaTime);
        }
        else // Rotate in world-space
        {
            _gameObjectToRotate.transform.RotateAround(_centralBoundsPosition, Vector3.right, _degsPerSecond.x * deltaTime);
            _gameObjectToRotate.transform.RotateAround(_centralBoundsPosition, Vector3.up, _degsPerSecond.y * deltaTime);
            _gameObjectToRotate.transform.RotateAround(_centralBoundsPosition, Vector3.forward, _degsPerSecond.z * deltaTime);
        }
    }
}
