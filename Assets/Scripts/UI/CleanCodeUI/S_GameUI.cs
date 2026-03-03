using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

public class S_GameUI : MonoBehaviour
{
    //private S_PauseWindow pauseWindow;

    private S_PauseWindow pauseWindow;
    private S_OptionsWindow optionsWindow;
    private S_AudioWindow audioWindow;
    private S_ControlsWindow controlsWindow;
    private S_GameOverWindow gameOverWindow;
    private S_LeaderBoardWindow leaderBoardWindow;
    private S_VirtualKeyboard virtualKeyboard;
    private S_TutorialWindow tutorialWindow;

    public bool gameOver = false;

    private float timer = 2.5f;
    private bool startTimer = false;

    [SerializeField] private GameObject playerUI;

    private Stack<S_BaseWindow> currentWindowStack = new Stack<S_BaseWindow>();

    private void Awake()
    {
        pauseWindow = GetComponentInChildren<S_PauseWindow>(true);
        optionsWindow = GetComponentInChildren<S_OptionsWindow>(true);
        audioWindow = GetComponentInChildren<S_AudioWindow>(true);
        controlsWindow = GetComponentInChildren<S_ControlsWindow>(true);
        gameOverWindow = GetComponentInChildren<S_GameOverWindow>(true);
        leaderBoardWindow = GetComponentInChildren<S_LeaderBoardWindow>(true);
        virtualKeyboard = GetComponentInChildren<S_VirtualKeyboard>(true);
        tutorialWindow = GetComponentInChildren<S_TutorialWindow>(true);
    }

    private void Update()
    {
            CheckIfPauseOn();
       
        if (startTimer)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0.0f)
        {
            showGameOver();

        }
    }
   

    public void CheckIfPauseOn()
    {
        if (!gameOver)
        {
            //if (Input.GetKeyDown(KeyCode.Escape) )
            if (S_UIInput.instance.PauseKeyboard)
            {
                if (currentWindowStack.Count == 0)
                {
                    playerUI.SetActive(false);
                    Time.timeScale = 0.0f;
                    //ShowWindow(pauseWindow);
                    showPauseWindow();
                    Debug.Log("Current Window Stack in PAUSEON(IF 0) " + currentWindowStack.Count);

                }
                else if (currentWindowStack.Count > 1)
                {
                    //Time.timeScale = 1.0f;
                    closeCurrentWindow();
                    Debug.Log("Current Window Stack in PAUSEON(IF > 0): " + currentWindowStack.Count);
                }
                else if (currentWindowStack.Count == 1)
                {
                    playerUI.SetActive(true);
                    Time.timeScale = 1.0f;
                    closeCurrentWindow();
                    Debug.Log("Current Window Stack in PAUSEON(IF > 0): " + currentWindowStack.Count);
                }
            }
            else if (S_UIInput.instance.PauseController)
            {
                if (currentWindowStack.Count == 0)
                {
                    playerUI.SetActive(false);
                    Time.timeScale = 0.0f;
                    showPauseWindow();
                    Debug.Log("Current Window Stack in PAUSEON(IF > 0): " + currentWindowStack.Count);
                }
                else if (currentWindowStack.Count == 1)
                {
                    playerUI.SetActive(true);
                    Time.timeScale = 1.0f;
                    closeCurrentWindow();
                    Debug.Log("Current Window Stack in PAUSEON(IF > 0): " + currentWindowStack.Count);
                }

            }
            else if (S_UIInput.instance.BackButtonController)
            {
                if (currentWindowStack.Count > 1)
                {
                    //Time.timeScale = 1.0f;
                    closeCurrentWindow();
                    Debug.Log("Current Window Stack in PAUSEON(IF > 0): " + currentWindowStack.Count);
                }
                else if (currentWindowStack.Count == 1)
                {
                    playerUI.SetActive(true);
                    Time.timeScale = 1.0f;
                    closeCurrentWindow();
                    Debug.Log("Current Window Stack in PAUSEON(IF > 0): " + currentWindowStack.Count);
                }

            }
        }
        else
        {
            hidePlayerUI();
        }

    }

    private void ShowWindow(S_BaseWindow window)
    {
        if (currentWindowStack.Count != 0)
        {
            currentWindowStack.Peek().Hide();
        }
        //currentWindowStack.Peek()?.Hide();
        Debug.Log("DEBUG");
        currentWindowStack.Push(window);
        window.Show();
        
        //currentWindowStack.Peek().Show();
    }

    internal void closeCurrentWindow()
    {
        if(currentWindowStack.Count == 0)
        {
            return;
        }
        
        //var currentWindow = currentWindowStack.Pop();
        currentWindowStack.Pop().Hide();

        //currentWindowStack.Peek()?.Show();
        if (currentWindowStack.Count != 0)
        {
            currentWindowStack.Peek().Show();
        }
        Debug.Log("Current Window Stack in CLOSECURRENTWINDOW: " + currentWindowStack.Count);
    }

    //---------- ALL WINDOWS ----------

    internal void showOptionsWindow()
    {
        ShowWindow(optionsWindow);
        Debug.Log("Current Window Stack in SHOWOPTIONSWINDOW: " + currentWindowStack.Count);
    }

    internal void showAudioWindow()
    {
        ShowWindow(audioWindow);
        Debug.Log("Current Window Stack in SHOWAUDIOWINDOW: " + currentWindowStack.Count);
    }

    internal void showPauseWindow()
    {
        ShowWindow(pauseWindow);
        Debug.Log("Current Window Stack in SHOWPAUSEWINDOW: " + currentWindowStack.Count);
    }

    internal void showControlsWindow()
    {
        ShowWindow(controlsWindow);
        Debug.Log("Current Window Stack in SHOWPAUSEWINDOW: " + currentWindowStack.Count);
    }

    internal void showGOWindow()
    {
        ShowWindow(gameOverWindow);
        Debug.Log("Current Window Stack in SHOWPAUSEWINDOW: " + currentWindowStack.Count);
    }

    internal void showLeaderboardWindow()
    {
        ShowWindow(leaderBoardWindow);
        Debug.Log("Current Window Stack in SHOWPAUSEWINDOW: " + currentWindowStack.Count);
    }

    internal void showVirtualKeyboard()
    {
        ShowWindow(virtualKeyboard);
        Debug.Log("Current Window Stack in SHOWPAUSEWINDOW: " + currentWindowStack.Count);
    }

    internal void showTutorialWindow()
    {
        ShowWindow(tutorialWindow);
        Debug.Log("Current Window Stack in SHOWPAUSEWINDOW: " + currentWindowStack.Count);
    }

    //--------------------

    internal void showPlayerUI()
    {
        playerUI.SetActive(true);
    }
    internal void hidePlayerUI()
    {
        playerUI.SetActive(false);
    }


    public void showGameOverWindow()
    {
        playerUI.SetActive(false);
        gameOver = true;
        S_A_AudioManager.Instance.StopSFX(S_A_AudioManager.Instance.wind);
        S_A_AudioManager.Instance.StopMusic(S_A_AudioManager.Instance.currentClip);
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.crash);
        startTimer = true;
        Debug.Log("Game Over");
    }

    private void showGameOver()
    {
        showGOWindow();
        startTimer = false;
        timer = 2.5f;
    }

    public void getEndScore(float endScore)
    {
        gameOverWindow.endScore(endScore);
    }
}
