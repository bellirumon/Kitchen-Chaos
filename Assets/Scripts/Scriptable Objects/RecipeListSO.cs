using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecipeList", menuName = "ScriptableObjects/RecipeListSO")]
public class RecipeListSO : ScriptableObject
{
    [SerializeField] private List<RecipeSO> _recipeSOList;
    public List<RecipeSO> RecipeSOList => _recipeSOList;
}
