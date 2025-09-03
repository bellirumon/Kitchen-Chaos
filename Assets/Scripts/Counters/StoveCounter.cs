using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{

    public event Action<State> OnStateChanged;
    public event Action<float> OnProgressChanged;

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }


    [SerializeField] private FryingRecipeSO[] _fryingRecipeSOs;
    [SerializeField] private BurningRecipeSO[] _burningRecipeSOs;

    private State _state;
    private float _fryingTimer;
    private FryingRecipeSO _fryingRecipeSO;
    private float _burningTimer;
    private BurningRecipeSO _burningRecipeSO;


    private void Start()
    {
        _state = State.Idle;
    }


    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                break;

                case State.Frying:
                _fryingTimer += Time.deltaTime;

                OnProgressChanged?.Invoke(_fryingTimer / _fryingRecipeSO.FryingTimerMax);

                if (_fryingTimer > _fryingRecipeSO.FryingTimerMax)
                {
                    //Fried
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(_fryingRecipeSO.Output, this);

                    _burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);
                    _state = State.Fried;
                    _burningTimer = 0f;

                    OnStateChanged?.Invoke(_state);
                }
                break;

                case State.Fried:
                _burningTimer += Time.deltaTime;

                OnProgressChanged?.Invoke(_burningTimer / _burningRecipeSO.BurningTimerMax);

                if (_burningTimer > _burningRecipeSO.BurningTimerMax)
                {
                    //Fried
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(_burningRecipeSO.Output, this);

                    _state = State.Burned;

                    OnStateChanged?.Invoke(_state);

                    OnProgressChanged?.Invoke(0f);
                }
                break;

                case State.Burned:
                break;
            }
        }
    }



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

                    //transfer the object from the player to this counter and reset the frying progress
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);

                    _state = State.Frying;
                    _fryingTimer = 0f;

                    OnStateChanged?.Invoke(_state);
                    OnProgressChanged?.Invoke(_fryingTimer / _fryingRecipeSO.FryingTimerMax);
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
                //player is carrying an object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().KitchenObjectSO))
                    {
                        GetKitchenObject().DestroySelf();

                        _state = State.Idle;

                        OnStateChanged?.Invoke(_state);
                        OnProgressChanged?.Invoke(0f);
                    }
                }
            }
            else
            {
                //player is not carrying anything

                //transfer the object from the counter to the player
                GetKitchenObject().SetKitchenObjectParent(player);

                _state = State.Idle;

                OnStateChanged?.Invoke(_state);
                OnProgressChanged?.Invoke(0f);
            }
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(input);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.Output;
        }
        else
        {
            return null;
        }
    }


    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO fryingRecipeSO in _fryingRecipeSOs)
        {
            if (input == fryingRecipeSO.Input)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }


    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (BurningRecipeSO burningRecipeSO in _burningRecipeSOs)
        {
            if (input == burningRecipeSO.Input)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }


    public bool IsFried()
    {
        return (_state == State.Fried);
    }


}
