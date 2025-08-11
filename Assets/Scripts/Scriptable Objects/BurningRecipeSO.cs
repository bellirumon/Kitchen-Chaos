using UnityEngine;

[CreateAssetMenu(fileName = "NewBurningRecipe", menuName = "ScriptableObjects/BurningRecipeSO")]
public class BurningRecipeSO : ScriptableObject
{
    [SerializeField] private KitchenObjectSO _input;
    public KitchenObjectSO Input => _input;

    [SerializeField] private KitchenObjectSO _output;
    public KitchenObjectSO Output => _output;

    [SerializeField] private float _burningTimerMax;
    public float BurningTimerMax => _burningTimerMax;
}
