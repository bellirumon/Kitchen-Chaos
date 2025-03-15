using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private Transform _counterTopPoint;
    public Transform CounterTopPoint => _counterTopPoint;
    [SerializeField] private ClearCounter _secondClearCounter;

    private bool _testing = false;

    private KitchenObject _kitchenObject;


    private void Update()
    {
        if (_testing && Input.GetKeyDown(KeyCode.T))
        {
            if (_kitchenObject != null)
            {
                _kitchenObject.ClearCounter = _secondClearCounter;
            }
        }
    }

    public void Interact()
    {
        //if the counter has no kitchen object, spawn one
        if (_kitchenObject == null)
        {
            Transform kitchenObjectTr = Instantiate(_kitchenObjectSO.Prefab, _counterTopPoint);
            kitchenObjectTr.GetComponent<KitchenObject>().ClearCounter = this;
        }
        else
        {
            //if the counter already has a kitchen object on it
            Debug.Log(_kitchenObject.ClearCounter);
        }
    }


    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
