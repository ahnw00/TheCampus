using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SFXSliderManager : MonoBehaviour
{
    private Slider sfxSlider;

    void Awake()
    {
        sfxSlider = GetComponent<Slider>();
        sfxSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        SoundManager.Instance.SetSFXValue(value);
    }
}