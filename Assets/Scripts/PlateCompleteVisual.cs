using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [System.Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject _PlatekitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSOGameObjectList;


    private void Start()
    {
        _PlatekitchenObject.OnIngredientAdded += PlatekitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in _kitchenObjectSOGameObjectList)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }


    private void PlatekitchenObject_OnIngredientAdded(KitchenObjectSO kitchenObjectSO)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in _kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSO_GameObject.kitchenObjectSO == kitchenObjectSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            } 
        }
    }
}
