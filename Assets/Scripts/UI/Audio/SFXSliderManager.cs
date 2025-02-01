using UnityEngine;
using UnityEngine.UI;

public class SFXSliderManager : MonoBehaviour
{
    private Slider sfxSlider;

    void Awake()
    {
        sfxSlider = GetComponent<Slider>();

        if (SoundManager.Instance != null)
        {
            sfxSlider.value = SoundManager.Instance.SFXVolume;
        }

        sfxSlider.onValueChanged.AddListener(value =>
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.SFXVolume = value; 
                SoundManager.Instance.SetSFXValue(value); 
            }
        });
    }
}