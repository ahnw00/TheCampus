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
        if (bgmSlider != null && FindAnyObjectByType<SoundManager>() != null)
        {
            bgmSlider.value = FindAnyObjectByType<SoundManager>().BGMVolume;
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (FindAnyObjectByType<SoundManager>() != null)
        {
            FindAnyObjectByType<SoundManager>().SetBGMValue(value);
        }
    }
}