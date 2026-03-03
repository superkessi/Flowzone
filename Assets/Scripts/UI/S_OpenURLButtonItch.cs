using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.WebRequestMethods;
using System;

public class OpenURLButtonItch : MonoBehaviour
{
    public string url; 

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenURL);
    }

    public void OpenURL()
    {
        Application.OpenURL("https://s4g.itch.io/flowzone");
    }

  
}
