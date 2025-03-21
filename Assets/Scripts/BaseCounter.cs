using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform _counterTopPoint;

    private KitchenObject _kitchenObject;


    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }


    public Transform GetKitchenObjectPlacementPoint()
    {
        return _counterTopPoint;
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
