using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;


    private void Update()
    {
        //to store player input (2D)
        Vector2 inputDir = Vector2.zero;

        //get player input
        if (Input.GetKey(KeyCode.W))
        {
            inputDir.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDir.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputDir.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputDir.x = 1;
        }

        //normalize to account for diagonal movement 
        inputDir = inputDir.normalized;

        //cast 2D input to 3D vector for world movement
        Vector3 moveDir = new(inputDir.x, 0f, inputDir.y);

        //move player
        transform.position += Time.deltaTime * moveSpeed * moveDir;

        //rotate player to face direction of movement
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

    }
}
