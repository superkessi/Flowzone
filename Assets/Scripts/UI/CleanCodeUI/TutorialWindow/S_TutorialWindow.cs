using UnityEngine;

public class S_TutorialWindow : S_BaseWindow
{

    [SerializeField] private S_GameUI gameUI;
    [SerializeField] private S_MainUI mainUI;

    public void OnBackButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        if (gameUI != null)
        {
            gameUI.closeCurrentWindow();
        }
        else if (mainUI != null)
        {
            mainUI.closeCurrentWindow();
        }

    }
}
