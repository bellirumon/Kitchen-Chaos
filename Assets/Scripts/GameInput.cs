using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    
    
    public Vector2 GetMovementVectorNormalized()
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

        return inputDir;
    }

}
