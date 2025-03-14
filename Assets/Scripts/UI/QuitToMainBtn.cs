using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainBtn : MonoBehaviour
{
    [SerializeField] AudioClip lobbyBGM;
    public void ToStartScene()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.ChangeBgmClip(lobbyBGM);
        SoundManager.Instance.SaveVolume();
        SceneManager.LoadScene("StartScene");
    }
}