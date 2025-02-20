using UnityEngine;
using UnityEngine.UI;

public class SFXSliderManager : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void OnEnable()
    {
        if (sfxSlider != null && SoundManager.Instance != null)
        {
            sfxSlider.value = SoundManager.Instance.SFXVolume;
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetSFXValue(value);
        }
    }
}