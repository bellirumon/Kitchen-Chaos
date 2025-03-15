using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    public event Action<ClearCounter> OnSelectedCounterChanged;

    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _interactDist = 2f; //the max dist for interaction raycast
    [SerializeField] private LayerMask _countersLayerMask;
    [SerializeField] private Transform _kitchenObjectHoldPoint; //the point where the player will hold objects in hand
    [SerializeField] private GameInput _gameInput;

    public bool IsWalking { get; private set; }

    private float _playerHeight = 2f;
    private float _playerRadius = 0.7f;
    private float _moveDist; //how much distance the player will move in a frame 
    private bool _canMove; //can the player move or not
    private Vector3 _lastInteractDir; //the latest move dir before moveDir becoms 0 (player stops pressing any input buttons)
    private ClearCounter _selectedCounter; //to keep track of the currently selected clear counter
    private KitchenObject _kitchenObject; //to keep track of the current kitchen object the player has


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("An instance of Player already exists! Destroying this instance");
            Destroy(gameObject);

            return;
        }    

        Instance = this;
    }


    private void Start()
    {
        _gameInput.OnInteractInputAction += GameInput_OnInteractInputAction;
    }


    private void GameInput_OnInteractInputAction()
    {
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }


    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }


    private void HandleInteractions()
    {
        //get the normalized player input direction vectosr (2D movement)
        Vector2 inputDir = _gameInput.GetMovementVectorNormalized();

        //cast 2D input to 3D vector for world movement
        Vector3 moveDir = new(inputDir.x, 0f, inputDir.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractDir = moveDir;
        }

        //do a raycast to check whether the player can interact with something interactable or not
        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycastHit,_interactDist, _countersLayerMask))
        {
            //if raycast true, player is close enough to something interactable

            //get a reference to the interactable object
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //interactable thing is a KitchenObjectParent
                if (clearCounter != _selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                //the interactable thing is not a KitchenObjectParent, hence no counter is selected
                SetSelectedCounter(null);
            }
        }
        else
        {
            //if raycast detects nothing, remove any selected counter
            SetSelectedCounter(null);
        }
    }


    private void HandleMovement()
    {
        //get the normalized player input direction vectosr (2D movement)
        Vector2 inputDir = _gameInput.GetMovementVectorNormalized();

        //cast 2D input to 3D vector for world movement
        Vector3 moveDir = new(inputDir.x, 0f, inputDir.y);

        //compute the distance to be moved
        _moveDist = Time.deltaTime * _moveSpeed;

        //do a raycast for collision detection
        _canMove = !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * _playerHeight), _playerRadius, moveDir, _moveDist);

        if (!_canMove)
        {
            //if cannot move (diagonal capsule cast)

            //attempt only x movement (wall hug movement)
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized; //account for diagonal movement error

            //do a raycast for collision detection
            _canMove = !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * _playerHeight), _playerRadius, moveDirX, _moveDist);

            if (_canMove)
            {
                //move in x direction only (wall hug)
                moveDir = moveDirX;
            }
            else
            {
                //attempt only z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized; //account for diagonal movement error

                //do a raycast for collision detection
                _canMove = !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * _playerHeight), _playerRadius, moveDirZ, _moveDist);

                if (_canMove)
                {
                    //move in z direction only (wall hug)
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in either direction (player is trying to move into a corner)
                }
            }

        }

        //move player if possible
        if (_canMove)
        {
            transform.position += _moveDist * moveDir;
        }

        //update player walking state
        IsWalking = (moveDir != Vector3.zero);

        //rotate player to face direction of movement
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotationSpeed);
    }


    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        //set the selected counter
        _selectedCounter = selectedCounter;

        //fire the event to inform that a counter has been selected
        OnSelectedCounterChanged?.Invoke(_selectedCounter);
    }

    public Transform GetKitchenObjectPlacementPoint()
    {
        return _kitchenObjectHoldPoint;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
