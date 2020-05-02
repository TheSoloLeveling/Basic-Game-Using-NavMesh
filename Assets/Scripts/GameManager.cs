using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // What level the game is currently in
    // Load and unload game levels
    // Keep track of the game state
    // generate other persistent system

    private string _currentLevelName = string.Empty;

    List<AsyncOperation> _LoadOperations;  // to track the number of AsyncOperations we have

    private void Start()
    {
        DontDestroyOnLoad(gameObject);   // to make sure the game object of the game manager will never get Destroyed while loading scenes

        _LoadOperations = new List<AsyncOperation>();

        LoadLevel("Main");
    }

    void OnLoadOperationComplete(AsyncOperation ao)  // the event completed of the AsyncOperation need the AsyncOperation argument
    {
        if (_LoadOperations.Contains(ao))
        {
            _LoadOperations.Remove(ao);

            // transistion between scenes or dispatch message
        }

        Debug.Log("load Complete");
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);  // AsyncOperation track and knows the operation of the Async load scene
                                                                                             // the additive Mode is to run multiples scenes additivly (simultanly)
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to load level" + levelName);
            return;  // if the scene doesn't exist dont call the completed event
        }

        ao.completed += OnLoadOperationComplete;  // AsynOperation contains a couple events like the completed event (completed event contains listeners who listen to it, we +=)
        _LoadOperations.Add(ao);

        _currentLevelName = levelName;

    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to load level" + levelName);
            return;  // if the scene doesn't exist dont call the completed event
        }

        ao.completed += OnUnloadOperationComplete;
    }
}
