using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject bgmUnmutedIcon;
    [SerializeField] private GameObject bgmMutedIcon;
    [SerializeField] private GameObject sfxUnmutedIcon;
    [SerializeField] private GameObject sfxMutedIcon;

    private float lastBGMVolume = 1f;
    private float lastSFXVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        bgmSlider.value = lastBGMVolume;
        sfxSlider.value = lastSFXVolume;

        ToggleBGMMute(false);
        ToggleSFXMute(false);

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    // BGM ��������
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);


        if (volume <= 0.0001f)
        {
            bgmMutedIcon.SetActive(true);
            bgmUnmutedIcon.SetActive(false);
        }

        else
        {
            bgmMutedIcon.SetActive(false);
            bgmUnmutedIcon.SetActive(true);
            lastBGMVolume = volume;
        }
    }

    // SFX ��������
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);

        if (volume <= 0.0001f)
        {
            sfxMutedIcon.SetActive(true);
            sfxUnmutedIcon.SetActive(false);
        }

        else
        {
            sfxMutedIcon.SetActive(false);
            sfxUnmutedIcon.SetActive(true);
            lastSFXVolume = volume;
        }
    }

    // BGM ���Ұ� ���
    public void ToggleBGMMute(bool isMuted)
    {
        if (isMuted)
        {   // ���Ұ�
            audioMixer.SetFloat("BGM", -80f);
            bgmSlider.value = 0f;
            UpdateMuteIcon(bgmMutedIcon, bgmUnmutedIcon);
        }

        else
        {   // ���Ұ� ����
            audioMixer.SetFloat("BGM", Mathf.Log10(lastBGMVolume) * 20);
            bgmSlider.value = lastBGMVolume;
            UpdateMuteIcon(bgmUnmutedIcon, bgmMutedIcon);
        }
    }

    // SFX ���Ұ� ���
    public void ToggleSFXMute(bool isMuted)
    {
        if (isMuted)
        {   // ���Ұ�
            audioMixer.SetFloat("SFX", -80f);
            sfxSlider.value = 0f;
            UpdateMuteIcon(sfxMutedIcon, sfxUnmutedIcon);
        }

        else
        {   // ���Ұ� ����
            audioMixer.SetFloat("SFX", Mathf.Log10(lastSFXVolume) * 20);
            bgmSlider.value = lastSFXVolume;
            UpdateMuteIcon(sfxUnmutedIcon, sfxMutedIcon);
        }
    }

    // Mute ������ ������Ʈ
    private void UpdateMuteIcon(GameObject unmutedIcon, GameObject mutedIcon)
    {
        unmutedIcon.SetActive(!mutedIcon.activeSelf);
        mutedIcon.SetActive(!unmutedIcon.activeSelf);
    }
}
