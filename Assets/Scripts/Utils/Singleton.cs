using UnityEngine;

/// <summary>
/// Singleton wrapper class to convert anything into a singleton.
/// </summary>
/// <typeparam name="T">The type of object that will become a singleton.</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// Our private static instance of the type of object.
    /// </summary>
    private static T _instance;

    /// <summary>
    /// Unity awake hook to construct an instance right away so we always have one available.
    /// </summary>
    public virtual void Awake()
    {
        // If the _instance hasn't already been instantiated...
        if (_instance == null)
        {
            // ...then make this it.
            _instance = this as T;
        }
        else // If we tried to create a second instance of this singleton then destroy this (new) instance because we already have an instance!
        {
            Debug.LogWarning("Asked to create a second Singleton when one already exists!");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Static getter property (Note: this is a PROPERTY not a method - so we call it like Foo.Instance.bar()!)
    /// </summary>
    public static T Instance
    {
        get
        {
            // If this instance was null...
            if (_instance == null)
            {
                // ...attempt to find an existing instance.
                _instance = FindObjectOfType<T>();

                // If we didn't find one, or the one we found was null...
                if (_instance == null)
                {
                    // ...instantiate a new instance.
                    var obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }

            // Finally return the instance - whether that's an existing one, or the one we just created
            return _instance;
        }
    }

    /// <summary>
    /// Method to mark the _instance as `DontDestroyOnLoad` so it persists between scene loads.
    /// </summary>
    public static void MarkDontDestroyOnLoad() { DontDestroyOnLoad(_instance); }
    
}

