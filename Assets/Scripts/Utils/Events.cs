
using UnityEngine;
using UnityEngine.Events;

public class Events 
{
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { } // the previous and current GameState
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }
}
