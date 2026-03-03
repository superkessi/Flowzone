using UnityEngine;

public class S_A_MainMenuOptionsWindow : S_A_BaseWindow
{
    [SerializeField]
    private S_A_MainMenuUIManager gameUiManager;

    public void OnBackButtonClicked()
    {
        gameUiManager.WindowDictionary["mainOptions"].Hide();
        gameUiManager.WindowDictionary["mainMenu"].Show();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && gameUiManager.WindowDictionary["mainOptions"].IsShown())
        {
            gameUiManager.WindowDictionary["mainOptions"].Hide();
            gameUiManager.WindowDictionary["mainMenu"].Show();
        }
    }
}
