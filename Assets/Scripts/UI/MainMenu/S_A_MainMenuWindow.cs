using UnityEngine;
using UnityEngine.Playables;

public class S_A_MainMenuWindow : S_A_BaseWindow
{
    [SerializeField]
    private S_A_MainMenuUIManager gameUiManager;

    [SerializeField]
    private S_A_SceneLoader gameSceneLoader;

  

    [SerializeField] GameObject directorLoop;
    [SerializeField] GameObject directorStart;



    public void OnStartButtonClicked(int index)
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
       
        directorLoop.SetActive(false);
        directorStart.SetActive(true);
        this.Hide();
        Time.timeScale = 1.0f;
    }

    public void LoadGameScene(int index)
    {
        gameSceneLoader.LoadSceneByIndex(index);
        Debug.Log($"Try Loading scene : {index}");
    }

    public void OnOptionsButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameUiManager.WindowDictionary["mainMenu"].Hide();
        //gameUiManager.WindowDictionary["mainOptions"].Show();
        gameUiManager.WindowDictionary["mainOptions"].Show();
    }

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        UnityEditor.EditorApplication.isPlaying = false;
#else
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        Application.Quit();
#endif
    }

}
