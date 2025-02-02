using UnityEngine;
using UnityEngine.UI;

public class SFXMuteManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void MuteSFX()
    {
        SoundManager.Instance.LastSFXVolume = slider.value;
        slider.value = 0.0001f;
    }

    public void UnMuteSFX()
    {
        slider.value = SoundManager.Instance.LastSFXVolume;
    }
}
