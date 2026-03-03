using System.Collections.Generic;
using UnityEngine;

public class S_MainUI : MonoBehaviour
{
    //private S_PauseWindow pauseWindow;

    private S_MainMenuWindow mainMenuWindow;
    private S_OptionsWindow optionsWindow;
    private S_AudioWindow audioWindow;
    private S_ControlsWindow controlsWindow;
    private S_CreditsWindow creditsWindow;
    private S_TutorialWindow tutorialWindow;

    private Stack<S_BaseWindow> currentWindowStack = new Stack<S_BaseWindow>();

    private void Awake()
    {
      
        mainMenuWindow = GetComponentInChildren<S_MainMenuWindow>(true);
        showMainMenuWindow();

        creditsWindow = GetComponentInChildren<S_CreditsWindow>(true);
        optionsWindow = GetComponentInChildren<S_OptionsWindow>(true);
        audioWindow = GetComponentInChildren<S_AudioWindow>(true);
        controlsWindow = GetComponentInChildren<S_ControlsWindow>(true);
        tutorialWindow = GetComponentInChildren<S_TutorialWindow>(true);

    }



    private void Update()
    {
        CheckIfWindowOpen();
        //CheckIfPauseOn();

        //if (startTimer)
        //{
        //    timer -= Time.deltaTime;
        //}

        //if (timer <= 0.0f)
        //{
        //    showGameOver();

        //}
    }


    public void CheckIfWindowOpen()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) )
        if (S_UIInput.instance.PauseKeyboard)
        {
            
            if (currentWindowStack.Count > 1)
            {
                closeCurrentWindow();
                Debug.Log("Current Window Stack in PAUSEON(IF > 0): " + currentWindowStack.Count);
            }
           
        }

        else if (S_UIInput.instance.BackButtonController)
        {
            if (currentWindowStack.Count > 1)
            {
                closeCurrentWindow();
                Debug.Log("Current Window Stack in PAUSEON(IF > 0): " + currentWindowStack.Count);
            }

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
        if (currentWindowStack.Count == 0)
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

    internal void showMainMenuWindow()
    {
        ShowWindow(mainMenuWindow);
        Debug.Log("Current Window Stack in SHOWOPTIONSWINDOW: " + currentWindowStack.Count);
    }
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

    internal void showControlsWindow()
    {
        ShowWindow(controlsWindow);
        Debug.Log("Current Window Stack in SHOWPAUSEWINDOW: " + currentWindowStack.Count);
    }

    internal void showCreditsWindow()
    {
        ShowWindow(creditsWindow);
        Debug.Log("Current Window Stack in SHOWPAUSEWINDOW: " + currentWindowStack.Count);
    }

    internal void showTutorialWindow()
    {
        ShowWindow(tutorialWindow);
        Debug.Log("Current Window Stack in SHOWPAUSEWINDOW: " + currentWindowStack.Count);
    }





    //--------------------



}
