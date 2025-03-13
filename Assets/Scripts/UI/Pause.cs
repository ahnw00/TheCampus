using UnityEngine;

public class Pause : MonoBehaviour
{
    SoundManager soundManager;
    [SerializeField] private GameObject completeAnim;
    [SerializeField] private GameObject endingAnim;
    AudioSource bgmSource;

    private void Start()
    {
        soundManager = SoundManager.Instance;
        bgmSource = soundManager.BGMsource;
    }

    public void PauseFunc()
    {
        Time.timeScale = 0f;
        if (completeAnim.activeSelf || endingAnim.activeSelf)
            bgmSource.Pause();
    }

    public void ResumeFunc()
    {
        Time.timeScale = 1f;
        if (!bgmSource.isPlaying)
            bgmSource.Play();
    }
}
