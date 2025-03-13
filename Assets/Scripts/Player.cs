using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 10f;

    [SerializeField] private GameInput gameInput;

    public bool IsWalking { get; private set; }


    private void Update()
    {
        //get the normalized player input direction vector (2D movement)
        Vector2 inputDir = gameInput.GetMovementVectorNormalized();

        //cast 2D input to 3D vector for world movement
        Vector3 moveDir = new(inputDir.x, 0f, inputDir.y);

        IsWalking = (moveDir != Vector3.zero);

        //move player
        transform.position += Time.deltaTime * _moveSpeed * moveDir;

        //rotate player to face direction of movement
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotationSpeed);
    }
}
