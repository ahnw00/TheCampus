using UnityEngine;
using UnityEngine.UI;

public class SFXMuteManager : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject sfxMuted;
    [SerializeField] private GameObject sfxUnmuted;

    private void Awake()
    {
        if (sfxSlider != null)
        {
            UpdateMuteIcons(sfxSlider.value);
            sfxSlider.onValueChanged.AddListener(UpdateMuteIcons);
        }
    }

    private void UpdateMuteIcons(float value)
    {
        bool isMuted = value <= 0.0001;
        if (sfxMuted != null) sfxMuted.SetActive(isMuted);
        if (sfxUnmuted != null) sfxUnmuted.SetActive(!isMuted);
    }
}
