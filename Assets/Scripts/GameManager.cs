using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { } // the previous and current GameState

public class GameManager : Singleton<GameManager>
{
    #region Objectives
    // What level the game is currently in
    // Load and unload game levels
    // Keep track of the game state
    // generate other persistent system

    // Toggle Pause
    // Trigger method via 'escape' key
    // Trigger method via pause menu
    // Pause simulation when in pause state
    // modify cursor to use pointer when in pause state

    #endregion

    public enum GameState  // what kind of gameState exist
    {
        PREGAME,
        RUNNING,
        PAUSED,
    }

    public GameObject[] SystemPrefabs; // Objects that the GameManager need to track
    public EventGameState OnGameStateChanged; // define an event of the type EventGameState

    List<GameObject> _instancedSystemPrefabs;
    List<AsyncOperation> _LoadOperations;  // to track the number of AsyncOperations we have
    GameState _currentGameState = GameState.PREGAME;

    private string _currentLevelName = string.Empty;
    public GameState CurrentGameState   // define the accessor of the CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; } // value: keyword that all setters have that passes in whatever is been set into this bracket 
    }

    private void Start()
    {      
        DontDestroyOnLoad(gameObject);   // to make sure the game object of the game manager will never get Destroyed while loading scenes

        _LoadOperations = new List<AsyncOperation>();
        _instancedSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();
    }

    private void Update()
    {
        if (_currentGameState == GameState.PREGAME)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnLoadOperationComplete(AsyncOperation ao)  // the event completed of the AsyncOperation need the AsyncOperation argument
    {
        if (_LoadOperations.Contains(ao))
        {
            _LoadOperations.Remove(ao);

            if (_LoadOperations.Count == 0)  // if all the loads are completet we update state to running
            {
                UpdateState(GameState.RUNNING);
            }
           

            // transistion between scenes or dispatch message
        }

        Debug.Log("load Complete");
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
    }

    void UpdateState(GameState state) // update the state of the game
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch( _currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1f; // back to normal time of the game
                break;

            case GameState.RUNNING:
                Time.timeScale = 1f; // back to normal time of the game
                break;

            case GameState.PAUSED:
                Time.timeScale = 0f; // pausing any simulation on game;
                break;

            default:
                break;          
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i =0; i < SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance); 
        }
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

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i= 0; i < _instancedSystemPrefabs.Count; i++)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();
    }

    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void TogglePause()
    {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);  // condition ? true : false
    }
}
