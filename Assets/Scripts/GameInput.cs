using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event Action OnInteractInputAction;
    public event Action OnInteractAlternateInputAction;
    public event Action OnPauseAction;

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

}
