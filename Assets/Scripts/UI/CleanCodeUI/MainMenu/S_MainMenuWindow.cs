using UnityEngine;

public class S_MainMenuWindow : S_BaseWindow
{
    [SerializeField] private S_MainUI mainUI;
    public void OnOptionsButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        mainUI.showOptionsWindow();
    }

    public void OnCreditsButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        mainUI.showCreditsWindow();
    }
}
