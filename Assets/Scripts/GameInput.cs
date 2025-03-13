using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public event Action OnInteractInputAction;


    //a reference to the generated class from the Player Input Actions asset
    private PlayerInputActions _playerInputActions;


    private void Awake()
    {
        //instantiate an object of class
        _playerInputActions = new PlayerInputActions();

        //enable the "Player" Action Map 
        _playerInputActions.Player.Enable();

        //add a listener to the "Interact" input action
        _playerInputActions.Player.Interact.performed += Interact_performed;
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
