using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 10f;

    [SerializeField] private GameInput gameInput;
    public bool IsWalking { get; private set; }

    private float _playerHeight = 2f;
    private float _playerRadius = 0.7f;
    
    private float _moveDist;
    
    private bool _canMove;


    private void Update()
    {
        //get the normalized player input direction vectosr (2D movement)
        Vector2 inputDir = gameInput.GetMovementVectorNormalized();

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
