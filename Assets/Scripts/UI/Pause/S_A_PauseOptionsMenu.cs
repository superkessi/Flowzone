using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class S_A_PauseOptionsMenu : S_A_BaseWindow
{

    //S_A_AudioManager audioManager;

    [SerializeField]
    private S_A_PauseUIManager gameUiManager;

    [SerializeField]
    private S_A_MainMenuUIManager mainUiManager;

    [SerializeField]
    private PlayableAsset ClosePauseOptionsWindow;

    [SerializeField]
    private PlayableAsset OpenPauseWindow;

    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    GameObject firstPauseNavigationButton;

    [SerializeField]
    GameObject firstAudioNavigationButton;

    [SerializeField]
    private GameObject firstSelectedButton;
    [SerializeField]
    private GameObject soundSelectedButton;
    [SerializeField]
    private GameObject controlsSelectedButton;
    [SerializeField]
    private GameObject pauseSelectedButton;

    private void Awake()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }


    //private void Awake()
    //{
    //    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<S_A_AudioManager>();
    //}

    public void OnBackButtonClicked()
    {


        //gameUiManager.WindowDictionary["options"].Hide();
        //gameUiManager.WindowDictionary["pauseMain"].Show();
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        //gameUiManager.GetComponent<PlayableDirector>().Play(ClosePauseOptionsWindow);
        this.Hide();
        gameUiManager.WindowDictionary["pauseMain"].Show();
        eventSystem.SetSelectedGameObject(pauseSelectedButton);

    }

    public void OnBackMainButtonClicked()
    {


        //gameUiManager.WindowDictionary["options"].Hide();
        //gameUiManager.WindowDictionary["pauseMain"].Show();
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        //gameUiManager.GetComponent<PlayableDirector>().Play(ClosePauseOptionsWindow);
        this.Hide();
        mainUiManager.WindowDictionary["mainMenu"].Show();
        eventSystem.SetSelectedGameObject(pauseSelectedButton);

    }

    public void OnAudioButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameUiManager.WindowDictionary["options"].Hide();
        gameUiManager.WindowDictionary["soundMenuWindow"].Show();
        eventSystem.SetSelectedGameObject(soundSelectedButton);
    }

    public void OnAudioMainButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        this.Hide();
        mainUiManager.WindowDictionary["soundMenu"].Show();
        eventSystem.SetSelectedGameObject(soundSelectedButton);
    }

    public void OnControlsButtonClicked()
    {
        this.Hide();
        gameUiManager.WindowDictionary["rebindingWindow"].Show();
        eventSystem.SetSelectedGameObject(controlsSelectedButton);
    }

    public void OnControlsMainButtonClicked()
    {
        this.Hide();
        mainUiManager.WindowDictionary["rebindingMenu"].Show();
        eventSystem.SetSelectedGameObject(controlsSelectedButton);
    }

    public void PauseMenuShow()
    {

        gameUiManager.WindowDictionary["options"].Hide();
        gameUiManager.WindowDictionary["pauseMain"].Show();
        eventSystem.SetSelectedGameObject(pauseSelectedButton);
       // gameUiManager.GetComponent<PlayableDirector>().Play(OpenPauseWindow);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && gameUiManager.WindowDictionary["options"].IsShown())
        {
            gameUiManager.GetComponent<PlayableDirector>().Play(ClosePauseOptionsWindow);
        }
    }
}