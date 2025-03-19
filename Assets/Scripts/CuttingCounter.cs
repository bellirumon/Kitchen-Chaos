using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter
{

    public event Action<float> OnProgressChanged;
    public event Action OnCut;

    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSO;

    private int _cuttingProgress;


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

                    //transfer the object from the player to this counter and reset the cutting progress
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    _cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);

                    //pass a normalized 9b/w 0 and 1) cutting progress as it is to be the fill amount of the progress bar image
                    OnProgressChanged?.Invoke((float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax); 
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
            _cuttingProgress++;

            //fire the event to play the knife animation
            OnCut?.Invoke();

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);

            //fire the event to update the progress bar
            //pass a normalized 9b/w 0 and 1) cutting progress as it is to be the fill amount of the progress bar image
            OnProgressChanged?.Invoke((float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax);

            if (_cuttingProgress >= cuttingRecipeSO.CuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().KitchenObjectSO);

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(input);

        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.Output;
        }
        else
        {
            return null;
        }
    }


    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in _cuttingRecipeSO)
        {
            if (input == cuttingRecipeSO.Input)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }

}
