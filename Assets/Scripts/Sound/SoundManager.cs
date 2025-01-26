using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioMixer audioMixer;
    private float BGMVolume { get; set; } = 1f;
    private float SFXVolume { get; set; } = 1f;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetBGMValue(float value)
    {
        BGMVolume = value;
        audioMixer.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    public void SetSFXValue(float value)
    {
        SFXVolume = value;
        audioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }
}
