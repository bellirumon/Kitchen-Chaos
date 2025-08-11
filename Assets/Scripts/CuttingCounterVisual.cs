using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCountCerVisual : MonoBehaviour
{

    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Animator _animator;


    private void Start()
    {
        _cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut()
    {
        _animator.SetTrigger(CUT);
    }

    
}
