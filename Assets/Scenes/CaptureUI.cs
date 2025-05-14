using UnityEngine;
using UnityEngine.UI;

public class CaptureUI : MonoBehaviour
{
    public Slider progressBar;

    public void SetProgress(float value)
    {
        progressBar.value = value;
    }

    public void Show(bool visible)
    {
        progressBar.gameObject.SetActive(visible);
    }
}
