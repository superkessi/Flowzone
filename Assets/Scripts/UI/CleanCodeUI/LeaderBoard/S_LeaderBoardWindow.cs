using TMPro;
using UnityEngine;

public class S_LeaderBoardWindow : S_BaseWindow
{
    [SerializeField] private S_GameUI gameUI;
    [SerializeField] private S_A_SceneLoader gameSceneLoader;
    [SerializeField] private S_A_VirtualTastatur vKeyboard;
    [SerializeField] private TextMeshProUGUI endScore;
    [SerializeField] private TextMeshProUGUI shownScore;
    [SerializeField] private S_A_ScoreManager scoreManager;

    VirtualKeyboard vk = new VirtualKeyboard();

    private void Awake()
    {
        
    }

    private void Update()
    {
        shownScore.text = endScore.text;
        OnBackButtonGamepad();
        OpenKeyboard();
        OnSubmitClicked();
    }

    public void OnBackButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameUI.closeCurrentWindow();
    }

    public void OnBackButtonGamepad()
    {
        if (S_UIInput.instance.BackButtonController)
        {
            S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
            gameUI.closeCurrentWindow();
        }
    }

    public void OnNameInputClicked()
    {
        //System.Diagnostics.Process.Start("OSK.exe");
        //TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default);
        //vk.ShowOnScreenKeyboard();
        //this.Hide();
        //vKeyboard.Show();
        gameUI.showVirtualKeyboard();
    }

    public void OpenKeyboard()
    {
        //System.Diagnostics.Process.Start("OSK.exe");
        //TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default);
        //vk.ShowOnScreenKeyboard();
        if (S_UIInput.instance.OpenKeyboard)
        {
            //this.Hide();
            //vKeyboard.Show();
            gameUI.showVirtualKeyboard();
        }
        
    }

    public void OnSubmitClicked()
    {
        if (S_UIInput.instance.Submit)
        {
            scoreManager.SubmitScore();
        }
       
    }
}
