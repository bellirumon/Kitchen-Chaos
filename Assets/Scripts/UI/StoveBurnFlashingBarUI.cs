using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter _stoveCounter;
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Start()
    {
        _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        _animator.SetBool(IS_FLASHING, false);
    }


    private void StoveCounter_OnProgressChanged(float progress)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = (_stoveCounter.IsFried() && progress >= burnShowProgressAmount);

        _animator.SetBool(IS_FLASHING, show);
    }

}
