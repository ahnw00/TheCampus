using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BGMSliderManager : MonoBehaviour
{
    private Slider bgmSlider;

    void Awake()
    {
        bgmSlider = GetComponent<Slider>();
        bgmSlider.value = PlayerPrefs.GetFloat("BGM", 1f);
        bgmSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }


    private void OnSliderValueChanged(float value)
    {
        SoundManager.Instance.SetBGMValue(value);
        PlayerPrefs.SetFloat("BGM", value);
    }
}
