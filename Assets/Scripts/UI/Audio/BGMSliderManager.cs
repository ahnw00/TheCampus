using UnityEngine;
using UnityEngine.UI;

public class BGMSliderManager : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;

    private void Awake()
    {
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void OnEnable()
    {
        if (bgmSlider != null && SoundManager.Instance != null)
        {
            bgmSlider.value = SoundManager.Instance.BGMVolume;
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetBGMValue(value);
        }
    }
}