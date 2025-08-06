using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _hasProgressGameObject;
    [SerializeField] private Image _barImage;

    private IHasProgress _hasProgress;


    private void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();

        if (_hasProgress == null )
        {
            Debug.LogError("GameObject does not have component IHasProgress");
        }

        _hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        _barImage.fillAmount = 0;
        Hide();
    }


    private void HasProgress_OnProgressChanged(float normalizedProgress)
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
