using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class S_ControlsWindow : S_BaseWindow
{
    [SerializeField] private S_GameUI gameUI;
    [SerializeField] private S_MainUI mainUI;

    [SerializeField] private Toggle toggle;

    [SerializeField] private InputActionAsset move;
    [SerializeField] private InputAction moveAction;

    [SerializeField] private PlayerInput playerInput;

    bool toggleIsOn;

    private void Awake()
    {
        toggle.isOn = toggleIsOn;
        moveAction = move.FindActionMap("Player").FindAction("Move");
    }

    private void Update()
    {
        toggleIsOn = DebugToggle();
    }
    public void OnBackButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        if (gameUI != null)
        {
            gameUI.closeCurrentWindow();
        }
        else if (mainUI != null)
        {
            mainUI.closeCurrentWindow();
        }
    }

    public bool DebugToggle()
    {
        if (toggle.isOn)
        {
            Debug.Log("on");
            //moveAction.ApplyBindingOverride("<Gamepad>/leftStick/left", path: "<Gamepad>/rightStick/left");
            //moveAction.ApplyBindingOverride("<Gamepad>/leftStick/right", path: "<Gamepad>/rightStick/right");
            playerInput.actions["Move"].ChangeBinding(4).WithPath("<Gamepad>/rightStick/left");
            playerInput.actions["Move"].ChangeBinding(5).WithPath("<Gamepad>/rightStick/right");
            return toggle.isOn = true;

        }
        else
        {
            Debug.Log("off");
            playerInput.actions["Move"].ChangeBinding(4).WithPath("<Gamepad>/leftStick/left");
            playerInput.actions["Move"].ChangeBinding(5).WithPath("<Gamepad>/leftStick/right");
            return toggle.isOn = false;
        }
        
    }
}
