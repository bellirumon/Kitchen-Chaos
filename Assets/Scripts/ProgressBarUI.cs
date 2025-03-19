using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Image _barImage;


    private void Start()
    {
        _cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;

        _barImage.fillAmount = 0;
        Hide();
    }


    private void CuttingCounter_OnProgressChanged(float normalizedProgress)
    {
        _barImage.fillAmount = normalizedProgress;

        if (normalizedProgress == 0f || normalizedProgress == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
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
