using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource BGMsource;
    [SerializeField] private AudioSource SFXsource;
    public float BGMVolume { get; set; }
    public float SFXVolume { get; set; }

    private void Start()
    {
        // 게임 실행 시 볼륨 무조건 1로 고정되는 문제 때문에; 게임 종료 직전 볼륨 강제적용
        audioMixer.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(BGMVolume, 0.0001f, 1f)) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(SFXVolume, 0.0001f, 1f)) * 20);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolume();
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
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

    public void ChangeBgmClip(AudioClip clip)
    {
        BGMsource.clip = clip;
        BGMsource.Play();
    }

    public void ChangeSfxClip(AudioClip clip)
    {
        SFXsource.clip = clip;
        SFXsource.Play();
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
