using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{

    public event Action OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;


    public override void Interact(Player player)
    {
        //if the counter has no kitchen object, spawn one
        if (!HasKitchenObject())
        {
            Transform kitchenObjectTr = Instantiate(_kitchenObjectSO.Prefab);
            kitchenObjectTr.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke();
        }
    }

}
