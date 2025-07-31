using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList;


    private List<KitchenObjectSO> _kitchenObjectSOList;


    private void Awake()
    {
        _kitchenObjectSOList = new List<KitchenObjectSO>();
    }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!_validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //not a valid ingredient
            return false;
        }

        if (_kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //plate already has this type of ingredient
            return false;
        }
        else
        {
            _kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
    }

}
