using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_A_RebindingWIndow : S_A_BaseWindow
{
    [SerializeField]
    private S_A_PauseUIManager gameUiManager;

    [SerializeField]
    private S_A_MainMenuUIManager mainUiManager;

    EventSystem eventSystem;

    [SerializeField]
    private GameObject firstSelectedButton;

    [SerializeField]
    private GameObject optionsSelectedButton;

    private void Awake()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }
    public void OnBackButtonClicked()
    {
        this.Hide();
        gameUiManager.WindowDictionary["options"].Show();
        eventSystem.SetSelectedGameObject(optionsSelectedButton);
    }

    public void OnBackMainButtonClicked()
    {
        this.Hide();
        mainUiManager.WindowDictionary["optionsMain"].Show();
        eventSystem.SetSelectedGameObject(optionsSelectedButton);
    }
}
