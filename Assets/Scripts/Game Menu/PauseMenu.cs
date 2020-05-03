using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;

    private void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeClick);  // if the button is clicked, the HandleresumeClick is listenning to it
        RestartButton.onClick.AddListener(HandleRestartClick);
        QuitButton.onClick.AddListener(HandleQuitClick);
    }

    void HandleResumeClick()
    {
        GameManager.Instance.TogglePause();
    }

    void HandleRestartClick()
    {
        GameManager.Instance.RestartGame();
    }

    void HandleQuitClick()
    {
        GameManager.Instance.QuitGame();
    }
}
