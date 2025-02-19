using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioMixer audioMixer;
    public float BGMVolume { get; set; }
    public float SFXVolume { get; set; }
    public float LastBGMVolume;
    public float LastSFXVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        LoadVolume();
    }

    public void SetBGMValue(float value)
    {
        BGMVolume = value;
        audioMixer.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        SaveVolume();
    }

    public void SetSFXValue(float value)
    {
        SFXVolume = value;
        audioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        SaveVolume();
    }

    public void LoadVolume()
    {
        BGMVolume = PlayerPrefs.GetFloat("BGM", 1f);
        SFXVolume = PlayerPrefs.GetFloat("SFX", 1f);
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("BGM", BGMVolume);
        PlayerPrefs.SetFloat("SFX", SFXVolume);
        PlayerPrefs.Save();
    }
}
