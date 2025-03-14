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
        if (sfxSlider != null && FindAnyObjectByType<SoundManager>() != null)
        {
            sfxSlider.value = FindAnyObjectByType<SoundManager>().SFXVolume;
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (FindAnyObjectByType<SoundManager>() != null)
        {
            FindAnyObjectByType<SoundManager>().SetSFXValue(value);
        }
    }
}