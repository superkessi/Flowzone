using System.Collections.Generic;
using UnityEngine;

public class S_A_BaseWindow : MonoBehaviour, S_A_IWindow
{

    public bool escPressed { get; set; }

    bool shown = false;

    Dictionary<string ,S_A_BaseWindow> baseWindow = new Dictionary<string, S_A_BaseWindow>();
    public virtual void Show()
    {
        gameObject.SetActive(true);
        shown = true;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        shown = false;
    }

    public bool IsShown()
    {
        return shown;
    }
}


