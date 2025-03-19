using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public event Action OnInteractInputAction;
    public event Action OnInteractAlternateInputAction;


    //a reference to the generated class from the Player Input Actions asset
    private PlayerInputActions _playerInputActions;


    private void Awake()
    {
        //instantiate an object of class
        _playerInputActions = new PlayerInputActions();

        //enable the "Player" Action Map 
        _playerInputActions.Player.Enable();

        //add a listener to the "Interact" and "InteractAlternate" input actions
        _playerInputActions.Player.Interact.performed += Interact_performed;
        _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateInputAction?.Invoke();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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
