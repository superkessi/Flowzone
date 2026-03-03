using UnityEngine;
using UnityEngine.EventSystems;

public class S_BaseWindow : MonoBehaviour, S_IWindow
{
    private GameObject lastSelectedButton;

    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    private GameObject firstSelectedButton;
    public void Show()
    {
        gameObject.SetActive(true);
        if (lastSelectedButton != null)
        {
            eventSystem.SetSelectedGameObject(lastSelectedButton);
        }
        else
        {
            eventSystem.SetSelectedGameObject(firstSelectedButton);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
