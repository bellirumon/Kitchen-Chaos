using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
   
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    public KitchenObjectSO KitchenObjectSO => _kitchenObjectSO;


    //to keep track of the clear counter that this kitchen object is put on
    public ClearCounter ClearCounter 
    { 
        get
        {
            return ClearCounter;
        }

        set
        {
            //if the kitchen object currently already has a clear counter
            if (ClearCounter != null)
            {
                //ensure that the current clear counter gets rid of this kitchen object and disowns it
                ClearCounter.ClearKitchenObject();
            }

            ClearCounter = value; //set the current clear counter for this kitchen object to the newly received clear counter
            value.SetKitchenObject(this); //tell the newly received clear counter to own this kitchen object as its own 

            //update the position of this kitchen object so that the newly received clear counter now shows this kitchen object on top of it
            transform.parent = value.CounterTopPoint;
            transform.localPosition = Vector3.zero;
        } 
    }


}
