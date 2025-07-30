using UnityEngine;

[CreateAssetMenu(fileName = "NewFryingRecipe", menuName = "ScriptableObjects/FryingRecipeSO")]
public class FryingRecipeSO : ScriptableObject
{
    [SerializeField] private KitchenObjectSO _input;
    public KitchenObjectSO Input => _input;

    [SerializeField] private KitchenObjectSO _output;
    public KitchenObjectSO Output => _output;

    [SerializeField] private float _fryingTimerMax;
    public float FryingTimerMax => _fryingTimerMax;
}
