using UnityEngine;
using UnityEngine.EventSystems;

public class S_OptionsWindow : S_BaseWindow
{
    [SerializeField] private S_GameUI gameUI;
    [SerializeField] private S_MainUI mainUI;
    bool pause;

    public void OnAudioButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        if (gameUI != null)
        {
            gameUI.showAudioWindow();
        }
        else if (mainUI != null)
        {
            mainUI.showAudioWindow();
        }
    }

    public void OnBackButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        if(gameUI != null)
        {
            gameUI.closeCurrentWindow();
        }
        else if(mainUI != null)
        {
            mainUI.closeCurrentWindow();
        }
       
    }

    public void OnControlsButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        if (gameUI != null)
        {
            gameUI.showControlsWindow();
        }
        else if (mainUI != null)
        {
            mainUI.showControlsWindow();
        }
    }

    public void OnTutorialButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        if (gameUI != null)
        {
            gameUI.showTutorialWindow();
        }
        else if (mainUI != null)
        {
            mainUI.showTutorialWindow();
        }
    }
}
