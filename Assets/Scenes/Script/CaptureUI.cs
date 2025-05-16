using UnityEngine;
using UnityEngine.UI;

public class CaptureUI : MonoBehaviour
{
    public Image fillImage;

    public void SetProgress(float value)
    {
        fillImage.fillAmount = Mathf.Clamp01(value);
    }

    public void Show(bool visible)
    {
        fillImage.transform.parent.gameObject.SetActive(visible);
    }
}
