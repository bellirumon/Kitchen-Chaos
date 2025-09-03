using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;


    private void Start()
    {
        _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }


    private void StoveCounter_OnProgressChanged(float progress)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = (_stoveCounter.IsFried() && progress >= burnShowProgressAmount);

        if (show) Show();
        else Hide();
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
