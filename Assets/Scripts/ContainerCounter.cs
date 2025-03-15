using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event Action OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is not carrying anything, spawn an object
            Transform kitchenObjectTr = Instantiate(_kitchenObjectSO.Prefab);
            kitchenObjectTr.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke();
        }
    }

}
