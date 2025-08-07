using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event Action OnRecipeSpawned;
    public event Action OnRecipeCompleted;
    public event Action OnRecipeSuccess;
    public event Action OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO _recipeListSO;

    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipesMax = 4;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("DeliveryManager Instance already exists. Destroying this one...");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _waitingRecipeSOList = new List<RecipeSO>();
    }


    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeSOList.Count < _waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = _recipeListSO.RecipeSOList[UnityEngine.Random.Range(0, _recipeListSO.RecipeSOList.Count)];
                _waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke();
            }
        }
    }


    //TODO - Improve this
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];

            if (plateKitchenObject.GetKitchenObjectSOList().Count == waitingRecipeSO.KitchenObjectSOList.Count)
            {
                //plate and recipe have the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.KitchenObjectSOList)
                {
                    //cycling thru all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling thru all ingredients on the plate
                        if (recipeKitchenObjectSO == plateKitchenObjectSO)
                        {
                            //ingredient matches
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //this recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    //player delivered the correct recipe
                    _waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke();
                    OnRecipeSuccess?.Invoke();
                    return;
                }
            }
        }

        //no matches found - the player did not deliver a correct recipe
        OnRecipeFailed?.Invoke();
    }


    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return _waitingRecipeSOList;
    }

}
