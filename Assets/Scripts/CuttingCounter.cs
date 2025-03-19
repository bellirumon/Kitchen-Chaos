using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //counter has no kitchen object on it
            if (player.HasKitchenObject())
            {
                //player is carrying a kitchen object
                if (HasRecipeWithInput(player.GetKitchenObject().KitchenObjectSO))
                {
                    //there is a recipe for the kitchen object being carried by the player, hence object is dropable

                    //transfer the object from the player to this counter
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                else
                {
                    //object does not have a cutting recipe, do not allow this object to be dropped on this counter

                }
                
            }
            else
            {
                //player is not carrying anything
            }
        }
        else
        {
            //counter already has a kitchen object on it
            if (player.HasKitchenObject())
            {
                //player is already carrying an object
            }
            else
            {
                //player is not carrying anything

                //transfer the object from the counter to the player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().KitchenObjectSO))
        {
            //counter has a kitchen object AND it has a cutting recipe, so cut it (destroy kitchen object and spawn in sliced kitchen object)
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().KitchenObjectSO);

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in _cuttingRecipeSO)
        {
            if (inputKitchenObjectSO == cuttingRecipeSO.Input)
            {
                return true;
            }
        }

        return false;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in _cuttingRecipeSO)
        {
            if (input == cuttingRecipeSO.Input)
            {
                return cuttingRecipeSO.Output;
            }
        }

        return null;
    }

}
