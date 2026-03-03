using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;


public class S_A_PauseMenu : S_A_BaseWindow
{

    //S_A_AudioManager audioManager;

    [SerializeField]
    private S_A_PauseUIManager gameUiManager;

    [SerializeField]
    private S_A_SceneLoader gameSceneLoader;

    [SerializeField]
    private PlayableAsset ClosePauseWindow;

    [SerializeField]
    private PlayableAsset OpenPauseOptionsWindow;

    [SerializeField]
    private GameObject playerUI;


    EventSystem eventSystem;

    [SerializeField]
    private GameObject firstSelectedButton;
    [SerializeField]
    private GameObject secondSelectedButton;

    private void Awake()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }

    //private void Awake()
    //{
    //    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<S_A_AudioManager>();
    //}


    public void OnContinueButtonClicked()
    {
        playerUI.SetActive(true);
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameUiManager.WindowDictionary["pauseMain"].Hide();
        Time.timeScale = 1.0f;
    }

    public void OnRestartButtonClicked(int index)
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameSceneLoader.LoadSceneByIndex(index);
        Time.timeScale = 1.0f;
    }

    public void OnOptionsButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        this.Hide();
        gameUiManager.WindowDictionary["options"].Show();
        eventSystem.SetSelectedGameObject(secondSelectedButton);
        //gameUiManager.GetComponent<PlayableDirector>().Play(ClosePauseWindow);
    }

    public void OptionsMenuShow()
    {
        
        gameUiManager.WindowDictionary["pauseMain"].Hide();
        gameUiManager.WindowDictionary["options"].Show();
        //gameUiManager.GetComponent<PlayableDirector>().Play(OpenPauseOptionsWindow);
    }

    public void ShowWindow()
    {
        
    }

    public void OnMainMenuButtonClicked(int index)
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameSceneLoader.LoadSceneByIndex(index);
        Time.timeScale = 1.0f;
    }
}
