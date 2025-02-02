using UnityEngine;
using UnityEngine.UI;

public class BGMMuteManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void MuteBGM()
    {
        SoundManager.Instance.LastBGMVolume = slider.value;
        slider.value = 0.0001f;
    }

    public void UnMuteBGM()
    {
        slider.value = SoundManager.Instance.LastBGMVolume;
    }
}
