using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private ClearCounter _clearCounter;
    [SerializeField] private GameObject _visualGameObject;


    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }


    private void Player_OnSelectedCounterChanged(ClearCounter selectedCounter)
    {
        //check if the counter selected by the player and this clear counter are same
        if (selectedCounter == _clearCounter)
            ShowVisual();
        else
            HideVisual();
    }


    private void ShowVisual()
    {
        _visualGameObject.SetActive(true);
    }


    private void HideVisual()
    {
        _visualGameObject.SetActive(false);
    }

}
