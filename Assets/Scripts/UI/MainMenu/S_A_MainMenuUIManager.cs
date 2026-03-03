using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_A_MainMenuUIManager : MonoBehaviour
{
    private static S_A_MainMenuUIManager instance;

    public static S_A_MainMenuUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (new GameObject("UserInterfaceManager")).AddComponent<S_A_MainMenuUIManager>();
                //DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }


    [SerializeField]
    private S_A_MainMenuWindow mainMenuWindow;

    [SerializeField]
    private S_A_MainMenuOptionsWindow mainOptionsWindow;

    [SerializeField]
    private S_A_PauseOptionsMenu optionsWindow;

    [SerializeField]
    private S_A_SoundMenuWindow soundMenuWindow;

    [SerializeField]
    private S_A_RebindingWIndow rebindingWindow;

    [SerializeField]
    public Dictionary<string, S_A_BaseWindow> WindowDictionary = new Dictionary<string, S_A_BaseWindow>();


    private void Awake()
    {
        Cursor.visible = true;
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }

        WindowDictionary.Add("mainMenu", mainMenuWindow);
        WindowDictionary.Add("mainOptions", mainOptionsWindow);
        WindowDictionary.Add("optionsMain", optionsWindow);
        WindowDictionary.Add("soundMenu", soundMenuWindow);
        WindowDictionary.Add("rebindingMenu", rebindingWindow);

    }

    private void Update()
    {
        //checkIfPauseShown();
    }

    //private void checkIfPauseShown()
    //{

    //    var x = WindowDictionary.Where(kvp => kvp.Key != "pauseMain").ToDictionary(item => item.Key, item => item.Value).FirstOrDefault();
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {

    //        if (!WindowDictionary["pauseMain"].IsShown() && !WindowDictionary[x.Key].IsShown())
    //        {
    //            WindowDictionary["pauseMain"].Show();
    //            Time.timeScale = 0.0f;
    //        }

    //        else if (WindowDictionary["pauseMain"].IsShown() && !WindowDictionary[x.Key].IsShown())
    //        {
    //            WindowDictionary["pauseMain"].Hide();
    //            Time.timeScale = 1.0f;
    //        }
    //    }

    //}
}

