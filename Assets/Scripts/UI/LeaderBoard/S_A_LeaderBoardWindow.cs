using TMPro;
using UnityEngine;

public class S_A_LeaderBoardWindow : S_A_BaseWindow
{
    [SerializeField]
    private S_A_PauseUIManager gameUiManager;

    [SerializeField]
    private S_A_SceneLoader gameSceneLoader;

    [SerializeField]
    private S_A_VirtualTastatur vKeyboard;

    [SerializeField]
    private TextMeshProUGUI endScore;

    [SerializeField]
    private TextMeshProUGUI shownScore;

    

    VirtualKeyboard vk = new VirtualKeyboard();

    private void Update()
    {
        shownScore.text = endScore.text;
    }

    public void OnBackButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameUiManager.WindowDictionary["leaderBoardWindow"].Hide();
        gameUiManager.WindowDictionary["gameOverWindow"].Show();
    }

    public void OnNameInputClicked()
    {
        //System.Diagnostics.Process.Start("OSK.exe");
        //TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default);
        //vk.ShowOnScreenKeyboard();
        this.Hide();
        vKeyboard.Show();
    }
}
