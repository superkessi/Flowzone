using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_UIInput : MonoBehaviour
{

    public static S_UIInput instance;

    public bool PauseKeyboard {  get; private set; }
    public bool BackButtonController { get; private set; }
    public bool PauseController { get; private set; }
    public bool LeaderBoardController { get; private set; }
    public bool OpenKeyboard { get; private set; }
    public bool Submit { get; private set; }


    private PlayerInput _playerInput;
    private InputAction _pauseKeyboard;
    private InputAction _backButtonController;
    private InputAction _pauseController;
    private InputAction _leaderBoardController;
    private InputAction _openKeyboard;
    private InputAction _submit;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        _playerInput = GetComponent<PlayerInput>();

        SetupInputActions();

    }



    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
    }

   

    private void SetupInputActions()
    {
        _pauseKeyboard = _playerInput.actions["PauseKeyboard"];
        _backButtonController = _playerInput.actions["BackButtonGamepad"];
        _pauseController = _playerInput.actions["PauseGamepad"];
        _leaderBoardController = _playerInput.actions["LeaderBoard"];
        _openKeyboard = _playerInput.actions["OpenKeyboard"];
        _submit = _playerInput.actions["Submit"];
    }

    private void UpdateInputs()
    {
        PauseKeyboard = _pauseKeyboard.WasPressedThisFrame();
        BackButtonController = _backButtonController.WasPressedThisFrame();
        PauseController = _pauseController.WasPressedThisFrame();
        LeaderBoardController = _leaderBoardController.WasPressedThisFrame();
        OpenKeyboard = _openKeyboard.WasPressedThisFrame();
        Submit = _submit.WasPressedThisFrame();
    }
}
