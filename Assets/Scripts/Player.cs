using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _interactDist = 2f; //the max dist for interaction raycast
    [SerializeField] private LayerMask _countersLayerMask;

    [SerializeField] private GameInput _gameInput;

    public bool IsWalking { get; private set; }

    private float _playerHeight = 2f;
    private float _playerRadius = 0.7f;
    private float _moveDist; //how much distance the player will move in a frame 
    private bool _canMove; //can the player move or not
    private Vector3 _lastInteractDir; //the latest move dir before moveDir becoms 0 (player stops pressing any input buttons)


    private void Start()
    {
        _gameInput.OnInteractInputAction += GameInput_OnInteractInputAction;
    }


    private void GameInput_OnInteractInputAction()
    {
        //get the normalized player input direction vectosr (2D movement)
        Vector2 inputDir = _gameInput.GetMovementVectorNormalized();

        //cast 2D input to 3D vector for world movement
        Vector3 moveDir = new(inputDir.x, 0f, inputDir.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractDir = moveDir;
        }

        //do a raycast to check whether the player can interact with something or not
        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycastHit, _interactDist, _countersLayerMask))
        {
            //if raycast true, player is close enough to something

            //check if this something can be interacted with
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
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

        //do a raycast to check whether the player can interact with something or not
        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycastHit,_interactDist, _countersLayerMask))
        {
            //if raycast true, player is close enough to something

            //check if this something can be interacted with
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                
            }
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

}
