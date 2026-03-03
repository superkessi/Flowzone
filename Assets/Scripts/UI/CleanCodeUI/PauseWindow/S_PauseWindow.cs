using UnityEngine;

public class S_PauseWindow : S_BaseWindow
{

    [SerializeField] private S_GameUI gameUI;
    [SerializeField] private S_A_SceneLoader gameSceneLoader;


    public void OnContinueButtonClicked()
    {
        //playerUI.SetActive(true);
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        // gameUiManager.WindowDictionary["pauseMain"].Hide();
        gameUI.showPlayerUI();
        gameUI.closeCurrentWindow();
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
        gameUI.showOptionsWindow();
    }

    public void OnMainMenuButtonClicked(int index)
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameSceneLoader.LoadSceneByIndex(index);
        Time.timeScale = 1.0f;
    }
}
