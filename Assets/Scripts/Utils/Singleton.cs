using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> // Generic class using T as the variable name for the type that is going to be used inside the class
                                                            // class requires that T the type paste in is an object that is meant to extend the singleton of the same type (for security we will create instance only inside the singleton) 
{
    private static T instance; 
    public static T Instance  // access the global instance without modifying it (getter only)
    {
        get { return instance; }
    }

    public static bool isInitialized // to know whether not an instance has already been set
    {
        get { return instance != null; }
    }

    protected virtual void Awake()  // set the instance property (accessible and meant to be overidden by any class that extend singleton) 
    {
        if (instance != null)
        {
            Debug.LogError("[singleton] Trying to instantiate a second instance of a singleton class.");
        }
        else
        {
            instance = (T) this;
        }
    }

    protected virtual void OnDestroy()  // make sure if the object is destroyed another singleton will be created
                                        // (accessible and meant to be overidden by any class that extend singleton) 
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
