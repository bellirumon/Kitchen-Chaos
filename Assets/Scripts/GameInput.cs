using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public event Action OnInteractInputAction;
    public event Action OnInteractAlternateInputAction;
    public event Action OnPauseAction;


    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause
    }


    public static GameInput Instance { get; private set; }

    //a reference to the generated class from the Player Input Actions asset
    private PlayerInputActions _playerInputActions;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("GameInput instance already exists. Destroying this one...");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //instantiate an object of class
        _playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            _playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        //enable the "Player" Action Map 
        _playerInputActions.Player.Enable();

        //add a listener to the "Interact" and "InteractAlternate" input actions
        _playerInputActions.Player.Interact.performed += Interact_Performed;
        _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
        _playerInputActions.Player.Pause.performed += Pause_Performed;


    }


    private void OnDestroy()
    {
        _playerInputActions.Player.Interact.performed -= Interact_Performed;
        _playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_Performed;
        _playerInputActions.Player.Pause.performed -= Pause_Performed;

        _playerInputActions.Dispose();
    }


    private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke();
    }

    private void InteractAlternate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateInputAction?.Invoke();
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractInputAction?.Invoke();
    }


    public Vector2 GetMovementVectorNormalized()
    {
        //read and store player input via New Input System
        Vector2 inputDir = _playerInputActions.Player.Move.ReadValue<Vector2>();

        //normalize to account for diagonal movement 
        inputDir = inputDir.normalized;

        return inputDir;
    }


    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.MoveUp:
            return _playerInputActions.Player.Move.bindings[1].ToDisplayString();

            case Binding.MoveDown:
            return _playerInputActions.Player.Move.bindings[2].ToDisplayString();

            case Binding.MoveLeft:
            return _playerInputActions.Player.Move.bindings[3].ToDisplayString();

            case Binding.MoveRight:
            return _playerInputActions.Player.Move.bindings[4].ToDisplayString();

            case Binding.Interact:
            return _playerInputActions.Player.Interact.bindings[0].ToDisplayString();

            case Binding.InteractAlternate:
            return _playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();

            case Binding.Pause:
            return _playerInputActions.Player.Pause.bindings[0].ToDisplayString();

        }
    }


    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        _playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.MoveUp:
            inputAction = _playerInputActions.Player.Move;
            bindingIndex = 1;
            break;

            case Binding.MoveDown:
            inputAction = _playerInputActions.Player.Move;
            bindingIndex = 2;
            break;

            case Binding.MoveLeft:
            inputAction = _playerInputActions.Player.Move;
            bindingIndex = 3;
            break;

            case Binding.MoveRight:
            inputAction = _playerInputActions.Player.Move;
            bindingIndex = 4;
            break;

            case Binding.Interact:
            inputAction = _playerInputActions.Player.Interact;
            bindingIndex = 0;
            break;

            case Binding.InteractAlternate:
            inputAction = _playerInputActions.Player.InteractAlternate;
            bindingIndex = 0;
            break;

            case Binding.Pause:
            inputAction = _playerInputActions.Player.Pause;
            bindingIndex = 0;
            break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete((callback) =>
            {
                _playerInputActions.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, _playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();

    }

}
