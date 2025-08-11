using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecipe", menuName = "ScriptableObjects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    [SerializeField] private List<KitchenObjectSO> _kitchenObjectSOList;
    public List<KitchenObjectSO> KitchenObjectSOList => _kitchenObjectSOList;

    public string _recipeName;
}
