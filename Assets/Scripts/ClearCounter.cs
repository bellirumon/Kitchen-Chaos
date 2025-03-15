using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private Transform _counterTopPoint;

    private KitchenObject _kitchenObject;


    public void Interact(Player player)
    {
        //if the counter has no kitchen object, spawn one
        if (_kitchenObject == null)
        {
            Transform kitchenObjectTr = Instantiate(_kitchenObjectSO.Prefab, _counterTopPoint);
            kitchenObjectTr.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            //if the counter already has a kitchen object, let the player pick up the object
            _kitchenObject.SetKitchenObjectParent(player);
        }
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
