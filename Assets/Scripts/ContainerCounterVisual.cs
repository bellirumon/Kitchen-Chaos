using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{

    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter _containerCounter;
    [SerializeField] private Animator _animator;


    private void Start()
    {
        _containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject()
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
