using UnityEngine;

public class S_CreditsWindow : S_BaseWindow
{
    [SerializeField] private S_MainUI mainUI;
    public void OnBackButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        mainUI.closeCurrentWindow();

    }
}
