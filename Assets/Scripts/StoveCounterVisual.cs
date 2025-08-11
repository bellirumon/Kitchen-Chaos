using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private GameObject _particlesGameObject;

    private void Start()
    {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
    }

    private void _stoveCounter_OnStateChanged(StoveCounter.State state)
    {
        bool showVisual = (state == StoveCounter.State.Frying || state == StoveCounter.State.Fried);

        _stoveOnGameObject.SetActive(showVisual);
        _particlesGameObject.SetActive(showVisual);
    }
}
