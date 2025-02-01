using UnityEngine;
using UnityEngine.UI;

public class BGMSliderManager : MonoBehaviour
{
    private Slider bgmSlider;

    void Awake()
    {
        bgmSlider = GetComponent<Slider>();

        if (SoundManager.Instance != null)
        {
            bgmSlider.value = SoundManager.Instance.BGMVolume;
        }

        bgmSlider.onValueChanged.AddListener(value =>
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.BGMVolume = value; // SoundManager 변수 업데이트
                SoundManager.Instance.SetBGMValue(value);  // 실제 적용
            }
        });
    }
}