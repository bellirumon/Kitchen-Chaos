using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visualGameObject;


    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }


    private void Player_OnSelectedCounterChanged(BaseCounter selectedCounter)
    {
        //check if the counter selected by the player and this clear counter are same
        if (selectedCounter == _baseCounter)
            ShowVisual();
        else
            HideVisual();
    }


    private void ShowVisual()
    {
        foreach (GameObject obj in _visualGameObject)
        {
            obj.SetActive(true);
        }
    }


    private void HideVisual()
    {
        foreach (GameObject obj in _visualGameObject)
        {
            obj.SetActive(false);
        }
    }

}
