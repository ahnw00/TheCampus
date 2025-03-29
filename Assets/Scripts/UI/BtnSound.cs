using UnityEngine;
using UnityEngine.UI;

public class BtnSound : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    [SerializeField] AudioClip btnClip;

    private void OnEnable()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(PlayBtnSound);
    }

    void PlayBtnSound()
    {
        if(soundManager)
            soundManager.ChangeSfxClip(btnClip);
    }
}
