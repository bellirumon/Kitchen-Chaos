using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private Transform _tomatoPrefab;
    [SerializeField] private Transform _counterTopPoint;
    

    public void Interact()
    {
        Transform tomatoTr = Instantiate(_tomatoPrefab, _counterTopPoint);
        tomatoTr.localPosition = Vector3.zero;
    }

}
