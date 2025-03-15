using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KitchenObject : MonoBehaviour
{
   
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    public KitchenObjectSO KitchenObjectSO => _kitchenObjectSO;


    //to keep track of the parent that this kitchen object is owned by
    private IKitchenObjectParent _kitchenObjectParent; 
    

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //if the kitchen object currently already has a parent
        if (_kitchenObjectParent != null)
        {
            //ensure that the current parent disowns it
            _kitchenObjectParent.ClearKitchenObject();
        }

        _kitchenObjectParent = kitchenObjectParent; //set the current parent for this kitchen object to the newly received parent
        kitchenObjectParent.SetKitchenObject(this); //tell the newly received parent to own this kitchen object

        //update the position of this kitchen object so that the newly received parent now shows this kitchen object on top of it
        transform.parent = kitchenObjectParent.GetKitchenObjectPlacementPoint();
        transform.localPosition = Vector3.zero;
    }

    
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return _kitchenObjectParent;
    }


}
