using UnityEngine;
using UnityEngine.UI;

public class BGMMuteManager : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private GameObject bgmMuted;
    [SerializeField] private GameObject bgmUnmuted;

    private void Awake()
    {
        if (bgmSlider != null) 
        { 
            UpdateMuteIcons(bgmSlider.value);
            bgmSlider.onValueChanged.AddListener(UpdateMuteIcons);
        }
    }

    private void UpdateMuteIcons(float value)
    {
        bool isMuted = value <= 0.0001;
        if (bgmMuted != null) bgmMuted.SetActive(isMuted);
        if (bgmUnmuted != null) bgmUnmuted.SetActive(!isMuted);
    }
}
