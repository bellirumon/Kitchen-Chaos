using UnityEngine;

[CreateAssetMenu(fileName = "NewCuttingRecipe", menuName = "ScriptableObjects/CuttingRecipeSO")]
public class CuttingRecipeSO : ScriptableObject
{
    [SerializeField] private KitchenObjectSO _input;
    public KitchenObjectSO Input => _input;

    [SerializeField] private KitchenObjectSO _output;
    public KitchenObjectSO Output => _output;

    [SerializeField] private int _cuttingProgressMax;
    public int CuttingProgressMax => _cuttingProgressMax;
}
