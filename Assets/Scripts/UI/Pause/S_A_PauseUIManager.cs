using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_A_PauseUIManager : MonoBehaviour
{
    private static S_A_PauseUIManager instance;

    public static S_A_PauseUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (new GameObject("UserInterfaceManager")).AddComponent<S_A_PauseUIManager>();
                //DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }


    [SerializeField]
    private S_A_PauseMenu pauseWindow;

    [SerializeField]
    private S_A_PauseOptionsMenu optionsWindow;

    [SerializeField]
    private S_A_GameOverWindow gameOverWindow;

    [SerializeField]
    private S_A_LeaderBoardWindow leaderBoardWindow;

    [SerializeField]
    private S_A_SoundMenuWindow soundMenuWindow;

    [SerializeField]
    private S_A_RebindingWIndow rebindingWindow;

    [SerializeField]
    public Dictionary<string, S_A_BaseWindow> WindowDictionary = new Dictionary<string, S_A_BaseWindow>();

    public bool gameOver = false;

    private float timer = 2.5f;
    private bool startTimer = false;

    EventSystem eventSystem;

    [SerializeField]
    private GameObject firstSelectedButton;

    [SerializeField]
    private GameObject playerUI;



    private void Awake()
    {

        eventSystem = EventSystem.current;

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }

        WindowDictionary.Add("pauseMain", pauseWindow);
        WindowDictionary.Add("options", optionsWindow);
        WindowDictionary.Add("gameOverWindow", gameOverWindow);
        WindowDictionary.Add("leaderBoardWindow", leaderBoardWindow);
        WindowDictionary.Add("soundMenuWindow", soundMenuWindow);
        WindowDictionary.Add("rebindingWindow", rebindingWindow);

    }

    private void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0.0f)
        {
            showGameOver();
            
        }

        

    }

    public void checkIfPauseShown()
    {
        if (!gameOver)
        {

            var x = WindowDictionary.Where(kvp => kvp.Key != "pauseMain").ToDictionary(item => item.Key, item => item.Value).FirstOrDefault();
            //if (Input.GetKeyDown(KeyCode.Escape))
            //{

            //if (!WindowDictionary["pauseMain"].IsShown() && !WindowDictionary[x.Key].IsShown())
            //{
            //    WindowDictionary["pauseMain"].Show();
            //    Time.timeScale = 0.0f;
            //    eventSystem.SetSelectedGameObject(firstSelectedButton);

            //}

            //else if (WindowDictionary["pauseMain"].IsShown() && !WindowDictionary[x.Key].IsShown())
            //{
            //    WindowDictionary["pauseMain"].Hide();
            //    Time.timeScale = 1.0f;
            //}

            if (!WindowDictionary["pauseMain"].IsShown() && !WindowDictionary["soundMenuWindow"].IsShown() && !WindowDictionary["rebindingWindow"].IsShown() && !WindowDictionary["options"].IsShown())
            {
                playerUI.SetActive(false);
                WindowDictionary["pauseMain"].Show();
                Time.timeScale = 0.0f;
                eventSystem.SetSelectedGameObject(firstSelectedButton);

            }

            else if (WindowDictionary["pauseMain"].IsShown() && !WindowDictionary["soundMenuWindow"].IsShown() && !WindowDictionary["rebindingWindow"].IsShown() && !WindowDictionary["options"].IsShown())
            {
                playerUI.SetActive(true);
                WindowDictionary["pauseMain"].Hide();
                Time.timeScale = 1.0f;
            }
            //else if (!WindowDictionary["pauseMain"].IsShown() && WindowDictionary[x.Key].IsShown())
            //{
            //    Time.timeScale = 0.0f;

            //if (WindowDictionary["soundMenuWindow"].IsShown() || WindowDictionary["rebindingWindow"].IsShown())
            //    {
            //        WindowDictionary["pauseMain"].Hide();
            //        //WindowDictionary["soundMenuWindow"].Hide();
            //        //WindowDictionary["rebindingWindow"].Hide();
            //        //WindowDictionary["options"].Show();
            //        Time.timeScale = 0.0f;
            //        Debug.Log("In Function");
            //    eventSystem.SetSelectedGameObject(eventSystem.currentSelectedGameObject);
            //}
            //}
            //}else if(WindowDictionary["pauseMain"].IsShown() && WindowDictionary[x.Key].IsShown())
            //{
            //    Time.timeScale = 0.0f;
            //}

            //if (!WindowDictionary["pauseMain"].IsShown() && !WindowDictionary[x.Key].IsShown())
            //{
            //    WindowDictionary["pauseMain"].Show();
            //    Time.timeScale = 0.0f;
            //    eventSystem.SetSelectedGameObject(firstSelectedButton);

            //}

            //else if (WindowDictionary["pauseMain"].IsShown())
            //{
            //    WindowDictionary["pauseMain"].Hide();
            //    Time.timeScale = 1.0f;
            //}
            //}
        }
        else
        {
            playerUI.SetActive(false);
        }

    }
   
    public void showGameOverWindow()
    {
        playerUI.SetActive(false);
        gameOver = true;
        S_A_AudioManager.Instance.StopSFX(S_A_AudioManager.Instance.wind);
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.crash);
        startTimer = true;
        Debug.Log("Game Over");
        //gameOverWindow.Show();
        //gameOver = true;
    }

    public void getEndScore(float endScore)
    {
        gameOverWindow.endScore(endScore);
    }

    private void showGameOver()
    {
        gameOverWindow.Show();
        startTimer = false;
        timer = 2.5f;
    }
}

