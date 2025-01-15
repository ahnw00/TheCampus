using UnityEngine;
using UnityEngine.UI;

public class UnmuteManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UnmuteBGM()
    {
        slider.value = 0.5f;
    }

    public void UnmuteSFX()
    {
        slider.value = 0.5f;
    }
}
