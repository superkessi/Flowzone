using System;
using UnityEngine;
using UnityEngine.UI;

public class S_TutorialToggle : MonoBehaviour
{
    private Toggle toggle;
    private const string TutorialKey = "TutorialPlayed";


    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        var tutorialPlayed = PlayerPrefs.GetInt(TutorialKey, 0) == 1;
        toggle.isOn = !tutorialPlayed;
        
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isToggled)
    {
        if (isToggled)
        {
            PlayerPrefs.DeleteKey(TutorialKey);
        }
        else
        {
            PlayerPrefs.SetInt(TutorialKey, 1);
        }

        PlayerPrefs.Save();
    }
}
