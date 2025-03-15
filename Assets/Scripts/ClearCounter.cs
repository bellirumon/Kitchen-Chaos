using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private Transform _counterTopPoint;
    

    public void Interact()
    {
        Transform kitchenObjectTr = Instantiate(_kitchenObjectSO.Prefab, _counterTopPoint);
        kitchenObjectTr.localPosition = Vector3.zero;
    }

}
